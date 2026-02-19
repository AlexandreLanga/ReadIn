using ReadIn.Domain.Entities;
using ReadIn.Interfaces.ILeitura;
using ReadIn.Interfaces.ILivro;
using ReadIn.Repositories;

namespace ReadIn.Domain.Services
{
    public class LeituraService : ILeituraService
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly ILivroService _livroService;

        public LeituraService(
            ILeituraRepository leituraRepository,
            ILivroService livroService
        )
        {
            _leituraRepository = leituraRepository;
            _livroService = livroService;
        }

        public void AcompanharProgressoLivro()
        {
            var linhas = _livroService.ObterLivrosCadastrados();
            _livroService.ExibirLivrosCadastrados(linhas);

            Console.WriteLine("Digite o ID dos livro que gostaria de consultar o progresso:");

            Livro livroConsultaProgresso = new Livro();
            livroConsultaProgresso.LivroId = int.Parse(Console.ReadLine());
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
            _leituraRepository.SalvarLeitura(leituraLivro);
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

        private string EscolherLivro(string livro)
        {
            Console.Clear();

            int achou = 0, contador = 1;
            string opcao = "";

            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.WriteLine("| Nunca é uma hora ruim para ler e adquirir vastos conhecimentos, certo? |");
            Console.WriteLine("| Qual livro vamos ler agora?                                            |");
            Console.WriteLine("+------------------------------------------------------------------------+\n");


            string[] livros = _livroService.ObterLivrosCadastrados();

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
    }
}