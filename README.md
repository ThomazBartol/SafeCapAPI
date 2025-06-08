# SafeCap API

**SafeCap** é uma API REST desenvolvida em .NET 8 com Entity Framework Core e banco de dados Oracle, desenvolvida como parte da Global Solution do primeiro semestre de 2025.
Essa API consiste em gerenciar Usuários, Leituras feitas por sensores em nossos SafeCaps (Bonés Inteligentes), e também Alertas baseados nessas leituras.

## Rotas Disponíveis

---

### Usuários (`/api/users`)

- **GET /api/users** — Lista os Usuários, com filtros opcionais via query params:

  | Query Param  | Tipo    | Descrição                                    | Exemplo         |
  |--------------|---------|----------------------------------------------|-----------------|
  | name         | string  | Filtra os usuários pelo nome de usuário      | `/api/users?name=Gabriel` |
  | email        | string  | Filtra os usuários pelo email                | `/api/users?email=gabriel@gmail.com` |

- **GET /api/users/{id}** — Busca usuário pelo ID.

- **POST /api/users** — Cria um novo usuário.

- **PUT /api/users/{id}** — Atualiza um usuário existente.

- **DELETE /api/users/{id}** — Remove um usuário.

---

## Instruções de Execução

1. Clone o repositório:
   ```bash
   git clone https://github.com/ThomazBartol/SafeCapAPI.git
   cd SafeCapAPI/

2. Crie dentro da pasta SafeCap (no mesmo diretório que o .csproj):
    arquivo .env contendo:
   ```bash
   ORACLE_CONNECTION_STRING=User Id={usuário};Password={senha};Data Source=oracle.fiap.com.br:1521/ORCL

4. Rode o projeto com o comando:
   ```bash
   dotnet run

5. Caso o Swagger não abra sozinho acesse em:
   https://localhost:7266/swagger/index.html

## 👥 INTEGRANTES DO GRUPO
===========================

- RM555323 - Thomaz Oliveira Vilas Boas Bartol
- RM556089 - Vinicius Souza Carvalho
- RM556972 - Gabriel Duarte Pinto
