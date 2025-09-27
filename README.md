# Meu Negócio Simples API (Backend)

Este é o backend do sistema Meu Negócio Simples, desenvolvido em .NET. Ele fornece a API para autenticação, produtos, fornecedores, estoque, financeiro e vendas.

## Funcionalidades
- Autenticação JWT
- Cadastro e gestão de produtos
- Controle de fornecedores
- Gestão de estoque
- Módulo financeiro
- Ponto de venda (PDV)

## Como executar

1. Instale o .NET SDK (>= 7.0)
2. Execute o comando:
   ```bash
   dotnet run
   ```
3. A API estará disponível em `http://localhost:5000` (ou porta configurada)

## Estrutura de pastas
- `Migrations`: Migrações do banco de dados
- `src`: Código principal da API
- `appsettings.json`: Configurações do projeto

## Requisitos
- .NET 7 ou superior
- Banco de dados SQL Server (configurável)

## Licença
Este projeto é proprietário. O uso, cópia, modificação ou distribuição não são permitidos sem autorização expressa do autor.
