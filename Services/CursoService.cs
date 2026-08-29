using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

public class CursoService
{
    private readonly List<Curso> _cursos;

    public CursoService()
    {
        _cursos = new List<Curso>();
    }

    public void CadastrarCurso(string codigo, string nome, TipoCurso tipo)
    {
        if (_cursos.Any(c => c.Codigo == codigo.ToUpper().Trim()))
        {
            throw new InvalidOperationException($"Já existe um curso cadastrado com o código '{codigo}'.");
        }

        var novoCurso = new Curso(codigo, nome, tipo);
        _cursos.Add(novoCurso);
    }

    public IReadOnlyList<Curso> ObterTodosCursos()
    {
        return _cursos.AsReadOnly();
    }
}