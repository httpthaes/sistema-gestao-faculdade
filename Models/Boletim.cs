namespace SistemaGestaoFaculdade.Models;

public class Boletim
{
    private readonly List<ItemBoletim> _itens = new();
    public IReadOnlyList<ItemBoletim> Itens => _itens.AsReadOnly();

    public void LancarNota(Disciplina disciplina, decimal nota)
    {
        if (disciplina is null)
            throw new ArgumentException("A disciplina informada é inválida.", nameof(disciplina));

        var itemExistente = _itens.FirstOrDefault(i => i.Disciplina.Codigo == disciplina.Codigo);

        if (itemExistente is null)
            _itens.Add(new ItemBoletim(disciplina, nota));
        else
            itemExistente.AtualizarNota(nota);
    }
}