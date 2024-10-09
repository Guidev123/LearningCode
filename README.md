 <h1>API - Microsserviço Learning Code</h1>
        <p>Este projeto é uma API que faz parte do microsserviço <strong>Learning Code</strong>. Ela é responsável pelas seguintes operações relacionadas aos usuários da aplicação:</p>
        <ul>
            <li>Login</li>
            <li>Criação de usuários</li>
            <li>Exclusão lógica de usuários</li>
        </ul>

  <h2>Autenticação</h2>
        <p>Para acessar os endpoints, são necessárias duas autenticações que devem ser incluídas no cabeçalho (Header) das requisições:</p>
        <ul>
            <li><strong>AcessSecretKey:</strong> Utilizada para acessar os endpoints de criação e login de usuários.</li>
            <li><strong>Token:</strong> Este token é retornado pelo endpoint de login e, juntamente com a <code>AcessSecretKey</code>, permite o acesso a todos os outros endpoints da API.</li>
        </ul>

  <h2>Endpoints</h2>
        <p>A API possui os seguintes endpoints:</p>


 ![image](https://github.com/user-attachments/assets/89c8ad80-725f-4173-a82e-6e53511ee8fc)


<h3>Exemplo de Requisição: /api/customers/register</h3>
        <pre>
        POST /api/customers/register
        Body:
        {
          "fullName": "string",
          "phone": "string",
          "document": "string",
          "email": "string",
          "password": "string",
          "birthDate": "2024-10-08T23:07:35.965Z"
        }
        </pre>

  <h3>Exemplo de Requisição: /api/customers/login</h3>
        <pre>
        POST /api/customers/login
        Body:
        {
          "email": "string",
          "password": "string"
        }
        </pre>

  <h2>Observações</h2>
  <p>Certifique-se de sempre enviar o <strong>AcessSecretKey</strong> e o <strong>Token</strong> corretos, caso contrário, o acesso aos endpoints será negado.</p>
