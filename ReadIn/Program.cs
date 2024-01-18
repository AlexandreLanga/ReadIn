using System;
using System.Timers;
using System.Linq.Expressions;
using ReadIn;

namespace ReadIn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            MenuPrincipal();
        }
        public static void MenuPrincipal()
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
            //Console.CursorLeft = 15;
            //Console.CursorTop = 0;
            int opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 0:
                    Console.WriteLine("+---------------+");
                    Console.WriteLine("| Volte sempre! |");
                    Console.WriteLine("+---------------+");
                    break;
                case 1: CadastrarLivro();
                    break;
                case 2: AnotarLeitura();
                    break;
                case 3: ListarLivros();
                    break;
                default:
                    Console.WriteLine("\n+----------------------------------------------------------+");
                    Console.WriteLine("| Opção não reconhecida, o menu principal será recarregado |");
                    Console.WriteLine("+----------------------------------------------------------+\n");
                    Console.ReadKey();
                    Console.Clear();
                    MenuPrincipal();
                    break;
            }
        }
        public static void CadastrarLivro()
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

            int livroId           = livro.LivroId;
            string livroNome      = livro.Nome;
            string livroAutor     = livro.Autor;
            int livroPaginas      = livro.Paginas;
            int livroCapitulos    = livro.Capitulos;
            string livroDescricao = livro.Descricao;

            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
        }
        public static void AnotarLeitura()
        {
            Livro livro = new Livro();

            string livroEscolha = "";
            livro.Nome = EscolherLivro(livroEscolha);

            Console.Clear();
            Console.WriteLine($"| Sempre é um ótimo dia para ler {livro.Nome}!");
            Console.WriteLine("| Assim que você confirmar, o contador será acionado e seu tempo de leitura começará!");
            Console.WriteLine("+------------------------------------------------------------------------------------");
            Console.WriteLine("\nPressione qualquer tecla para confirmar\n");
            Console.ReadKey();
            Console.Clear();

            DateTime inicioLeitura = DateTime.Now;
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("| Seu temporizador foi iniciado!                      |");
            Console.WriteLine("| Assim que terminar sua leitura, confirme para parar |");
            Console.WriteLine("| o temporizador                                      |");
            Console.WriteLine("+-----------------------------------------------------+");
            Console.WriteLine("\nPressione qualquer tecla para confirmar\n");
            Console.ReadKey();
            DateTime fimLeitura = DateTime.Now;

            Console.Clear();
            Console.WriteLine("+-----------------------------------------------------------------------------+");
            Console.WriteLine("| Sua leitura chegou ao fim! Vamos ver o quanto você progrediu nessa jornada! |");
            Console.WriteLine("+-----------------------------------------------------------------------------+");

            Console.WriteLine("\n+-----------------------------------+");
            Console.WriteLine("| Em qual página voçê parou de ler? |");
            Console.WriteLine("+-----------------------------------+\n");
            livro.PaginasLidas = int.Parse(Console.ReadLine());

            Console.WriteLine("\n+----------------------------------------------------------------------------------------------+");
            Console.WriteLine("| Qual seu ponto de referência para a próxima leitura? (linha, frase, começo de paragráfo etc) |");
            Console.WriteLine("+----------------------------------------------------------------------------------------------+\n");
            livro.PontoReferencia = Console.ReadLine();

            Console.Clear();
            var tempoDeLeitura = fimLeitura - inicioLeitura;

            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| Veja a progressão da sua leitura: |");
            Console.WriteLine("+-----------------------------------+-------------------------------------------------------------------------------");
            Console.WriteLine($"| Livro lido: {livro.Nome}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Horário de início da leitura: {inicioLeitura}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Horário de finalização da leitura: {fimLeitura}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Tempo em leitura: {tempoDeLeitura}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Páginas lidas: {livro.PaginasLidas}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| Último ponto de referência: {livro.PontoReferencia}");
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------");

            DateTime leuPadrao, leuMediano, leuAvancado;
            DateTime.TryParse("00:15", out leuPadrao);
            DateTime.TryParse("00:30", out leuMediano);
            DateTime.TryParse("00:59", out leuAvancado);

            if (tempoDeLeitura.Minutes <= leuPadrao.Minute)
            {
                Console.WriteLine("\nAquela leitura rapidinha no tempo vago, quem nunca?");
            }

            if (tempoDeLeitura.Minutes > leuPadrao.Minute && tempoDeLeitura.Minutes <= leuMediano.Minute)
            {
                Console.WriteLine("\nAquela doce e alegre leitura simples para não perder o costume!");
            }

            if (tempoDeLeitura.Minutes >= leuMediano.Minute && tempoDeLeitura.Minutes < leuAvancado.Minute)
            {
                Console.WriteLine("\nLer por mais de meia hora definitivamente aumenta sua criatividade!");
            }

            if (tempoDeLeitura.Minutes >= leuAvancado.Minute)
            {
                Console.WriteLine("\nUau! Você leu por uma hora ou mais! Você definitivamente leu bastante, se orgulhe disso!");
            }

            Console.ReadKey();

            try
            {
                string arquivo = $"Progresso_{livro.Nome}.txt";
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
                string dados = $"{livro.Nome}\n" +
                               $"{inicioLeitura}\n" +
                               $"{fimLeitura}\n" +
                               $"{tempoDeLeitura}\n" +
                               $"{livro.PaginasLidas}\n" +
                               $"{livro.PontoReferencia}\n";
                File.AppendAllText(caminho, dados);
                Console.WriteLine($"\nProgresso salvo em {caminho}");
                Console.WriteLine("Você será encamihado para o menu principal!");
                Console.ReadKey();
                MenuPrincipal();
            }
            catch (Exception ex)
            {

            }
            Console.ReadKey();
        }
        public static void ListarLivros()
        {
            Console.Clear();
            string arquivo = "Livros.txt";
            string diretorio = "C:\\Livros";
            string caminho = Path.Combine(diretorio, arquivo);
            string[] livros = File.ReadAllLines(caminho);

            int contadorLinhas = 0,contadorLivros = 0;

            foreach (string linha in livros)
            {
                contadorLinhas++;

                if (contadorLinhas == 1)
                {
                    Console.WriteLine($"| Id do livro: {linha}");
                }
                if (contadorLinhas == 2)
                {
                    Console.WriteLine($"| Nome do livro: {linha}");
                }
                if (contadorLinhas == 3)
                {
                    Console.WriteLine($"| Nome do autor: {linha}");
                }
                if (contadorLinhas == 4)
                {
                    Console.WriteLine($"| Quantidade de páginas do livro: {linha}");
                }
                if (contadorLinhas == 5)
                {
                    Console.WriteLine($"| Total de capítulos: {linha}");
                }
                if (contadorLinhas == 6)
                {
                    Console.WriteLine($"| Descrição do livro:\n| {linha}");
                    Console.WriteLine("+-----------------------------------------\n");
                    contadorLivros++;
                    contadorLinhas = 0;
                }
            }
            Console.WriteLine($"Total de livros cadastrados = {contadorLivros}\n");
            Console.WriteLine("Clique em qualquer tecla para voltar para o menu principal\n");
            Console.ReadKey();
            MenuPrincipal();
        }
        #region Livros
        public static string EscolherLivro(string livro)
        {
            Console.Clear();

            int achou = 0, contador = 1;
            string opcao = "";

            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.WriteLine("| Nunca é uma hora ruim para ler e adquirir vastos conhecimentos, certo? |");
            Console.WriteLine("| Qual livro vamos ler agora?                                            |");
            Console.WriteLine("+------------------------------------------------------------------------+\n");

            string arquivo = "Livros.txt";
            string diretorio = "C:\\Livros";
            string caminho = Path.Combine(diretorio, arquivo);
            string[] livros = File.ReadAllLines(caminho);

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
        public static int RedefinirId(int livroId)
        {
            Console.Clear();
            Console.WriteLine("Qual Id deseja selecionar para o seu livro?");
            livroId = int.Parse(Console.ReadLine());
            Console.WriteLine($"O novo Id do seu livro é: {livroId}");
            Console.ReadKey();
            return livroId;  
        }
        public static int RedefinirPaginas(int livroPaginas)
        {
            Console.Clear();
            Console.WriteLine("Quantas páginas deseja selecionar para o seu livro?");
            livroPaginas = int.Parse(Console.ReadLine());
            Console.WriteLine($"A nova quantia de páginas do seu livro é: {livroPaginas}");
            Console.ReadKey();
            return livroPaginas;
        }
        public static int RedefinirCapitulos(int livroCapitulos)
        {
            Console.Clear();
            Console.WriteLine("Quantos capítulos deseja selecionar para o seu livro?");
            livroCapitulos = int.Parse(Console.ReadLine());
            Console.WriteLine($"A nova quantia de capítulos do seu livro é: {livroCapitulos}");
            Console.ReadKey();
            return livroCapitulos;
        }
        public static string RedefinirNome(string livroNome)
        {
            Console.Clear();
            Console.WriteLine("Qual nome do livro deseja selecionar para o seu livro?");
            livroNome = (Console.ReadLine());
            Console.WriteLine($"O novo nome do seu livro é: {livroNome}");
            Console.ReadKey();
            return livroNome;
        }
        public static string RedefinirAutor(string livroAutor)
        {
            Console.Clear();
            Console.WriteLine("Qual o novo nome do autor para o seu livro?");
            livroAutor = (Console.ReadLine());
            Console.WriteLine($"\nO novo autor do seu livro é: {livroAutor}");
            Console.ReadKey();
            return livroAutor;
        }
        public static string RedefinirDescricao(string livroDescricao)
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

                    var alterado = livroDescricao.Replace(antigo,novo);
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
        public static void ConfirmarCadastroLivro(int livroId,string livroNome,string livroAutor,int livroPaginas,int livroCapitulos, string livroDescricao)
        {
            Livro livro = new Livro();

            livro.LivroId = livroId;
            livro.Nome = livroNome;
            livro.Autor = livroAutor;
            livro.Paginas = livroPaginas;
            livro.Capitulos = livroCapitulos;
            livro.Descricao = livroDescricao;

            int opcao = 0;
            Console.Clear();

            Console.WriteLine("| As informações do seu livro são:");
            Console.WriteLine("| Id: " + livro.LivroId);
            Console.WriteLine("| Nome: " + livro.Nome);
            Console.WriteLine("| Autor: " + livro.Autor);
            Console.WriteLine("| Pagínas do livro: " + livro.Paginas);
            Console.WriteLine("| Capítulos: " + livro.Capitulos);
            Console.WriteLine("| Descrição:\n| " + livro.Descricao);
            Console.WriteLine("+----------------------------------");

            Console.WriteLine("\n+-------------------------------------------+");
            Console.WriteLine("| Deseja confirmar o cadastro do seu livro? |");
            Console.WriteLine("+-------------------------------------------+");
            Console.WriteLine("| 1 - Sim                                   |");
            Console.WriteLine("| 2 - Não                                   |");
            Console.WriteLine("+-------------------------------------------+\n");
            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
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
                    Console.WriteLine($"Livro cadastrado com sucesso em {caminho}, verifique!");
                    Console.ReadKey();
                    MenuPrincipal();
                }
                catch (Exception erro)
                {
                    Console.WriteLine("Ocorreu um erro ao cadastrar seu livro! Verifique o log e tente novamente!");
                    Console.WriteLine("Você será encaminhado para o menu principal.");
                    Console.ReadKey();
                    MenuPrincipal();
                }
            }
            else
            {
                Console.WriteLine("Certo... O que deseja fazer?");
                Console.WriteLine("1 - Redefinir as informações");
                Console.WriteLine("2 - Voltar ao menu principal");

                opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {
                    Console.WriteLine("Qual informação deseja redefinir?");
                    Console.WriteLine("1 - Id do livro");
                    Console.WriteLine("2 - Nome do livro");
                    Console.WriteLine("3 - Autor do livro");
                    Console.WriteLine("4 - Páginas do livro");
                    Console.WriteLine("5 - Capítulos");
                    Console.WriteLine("6 - Descrição");

                    opcao = int.Parse(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            livroId = RedefinirId(livro.LivroId);
                            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
                            break;
                        case 2:
                            livroNome = RedefinirNome(livro.Nome);
                            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
                            break;
                        case 3:
                            livroAutor = RedefinirAutor(livro.Autor);
                            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
                            break;
                        case 4:
                            livroPaginas = RedefinirPaginas(livro.Paginas);
                            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
                            break;
                        case 5:
                            livroCapitulos = RedefinirCapitulos(livro.Capitulos);
                            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
                            break;
                        case 6:
                            livroDescricao = RedefinirDescricao(livro.Descricao);
                            ConfirmarCadastroLivro(livroId,livroNome,livroAutor,livroPaginas,livroCapitulos,livroDescricao);
                            break;
                    }
                }
                else
                {
                    MenuPrincipal();
                }
            }
        }
        #endregion
    }
}