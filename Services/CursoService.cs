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

    public void ConsultarCurso(
        string codigoCurso,
        VinculoCursoDisciplinaService vinculoService,
        MatriculaService matriculaService)
    {
        var curso = _cursos.FirstOrDefault(c =>
            c.Codigo == codigoCurso.ToUpper().Trim());

        if (curso is null)
            throw new InvalidOperationException("Curso não encontrado.");

        Console.WriteLine("\n=============== Curso =================\n");
        Console.WriteLine($"Nome: {curso.Nome}");
        Console.WriteLine($"Código: {curso.Codigo}");
        Console.WriteLine($"Tipo: {curso.Tipo}");

        Console.WriteLine("\nDisciplinas:");

        var disciplinas =
            vinculoService.ObterDisciplinasDoCurso(curso.Codigo);

        if (disciplinas.Count == 0)
        {
            Console.WriteLine("Nenhuma disciplina vinculada.");
        }
        else
        {
            foreach (var disciplina in disciplinas)
            {
                Console.WriteLine(
                    $"{disciplina.Nome} - Professor: {disciplina.ProfessorResponsavel.Nome}");
            }
        }

        Console.WriteLine("\nAlunos matriculados:");

        var matriculas = matriculaService
            .ObterTodasMatriculas()
            .Where(m => m.Curso.Codigo == curso.Codigo)
            .ToList();

        if (matriculas.Count == 0)
        {
            Console.WriteLine("Nenhum aluno matriculado.");
        }
        else
        {
            foreach (var matricula in matriculas)
            {
                Console.WriteLine(
                    $"{matricula.Aluno.Nome} ({matricula.Aluno.Matricula})");
            }
        }

        Console.WriteLine("\n=======================================");
    }
}