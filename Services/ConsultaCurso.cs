using SistemaGestaoFaculdade.Models;
namespace SistemaGestaoFaculdade.Services;

public class ConsultaCurso
{
    private readonly List<Curso> cursos;

        public ConsultaCurso(List<Curso> cursos)
        {
           this.cursos = cursos;
        }

        // Método que busca o curso na lista pelo nome
        public Curso? BuscarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return null;
            }

            return cursos.FirstOrDefault(c => 
                c.Nome.Equals(nome.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // Método que exibe os dados conforme nome do curso digitado
        public void ExibirConsultaPorNome(string nome)
        {
            Curso? curso = BuscarPorNome(nome);

            if (curso == null)
            {
                Console.WriteLine($"\nCurso '{nome}' não encontrado!");
                return;
            }

            // 1. Cabeçalho do Curso
            Console.WriteLine($"\nCurso: {curso.Nome}");
            Console.WriteLine($"Tipo: {curso.Tipo}\n");

            // 2. Lista de Disciplinas e Professores
            Console.WriteLine("Disciplinas:\n");
            if (curso.Disciplinas != null && curso.Disciplinas.Count > 0)
            {
                foreach (var disciplina in curso.Disciplinas)
                {
                    Console.WriteLine(disciplina.Nome);
                    
                    // Ajuste o nome da propriedade se na sua classe Disciplina for diferente de ProfessorResponsavel
                    string nomeProfessor = disciplina.ProfessorResponsavel?.Nome ?? "Não informado";
                    Console.WriteLine($"Professor: {nomeProfessor}\n");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma disciplina cadastrada.\n");
            }

            // 3. Lista de Alunos Matriculados
            Console.WriteLine("Alunos matriculados:\n");
            if (curso.AlunosMatriculados != null && curso.AlunosMatriculados.Count > 0)
            {
                foreach (var aluno in curso.AlunosMatriculados)
                {
                    Console.WriteLine(aluno.Nome);
                }
            }
            else
            {
                Console.WriteLine("Nenhum aluno matriculado.");
            }
        }
}