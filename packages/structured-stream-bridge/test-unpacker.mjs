import assert from 'node:assert/strict';
import { reassembleSSETranslationStream, recoverStructuredPayload } from './stream-unpacker.mjs';

// 1. Gemini Vertex AI functionCall SSE test
const geminiSSE = [
  'data: ' + JSON.stringify({
    candidates: [{
      content: {
        parts: [
          { thought: true, text: 'Checking locked legal glossary...' },
          { functionCall: { name: 'emit_translated_units', args: { translations: ['赔偿方应当在48小时内书面通知许可方[^1]。'] } } }
        ]
      }
    }]
  }),
  'data: [DONE]'
].join('\n');

const res1 = reassembleSSETranslationStream(geminiSSE);
assert.equal(res1.protocol, 'vertex-gemini');
assert.deepEqual(res1.units, ['赔偿方应当在48小时内书面通知许可方[^1]。']);

// 2. Truncation-resilient partial JSON recovery test
const truncated = '{"translations": ["第一段完整译文[^1]。", "第二段在流式传输末尾被网关截断但已输出完整句子。';
const res2 = recoverStructuredPayload(truncated);
assert.equal(res2.partialRecovered, true);
assert.equal(res2.translations.length, 2);

console.log('structured-stream-bridge tests passed (2/2)');
