using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

public class MatriculaService
{
	private readonly List<Matricula> _matriculas;

	public MatriculaService()
	{
		_matriculas = new List<Matricula>();
	}

	public Matricula MatricularAluno(Aluno aluno, Curso curso)
	{
		if (aluno is null)
			throw new ArgumentException("O aluno informado é inválido.", nameof(aluno));

		if (curso is null)
			throw new ArgumentException("O curso informado é inválido.", nameof(curso));


		bool jaMatriculado = _matriculas.Any(m => m.Aluno.Matricula == aluno.Matricula && m.Curso.Codigo == curso.Codigo);

		if (jaMatriculado)
			throw new InvalidOperationException($"O aluno '{aluno.Nome}' já está matriculado no curso '{curso.Codigo}'.");

	
		var novaMatricula = new Matricula(aluno, curso);
		_matriculas.Add(novaMatricula);

		return novaMatricula;
	}

	public IReadOnlyList<Matricula> ObterTodasMatriculas()
	{
		return _matriculas.AsReadOnly();
	}

	public IReadOnlyList<Matricula> ObterMatriculasPorAluno(string matriculaAluno)
	{
		return _matriculas.Where(m => m.Aluno.Matricula == matriculaAluno.Trim()).ToList().AsReadOnly();
	}
}
