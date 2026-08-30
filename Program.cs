using SistemaGestaoFaculdade.Models;
using SistemaGestaoFaculdade.Services;

var cursoService = new CursoService();
var professorService = new ProfessorService();
bool executando = true;

while (executando)
{
    Console.WriteLine("\n========= GESTÃO DA FACULDADE =========");
    Console.WriteLine("1 - Cadastrar curso");
    Console.WriteLine("2 - Cadastrar professor");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("=======================================");
    Console.Write("Escolha uma opção: ");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
        {
            Console.WriteLine("\n--- CADASTRO DE CURSO ---");
            Console.Write("Código: ");
            string codigo = Console.ReadLine() ?? "";

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.WriteLine("Tipo do Curso:");
            Console.WriteLine("1 - Graduação");
            Console.WriteLine("2 - Pós-graduação");
            Console.Write("Opção: ");
            string tipoInput = Console.ReadLine() ?? "";

            TipoCurso tipo = tipoInput == "2" ? TipoCurso.PosGraduacao : TipoCurso.Graduacao;

            try
            {
                cursoService.CadastrarCurso(codigo, nome, tipo);
                Console.WriteLine("\nCurso cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar curso: {ex.Message}");
            }
            break;
        }

        case "2":
        {
            Console.WriteLine("\n--- CADASTRO DE PROFESSOR ---");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("CPF: ");
            string cpf = Console.ReadLine() ?? "";

            Console.Write("E-mail: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Registro: ");
            string registro = Console.ReadLine() ?? "";

            Console.Write("Especialidade: ");
            string especialidade = Console.ReadLine() ?? "";

            try
            {
                professorService.CadastrarProfessor(nome, cpf, email, registro, especialidade);
                Console.WriteLine("\nProfessor cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar professor: {ex.Message}");
            }
            break;
        }

        case "0":
        {
            executando = false;
            Console.WriteLine("\nSaindo do sistema...");
            break;
        }
        
        default:
            Console.WriteLine("\nOpção inválida! Tente novamente.");
            break;
    }
}