namespace SistemaGestaoFaculdade.Models;

public abstract class Pessoa
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }

    protected Pessoa(string nome, string cpf, string email)
    {
        if(string.IsNullOrWhiteSpace(nome))
        throw new ArgumentException("O nome é obrigatório.");

        if(string.IsNullOrWhiteSpace(cpf))
        throw new ArgumentException("O CPF é obrigatório.");

        cpf = cpf.Trim();

        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            throw new ArgumentException("O CPF deve ter 11 números, sem letras ou sinais.");

        Nome = nome.Trim();
        Cpf = cpf;
        Email = email?.Trim();
    }
}