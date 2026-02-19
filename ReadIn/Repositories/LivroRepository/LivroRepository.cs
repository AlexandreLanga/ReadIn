using ReadIn.Domain.Entities;
using ReadIn.Interfaces.ILivro;

namespace ReadIn.Repositories.LivroRepository
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