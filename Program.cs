using SistemaGestaoFaculdade.Models;
using SistemaGestaoFaculdade.Services;

void Prosseguir()
{
    Console.WriteLine("\nPressione ENTER para continuar...");
    Console.ReadLine();
    Console.Clear();
}

var cursoService = new CursoService();
var professorService = new ProfessorService();
var alunoService = new AlunoService();
var disciplinaService = new DisciplinaService();
var matriculaService = new MatriculaService();
var vinculoCursoDisciplinaService = new VinculoCursoDisciplinaService();
bool executando = true;
var boletimService = new BoletimService(matriculaService, vinculoCursoDisciplinaService);

while (executando)
{ 

    Console.WriteLine("\n=======================================");
    Console.WriteLine("\tGESTÃO DA FACULDADE\t");
    Console.WriteLine("=======================================\n");

    Console.WriteLine("1 - Cadastrar curso");
    Console.WriteLine("2 - Cadastrar professor");
    Console.WriteLine("3 - Cadastrar aluno");
    Console.WriteLine("4 - Cadastrar diciplina");
    Console.WriteLine("5 - Vincular diciplina a um curso");
    Console.WriteLine("6 - Matricular aluno em curso");
    Console.WriteLine("7 - Lançar nota");
    Console.WriteLine("8 - Consultar pessoas");


    Console.WriteLine("11 - Consultar boletim");

    Console.WriteLine("0 - Sair");

    Console.Write("\nEscolha uma opção: ");
    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t   Cadastro de Curso\t");
            Console.WriteLine("=======================================\n");

            Console.Write("Código: ");
            string codigo = Console.ReadLine() ?? "";

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.WriteLine("\nTipo do Curso:");
            Console.WriteLine("1 - Graduação | 2 - Pós-graduação");
            Console.Write("\nOpção: ");
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

            Prosseguir();
            break;
        }

        case "2":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t Cadastro de Professor\t");
            Console.WriteLine("=======================================\n");

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

            Prosseguir();
            break;
        }

        case "3":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t   Cadastro de Aluno\t");
            Console.WriteLine("=======================================\n");
            
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("CPF: ");
            string cpf = Console.ReadLine() ?? "";

            Console.Write("E-mail: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Matrícula: ");
            string matricula = Console.ReadLine() ?? "";

            try
            {
                alunoService.CadastrarAluno(nome, cpf, email, matricula);
                Console.WriteLine("\nAluno cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar aluno: {ex.Message}");
            }

            Prosseguir();
            break;
        }

        case "4":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\tCadastro de Disciplina\t");
            Console.WriteLine("=======================================\n");

            try
            {
                var professores = professorService.ObterTodosProfessores();

                if (professores.Count == 0)
                {
                    Console.WriteLine("Nenhum professor cadastrado. Cadastre um professor antes de cadastrar uma disciplina.");

                    Prosseguir();
                    break;
                }

                Console.Write("Código: ");
                string codigo = Console.ReadLine() ?? "";

                Console.Write("Nome: ");
                string nome = Console.ReadLine() ?? "";

                Console.Write("Carga horária: ");
                int cargaHoraria = int.TryParse(Console.ReadLine(), out int ch) ? ch : 0;

                Console.WriteLine("\nProfessores cadastrados:");
                foreach (var p in professores)
                    Console.WriteLine($"Registro: {p.Registro} | Nome: {p.Nome}");

                Console.Write("\nRegistro do professor responsável: ");
                string registro = Console.ReadLine() ?? "";

                var professorResponsavel = professores.FirstOrDefault(p => p.Registro == registro.Trim());

                if (professorResponsavel is null)
                {
                    Console.WriteLine($"Erro ao cadastrar disciplina: Não foi encontrado nenhum professor com o registro '{registro}'.");

                    Prosseguir();
                    break;
                }

                disciplinaService.CadastrarDisciplina(codigo, nome, cargaHoraria, professorResponsavel);
                Console.WriteLine("\nDisciplina cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar disciplina: {ex.Message}");
            }

            Prosseguir();
            break;
        }

        case "5":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("    Vincular Disciplina a um Curso\t");
            Console.WriteLine("=======================================\n");
           
            try
            {
                var cursos = cursoService.ObterTodosCursos();
                var disciplinas = disciplinaService.ObterTodasDisciplinas();

                if (cursos.Count == 0 || disciplinas.Count == 0)
                {
                    Console.WriteLine("É necessário ter ao menos um curso e uma disciplina cadastrados.");

                    Prosseguir();
                    break;
                }

                Console.WriteLine("Cursos cadastrados:");
                foreach (var c in cursos)
                    Console.WriteLine($"Código: {c.Codigo} | Nome: {c.Nome}");

                Console.Write("\nCódigo do curso: ");
                string codigoCurso = Console.ReadLine() ?? "";

                var curso = cursos.FirstOrDefault(c => c.Codigo == codigoCurso.ToUpper().Trim());

                if (curso is null)
                {
                    Console.WriteLine($"Erro ao vincular disciplina ao curso: não foi encontrado nenhum curso com o código '{codigoCurso}'.");

                    Prosseguir();
                    break;
                }

                Console.WriteLine("\nDisciplinas cadastradas:");
                foreach (var d in disciplinas)
                    Console.WriteLine($"Código: {d.Codigo} | Nome: {d.Nome}");

                Console.Write("\nCódigo da disciplina: ");
                string codigoDisciplina = Console.ReadLine() ?? "";

                var disciplina = disciplinas.FirstOrDefault(d => d.Codigo == codigoDisciplina.ToUpper().Trim());

                if (disciplina is null)
                {
                    Console.WriteLine($"Erro ao vincular disciplina ao curso: não foi encontrada nenhuma disciplina com o código '{codigoDisciplina}'.");

                    Prosseguir();
                    break;
                }

                vinculoCursoDisciplinaService.VincularDisciplinaAoCurso(curso, disciplina);
                Console.WriteLine("\nDisciplina vinculada ao curso com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao vincular disciplina ao curso: {ex.Message}");
            }

            Prosseguir();
            break;
        }

        case "6":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("       Matricular Aluno em Curso\t");
            Console.WriteLine("=======================================\n");

            try
            {
                var alunos = alunoService.ObterTodosAlunos();
                var cursos = cursoService.ObterTodosCursos();

                if (alunos.Count == 0 || cursos.Count == 0)
                {
                    Console.WriteLine("É necessário ter ao menos um aluno e um curso cadastrados.");

                    Prosseguir();
                    break;
                }

                Console.WriteLine("Alunos cadastrados:");
                foreach (var a in alunos)
                    Console.WriteLine($"Matrícula: {a.Matricula} | Nome: {a.Nome}");

                Console.Write("\nMatrícula do aluno: ");
                string matriculaAluno = Console.ReadLine() ?? "";

                var aluno = alunos.FirstOrDefault(a => a.Matricula == matriculaAluno.Trim());

                if (aluno is null)
                {
                    Console.WriteLine($"Erro ao matricular aluno: não foi encontrado nenhum aluno com a matrícula '{matriculaAluno}'.");

                    Prosseguir();
                    break;
                }

                Console.WriteLine("\nCursos cadastrados:");
                foreach (var c in cursos)
                    Console.WriteLine($"Código: {c.Codigo} | Nome: {c.Nome} | Tipo: {c.Tipo}");

                Console.Write("\nCódigo do curso: ");
                string codigoCurso = Console.ReadLine() ?? "";

                var curso = cursos.FirstOrDefault(c => c.Codigo == codigoCurso.ToUpper().Trim());

                if (curso is null)
                {
                    Console.WriteLine($"Erro ao matricular aluno: não foi encontrado nenhum curso com o código '{codigoCurso}'.");

                    Prosseguir();
                    break;
                }

                matriculaService.MatricularAluno(aluno, curso);
                Console.WriteLine($"\nAluno matriculado com sucesso no curso '{curso.Nome}'!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao matricular aluno: {ex.Message}");
            }

            Prosseguir();
            break;
        }

        case "7": // Samla
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t      Lançar Nota\t");
            Console.WriteLine("=======================================\n");

            Console.Write("Matrícula do aluno: ");
            string matriculaAluno = Console.ReadLine() ?? "";

            Console.Write("Código do curso: ");
            string codigoCurso = Console.ReadLine() ?? "";

            Console.Write("Código da disciplina: ");
            string codigoDisciplina = Console.ReadLine() ?? "";

            Console.Write("\nNota: ");
            decimal nota = decimal.Parse(Console.ReadLine() ?? "0");

            try
            {
                boletimService.LancarNota(
                    matriculaAluno,
                    codigoCurso,
                    codigoDisciplina,
                    nota);

                Console.WriteLine("\nNota lançada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }

            Prosseguir();
            break;
        }

        case "8": // Samla
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t  Consulta de Pessoas\t");
            Console.WriteLine("=======================================\n");

            alunoService.ConsultarAlunos(matriculaService);
            professorService.ConsultarProfessores();

            Prosseguir();
            break;
        }

        case "9":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t  Consulta de Cursos\t");
            Console.WriteLine("=======================================\n");

            // Código aqui (cada vez que adicionar um novo break, inserir antes a função Prosseguir();

            Prosseguir();
            break;
        }
        case "10":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\tConsulta de Matrículas\t");
            Console.WriteLine("=======================================\n");

            // Código aqui (cada vez que adicionar um novo break, inserir antes a função Prosseguir();

            Prosseguir();
            break;
        }
        case "11": // Samla
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\t Consulta de Boletim\t");
            Console.WriteLine("=======================================\n");

            Console.Write("Matrícula do aluno: ");
            string matriculaAluno = Console.ReadLine() ?? "";

            Console.Write("Código do curso: ");
            string codigoCurso = Console.ReadLine() ?? "";

            try
            {
                boletimService.ConsultarBoletim(
                    matriculaAluno,
                    codigoCurso);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }

            Prosseguir();
            break;
        }

        case "12":
        {
            Prosseguir();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("\tEnvio de Notificações\t");
            Console.WriteLine("=======================================\n");

            // Código aqui (cada vez que adicionar um novo break, inserir antes a função Prosseguir();

            Prosseguir();
            break;
        }
        case "0":
        {
            executando = false;
            Console.WriteLine("\nSaindo do sistema...");
            break;
        }

        default:
        {
            Console.WriteLine("\nOpção inválida! Tente novamente.");

            Prosseguir();
            break;
        }
    }
}