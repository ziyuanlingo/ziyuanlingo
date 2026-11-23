# ZiyuanLingo

**Stateful AI Document Translation Platform for Academic, Legal & Technical Literature (EN / 中文 / 日本語)**

- **Official Cloud Platform & Live Web Studio:** [https://ziyuanlingo.com](https://ziyuanlingo.com)
- **Live Cloud Translation Studio:** [https://ziyuanlingo.com/app](https://ziyuanlingo.com/app)
- **Pricing & Plans:** [https://ziyuanlingo.com/pricing](https://ziyuanlingo.com/pricing)
- **Documentation & API Reference:** [https://ziyuanlingo.com/docs](https://ziyuanlingo.com/docs)
- **Current Release:** `v2.0.0` Production GA (Apache-2.0 Open-Core)

---

## Overview

Generic chat translators drop footnotes, break two-column LaTeX equations, and drift on defined legal or scientific terminology across 100+ page manuscripts. **ZiyuanLingo** combines an Apache-2.0 local desktop & QA engine with **ZiyuanLingo Cloud**—a production SaaS platform powered by Google Cloud and Vertex AI for English, Simplified Chinese, and Japanese document workflows.

### Open-Core Commercial Architecture

| Capability | Community Open Source (`$0`) | Cloud Pro (`$19 / mo` · `$15 / mo` annual) | Team Cloud (`$49 / seat / mo`) |
| :--- | :--- | :--- | :--- |
| **Deployment** | Local Windows x64 Desktop Client (BYOK) | **Managed Cloud Workspace (Zero Setup)** | **Multi-Seat Shared Organization Cloud** |
| **Translation Pipeline** | User-supplied personal API keys | **Managed Multi-Model Cloud Pipeline (Vertex AI)** | **Dedicated High-Concurrency Cloud Routing** |
| **Document Formats** | Plain text, Markdown & excerpt QA | **Two-column academic PDF, DOCX & LaTeX reconstruction** | **Batch repository & regulatory dossier pipelines** |
| **Glossary & Memory** | Single-device JSON glossary | **Cloud-synced 3-tier glossary (5,000 terms)** | **Shared org terminology registry & RBAC audit logs** |
| **Monthly Volume** | Local BYOK limits | **1,500 pages / month included** | **5,000 pooled pages / seat / month** |

---

## What's New in `v2.0.0` Production GA

1. **Live Cloud Translation Pipeline (`/app`):** Real-time trilingual translation across `Academic STEM`, `Legal Contract`, `Historical Monograph`, and `Technical Publishing` registers with instant 3-level invariant verification.
2. **Structured Stream Bridge (`packages/structured-stream-bridge`):** Deterministic function-calling SSE unpacker supporting both Vertex AI Gemini `functionCall` parts (automatically filtering internal `thought` tokens) and chunked tool-call streams with truncation-resilient array recovery.
3. **Three-Level Automated Invariant QA:**
   - **Level 1:** Paragraph count & footnote marker (`[^n]`) bijection check.
   - **Level 2:** Cross-lingual numeric, currency, percentage, and citation equivalence audit.
   - **Level 3:** Mandatory locked glossary hit attestation.

---

## Repository Structure

```text
ziyuanlingo/
├── desktop/
│   ├── ZiyuanLingoApp.cs                 # Native 64-bit Windows Desktop Studio
│   ├── ZiyuanLingoSetup.cs               # Windows x64 Setup Installer source
│   └── build-desktop.mjs                 # Automated build & packaging script
├── packages/
│   └── structured-stream-bridge/
│       ├── stream-unpacker.mjs           # Vertex AI / SSE structured output unpacker
│       └── test-unpacker.mjs             # Automated stream recovery unit tests
├── workflows/
│   └── domain-workflows.json             # STEM, Legal, History & Publishing rules
├── dictionaries/
│   └── core-glossary-en-zh-ja.json       # Trilingual academic & legal starter lexicon
├── web-studio/
│   ├── qa-engine.js                      # Cloud Studio v2.0 & 3-level invariant QA
│   └── i18n.js                           # Trilingual UI localization (EN / ZH / JA)
├── LICENSE                               # Apache License 2.0
└── README.md
```

## Quick Verification

```bash
node packages/structured-stream-bridge/test-unpacker.mjs
```

---

## Maintainer & Company

- **Founder & Lead Architect:** Nagamori Madokawa (`窓川 長盛`)
- **Headquarters:** ZiyuanLingo — Shibuya Scramble Square, 2-24-12 Shibuya, Shibuya-ku, Tokyo 150-6139, Japan
- **Contact:** [founder@ziyuanlingo.com](mailto:founder@ziyuanlingo.com)
- **Website:** [https://ziyuanlingo.com](https://ziyuanlingo.com)
