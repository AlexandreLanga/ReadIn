using ReadIn.Domain.Services;
using ReadIn.Interfaces;
using ReadIn.Repositories;
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

            Menu menu = new Menu(livroService);
            menu.ExibirMenu();
        }
    }
}