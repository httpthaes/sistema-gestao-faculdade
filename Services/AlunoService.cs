using SistemaGestaoFaculdade.Models;
namespace SistemaGestaoFaculdade.Services;

public class AlunoService
{
    private readonly List<Aluno> _alunos;

    public AlunoService()
    {
        _alunos = new List<Aluno>();
    }

    public void CadastrarAluno(string nome, string cpf, string email, string matricula)
    {
        if(_alunos.Any(a => a.Cpf == cpf.Trim()))
            throw new InvalidOperationException($"Já existe um aluno cadastrado com o CPF '{cpf}'.");
        
        if(_alunos.Any(a => a.Matricula == matricula.Trim()))
            throw new InvalidOperationException($"Já existe um aluno cadastrado com a matrícula '{matricula}'.");

            var novoAluno = new Aluno(nome, cpf, email, matricula);
            _alunos.Add(novoAluno);
    }

    public IReadOnlyList<Aluno> ObterTodosAlunos()
    {
        return _alunos.AsReadOnly();
    }

    //Samla
    public void ConsultarAlunos(MatriculaService matriculaService)
    {
        if (!_alunos.Any())
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }
        
        Console.WriteLine("=============== Alunos ================\n");

        foreach (var aluno in _alunos)
        {
            Console.WriteLine($"Nome: {aluno.Nome}");
            Console.WriteLine($"CPF: {aluno.Cpf}");
            Console.WriteLine($"E-mail: {aluno.Email}");
            Console.WriteLine($"Matrícula: {aluno.Matricula}");

            var matriculas = matriculaService.ObterMatriculasPorAluno(aluno.Matricula);

            Console.WriteLine("Cursos em que está matriculado: ");

            if (!matriculas.Any())
            {
                Console.WriteLine("Nenhum curso matriculado\n");
            }
            else
            {
                foreach (var matricula in matriculas)
                {
                    Console.WriteLine($"{matricula.Curso.Nome}");
                }
            }

            Console.WriteLine();
        }
    }
}