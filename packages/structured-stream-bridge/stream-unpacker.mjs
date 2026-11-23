/**
 * ZiyuanLingo Structured Stream Bridge (v2.0.0)
 * Deterministic Function-Calling & Incremental SSE JSON Unpacker for
 * Paragraph-Aligned Academic and Legal Document Translation Pipelines.
 *
 * Supports both Google Vertex AI / Gemini native candidates[] functionCall
 * payloads and OpenAI-compatible tool_calls SSE streams, with truncation-safe
 * string recovery so long-context chapters never drop trailing segments.
 */

export const TRANSLATION_BATCH_TOOL_GEMINI = {
  functionDeclarations: [
    {
      name: 'emit_translated_units',
      description: 'Emit aligned translated paragraphs and verified glossary hits for the current manuscript chunk.',
      parameters: {
        type: 'object',
        properties: {
          translations: {
            type: 'array',
            items: { type: 'string' },
            description: 'Translated paragraphs strictly preserving source array length and [^n] markers.'
          },
          notes: {
            type: 'string',
            description: 'Optional terminology disambiguation audit note.'
          }
        },
        required: ['translations']
      }
    }
  ]
};

export function recoverStructuredPayload(rawArgs) {
  if (!rawArgs) return null;
  if (typeof rawArgs === 'object') return rawArgs;
  const str = String(rawArgs).trim();
  if (!str) return null;
  try {
    return JSON.parse(str);
  } catch (_) {
    // Truncation-resilient extraction for partial JSON array payloads
    const arrMatch = str.match(/"translations"\s*:\s*\[([\s\S]*)/);
    if (arrMatch) {
      const items = [];
      const itemRegex = /"((?:\\.|[^"\\])*)(?:"|$)/g;
      let m;
      while ((m = itemRegex.exec(arrMatch[1])) !== null) {
        if (!m[1]) continue;
        try {
          items.push(JSON.parse('"' + m[1] + '"'));
        } catch {
          items.push(m[1].replace(/\\n/g, '\n').replace(/\\"/g, '"'));
        }
      }
      if (items.length > 0) return { translations: items, partialRecovered: true };
    }
    return null;
  }
}

export function reassembleSSETranslationStream(rawSSEText) {
  const lines = String(rawSSEText).split(/\r?\n/);
  let plainText = '';
  let streamedToolArgs = '';
  let detectedGemini = false;

  for (const line of lines) {
    const trimmed = line.trim();
    if (!trimmed.startsWith('data:')) continue;
    const body = trimmed.slice(5).trim();
    if (!body || body === '[DONE]') continue;
    try {
      const chunk = JSON.parse(body);
      if (Array.isArray(chunk.candidates)) {
        detectedGemini = true;
        for (const cand of chunk.candidates) {
          for (const part of cand?.content?.parts || []) {
            if (typeof part?.text === 'string' && !part?.thought) {
              plainText += part.text;
            }
            if (part?.functionCall) {
              const args = part.functionCall.args ?? part.functionCall.arguments;
              streamedToolArgs += typeof args === 'string' ? args : JSON.stringify(args);
            }
          }
        }
      } else if (Array.isArray(chunk.choices)) {
        for (const ch of chunk.choices) {
          const delta = ch?.delta || ch?.message;
          if (!delta) continue;
          if (typeof delta.content === 'string') plainText += delta.content;
          for (const tc of delta.tool_calls || []) {
            if (typeof tc?.function?.arguments === 'string') {
              streamedToolArgs += tc.function.arguments;
            }
          }
        }
      }
    } catch {
      // Ignore malformed keepalive comments
    }
  }

  if (streamedToolArgs) {
    const unpacked = recoverStructuredPayload(streamedToolArgs);
    if (unpacked?.translations) {
      return {
        protocol: detectedGemini ? 'vertex-gemini' : 'openai-sse',
        units: unpacked.translations,
        partialRecovered: Boolean(unpacked.partialRecovered)
      };
    }
  }

  const fallbackMatch = plainText.match(/\[[\s\S]*\]/);
  if (fallbackMatch) {
    const parsed = JSON.parse(fallbackMatch[0]);
    if (Array.isArray(parsed)) {
      return { protocol: detectedGemini ? 'vertex-gemini' : 'openai-sse', units: parsed, partialRecovered: false };
    }
  }
  throw new Error('Unable to unpack aligned translation units from stream');
}
