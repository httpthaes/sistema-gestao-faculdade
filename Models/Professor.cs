using SistemaGestaoFaculdade.Models;

public class Professor : Pessoa
{
    public string Registro { get; private set;}
    public string Especialidade { get; private set;}

    public Professor(string nome, string cpf, string email, string registro, string especialidade) : base(nome, cpf, email)
    {
        if(string.IsNullOrWhiteSpace(registro))
            throw new ArgumentException("O registro do professor é obrigatório.", nameof(registro));

        if(string.IsNullOrWhiteSpace(especialidade))
            throw new ArgumentException("A especialidade do professor é obrigatória.", nameof(especialidade));

        Registro = registro.Trim();
        Especialidade = especialidade.Trim();
    }
}