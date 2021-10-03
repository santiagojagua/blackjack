﻿using System;
using clases.Objetos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clases
{
    public class Jugador
    {
        private List<Carta> mano = new List<Carta>();
        private int mipuntuacion;

        public bool Volar { get; set; }

        public Jugador()
        {
            Volar = false;
        }

        public void DarCarta(Carta CartasCrupier)
        {
            mano.Add(CartasCrupier);
        }

        public List<Carta> ShowHand()
        {
            return mano;
        }

        public Carta Ultimacarta()
        {
            return mano[mano.Count - 1];
        }

        public int GetPuntiacion()
        {
            mipuntuacion = 0;
            for (int i = 0; i < mano.Count; i++)
            {
                if ((int)mano[i].Value < 10)
                {
                    mipuntuacion += (int)mano[i].Value;
                }
                else
                {
                    mipuntuacion += 10;
                }
            }

            if (mano.Count == 2 && ((mano[0].Value == ValorCarta.Ace && (int)mano[1].Value >= 10) || (mano[1].Value == ValorCarta.Ace && (int)mano[0].Value >= 10)))
            {
                return 21;
            }

            if (mipuntuacion > 21)
            {
                Volar = true;
            }

            return mipuntuacion;
        }

        public void lanzarcatas()
        {
            mano.Clear();
            Volar = false;
        }
    }
}
