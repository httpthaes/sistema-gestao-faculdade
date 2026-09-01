using SistemaGestaoFaculdade.Models;
namespace SistemaGestaoFaculdade.Services;

public class DisciplinaService
{
    private readonly List<Disciplina> _disciplinas;
    private readonly ProfessorService _professorService;

    public DisciplinaService(ProfessorService professorService)
    {
        _disciplinas = new List<Disciplina>();
        _professorService = professorService;
    }

    public void CadastrarDisciplina(string codigo, string nome, int cargaHoraria, string registroProfessor)
    {
        var professor = _professorService.ObterTodosProfessores()
            .FirstOrDefault(p => p.Registro.Equals(registroProfessor.Trim(), StringComparison.OrdinalIgnoreCase));

        if (professor is null)
            throw new InvalidOperationException("Professor não encontrado. Cadastre o professor antes.");

        if (_disciplinas.Any(d => d.Codigo == codigo.ToUpper().Trim()))
            throw new InvalidOperationException($"Já existe uma disciplina cadastrada com o código '{codigo}'.");

        var novaDisciplina = new Disciplina(codigo, nome, cargaHoraria, professor);
        _disciplinas.Add(novaDisciplina);
    }

    public IReadOnlyList<Disciplina> ObterTodasDisciplinas()
    {
        return _disciplinas.AsReadOnly();
    }
}