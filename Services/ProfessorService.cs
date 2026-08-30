using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

public class ProfessorService
{
    private readonly List<Professor> _professores;

    public ProfessorService()
    {
        _professores = new List<Professor>();
    }

    public void CadastrarProfessor(string nome, string cpf, string email, string registro, string especialidade)
    {
        //Regra de negócio: Verificar se já existe um professor com o mesmo CPF
        if(_professores.Any(p =>p.Cpf == cpf.Trim()))
        
            throw new InvalidOperationException($"Já existe um professor cadastrado com o CPF '{cpf}'.");

        //Regra de negócio: Verificar se já existe um professor com o mesmo registro
        if(_professores.Any(p => p.Registro == registro.Trim()))
        
            throw new InvalidOperationException($"Já existe um professor cadastrado com o registro '{registro}'.");

        var novoProfessor = new Professor(nome, cpf, email, registro, especialidade);
        _professores.Add(novoProfessor);
    }

    public IReadOnlyList<Professor> ObterTodosProfessores()
    {
        return _professores.AsReadOnly();
    }
}
