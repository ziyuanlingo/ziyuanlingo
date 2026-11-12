# ZiyuanLingo

**Stateful AI Document Translation Studio for Academic, Legal & Technical Literature (EN / 中文 / 日本語)**

- **Official Website & Live Web Studio:** [https://ziyuanlingo.com](https://ziyuanlingo.com)
- **Open-Source Desktop Release:** `v1.1.0` (Apache-2.0 License)
- **Commercial Managed SaaS:** **ZiyuanLingo Cloud Pro** (`$19 / month`, powered by Google Cloud & Vertex AI — Early Access Open)

---

## Overview

Standard chat interfaces drop footnotes, break paragraph alignment, and drift on terminology across 100+ page academic papers and legal contracts. **ZiyuanLingo** provides a stateful translation pipeline with deterministic QA verification across **English**, **Simplified Chinese**, and **Japanese**.

### Open-Core Commercial Architecture

| Capability | Community Open Source (`v1.1.0` · `$0`) | ZiyuanLingo Cloud Pro (`$19 / mo` · Beta Free Quota) |
| :--- | :--- | :--- |
| **Distribution** | Apache-2.0 Local Desktop Client (BYOK) | **Managed Cloud SaaS Workspace** |
| **Model Connection** | Bring Your Own API Key (Gemini, OpenAI, Anthropic) | **Built-in Zero-Setup Multi-Model Engine (Vertex AI)** |
| **Long-Context & Rate Limits** | Subject to personal API key rate limits | **High-concurrency cloud routing & context caching** |
| **Document Formats** | Plain text, Markdown & excerpt QA | **Async two-column academic PDF layout reconstruction (Cloud Run)** |
| **Glossary & Memory** | Single-device local glossary | **Real-time cloud-synced 3-tier glossary & team collaboration** |

---

## Key Features in `v1.1.0`

1. **Four Domain Workflow Engines:**
   - `Academic STEM`: Preserves inline LaTeX equations, `[^n]` footnotes, and DOIs verbatim.
   - `Legal Contract`: Enforces strict modal verb mapping (`shall` / `may` / `must`) and defined terms.
   - `Historical Monograph`: Prevents modern anachronisms and preserves archival identifiers.
   - `Literary & Publishing`: Maintains character voice register and cross-scene pronoun continuity.
2. **Three-Level Automated Invariant QA:**
   - **Level 1:** Paragraph & footnote marker (`[^n]`) bijection check.
   - **Level 2:** Cross-lingual numeric, percentage, and date equivalence verification.
   - **Level 3:** Mandatory locked glossary hit attestation.
3. **Trilingual Interface:** Instant switching across English, Simplified Chinese (`简体中文`), and Japanese (`日本語`).

---

## Repository Structure

```text
ziyuanlingo/
├── desktop/
│   ├── ZiyuanLingoApp.cs          # Native 64-bit Windows Desktop Studio (v1.1.0)
│   ├── ZiyuanLingoSetup.cs        # Windows x64 Setup Installer source
│   └── build-desktop.mjs          # Automated build & packaging script
├── workflows/
│   └── domain-workflows.json      # Built-in STEM, Legal, History & Publishing rules
├── dictionaries/
│   └── core-glossary-en-zh-ja.json # Starter trilingual academic & legal lexicon
├── web-studio/
│   └── qa-engine.js               # Browser-side 3-level invariant QA & glossary lock
├── LICENSE                        # Apache License 2.0
└── README.md
```

## Building from Source (Windows x64)

ZiyuanLingo Desktop compiles cleanly on Windows 10/11 (x64) with zero external NuGet dependencies:

```bash
node desktop/build-desktop.mjs
```

Prebuilt Windows x64 binaries (`ZiyuanLingo-Setup-1.1.0-x64.exe` and `ZiyuanLingo-Portable-1.1.0-x64.zip`) are available on the [Releases](https://github.com/ziyuanlingo/ziyuanlingo/releases) page and [ziyuanlingo.com](https://ziyuanlingo.com/#download).

---

## Maintainer & Studio

- **Founder & Lead Engineer:** Nagamori Madokawa (`窓川 長盛`)
- **Studio:** ZiyuanLingo Studio — Shibuya-ku, Tokyo, Japan
- **Email:** [founder@ziyuanlingo.com](mailto:founder@ziyuanlingo.com)
- **Website:** [https://ziyuanlingo.com](https://ziyuanlingo.com)
