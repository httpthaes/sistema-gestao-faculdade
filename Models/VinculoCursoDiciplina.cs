namespace SistemaGestaoFaculdade.Models;

public class VinculoCursoDisciplina
{
    public Curso Curso { get; private set; }
    public Disciplina Disciplina { get; private set; }

    public VinculoCursoDisciplina(Curso curso, Disciplina disciplina)
    {
        if (curso is null)
            throw new ArgumentException("O curso informado é inválido.", nameof(curso));

        if (disciplina is null)
            throw new ArgumentException("A disciplina informada é inválida.", nameof(disciplina));

        Curso = curso;
        Disciplina = disciplina;
    }
}
