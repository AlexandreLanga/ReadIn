using ReadIn.Domain.Entities;
using ReadIn.Interfaces.ILivro;
using ReadIn.Repositories;

namespace ReadIn.Domain.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            ILivroRepository livroRepository
        )
        {
            _livroRepository = livroRepository;
        }
        
        public void CadastrarLivro()
        {
            int opcao = 0;

            Console.Clear();
            Livro livro = new Livro();
            Console.WriteLine("+--------------------------------------------+");
            Console.WriteLine("| Digite o número de identificação do livro: |");
            Console.WriteLine("+--------------------------------------------+\n");
            livro.LivroId = int.Parse(Console.ReadLine());

            Console.WriteLine("\n+-------------------------+");
            Console.WriteLine("| Digite o nome do livro: |");
            Console.WriteLine("+-------------------------+\n");
            livro.Nome = Console.ReadLine();

            Console.WriteLine("\n+---------------------------------+");
            Console.WriteLine("| Deseja inserir o nome do autor? |");
            Console.WriteLine("| 1 - Sim                         |");
            Console.WriteLine("| 2 - Não                         |");
            Console.WriteLine("+---------------------------------+\n");
            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                Console.WriteLine("\n+--------------------------+");
                Console.WriteLine("| Digite o nome do autor:  |");
                Console.WriteLine("+--------------------------+\n");
                livro.Autor = Console.ReadLine();
            }
            else
            {
                livro.Autor = "Não informado";
            }
            Console.WriteLine("\n+--------------------------------------+");
            Console.WriteLine("| Digite o número de páginas do livro: |");
            Console.WriteLine("+--------------------------------------+\n");
            livro.Paginas = int.Parse(Console.ReadLine());

            Console.WriteLine("\n+----------------------------------------+");
            Console.WriteLine("| Digite o número de capítulos do livro: |");
            Console.WriteLine("+----------------------------------------+\n");
            livro.Capitulos = int.Parse(Console.ReadLine());

            Console.WriteLine("\n+------------------------------+");
            Console.WriteLine("| Digite a descrição do livro: |");
            Console.WriteLine("+------------------------------+\n");
            livro.Descricao = Console.ReadLine();

            ApresentarLivroParaCadastro(livro);
        }

        public void ListarLivros()
        {
            var linhas = ObterLivrosCadastrados();
            ExibirLivrosCadastrados(linhas);
            int totalLivros = RetornarTotalDeLivros(linhas);

            Console.WriteLine($"Total de livros cadastrados = {totalLivros}");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        #region FUNÇÕES AUXILIARES

        #region REDEFINIR LIVRO

        private void RedefinirInformacoesLivro(Livro livro)
        {
            int opcao;
            Console.WriteLine("\n+------------------------------+");
            Console.WriteLine("| Certo... O que deseja fazer? |");
            Console.WriteLine("\n+------------------------------+");
            Console.WriteLine("| 1 - Redefinir as informações |");
            Console.WriteLine("| 2 - Voltar ao menu principal |");
            Console.WriteLine("\n+------------------------------+");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                Console.Clear();

                Console.WriteLine("\n+-----------------------------------+");
                Console.WriteLine("| Qual informação deseja redefinir? |");
                Console.WriteLine("\n+-----------------------------------+");
                Console.WriteLine("| 1 - Id do livro                   |");
                Console.WriteLine("| 2 - Nome do livro                 |");
                Console.WriteLine("| 3 - Autor do livro                |");
                Console.WriteLine("| 4 - Páginas do livro              |");
                Console.WriteLine("| 5 - Capítulos                     |");
                Console.WriteLine("| 6 - Descrição                     |");
                Console.WriteLine("\n+-----------------------------------+");

                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1: livro.LivroId = RedefinirId(livro.LivroId); break;
                    case 2: livro.Nome = RedefinirNome(livro.Nome ?? ""); break;
                    case 3: livro.Autor = RedefinirAutor(livro.Autor ?? ""); break;
                    case 4: livro.Paginas = RedefinirPaginas(livro.Paginas); break;
                    case 5: livro.Capitulos = RedefinirCapitulos(livro.Capitulos); break;
                    case 6: livro.Descricao = RedefinirDescricao(livro.Descricao ?? ""); break;
                }

                ApresentarLivroParaCadastro(livro);
            }
        }

        private static int RedefinirId(int livroId)
        {
            Console.Clear();
            Console.WriteLine("Qual Id deseja selecionar para o seu livro?");
            livroId = int.Parse(Console.ReadLine());
            Console.WriteLine($"O novo Id do seu livro é: {livroId}");
            Console.ReadKey();
            return livroId;
        }

        private static int RedefinirPaginas(int livroPaginas)
        {
            Console.Clear();
            Console.WriteLine("Quantas páginas deseja selecionar para o seu livro?");
            livroPaginas = int.Parse(Console.ReadLine());
            Console.WriteLine($"A nova quantia de páginas do seu livro é: {livroPaginas}");
            Console.ReadKey();
            return livroPaginas;
        }

        private static int RedefinirCapitulos(int livroCapitulos)
        {
            Console.Clear();
            Console.WriteLine("Quantos capítulos deseja selecionar para o seu livro?");
            livroCapitulos = int.Parse(Console.ReadLine());
            Console.WriteLine($"A nova quantia de capítulos do seu livro é: {livroCapitulos}");
            Console.ReadKey();
            return livroCapitulos;
        }

        private static string RedefinirNome(string livroNome)
        {
            Console.Clear();
            Console.WriteLine("Qual nome do livro deseja selecionar para o seu livro?");
            livroNome = (Console.ReadLine());
            Console.WriteLine($"O novo nome do seu livro é: {livroNome}");
            Console.ReadKey();
            return livroNome;
        }

        private static string RedefinirAutor(string livroAutor)
        {
            Console.Clear();
            Console.WriteLine("Qual o novo nome do autor para o seu livro?");
            livroAutor = (Console.ReadLine());
            Console.WriteLine($"\nO novo autor do seu livro é: {livroAutor}");
            Console.ReadKey();
            return livroAutor;
        }

        private static string RedefinirDescricao(string livroDescricao)
        {
            Console.Clear();
            Console.WriteLine("Deseja reescreve-la ou edita-la?");
            Console.WriteLine("1 - Editar");
            Console.WriteLine("2 - Reescrever");

            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine($"{livroDescricao}\n");
                    Console.WriteLine("Digite o trecho que deseja mudar: (exatamente igual)");
                    string antigo = Console.ReadLine();
                    Console.WriteLine("Digite o novo trecho no lugar do trecho antigo:");
                    string novo = Console.ReadLine();

                    var alterado = livroDescricao.Replace(antigo, novo);
                    Console.WriteLine($"Sua descrição é:\n{alterado}");
                    Console.WriteLine("\nDeseja alterar mais alguma coisa?");
                    Console.WriteLine("1 - Sim");
                    Console.WriteLine("2 - Não");

                    opcao = int.Parse(Console.ReadLine());
                    livroDescricao = alterado;
                } while (opcao != 2);

                return livroDescricao;
            }
            else
            {
                Console.WriteLine("Digite a nova descrição do seu livro:");
                livroDescricao = (Console.ReadLine());
                Console.WriteLine("\nDescrição salva!");
                Console.ReadKey();
                return livroDescricao;
            }
        }

        #endregion

        private void ApresentarLivroParaCadastro(Livro livro)
        {
            Console.Clear();

            int opcao;

            Console.Clear();

            Console.WriteLine("| As informações do seu livro são:");
            Console.WriteLine("| Id: " + livro.LivroId);
            Console.WriteLine("| Nome: " + livro.Nome);
            Console.WriteLine("| Autor: " + livro.Autor);
            Console.WriteLine("| Pagínas do livro: " + livro.Paginas);
            Console.WriteLine("| Capítulos: " + livro.Capitulos);
            Console.WriteLine("| Descrição:\n| " + livro.Descricao);
            Console.WriteLine("+------------------------------------------");

            Console.WriteLine("\n+-------------------------------------------+");
            Console.WriteLine("| Deseja confirmar o cadastro do seu livro? |");
            Console.WriteLine("+-------------------------------------------+");
            Console.WriteLine("| 1 - Sim                                   |");
            Console.WriteLine("| 2 - Não                                   |");
            Console.WriteLine("+-------------------------------------------+\n");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
                _livroRepository.SalvarLivro(livro);

            else
            {
                RedefinirInformacoesLivro(livro);
            }
        }

        public string[] ObterLivrosCadastrados()
        {
            return _livroRepository.LerLivros();
        }

        public void ExibirLivrosCadastrados(string[] linhas)
        {
            if (linhas.Length == 0)
            {
                Console.WriteLine("Nenhum livro cadastrado.");
                Console.ReadKey();
                return;
            }

            Console.Clear();

            for (int i = 0; i < linhas.Length; i += 6)
            {
                Console.WriteLine($"| Id: {linhas[i]}");
                Console.WriteLine($"| Nome: {linhas[i + 1]}");
                Console.WriteLine($"| Autor: {linhas[i + 2]}");
                Console.WriteLine($"| Páginas: {linhas[i + 3]}");
                Console.WriteLine($"| Capítulos: {linhas[i + 4]}");
                Console.WriteLine($"| Descrição: {linhas[i + 5]}");
                Console.WriteLine("+-----------------------------------------\n");
            }
        }

        private int RetornarTotalDeLivros(string[] linhas)
        {
            int totalLivros = linhas.Length / 6;
            return totalLivros;
        }

        #endregion
    }
}