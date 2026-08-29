namespace SistemaGestaoFaculdade.Models;

public class Curso
{
    public string Codigo {get; private set;}
    public string Nome {get; private set;}
    public TipoCurso Tipo {get; private set;}

    public Curso(string codigo, string nome, TipoCurso tipo)
    {
        if(string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código do curso é obrigatório.", nameof(codigo));

        if(string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do curso é obrigatório.", nameof(nome));

        Codigo = codigo.ToUpper().Trim();
        Nome = nome.Trim();
        Tipo = tipo;
    }
}