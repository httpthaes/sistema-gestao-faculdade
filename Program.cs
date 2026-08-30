using SistemaGestaoFaculdade.Models;
using SistemaGestaoFaculdade.Services;

var cursoService = new CursoService();
var professorService = new ProfessorService();
var alunoService = new AlunoService();
bool executando = true;

while (executando)
{
    Console.WriteLine("\n========= GESTÃO DA FACULDADE =========");
    Console.WriteLine("1 - Cadastrar curso");
    Console.WriteLine("2 - Cadastrar professor");
    Console.WriteLine("3 - Cadastrar aluno");
    Console.WriteLine("4 - Cadastrar diciplina");
    Console.WriteLine("5 - Vincular diciplina a um curso");
    Console.WriteLine("6 - Matricular aluno em curso");

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

        case "3":
        {
            Console.WriteLine("\n--- CADASTRO DE ALUNO ---");
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
            break;
        }

        case "4":
            {
                Console.WriteLine("\n--- CADASTRO DE DISCIPLINA ---");

                try
                {
                    var professores = professorService.ObterTodosProfessores();

                    if (professores.Count == 0)
                    {
                        Console.WriteLine("\nNenhum professor cadastrado. Cadastre um professor antes de cadastrar uma disciplina.");
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
                        Console.WriteLine($"\nErro ao cadastrar disciplina: não foi encontrado nenhum professor com o registro '{registro}'.");
                        break;
                    }

                    disciplinaService.CadastrarDisciplina(codigo, nome, cargaHoraria, professorResponsavel);
                    Console.WriteLine("\nDisciplina cadastrada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro ao cadastrar disciplina: {ex.Message}");
                }
                break;
            }

        case "5":
            {
                Console.WriteLine("\n--- VINCULAR DISCIPLINA A UM CURSO ---");

                try
                {
                    var cursos = cursoService.ObterTodosCursos();
                    var disciplinas = disciplinaService.ObterTodasDisciplinas();

                    if (cursos.Count == 0 || disciplinas.Count == 0)
                    {
                        Console.WriteLine("\nÉ necessário ter ao menos um curso e uma disciplina cadastrados.");
                        break;
                    }

                    Console.WriteLine("\nCursos cadastrados:");
                    foreach (var c in cursos)
                        Console.WriteLine($"Código: {c.Codigo} | Nome: {c.Nome}");

                    Console.Write("\nCódigo do curso: ");
                    string codigoCurso = Console.ReadLine() ?? "";

                    var curso = cursos.FirstOrDefault(c => c.Codigo == codigoCurso.ToUpper().Trim());

                    if (curso is null)
                    {
                        Console.WriteLine($"\nErro ao vincular disciplina ao curso: não foi encontrado nenhum curso com o código '{codigoCurso}'.");
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
                        Console.WriteLine($"\nErro ao vincular disciplina ao curso: não foi encontrada nenhuma disciplina com o código '{codigoDisciplina}'.");
                        break;
                    }

                    vinculoCursoDisciplinaService.VincularDisciplinaAoCurso(curso, disciplina);
                    Console.WriteLine("\nDisciplina vinculada ao curso com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro ao vincular disciplina ao curso: {ex.Message}");
                }
                break;
            }

        case "6":
            {
                Console.WriteLine("\n--- MATRICULAR ALUNO EM CURSO ---");

                try
                {
                    var alunos = alunoService.ObterTodosAlunos();
                    var cursos = cursoService.ObterTodosCursos();

                    if (alunos.Count == 0 || cursos.Count == 0)
                    {
                        Console.WriteLine("\nÉ necessário ter ao menos um aluno e um curso cadastrados.");
                        break;
                    }

                    Console.WriteLine("\nAlunos cadastrados:");
                    foreach (var a in alunos)
                        Console.WriteLine($"Matrícula: {a.Matricula} | Nome: {a.Nome}");

                    Console.Write("\nMatrícula do aluno: ");
                    string matriculaAluno = Console.ReadLine() ?? "";

                    var aluno = alunos.FirstOrDefault(a => a.Matricula == matriculaAluno.Trim());

                    if (aluno is null)
                    {
                        Console.WriteLine($"\nErro ao matricular aluno: não foi encontrado nenhum aluno com a matrícula '{matriculaAluno}'.");
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
                        Console.WriteLine($"\nErro ao matricular aluno: não foi encontrado nenhum curso com o código '{codigoCurso}'.");
                        break;
                    }

                    matriculaService.MatricularAluno(aluno, curso);
                    Console.WriteLine($"\nAluno matriculado com sucesso no curso '{curso.Nome}'! Boletim criado automaticamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro ao matricular aluno: {ex.Message}");
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