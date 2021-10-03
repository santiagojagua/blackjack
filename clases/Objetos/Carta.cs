using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clases.Objetos
{
    public struct Carta
    {
        private PintaCarta pinta;
        private ValorCarta valor;
        public PintaCarta Suit
        {
            get
            {
                return this.pinta;
            }
        }
        public ValorCarta Valor
        {
            get
            {
                return this.valor;
            }
        }
        public Carta(PintaCarta pinta, ValorCarta valor)
            : this()
        {
            this.pinta = pinta;
            this.valor = valor;
        }

        public override string ToString()
        {
            return string.Format("Pinta: {0}, Value: {1}", this.pinta, this.valor);
        }
    }
}
