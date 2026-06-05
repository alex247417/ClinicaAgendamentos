# 🏥 Clínica Agendamentos

Sistema de gestão de agendamentos de consultas médicas desenvolvido como desafio técnico.

## 🚀 Como executar localmente

### Pré-requisitos
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e rodando

### Executar com Docker
```bash
git clone https://github.com/alex247417/ClinicaAgendamentos.git
cd ClinicaAgendamentos
docker-compose up --build
```

A API estará disponível em: `http://localhost:8080`  
Swagger: `http://localhost:8080/swagger`

### Usuário de teste
| Campo | Valor |
|-------|-------|
| Email | admin@clinica.com |
| Senha | 123456 |

---

## 🏗️ Decisões Técnicas

### Arquitetura
Projeto estruturado seguindo **DDD (Domain-Driven Design)** com separação clara entre camadas:

- **Domain** — entidades e regras de negócio puras, sem dependências externas
- **Application** — casos de uso que orquestram o fluxo entre Domain e Infrastructure
- **Infrastructure** — implementação de repositórios com Dapper + SQLite
- **API** — controllers REST, autenticação JWT e Swagger

### Por que SQLite?
SQLite foi escolhido por ser leve, sem necessidade de servidor separado e facilmente portável via Docker volume — ideal para ambiente de desenvolvimento e avaliação.

### Por que Dapper?
Dapper oferece controle total sobre as queries SQL, performance superior ao EF Core em cenários simples e é explicitamente exigido pelo desafio.

### Por que Use Cases em vez de Services?
Use Cases deixam explícita a intenção de cada operação de negócio, alinhando-se melhor com os princípios de DDD e Clean Architecture.

---

## 📋 Regras de Negócio Implementadas

1. ✅ Paciente só pode ter 1 consulta por profissional por dia
2. ✅ Profissional só pode atender 1 consulta por horário
3. ✅ Atendimento apenas seg–sex, 08:00–18:00
4. ✅ Consultas com duração fixa de 30 minutos
5. ✅ Validação de disponibilidade antes de confirmar

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

## 🛠️ Tecnologias

| Tecnologia | Uso |
|-----------|-----|
| .NET 8 / ASP.NET Core | API REST |
| Dapper | Acesso a dados |
| SQLite | Banco de dados |
| JWT + BCrypt | Autenticação |
| xUnit + Moq | Testes |
| Docker | Containerização |
| React + Axios | Frontend |
| Swagger | Documentação |