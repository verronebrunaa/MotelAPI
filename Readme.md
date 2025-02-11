# MotelAPI

## 📌 Sobre o Projeto
MotelAPI é uma API RESTful desenvolvida em .NET Core para gerenciamento eficiente de motéis, oferecendo funcionalidades como cadastro de usuários, controle de reservas e faturamento. A API visa otimizar a gestão de dados de clientes, tipos de suítes e histórico de reservas, além de fornecer relatórios de faturamento mensal.

## 🚀 Tecnologias Utilizadas

<p align="center">
  <img src="https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Core">
  <img src="https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="Entity Framework Core">
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server">
  <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white" alt="JWT">
</p>

## 📋 Funcionalidades
- [ ] **Cadastro e login de usuários:** Garante a segurança no acesso à API.
- [ ] **Listagem de reservas filtradas por data:** Otimiza a consulta de histórico de reservas.
- [ ] **Obtenção de faturamento mensal:** Proporciona visibilidade para análise financeira.
- [ ] **Cadastro de moteis e de suítes:** 
- [ ] **Gerenciamento de reserva:** 

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
Antes de rodar o projeto, instale os pacotes necessários. Você pode usar o seguinte comando para instalar todas as dependências de uma vez:
```sh
dotnet restore
```
Caso tenha problemas na instalação, limpe o cache e restaure os pacotes manualmente:
```sh
dotnet nuget locals all --clear
```

### 📂 Passo 4: Configurar o Banco de Dados
1. Configure a string de conexão no arquivo `appsettings.json` com suas credenciais:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=SEU_SERVIDOR;Database=MotelDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
    }
    ```
2. Execute as migrações para criar as tabelas no banco:
    ```sh
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

**Dica:** Caso queira utilizar um banco de dados local, você pode instalar o SQL Server Express e configurar a string de conexão com `Server=localhost`.

### ▶️ Passo 5: Executar a API
Para rodar a API, utilize:
```sh
dotnet run
```
A API ficará disponível em `http://localhost:5000`. O Swagger da aplicação estará em `http://localhost:5027/index.html`

## 🧪 Testes (🚧 Em construção)
Para rodar os testes automatizados do projeto, execute:
```sh
dotnet test
```
Os testes são realizados com o framework xUnit e cobrem as principais funcionalidades da API.
