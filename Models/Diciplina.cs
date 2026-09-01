namespace SistemaGestaoFaculdade.Models;

public class Disciplina
{
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public int CargaHoraria { get; private set; }
    public Professor ProfessorResponsavel { get; private set; }

    public Disciplina(string codigo, string nome, int cargaHoraria, Professor professorResponsavel)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código da disciplina é obrigatório.", nameof(codigo));

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da disciplina é obrigatório.", nameof(nome));

        if (cargaHoraria <= 0)
            throw new ArgumentException("A carga horária deve ser maior que zero.", nameof(cargaHoraria));

        if (professorResponsavel is null)
            throw new ArgumentException("O professor responsável é obrigatório.", nameof(professorResponsavel));

        Codigo = codigo.ToUpper().Trim();
        Nome = nome.Trim();
        CargaHoraria = cargaHoraria;
        ProfessorResponsavel = professorResponsavel;
    }
}
