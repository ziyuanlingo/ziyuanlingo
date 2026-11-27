# ZiyuanLingo

[English](./README.md) | **简体中文** | [日本語](./README.ja.md)

**面向学术论文、涉外法律合同与专业技术文献的有状态 AI 文档精译平台（EN / 中文 / 日本語）**

- **官方云端平台与在线工作台：** [https://ziyuanlingo.com](https://ziyuanlingo.com)
- **实时云端精译工作台（Web Studio）：** [https://ziyuanlingo.com/app](https://ziyuanlingo.com/app)
- **商业方案与定价：** [https://ziyuanlingo.com/pricing](https://ziyuanlingo.com/pricing)
- **开发文档与 API 参考：** [https://ziyuanlingo.com/docs](https://ziyuanlingo.com/docs)
- **当前正式版本：** `v2.0.0` Production GA（Apache-2.0 Open-Core 开源核心架构）

---

## 项目概述

通用对话式翻译工具在处理百页级长篇文稿时，极易丢失文末脚注、破坏双栏 LaTeX 数学公式，并在跨章节上下文中出现已定义法律或科研专有术语的译名漂移。**ZiyuanLingo** 将遵循 Apache-2.0 协议的本地桌面端与不变量质检（QA）引擎，同基于 Google Cloud 与 Vertex AI 构建的生产级商业 SaaS 平台 **ZiyuanLingo Cloud** 深度融合，专为英文、简体中文与日文专业文献工作流打造确定性的长文档精译基础设施。

### Open-Core 商业架构

| 能力维度 | 社区开源版（`$0`） | Cloud Pro 专业云端版（`$19 / 月` · 年付 `$15 / 月`） | Team Cloud 团队企业版（`$49 / 席位 / 月`） |
| :--- | :--- | :--- | :--- |
| **部署形态** | Windows x64 本地桌面客户端（BYOK 自接密钥） | **免配置托管云端工作空间（Zero Setup）** | **多席位共享组织级云端空间** |
| **翻译管线** | 用户自备个人 API 密钥 | **托管式多模型云端并发管线（Vertex AI）** | **独享高并发云端路由队列** |
| **文档格式** | 纯文本、Markdown 与片段级质检 | **双栏学术 PDF、DOCX 与 LaTeX 版式重构** | **代码库批量本地化与涉外法规卷宗流水线** |
| **术语与记忆库** | 单机本地 JSON 术语表 | **云端实时同步三级术语状态表（5,000 词条）** | **组织级共享术语注册表与 RBAC 审计日志** |
| **月度处理量** | 取决于本地自备密钥配额 | **每月包含 1,500 页云端额度** | **每席位每月 5,000 页团队共享额度池** |

---

## `v2.0.0` 正式版核心特性

1. **实时云端精译管线（`/app`）：** 覆盖 `Academic STEM`（理工学术论文：LaTeX 公式双射掩码与 DOI 锁定）、`Legal Contract`（涉外法律合同：情态动词 `shall` / `may` / `must` 严谨映射质检与大写定义术语锁定）、`Historical Monograph`（史学专著）与 `Technical Publishing`（技术出版）四大专业语域，实时执行英/中/日精译与三层不变量校验。
2. **结构化流式拆包桥接器（`packages/structured-stream-bridge`）：** 确定性函数调用 SSE 流式解包器，同时兼容 Vertex AI Gemini 原生 `functionCall` 分片（自动剥离内部隐藏 `thought` 思考 Token）与分块工具调用流，内置抗截断 JSON 数组恢复机制。
3. **三层自动化不变量质检（Three-Level Automated Invariant QA）：**
   - **第 1 层（Level 1）：** 段落数量 1:1 对齐与学术/法律脚注锚点（`[^n]`）双射（Bijection）校验。
   - **第 2 层（Level 2）：** 跨语种数值、货币金额、百分比及文献引用等价性审计。
   - **第 3 层（Level 3）：** 强制锁定术语表（Locked Glossary）命中鉴证。

---

## 仓库目录结构

```text
ziyuanlingo/
├── desktop/
│   ├── ZiyuanLingoApp.cs                 # 原生 64 位 Windows 桌面精译工作台
│   ├── ZiyuanLingoSetup.cs               # Windows x64 安装引导程序源码
│   └── build-desktop.mjs                 # 自动化编译与打包脚本
├── packages/
│   └── structured-stream-bridge/
│       ├── stream-unpacker.mjs           # Vertex AI / SSE 结构化输出流式拆包器
│       └── test-unpacker.mjs             # 流式抗截断恢复自动化单元测试
├── workflows/
│   └── domain-workflows.json             # 理工学术、涉外法律、史学与技术出版规则定义
├── dictionaries/
│   └── core-glossary-en-zh-ja.json       # 英中日三语学术与法律基础核心词典
├── web-studio/
│   ├── qa-engine.js                      # Cloud Studio v2.0 与三层不变量 QA 校验引擎
│   └── i18n.js                           # 英 / 中 / 日三语界面国际化词典
├── LICENSE                               # Apache License 2.0 开源许可证
├── README.md                             # 英文说明文档（默认首页）
├── README.zh-CN.md                       # 简体中文说明文档
└── README.ja.md                          # 日文说明文档
```

## 快速开始

使用 Node.js（v18+）在本地运行确定性结构化流式拆包器的自动化验证套件：

```bash
node packages/structured-stream-bridge/test-unpacker.mjs
```

---

## 维护者与公司信息

- **创始人兼首席架构师：** Nagamori Madokawa（`窓川 長盛`）
- **公司总部：** ZiyuanLingo — 日本东京都涩谷区涩谷 2-24-12 涩谷 Scramble Square（邮编 150-6139）
- **联系邮箱：** [founder@ziyuanlingo.com](mailto:founder@ziyuanlingo.com)
- **官方网站：** [https://ziyuanlingo.com](https://ziyuanlingo.com)

## 开源许可

本项目采用 Apache License 2.0 协议开源，详情请参阅 [LICENSE](./LICENSE)。
