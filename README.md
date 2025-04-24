# Projeto Agenda de Contatos
## Feita por Arthur  Ciliberti de Oliveira

Este projeto tem como objetivo desenvolver uma **API com um CRUD e com interface visual (simples)** para uma **agenda de contatos** que permita:

- Cadastrar novos contatos
- Listar todos ou filtrar
- Editar os dados diretamente na interface
- Excluir contatos, com **backup automático no banco de dados**
- Visualizar os contatos excluídos com data de exclusão
---

## Estrutura das Pastas
```
AgendaContatos/
├── AgendaContatos/              # Projeto principal da API (.NET 8)
│   ├── Controllers/             # ContatosController com endpoints REST
│   ├── Models/                  # Modelos de dados (Contato, DTOs)
│   ├── Data/                    # DbContext com configuração PostgreSQL
│   └── appsettings.json         # 🔴 ATENÇÃO: Altere o Host, Port, Database, Username e Password conforme o seu banco de dados
│
├── wwwroot/                     # Interface visual (HTML + JS)
│   ├── index.html               # Página com inputs, tabela e botões
│   └── script.js                # JavaScript separado (AJAX, máscaras, filtros)
│
├── sql/                         # Scripts SQL do projeto
│   ├── criar_tabelas.sql        # Criação das tabelas principais
│   ├── procedure_backup.sql     # Procedure de backup de exclusão
│   └── massa_teste.sql          # Inserção de contatos para testes
```

## Tecnologias e ferramentas utilizadas

- [.NET 8] – Framework da API
- [PostgreSQL] – Banco de dados relacional
- [Entity Framework Core] – ORM para conexão entre C# e o banco
- [jQuery] – Manipulação da DOM no frontend
- [jQuery Mask Plugin] – Máscaras para CPF e telefone
- HTML + CSS – Interface básica com foco funcional

---

## Informações importantes

- Para ver a interface gráfica, acesse depois de inicializar o projeto: http://localhost:5195/index.html
- **Importante:** ao rodar o projeto pela primeira vez, selecione **inicialização com HTTP** (não HTTPS), pois o navegador pode bloquear a interface por certificado inválido local.

## Scripts SQL

Todos os scripts estão na pasta [`/sql`]:

- `criar_tabelas.sql`: Criação das tabelas `contato` e `contato_backup`
- `procedure_backup.sql`: Procedure que move contatos excluídos para backup
- `massa_teste.sql`: Inserção de contatos para testes e filtros
