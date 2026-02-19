using ReadIn.Domain.Entities;
using ReadIn.Interfaces.ILeitura;

namespace ReadIn.Repositories.LeituraRepository
{
    public class LeituraRepository : ILeituraRepository
    {
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
    }
}
