using ReadIn.Domain.Entities;
using ReadIn.Interfaces;

namespace ReadIn.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        public void SalvarLivro(Livro livro)
        {
            try
            {
                string arquivo = "Livros.txt";
                string diretorio = "C:\\Livros";

                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }
                if (!File.Exists(arquivo))
                {
                    File.Create(arquivo);
                }

                string caminho = Path.Combine(diretorio, arquivo);
                string dados = $"{livro.LivroId}\n" +
                               $"{livro.Nome}\n" +
                               $"{livro.Autor}\n" +
                               $"{livro.Paginas}\n" +
                               $"{livro.Capitulos}\n" +
                               $"{livro.Descricao}\n";
                File.AppendAllText(caminho, dados);
                Console.WriteLine($"Livro salvo em {caminho}");
                Console.ReadKey();
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro ao salvar o livro! Você será encaminhado para o menu principal.");
                Console.ReadKey(); ;
            }
        }

        public void SalvarLeitura(Leitura leituraLivro)
        {
            try
            {
                string arquivo = $"Progresso_{leituraLivro.Nome}.txt";
                string diretorio = "C:\\Livros\\Progressos";

                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }
                if (!File.Exists(arquivo))
                {
                    File.Create(arquivo);
                }

                string caminho = Path.Combine(diretorio, arquivo);
                string dados = $"{leituraLivro.Nome}\n" +
                               $"{leituraLivro.Inicio}\n" +
                               $"{leituraLivro.Final}\n" +
                               $"{leituraLivro.Tempo}\n" +
                               $"{leituraLivro.PaginasLidas}\n" +
                               $"{leituraLivro.PontoReferencia}\n";
                File.AppendAllText(caminho, dados);
                Console.WriteLine($"\nProgresso salvo em {caminho}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao salvar a leitura! Você será encamihado para o menu principal.");
                Console.ReadKey();
            }
        }

        public string[] LerLivros()
        {
            string arquivo = "Livros.txt";
            string diretorio = "C:\\Livros";
            string caminho = Path.Combine(diretorio, arquivo);

            if (!File.Exists(caminho))
                return Array.Empty<string>();

            return File.ReadAllLines(caminho);
        }

    }
}