using ReadIn.Domain.Entities;

namespace ReadIn.Interfaces.ILivro
{
    public interface ILivroRepository
    {
        void SalvarLivro(Livro livro);
        string[] LerLivros();
    }
}
