using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Bloco
    {
        public string Nome { get; set; }
        public List<Bloco> Vizinhos { get; set; }
        public int OrdemX { get; set; }
        public int OrdemY { get; set; }
        public Dictionary<Bloco, string> PontosReferencia { get; set; }

        public Bloco(string nome, int ordemX, int ordemY)
        {
            this.Nome = nome;
            this.OrdemX = ordemX;
            this.OrdemY = ordemY;
            this.PontosReferencia = new Dictionary<Bloco, string>();
        }
    }
}
