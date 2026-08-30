using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

public class DisciplinaService
{
	private readonly List<Disciplina> _disciplinas;

	public DisciplinaService()
	{
		_disciplinas = new List<Disciplina>();
	}

	public void CadastrarDisciplina(string codigo, string nome, int cargaHoraria, Professor professorResponsavel)
	{
		//Regra de negócio: Verificar se já existe uma disciplina com o mesmo código
		if (_disciplinas.Any(d => d.Codigo == codigo.ToUpper().Trim()))
			throw new InvalidOperationException($"Já existe uma disciplina cadastrada com o código '{codigo}'.");

		var novaDisciplina = new Disciplina(codigo, nome, cargaHoraria, professorResponsavel);
		_disciplinas.Add(novaDisciplina);
	}

	public IReadOnlyList<Disciplina> ObterTodasDisciplinas()
	{
		return _disciplinas.AsReadOnly();
	}

	public Disciplina BuscarPorCodigo(string codigo)
	{
		var disciplina = _disciplinas.FirstOrDefault(d => d.Codigo == codigo.ToUpper().Trim());

		if (disciplina is null)
			throw new InvalidOperationException($"Não foi encontrada nenhuma disciplina com o código '{codigo}'.");

		return disciplina;
	}
}
