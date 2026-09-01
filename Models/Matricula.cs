namespace SistemaGestaoFaculdade.Models;

public class Matricula
{
    public Aluno Aluno { get; private set; }
    public Curso Curso { get; private set; }
    public Boletim Boletim { get; private set; }

    public Matricula(Aluno aluno, Curso curso)
    {
        if (aluno is null)
            throw new ArgumentException("O aluno informado é inválido.", nameof(aluno));

        if (curso is null)
            throw new ArgumentException("O curso informado é inválido.", nameof(curso));

        Aluno = aluno;
        Curso = curso;
        Boletim = new Boletim();
    }
}
