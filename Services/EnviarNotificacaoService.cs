using SistemaGestaoFaculdade.Models;
namespace SistemaGestaoFaculdade.Services;


public class EnviarNotificacaoService
{
    private readonly List<Aluno> listaAlunos;
    private readonly List<Professor> listaProfessores;

    public EnviarNotificacaoService(List<Aluno> listaAlunos, List<Professor> listaProfessores)
    {
        this.listaAlunos = listaAlunos;
        this.listaProfessores = listaProfessores;
    }

    // Método principal de envio da notificação
    public void EnviarNotificacao(string nomeDestinatario, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(nomeDestinatario))
        {
            Console.WriteLine("O nome do destinatário não pode ser vazio.");
            return;
        }

        if (string.IsNullOrWhiteSpace(mensagem))
        {
            Console.WriteLine("A mensagem não pode estar vazia.");
            return;
        }

        // Busca primeiro nos alunos; se não encontrar, busca nos professores
        Pessoa? destinatario = listaAlunos.FirstOrDefault(a => a.Nome.Equals(nomeDestinatario.Trim(), StringComparison.OrdinalIgnoreCase))
        ?? (Pessoa?)listaProfessores.FirstOrDefault(p => p.Nome.Equals(nomeDestinatario.Trim(), StringComparison.OrdinalIgnoreCase));

        // Se não encontrou ninguém cadastrado com esse nome
        if (destinatario == null)
        {
            Console.WriteLine($"Destinatário '{nomeDestinatario}' não encontrado no sistema!");
            return;
        }

        // Exibição no padrão exato solicitado no trabalho
        Console.WriteLine($"Notificação para {destinatario.Nome}:");
        Console.WriteLine($"{mensagem}");
    }
}
