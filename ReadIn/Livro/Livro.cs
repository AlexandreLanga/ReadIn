using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadIn
{
    public class Livro
    {
        public int LivroId { get; set; }
        public int Paginas { get; set; }
        public int Capitulos { get; set; }
        public string? Descricao { get; set; }
        public string? Nome { get; set; }
        public string? Autor { get; set; }
        public int PaginasLidas { get; set; }
        public string PontoReferencia { get; set; }
        public int Linhas { get; set; }
    }
}
