using ReadIn.Domain.Services;
using ReadIn.Interfaces.ILeitura;
using ReadIn.Interfaces.ILivro;
using ReadIn.Repositories.LeituraRepository;
using ReadIn.Repositories.LivroRepository;
using ReadIn.UI;

namespace ReadIn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Title = "ReadIn - Gerenciador de Leitura";

            LivroRepository livroRepository = new LivroRepository();
            ILivroService livroService = new LivroService(livroRepository);

            LeituraRepository leituraRepository = new LeituraRepository();
            ILeituraService leituraService = new LeituraService(leituraRepository,livroService);

            Menu menu = new Menu(livroService, leituraService);
            menu.ExibirMenu();
        }
    }
}