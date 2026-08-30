using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

public class VinculoCursoDisciplinaService
{
    private readonly List<VinculoCursoDisciplina> _vinculos;

    public VinculoCursoDisciplinaService()
    {
        _vinculos = new List<VinculoCursoDisciplina>();
    }

    public void VincularDisciplinaAoCurso(Curso curso, Disciplina disciplina)
    {
        if (curso is null)
            throw new ArgumentException("O curso informado é inválido.", nameof(curso));

        if (disciplina is null)
            throw new ArgumentException("A disciplina informada é inválida.", nameof(disciplina));

        bool jaVinculada = _vinculos.Any(v => v.Curso.Codigo == curso.Codigo && v.Disciplina.Codigo == disciplina.Codigo);

        if (jaVinculada)
            throw new InvalidOperationException($"A disciplina '{disciplina.Codigo}' já está vinculada ao curso '{curso.Codigo}'.");

        _vinculos.Add(new VinculoCursoDisciplina(curso, disciplina));
    }

    public IReadOnlyList<Disciplina> ObterDisciplinasDoCurso(string codigoCurso)
    {
        return _vinculos
            .Where(v => v.Curso.Codigo == codigoCurso.ToUpper().Trim())
            .Select(v => v.Disciplina)
            .ToList()
            .AsReadOnly();
    }
}
