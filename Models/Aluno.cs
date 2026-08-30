using SistemaGestaoFaculdade.Models;

public class Aluno : Pessoa
{
    public string Matricula { get ; private set;}

    public Aluno(string nome, string cpf, string email, string matricula) : base(nome, cpf, email)
    {
        if(string.IsNullOrWhiteSpace(matricula))
            throw new ArgumentException("A matrícula do aluno é obrigatória.", nameof(matricula));

        Matricula = matricula.Trim();
    }
}