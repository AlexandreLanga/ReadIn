namespace ReadIn.Domain.Entities
{
    public class Leitura : Livro
    {
        public DateTime Inicio { get; set;} 
        public DateTime Final { get; set;}
        public TimeSpan Tempo { get; set;}
    }
}