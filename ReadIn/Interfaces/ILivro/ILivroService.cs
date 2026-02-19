namespace ReadIn.Interfaces.ILivro
{
    public interface ILivroService
    {
        public void CadastrarLivro();
        public void ListarLivros();
        public string[] ObterLivrosCadastrados();
        public void ExibirLivrosCadastrados(string[] linhas);
    }
}
