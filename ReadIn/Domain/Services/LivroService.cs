using ReadIn.Domain.Entities;
using ReadIn.Interfaces;
using ReadIn.Repositories;

namespace ReadIn.Domain.Services
{
    public class LivroService : ILivroService
    {
        private readonly LivroRepository _livroRepository;

        public LivroService(
            LivroRepository livroRepository
        )
        {
            _livroRepository = livroRepository;
        }
        public void AnotarLeitura()
        {
            Leitura leituraLivro = new Leitura();

            string livroEscolha = "";
            leituraLivro.Nome = EscolherLivro(livroEscolha);

            Console.Clear();
            Console.WriteLine($"| Sempre é um ótimo dia para ler {leituraLivro.Nome}!");
            Console.WriteLine("| Assim que você confirmar, o contador será acionado e seu tempo de leitura começará!");
            Console.WriteLine("+------------------------------------------------------------------------------------");
            Console.WriteLine("\nPressione qualquer tecla para confirmar\n");
            Console.ReadKey();
            Console.Clear();

            leituraLivro.Inicio = DateTime.Now;
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("| Seu temporizador foi iniciado!                      |");
            Console.WriteLine("| Assim que terminar sua leitura, confirme para parar |");
            Console.WriteLine("| o temporizador                                      |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("\nPressione qualquer tecla para confirmar\n");
            Console.ReadKey();
            leituraLivro.Final = DateTime.Now;

            Console.Clear();
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine("| Sua leitura chegou ao fim! Vamos ver o quanto você progrediu nessa jornada! |");
            Console.WriteLine("+-----------------------------------------------------------------------------+");

            Console.WriteLine("\n+-----------------------------------+");
            Console.WriteLine("| Em qual página voçê parou de ler? |");
            Console.WriteLine("+-----------------------------------+\n");
            leituraLivro.PaginasLidas = int.Parse(Console.ReadLine());

            Console.WriteLine("\n+----------------------------------------------------------------------------------------------+");
            Console.WriteLine("| Qual seu ponto de referência para a próxima leitura? (linha, frase, começo de paragráfo etc) |");
            Console.WriteLine("+----------------------------------------------------------------------------------------------+\n");
            leituraLivro.PontoReferencia = Console.ReadLine();

            Console.Clear();
            leituraLivro.Tempo = leituraLivro.Final - leituraLivro.Inicio;

            AvaliarTempoDeLeitura(leituraLivro);
            _livroRepository.SalvarLeitura(leituraLivro);
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
            var linhas = _livroRepository.LerLivros();

            if (linhas.Length == 0)
            {
                Console.WriteLine("Nenhum livro cadastrado.");
                Console.ReadKey();
                return;
            }

            Console.Clear();

            int totalLivros = linhas.Length / 6;

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

            Console.WriteLine($"Total de livros cadastrados = {totalLivros}");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        #region FUNÇÕES

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

        private string EscolherLivro(string livro)
        {
            Console.Clear();

            int achou = 0, contador = 1;
            string opcao = "";

            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.WriteLine("| Nunca é uma hora ruim para ler e adquirir vastos conhecimentos, certo? |");
            Console.WriteLine("| Qual livro vamos ler agora?                                            |");
            Console.WriteLine("+------------------------------------------------------------------------+\n");


            string[] livros = _livroRepository.LerLivros();

            foreach (string linha in livros)
            {
                if (contador == 1)
                {
                    livro = linha + " - ";
                    contador++;
                }
                else
                {
                    if (contador == 2)
                    {
                        livro += linha;
                        Console.WriteLine($"| {livro}");
                        Console.WriteLine("-------------------------------------------------------------------------");
                        contador++;
                    }
                    else
                    {
                        if (contador == 6)
                        {
                            contador = 1;
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
            }

            Console.WriteLine("\n+----------------------+");
            Console.WriteLine("| Digite o Id do livro:|");
            Console.WriteLine("+----------------------+\n");
            opcao = (Console.ReadLine());

            foreach (string linha in livros)
            {
                if (opcao == linha)
                {
                    achou = 1;
                }
                else
                {
                    if (achou == 1)
                    {
                        livro = linha;
                        break;
                    }
                }
            }
            return livro;
        }

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
                RedefinirInformacoes(livro);
            }
        }

        private void RedefinirInformacoes(Livro livro)
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

        private void AvaliarTempoDeLeitura(Leitura leituraLivro)
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| Veja a progressão da sua leitura: |");
            Console.WriteLine("+-----------------------------------+-------------------------------------------------------------------------------");
            Console.WriteLine($"| Livro lido: {leituraLivro.Nome}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Horário de início da leitura: {leituraLivro.Inicio}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Horário de finalização da leitura: {leituraLivro.Final}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Tempo em leitura: {leituraLivro.Tempo}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Páginas lidas: {leituraLivro.PaginasLidas}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Último ponto de referência: {leituraLivro.PontoReferencia}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");

            DateTime leuPadrao, leuMediano, leuAvancado;
            DateTime.TryParse("00:15", out leuPadrao);
            DateTime.TryParse("00:30", out leuMediano);
            DateTime.TryParse("00:59", out leuAvancado);

            if (leituraLivro.Tempo.Minutes <= leuPadrao.Minute)
            {
                Console.WriteLine("\nAquela leitura rapidinha no tempo vago, quem nunca?");
            }

            if (leituraLivro.Tempo.Minutes > leuPadrao.Minute && leituraLivro.Tempo.Minutes <= leuMediano.Minute)
            {
                Console.WriteLine("\nAquela doce e alegre leitura simples para não perder o costume!");
            }

            if (leituraLivro.Tempo.Minutes >= leuMediano.Minute && leituraLivro.Tempo.Minutes < leuAvancado.Minute)
            {
                Console.WriteLine("\nLer por mais de meia hora definitivamente aumenta sua criatividade!");
            }

            if (leituraLivro.Tempo.Minutes >= leuAvancado.Minute)
            {
                Console.WriteLine("\nUau! Você leu por uma hora ou mais! Você definitivamente leu bastante, se orgulhe disso!");
            }

            Console.ReadKey();
        }

        #endregion
    }
}