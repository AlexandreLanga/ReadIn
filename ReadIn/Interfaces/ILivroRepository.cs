using ReadIn.Domain.Entities;

namespace ReadIn.Interfaces
{
    internal interface ILivroRepository
    {
        void SalvarLivro(Livro livro);
        void SalvarLeitura(Leitura leitura);
        string[] LerLivros();
    }
}
