using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clases.Objetos
{
    public class Baraja
    {
        Queue<Carta> MazoCartas;

        List<int> Cartasint;

        Random generador = new Random();

        private void GenerarCartasint()
        {
            Cartasint = new List<int>();
            for (int i = 0; i < 52; i++)
            {
                Cartasint.Add(i);
            }
        }

        public void Aleatorio()
        {
            MazoCartas = new Queue<Carta>();
            GenerarCartasint();
            for (int i = 51; i >= 0; i--)
            {
                int index = generador.Next(0, i);
                int temp = Cartasint[i];
                Cartasint[i] = Cartasint[index];
                Cartasint[index] = temp;
            }
            Barajajuego();
        }

        private void Barajajuego()
        {
            for (int i = 0; i < Cartasint.Count; i++)
            {
                PintaCarta pinta = (PintaCarta)(Cartasint[i] % 4);
                ValorCarta valor = (ValorCarta)(Cartasint[i] % 13 + 1);
                MazoCartas.Enqueue(new Carta(pinta, valor));
            }
        }

        public Carta CartasIniciales()
        {
            return MazoCartas.Dequeue();
        }
    }
}
