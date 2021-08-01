using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoSQLCLR
{
    public class ContaEmail
    {
        public string Remetente { get; set; }
        public string Servidor { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool SSL { get; set; }
    }
}
