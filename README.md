# Minimal API com JWT

## Descrição

Esta API minimalista, desenvolvida em parceria com a DIO, oferece funcionalidades para o cadastro e login de Administradores e Editores com autenticação JWT, garantindo níveis de permissões diferenciados. Os Administradores têm acesso total, incluindo a capacidade de alterar e deletar registros de veículos, enquanto os Editores podem apenas criar e visualizar esses registros. A API também inclui um CRUD de veículos com permissões restritas e utiliza Swagger para documentação interativa e fácil acesso aos endpoints.

## Funcionalidades

A API possui os seguintes endpoints:

### Administradores

- **POST** - `/administradores/login`: Realiza o login e retorna um token JWT.
- **POST** - `/administradores`: Cria um novo administrador.
- **GET** - `/administradores`: Lista todos os administradores.
- **GET** - `/administradores/{id}`: Obtém detalhes de um administrador específico.

### Veículos

- **POST** - `/veiculos`: Cria um novo veículo.
- **GET** - `/veiculos`: Lista todos os veículos.
- **GET** - `/veiculos/{id}`: Obtém detalhes de um veículo específico.
- **PUT** - `/veiculos/{id}`: Atualiza um veículo existente.
- **DELETE** - `/veiculos/{id}`: Deleta um veículo existente.

## Tecnologias Utilizadas

- **.NET Core 8**
- **AspNetCore.Authentication.JwtBearer** (Version: 8.0.8)
- **EntityFrameworkCore** (Version: 8.0.8)
- **EntityFrameworkCore.Design** (Version: 8.0.8)
- **Pomelo.EntityFrameworkCore.MySql** (Version: 8.0.2)
- **Swashbuckle.AspNetCore** (Version: 6.7.2)
- **AspNetCore.Mvc.Testing** (Version: 8.0.8)

## Instalação

1. **Clone o Repositório**

   ```bash
   git clone <URL_DO_REPOSITORIO>
   
2. **Abra o Projeto**
   
- Visual Studio: Abra o arquivo .sln com o Visual Studio.
- VS Code: Navegue até o diretório do projeto e execute:
  
  ```bash
  code .

3. **Instale as Dependências**
Navegue até o diretório do projeto e execute:

   ```bash
   dotnet restore
   
4. **Configure o Ambiente**
   
- Abra o arquivo appsettings.json e configure a string de conexão com o MySQL. Certifique-se de que o título da string de conexão seja "mysql".
   
5. **Execute a Aplicação**

- Visual Studio: Clique em "Iniciar" para rodar a aplicação.

- CLI: No diretório do projeto, execute:
  ```bash
  dotnet run
  
## Swagger

Para acessar a documentação interativa da API, inicie a aplicação e acesse [Swagger UI](http://localhost:5000/swagger) (substitua `localhost:5000` pela URL e porta em que a aplicação está rodando).

## Autenticação

Para testar a autenticação, utilize o usuário padrão com as seguintes credenciais:

- **Email**: administrador@teste.com
- **Senha**: 123456

Realize uma requisição POST para o endpoint `/administradores/login` com as credenciais acima para obter um token JWT.

# Testes

Para rodar os testes:

1. Navegue até a pasta de testes no projeto.
2. Configure a pasta de testes como o projeto padrão.
3. Execute os testes com o comando:

    ```bash
    dotnet test
    ```

## Links e Recursos

- [Documentação do .NET Core](https://docs.microsoft.com/dotnet/core)
- [Swagger](http://localhost:5000/swagger) (substitua `localhost:5000` pela URL e porta em que a aplicação está rodando)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)

## Contribuição
Se você deseja contribuir para o projeto, por favor, abra uma issue ou faça um pull request. Agradeço a sua colaboração!

- Você pode copiar e colar este conteúdo em um arquivo `.md` para documentar o processo de execução do seu projeto.


