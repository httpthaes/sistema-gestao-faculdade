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
}