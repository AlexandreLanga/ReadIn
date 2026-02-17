using ReadIn.Interfaces;

namespace ReadIn.UI
{
    public class Menu : IMenu
    {
        private readonly ILivroService _livroService;

        public Menu(ILivroService livroService)
        {
            _livroService = livroService;
        }

        public void ExibirMenu()
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("+--------+");
                Console.WriteLine("| ReadIn |");
                Console.WriteLine("+--------+\n");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("| Livros                      |");
                Console.WriteLine("|-----------------------------|");
                Console.WriteLine("| 1 - Cadastrar livro         |");
                Console.WriteLine("| 2 - Marca passos            |");
                Console.WriteLine("| 3 - Ver livros cadastrados  |");
                Console.WriteLine("| 0 - Sair                    |");
                Console.WriteLine("+-----------------------------+\n");

                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("+---------------+");
                        Console.WriteLine("| Volte sempre! |");
                        Console.WriteLine("+---------------+");
                        break;
                    case 1:
                        _livroService.CadastrarLivro();
                        break;
                    case 2:
                        _livroService.AnotarLeitura();
                        break;
                    case 3:
                        _livroService.ListarLivros();
                        break;
                    default:
                        Console.WriteLine("\n+----------------------------------------------------------+");
                        Console.WriteLine("| Opção não reconhecida, o menu principal será recarregado |");
                        Console.WriteLine("+----------------------------------------------------------+\n");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (opcao != 0);
        }
    }
}