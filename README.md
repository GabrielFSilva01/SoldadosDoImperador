# SoldadosDoImperador - Painel de Comando 🚀

![Warhammer 40k Aquila](https://upload.wikimedia.org/wikipedia/commons/thumb/e/e4/Aquila_of_the_Imperium_of_Man.svg/200px-Aquila_of_the_Imperium_of_Man.svg.png) ## 📜 Introdução

**SoldadosDoImperador** é uma aplicação web desenvolvida em ASP.NET Core MVC para gerenciamento de ativos militares no universo sombrio de Warhammer 40.000 (Grimdark). O sistema permite o cadastro, visualização, edição e exclusão de Astartes (Space Marines), suas missões, equipamentos, ordens e treinamentos, servindo como um painel de comando centralizado para oficiais do Imperium.

O projeto implementa autenticação e autorização robustas usando ASP.NET Core Identity, com níveis de acesso diferenciados para garantir a segurança dos dados imperiais.

## ✨ Funcionalidades Principais

* **Gerenciamento de Astartes (Soldados):** CRUD completo para perfis de Space Marines, incluindo Capítulo, Patente, Altura e Peso Corporal.
* **Gestão de Missões:** Criação de missões com objetivos, status e localização. Designação de **múltiplos** Astartes para cada missão (Relação Muitos-para-Muitos).
* **Controle de Treinamentos (Doutrinação):** Registro de sessões de treinamento com área de especialização e atribuição de **múltiplos** participantes (Relação Muitos-para-Muitos).
* **Emissão de Ordens:** Criação e atribuição de ordens diretas para Astartes individuais, com prazos e status.
* **Administração do Arsenal:** Gerenciamento do inventário de Itens de Batalha (Armas, Equipamentos, Utilitários) atribuídos aos Astartes.
* **Autenticação e Autorização:** Sistema de login seguro com ASP.NET Core Identity. Níveis de acesso definidos:
    * `PRIMARCH`: Acesso total (Administrador).
    * `Astartes`: Acesso de leitura (pode ser expandido).
* **Interface Temática:** Layout e estilos (CSS) customizados inspirados no universo *Grimdark* de Warhammer 40k.

## 🛠️ Tecnologias Utilizadas

* **Backend:** C#, ASP.NET Core MVC (.NET 7 ou superior recomendado)
* **Banco de Dados:** Entity Framework Core, SQL Server
* **Segurança:** ASP.NET Core Identity (Hashing PBKDF2 com HMAC-SHA256)
* **Frontend:** HTML, CSS, Bootstrap 5 (customizado), JavaScript (via ASP.NET Core)
* **Ferramentas:** Visual Studio 2022 / VS Code, .NET CLI, Git

## ⚙️ Configuração e Instalação

**Pré-requisitos:**

* [.NET SDK](https://dotnet.microsoft.com/download) (versão usada no projeto ou superior)
* [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express, Developer ou outra edição)
* Um cliente Git (como [Git for Windows](https://git-scm.com/download/win))

**Passos:**

1.  **Clonar o Repositório:**
    ```bash
    git clone [https://github.com/GabrielFSilva01/SoldadosDoImperador.git](https://github.com/GabrielFSilva01/SoldadosDoImperador.git)
    cd SoldadosDoImperador
    ```
2.  **Configurar a Connection String:**
    * Abra o arquivo `appsettings.json`.
    * Localize a seção `ConnectionStrings`.
    * Ajuste a string `"Conexao"` para apontar para o seu servidor SQL Server e defina o nome do banco de dados (ex: `Database=Warhammer`). Verifique se a autenticação (`Trusted_Connection=True` ou User ID/Password) está correta para o seu ambiente.
    ```json
     "ConnectionStrings": {
       "Conexao": "Server=SEU_SERVIDOR;Database=Warhammer;Trusted_Connection=True;TrustServerCertificate=True"
     }
    ```
3.  **Aplicar as Migrations:**
    * Abra o terminal na pasta raiz do projeto.
    * Execute o comando para criar/atualizar o banco de dados com todas as tabelas (incluindo as do Identity e os seeds):
    ```bash
    dotnet ef database update --context ContextoWarhammer
    ```
    *(O `--context` é necessário se houver mais de um DbContext).*
4.  **Executar a Aplicação:**
    ```bash
    dotnet run
    ```
    A aplicação estará disponível em `https://localhost:xxxx` (a porta será indicada no terminal).

## 🚀 Uso

1.  **Primeiro Acesso:** Ao acessar a aplicação, você será redirecionado para a página de login.
2.  **Login como PRIMARCH:** Use as credenciais definidas no arquivo `Data/SeedData.cs`:
    * **Email:** `PrimarchFerreira@ultramarine.com` (ou o que você definiu)
    * **Senha:** A senha forte que você definiu (ex: `Astarte!123`)
3.  **Navegação:** O painel de comando (Home Page) oferece acesso rápido às seções principais: Astartes, Missões, Doutrinação, Ordens e Arsenal.
4.  **Registro de Novos Usuários:** Novos usuários podem se registrar através do link "Registrar". Por padrão, eles receberão a *Role* "Astartes" (com acesso limitado, conforme configurado nos *Controllers*).

## 🔮 Funcionalidades Futuras (Sugestões)

* Implementação de **Esquadrões (Squads)** como entidade central.
* Criação de **Relatórios** de desempenho e status.
* Melhorias na UI para gerenciamento M-N (ex: seleção múltipla de soldados).
* Páginas de perfil de usuário mais detalhadas.
* Integração com APIs externas (se aplicável).

---

Pelo Imperador! Que este sistema sirva bem ao Trono Dourado.
