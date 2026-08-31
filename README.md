# Sistema de Gestão de Faculdade

![Status do Projeto](https://img.shields.io/badge/status-em%20desenvolvimento-brightgreen)
![Linguagem](https://img.shields.io/badge/linguagem-C%23-blue)
![Tecnologia](https://img.shields.io/badge/.NET-8.0-purple)


Sistema em C# desenvolvido via console para o gerenciamento de cadastros de uma instituição de ensino. O projeto aplica os pilares da Orientação a Objetos e boas práticas de código limpo (*Clean Code*).

## 🛠️ Tecnologias Utilizadas

* **C# / .NET:** Linguagem principal e plataforma de execução do projeto.
* **Git & GitHub:** Controle de versão, organização por branches e gerenciamento de Pull Requests via *Issues*.
* **Conceitos Aplicados:** Orientação a Objetos (Herança e Encapsulamento), Programação Defensiva e Tratamento de Exceções.

## 📂 Estrutura do Projeto

A arquitetura do código foi dividida em responsabilidades claras para facilitar a manutenção e o entendimento da equipe:

* **`Models/` (Os Dados):** Contém os moldes e estruturas de dados do sistema (`Curso`, `Pessoa`, `Professor` e `Aluno`). As classes definem os atributos e realizam validações básicas de formato (ex: garantir que o CPF tenha exatamente 11 dígitos numéricos).
* **`Services/` (Os Processos):** Contém os gerenciadores de regras de negócio (`CursoService`, `ProfessorService`, `AlunoService`). Controlam as listas em memória e impedem inconsistências, como duplicidade de CPFs, matrículas ou códigos de curso.
* **`Program.cs`:** Porta de entrada da aplicação que gerencia a interface visual do menu interativo no console e delega o processamento lógico para a camada de serviços.

## 🚀 Como Executar

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/) instalado em sua máquina.
2. Clone o repositório e navegue até a pasta raiz do projeto.
3. Execute o comando abaixo no terminal para iniciar o menu interativo:
   ```bash
   dotnet run