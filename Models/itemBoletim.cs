namespace SistemaGestaoFaculdade.Models;

public class ItemBoletim
{
    public Disciplina Disciplina { get; private set; }
    public decimal Nota { get; private set; }

    public ItemBoletim(Disciplina disciplina, decimal nota)
    {
        if (disciplina is null)
            throw new ArgumentException("A disciplina informada é inválida.", nameof(disciplina));

        if (nota < 0 || nota > 10)
            throw new ArgumentException("A nota deve estar entre 0 e 10.", nameof(nota));

        Disciplina = disciplina;
        Nota = nota;
    }

    public void AtualizarNota(decimal nota)
    {
        if (nota < 0 || nota > 10)
            throw new ArgumentException("A nota deve estar entre 0 e 10.", nameof(nota));

        Nota = nota;
    }

    public string ObterSituacao(TipoCurso tipoCurso)
    {
        decimal notaMinima = tipoCurso == TipoCurso.PosGraduacao ? 8 : 7;
        return Nota >= notaMinima ? "Aprovado" : "Reprovado";
    }
}
