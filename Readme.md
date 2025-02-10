# MotelAPI

## 📌 Sobre o Projeto
MotelAPI é uma API RESTful desenvolvida em .NET Core para gerenciamento de motéis, incluindo funcionalidades como cadastro de usuários, reservas e faturamento. 

## 🚀 Tecnologias Utilizadas
- .NET Core 9.0
- Entity Framework Core
- SQL Server
- JWT para autenticação

## 📋 Funcionalidades
- [] Cadastro e login de usuários com autenticação JWT
- [] Endpoint para listar reservas filtradas por data
- [] Endpoint otimizado para obter faturamento mensal
- [] Modelo relacional para tipos de suíte, motéis, clientes e reservas
- [] Cache para otimização da listagem de reservas

## 🛠️ Configuração do Ambiente

### 🔍 Passo 1: Verificar a Instalação do .NET SDK
Certifique-se de que o .NET SDK está instalado. Para verificar, execute:
```sh
dotnet --version
```
Se o .NET não estiver instalado, faça o download e instale a versão mais recente do SDK em:
[.NET Download](https://dotnet.microsoft.com/en-us/download/dotnet)

### 🔍 Passo 2: Verificar a Conexão com o NuGet
O NuGet é o gerenciador de pacotes do .NET. Verifique se ele está corretamente configurado:
```sh
dotnet nuget list source
```
Se o repositório oficial `https://api.nuget.org/v3/index.json` **não** estiver listado, adicione-o manualmente:
```sh
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
```

### 🔧 Passo 3: Instalar Dependências
Antes de rodar o projeto, instale os pacotes necessários:
```sh
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.4
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.4
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.4
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.4
```
Caso tenha problemas na instalação, limpe o cache e restaure os pacotes:
```sh
dotnet nuget locals all --clear
dotnet restore
```

### 📂 Passo 4: Configurar o Banco de Dados
1. Configure a string de conexão no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=MotelDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
}
```
2. Execute as migrações do banco de dados:
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### ▶️ Passo 5: Executar a API
Para rodar a API, utilize:
```sh
dotnet run
```
A API ficará disponível em `http://localhost:5000`. 
O Swagger da aplicação em `http://localhost:5027/index.html`

## 🔒 Autenticação JWT
A API utiliza autenticação via JWT. Para acessar endpoints protegidos:
1. Realize login e obtenha um token JWT.
2. Adicione o token no `Authorization` Header das requisições:
   ```sh
   Authorization: Bearer SEU_TOKEN_JWT
   ```

## 📌 Endpoints Principais

### 1️⃣ Autenticação
- **POST** `/api/auth/register` - Cadastro de usuário
- **POST** `/api/auth/login` - Login e geração do token JWT

### 2️⃣ Reservas
- **GET** `/api/reservas?dataInicio=YYYY-MM-DD&dataFim=YYYY-MM-DD` - Listar reservas filtradas por data
- **GET** `/api/reservas/faturamento?mes=MM&ano=YYYY` - Obter faturamento mensal

## ✅ Critérios de Avaliação
- Organização e clareza do código
- Segurança da API
- Eficiência das queries SQL

## 🤝 Contribuição
Se quiser contribuir, sinta-se à vontade para abrir um Pull Request! 😃

---
Feito com ❤️ por verronebrunaa 🚀

