using SistemaGestaoFaculdade.Models;

namespace SistemaGestaoFaculdade.Services;

public class EnviarNotificacaoService
{
    private readonly AlunoService _alunoService;
    private readonly ProfessorService _professorService;

    public EnviarNotificacaoService(
        AlunoService alunoService,
        ProfessorService professorService)
    {
        _alunoService = alunoService;
        _professorService = professorService;
    }

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

        var aluno = _alunoService
            .ObterTodosAlunos()
            .FirstOrDefault(a =>
                a.Nome.Equals(nomeDestinatario.Trim(),
                StringComparison.OrdinalIgnoreCase));

        if (aluno != null)
        {
            Console.WriteLine($"\nNotificação para {aluno.Nome}:");
            Console.WriteLine(mensagem);
            return;
        }

        var professor = _professorService
            .ObterTodosProfessores()
            .FirstOrDefault(p =>
                p.Nome.Equals(nomeDestinatario.Trim(),
                StringComparison.OrdinalIgnoreCase));

        if (professor != null)
        {
            Console.WriteLine($"\nNotificação para {professor.Nome}:");
            Console.WriteLine(mensagem);
            return;
        }

        Console.WriteLine($"\nDestinatário '{nomeDestinatario}' não encontrado no sistema!");
    }
}