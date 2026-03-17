# 💰 Backend - Sistema de Controle de Gastos

API responsável pelo gerenciamento das despesas do sistema de controle de gastos residenciais.

O backend fornece endpoints para cadastro, consulta, atualização e remoção de gastos registrados pelo usuário.

---

# 📌 Objetivo

Centralizar a lógica de processamento dos dados do sistema, garantindo o armazenamento, consulta e manipulação das informações relacionadas aos gastos.

---

# ⚙️ Tecnologias Utilizadas

* .NET
* Entity Framework Core
* MySQL
* MediatR
* Scalar

---

# 📦 Estrutura do Backend

```text
backend
│
├── Application
│
├── Infrastructure
│
└── WebAPI
```

---

# 📂 Descrição das Camadas

## Application

Responsável pela definição dos **modelos da aplicação**.

Aqui ficam estruturas utilizadas para representar os dados do sistema.

Exemplos:

* Modelos de dados
* DTOs
* Estruturas utilizadas entre as camadas

---

## Infrastructure

Responsável pela **lógica da aplicação e integração com o banco de dados**.

Nesta camada são implementados:

* Regras de negócio
* Persistência de dados
* Integração com banco de dados
* Implementações de serviços

---

## WebAPI

Camada responsável por expor os **endpoints HTTP da aplicação**.

Aqui estão:

* Controllers
* Configurações da API
* Registro de dependências
* Configuração do MediatR
* Documentação da API com Scalar

---

# 🚀 Funcionalidades da API

* Cadastro de gastos
* Atualização de gastos
* Exclusão de gastos
* Consulta de despesas
* Listagem de gastos por período

---

# ▶️ Como executar o backend

1. Navegue até a pasta do backend

2. Execute o comando:

```
dotnet run
```

A API será iniciada localmente para testes.
