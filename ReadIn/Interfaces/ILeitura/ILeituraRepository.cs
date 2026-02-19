using ReadIn.Domain.Entities;

namespace ReadIn.Interfaces.ILeitura
{
    public interface ILeituraRepository
    {
        void SalvarLeitura(Leitura leitura);
    }
}
