# ZiyuanLingo

[English](./README.md) | [简体中文](./README.zh-CN.md) | **日本語**

**学術論文・国際法務契約・技術専門書のためのステートフル AI ドキュメント翻訳プラットフォーム（EN / 中文 / 日本語）**

- **公式クラウド基盤 & ライブ Web Studio：** [https://ziyuanlingo.com](https://ziyuanlingo.com)
- **ライブクラウド翻訳スタジオ：** [https://ziyuanlingo.com/app](https://ziyuanlingo.com/app)
- **料金プラン：** [https://ziyuanlingo.com/pricing](https://ziyuanlingo.com/pricing)
- **技術ドキュメント & API リファレンス：** [https://ziyuanlingo.com/docs](https://ziyuanlingo.com/docs)
- **現行リリース：** `v2.0.0` Production GA（Apache-2.0 オープンコア）

---

## 概要

汎用的なチャット型翻訳ツールでは、100ページを超える長編原稿において脚注の脱落や 2段組 LaTeX 数式の破損が生じやすく、契約書や学術論文で定義された専門用語の訳語ブレ（ドリフト）も避けられません。**ZiyuanLingo** は、Apache-2.0 ライセンスのローカルデスクトップ & 品質検証（QA）エンジンと、Google Cloud および Vertex AI 上で本番稼働する商用 SaaS 基盤 **ZiyuanLingo Cloud** を統合し、英語・簡体字中国語・日本語の専門文書ワークフローに高い再現性を提供します。

### オープンコア商用アーキテクチャ

| 機能領域 | Community Open Source（`$0`） | Cloud Pro（`$19 / 月` · 年払い `$15 / 月`） | Team Cloud（`$49 / 席 / 月`） |
| :--- | :--- | :--- | :--- |
| **デプロイ形態** | ローカル Windows x64 デスクトップ版（BYOK） | **マネージドクラウドワークスペース（設定不要）** | **複数シート共有の組織向けクラウド基盤** |
| **翻訳パイプライン** | ユーザー個人の API キーを利用 | **マネージド・マルチモデル推論基盤（Vertex AI）** | **高並列・専用クラウドルーティング** |
| **対応ドキュメント** | プレーンテキスト・Markdown・抜粋 QA | **2段組学術 PDF・DOCX・LaTeX 組版再構築** | **リポジトリ一括処理 & 薬事・法務文書パイプライン** |
| **用語集・メモリ** | 単一端末のローカル JSON 用語集 | **クラウド同期 3層用語テーブル（5,000 語）** | **組織共有用語レジストリ & RBAC 監査ログ** |
| **月間処理ページ枠** | ローカル BYOK の上限に準拠 | **月間 1,500 ページ枠を標準付帯** | **1席あたり月間 5,000 ページの共有プール枠** |

---

## `v2.0.0` Production GA の主な機能

1. **ライブクラウド翻訳パイプライン（`/app`）：** `Academic STEM`（理系学術論文：LaTeX 数式マスキング・DOI 保護）、`Legal Contract`（国際法務契約：助動詞 `shall` / `may` / `must` の厳密な法務 QA および定義済み用語ロック）、`Historical Monograph`（歴史研究書）、`Technical Publishing`（技術出版）の 4 領域に対応し、3段階の不変条件（インバリアント）検証をリアルタイムに実行します。
2. **構造化ストリームブリッジ（`packages/structured-stream-bridge`）：** Vertex AI Gemini の `functionCall` パート（内部 `thought` トークンの自動フィルタリング対応）およびチャンク分割されたツール呼び出し SSE ストリームの双方に対応する確定的アンパッカーであり、出力途絶（トランケーション）耐性を備えた配列リカバリ機構を内蔵しています。
3. **3段階の自動不変条件（インバリアント）QA：**
   - **Level 1：** 段落数の 1対1 整合および脚注マーカー（`[^n]`）の全単射（ビジェクション）検証。
   - **Level 2：** 数値・通貨金額・パーセンテージ・引用表記の言語間等価性監査。
   - **Level 3：** ロック済み必須用語集（Locked Glossary）の適用証明（アテステーション）。

---

## リポジトリ構成

```text
ziyuanlingo/
├── desktop/
│   ├── ZiyuanLingoApp.cs                 # ネイティブ 64-bit Windows デスクトップスタジオ
│   ├── ZiyuanLingoSetup.cs               # Windows x64 セットアップインストーラーのソース
│   └── build-desktop.mjs                 # 自動ビルド・パッケージングスクリプト
├── packages/
│   └── structured-stream-bridge/
│       ├── stream-unpacker.mjs           # Vertex AI / SSE 構造化出力ストリームアンパッカー
│       └── test-unpacker.mjs             # ストリームリカバリ自動単体テスト
├── workflows/
│   └── domain-workflows.json             # 理系学術・法務契約・歴史・技術出版のルール定義
├── dictionaries/
│   └── core-glossary-en-zh-ja.json       # 英・中・日 3言語の学術・法務スターター辞書
├── web-studio/
│   ├── qa-engine.js                      # Cloud Studio v2.0 & 3段階不変条件 QA エンジン
│   └── i18n.js                           # 3言語 UI ローカライゼーション（EN / ZH / JA）
├── LICENSE                               # Apache License 2.0
├── README.md                             # 英語版ドキュメント（デフォルト）
├── README.zh-CN.md                       # 簡体字中国語版ドキュメント
└── README.ja.md                          # 日本語版ドキュメント
```

## クイックスタート

Node.js（v18 以降）環境で、構造化ストリームアンパッカーの単体検証スイートをローカル実行できます。

```bash
node packages/structured-stream-bridge/test-unpacker.mjs
```

---

## メンテナ・運営会社

- **創設者・リードアーキテクト：** Nagamori Madokawa（`窓川 長盛`）
- **本社所在地：** ZiyuanLingo — 〒150-6139 東京都渋谷区渋谷 2-24-12 渋谷スクランブルスクエア
- **連絡先：** [founder@ziyuanlingo.com](mailto:founder@ziyuanlingo.com)
- **Web サイト：** [https://ziyuanlingo.com](https://ziyuanlingo.com)

## ライセンス

Apache License 2.0 — 詳細は [LICENSE](./LICENSE) をご参照ください。
