using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

//Samla
public class BoletimService
{
    private readonly MatriculaService _matriculaService;
    private readonly VinculoCursoDisciplinaService _vinculoService;

    public BoletimService(
        MatriculaService matriculaService,
        VinculoCursoDisciplinaService vinculoService)
    {
        _matriculaService = matriculaService;
        _vinculoService = vinculoService;
    }

    public void LancarNota(
        string matriculaAluno,
        string codigoCurso,
        string codigoDisciplina,
        decimal nota)
    {
        var matricula = _matriculaService
            .ObterTodasMatriculas()
            .FirstOrDefault(m =>
                m.Aluno.Matricula == matriculaAluno &&
                m.Curso.Codigo == codigoCurso);

        if (matricula is null)
            throw new InvalidOperationException("O aluno não está matriculado neste curso.");

        var disciplina = _vinculoService
            .ObterDisciplinasDoCurso(codigoCurso)
            .FirstOrDefault(d => d.Codigo == codigoDisciplina);

        if (disciplina is null)
            throw new InvalidOperationException("A disciplina não pertence ao curso.");

        matricula.Boletim.LancarNota(disciplina, nota);
    }

    public void ConsultarBoletim(
        string matriculaAluno,
        string codigoCurso)
    {
        var matricula = _matriculaService
            .ObterTodasMatriculas()
            .FirstOrDefault(m =>
                m.Aluno.Matricula == matriculaAluno &&
                m.Curso.Codigo == codigoCurso);

        if (matricula is null)
            throw new InvalidOperationException("Matrícula não encontrada.");

        Console.WriteLine("\n============== Boletim ================\n");
        Console.WriteLine($"Aluno: {matricula.Aluno.Nome}");
        Console.WriteLine($"Matrícula: {matricula.Aluno.Matricula}");
        Console.WriteLine($"Curso: {matricula.Curso.Nome}");
        Console.WriteLine($"Tipo: {matricula.Curso.Tipo}");
        Console.WriteLine();

        foreach (var item in matricula.Boletim.Itens)
        {
            Console.WriteLine(item.Disciplina.Nome);
            Console.WriteLine($"Nota: {item.Nota}");
            Console.WriteLine($"Situação: {item.ObterSituacao(matricula.Curso.Tipo)}");
            Console.WriteLine();
        }

        Console.WriteLine("=======================================");
    }
}