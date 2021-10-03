using System;
using clases.Objetos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clases
{
    public class Administrador
    {
        private Baraja barajajuego = new Baraja();
        private List<Jugador> jugador = new List<Jugador>();
        private List<int> jugadorpuntuacion = new List<int>();
        private Jugador crupier = new Jugador();
        public bool Juego { get; set; }

        public Carta CartaVisiblecrupier
        {
            get
            {
                return crupier.ShowHand()[0];
            }
        }

        public Carta Ultimacartacrupier
        {
            get
            {
                return crupier.ShowHand()[crupier.ShowHand().Count - 1];
            }
        }

        public List<Carta> Getcartacrupier()
        {
            return crupier.ShowHand();
        }

        public Administrador()
        {
            Juego = false;
        }

        public Administrador(Jugador jugador1)
        {
            jugador.Add(jugador1);
        }

        public void Addjugador(Jugador newJugador)
        {
            jugador.Add(newJugador);
        }

        public void PrimerasDosCartas()
        {
            barajajuego.Aleatorio();

            crupier.DarCarta(barajajuego.CartasIniciales());
            crupier.DarCarta(barajajuego.CartasIniciales());

            for (int i = 0; i < jugador.Count; i++)
            {
                jugador[i].DarCarta(barajajuego.CartasIniciales());
                jugador[i].DarCarta(barajajuego.CartasIniciales());
            }
        }

        public Carta CartasIniciales()
        {
            return barajajuego.CartasIniciales();
        }

        public void EmpezarNuevojuego()
        {
            barajajuego = new Baraja();
            for (int i = 0; i < jugador.Count; i++)
            {
                jugador[i].lanzarcatas();
                crupier.lanzarcatas();
            }
            PrimerasDosCartas();
        }

        private int GetPuntuacionMano(List<Carta> cartas)
        {
            int puntuacion = 0;
            for (int i = 0; i < cartas.Count; i++)
            {
                int valorcartas = (int)cartas[i].Valor;
                if (valorcartas > 10)
                {
                    valorcartas = 10;
                }
                puntuacion += valorcartas;
            }

            if (puntuacion > 21)
            {
                puntuacion = -1;
            }

            if (cartas.Count == 2 && ((cartas[0].Valor == ValorCarta.Ace && (int)cartas[1].Valor >= 10) || (cartas[1].Valor == ValorCarta.Ace && (int)cartas[0].Valor >= 10)))
            {
                return 0;
            }

            return puntuacion;
        }

        public string PantallaPuntuacion()
        {
            StringBuilder mostrarpunticion = new StringBuilder();
            for (int i = 0; i < jugador.Count; i++)
            {
                mostrarpunticion.AppendLine(String.Format("Player {0} score: {1}", i, jugadorpuntuacion[i]));
            }
            return mostrarpunticion.ToString();
        }

        public void DaraCartaCrupier()
        {
            crupier.DarCarta(barajajuego.CartasIniciales());
        }

        public int GetDealerScore()
        {
            return crupier.GetPuntiacion();
        }
    }
}
