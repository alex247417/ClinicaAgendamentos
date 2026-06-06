# 🏥 Clínica Agendamentos

Sistema de gestão de agendamentos de consultas médicas desenvolvido como desafio técnico.

## 📋 Índice

- [Como executar](#-como-executar-localmente)
- [Acesso ao sistema](#-acesso-ao-sistema)
- [Estrutura do projeto](#-estrutura-do-projeto)
- [Regras de negócio](#-regras-de-negócio-implementadas)
- [Testes](#-testes)
- [Decisões técnicas](#️-decisões-técnicas)
- [Tecnologias](#️-tecnologias)

---

## 🚀 Como executar localmente

### Pré-requisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e rodando

### Executar com Docker

```bash
git clone https://github.com/alex247417/ClinicaAgendamentos.git
cd ClinicaAgendamentos
docker-compose up --build
```

| Serviço | URL |
|---------|-----|
| Frontend | http://localhost:3000 |
| API | http://localhost:8080 |
| Swagger | http://localhost:8080/swagger |

> O banco de dados SQLite é criado e populado automaticamente na primeira execução — nenhuma configuração adicional necessária.

---

## 🔐 Acesso ao sistema

| Campo | Valor |
|-------|-------|
| Email | admin@clinica.com |
| Senha | 123456 |

---

## 📁 Estrutura do projeto

```
ClinicaAgendamentos/
├── ClinicaAgendamentos.API/            # Controllers, Middleware, Swagger
├── ClinicaAgendamentos.Application/    # Casos de uso
├── ClinicaAgendamentos.Domain/         # Entidades e interfaces
├── ClinicaAgendamentos.Infrastructure/ # Repositórios (Dapper) e banco
├── ClinicaAgendamentos.Tests/          # Testes unitários
└── clinica-frontend/                   # React + Vite
```

## 📋 Regras de negócio implementadas

1. ✅ Paciente só pode ter 1 consulta por profissional por dia
2. ✅ Profissional só pode atender 1 consulta por horário
3. ✅ Atendimento apenas seg–sex, 08:00–18:00
4. ✅ Consultas com duração fixa de 30 minutos
5. ✅ Validação de disponibilidade antes de confirmar
6. ✅ Não é permitido agendar consultas em datas passadas

---

## 🧪 Testes

```bash
dotnet test
```

3 testes unitários cobrindo:
- Validação de agendamento em fim de semana
- Validação de horário inválido
- Regra de paciente duplicado no mesmo dia

---

## 🏗️ Decisões técnicas

### Arquitetura

Projeto estruturado seguindo **Clean Architecture + DDD** com separação clara entre camadas:

- **Domain** — entidades e regras de negócio puras, sem dependências externas
- **Application** — casos de uso que orquestram o fluxo entre Domain e Infrastructure
- **Infrastructure** — repositórios com Dapper + SQLite, inicialização do banco
- **API** — controllers REST, middleware de log, autenticação JWT e Swagger

### Por que SQLite?
Leve, sem servidor separado e portável via Docker volume — ideal para avaliação e desenvolvimento local.

### Por que Dapper?
Controle total sobre as queries SQL, performance superior ao EF Core em cenários simples, e explicitamente exigido pelo desafio.

### Por que Use Cases em vez de Services?
Use Cases tornam explícita a intenção de cada operação de negócio, alinhando-se com DDD e Clean Architecture — cada arquivo responde a uma única ação do sistema.

### Banco de dados
O schema é criado automaticamente via `DatabaseInitializer` na inicialização da aplicação. Um usuário administrador de teste é inserido via seed na primeira execução.

---

## 🛠️ Tecnologias

| Tecnologia | Uso |
|-----------|-----|
| .NET 8 / ASP.NET Core | API REST |
| Dapper | Acesso a dados |
| SQLite | Banco de dados |
| JWT + BCrypt | Autenticação |
| xUnit + Moq | Testes unitários |
| Docker + Docker Compose | Containerização |
| React + Vite + Axios | Frontend SPA |
| Swagger / OpenAPI | Documentação da API |
