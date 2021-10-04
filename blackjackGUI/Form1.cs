using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clases.Objetos;
using clases.AdministradorJuego;
using clases.Persona;

namespace blackjackGUI
{
    public partial class Blackjack : Form
    {
        public Jugador jugador;
        public Administrador aj;
        private static int empezarXPos = -10;
        private static int empezarcrupierXPos = 285;
        private int jugadorCartaXPos = empezarXPos;
        private int crupiercartaXPos = empezarcrupierXPos;
        public List<PictureBox> jugadorCartasToDisplay;
        public List<PictureBox> crupiercartas;


        public Blackjack()
        {
            InitializeComponent();
            aj = new Administrador();
            jugador = new Jugador();
            aj.Addjugador(jugador);
            jugadorCartasToDisplay = new List<PictureBox>();
            crupiercartas = new List<PictureBox>();

            splitButton.Hide();
        }

        private void DibujarCartasJugador(Carta carta)
        {
            jugadorCartaXPos += 30;
            PictureBox newCard = new PictureBox();
            Image img = Image.FromFile("../../card_images/" + carta.Valor + carta.Pinta + ".png");
            newCard.Image = img;
            newCard.Location = new System.Drawing.Point(jugadorCartaXPos, 180);
            newCard.Name = "nuevacarta";
            newCard.Size = new System.Drawing.Size(72, 99);
            this.Controls.Add(newCard);
            newCard.BringToFront();
            jugadorCartasToDisplay.Add(newCard);
        }

        private void CartaCrupierNoMostrada(Carta carta)
        {

            crupiercartaXPos -= 30;
            PictureBox blankCarta = new PictureBox();
            Image blankImage = Image.FromFile("../../card_images/b1fv.png");
            blankCarta.Image = blankImage;
            blankCarta.Location = new System.Drawing.Point(crupiercartaXPos, 12);
            blankCarta.Name = "nuevacarta";
            blankCarta.Size = new System.Drawing.Size(72, 99);
            this.Controls.Add(blankCarta);
            blankCarta.BringToFront();
            crupiercartas.Add(blankCarta);

            DibujarCrupier(carta);

        }

        private void DibujarCrupier(Carta carta)
        {
            crupiercartaXPos -= 30;
            PictureBox NuevaCarta = new PictureBox();
            Image img = Image.FromFile("../../card_images/" + carta.Valor + carta.Pinta + ".png");
            NuevaCarta.Image = img;
            NuevaCarta.Location = new System.Drawing.Point(crupiercartaXPos, 12);
            NuevaCarta.Name = "newCard";
            NuevaCarta.Size = new System.Drawing.Size(72, 99);
            this.Controls.Add(NuevaCarta);
            crupiercartas.Add(NuevaCarta);
        }

        private void standButton_Click(object sender, EventArgs e)
        {
            dealerScore.Text = "Dealer score: " + aj.GetPuntuacionCrupier();
            crupiercartaXPos = empezarcrupierXPos;
            RemoverCartas(crupiercartas);
            DibujarCrupier(aj.Getcartacrupier()[1]);
            DibujarCrupier(aj.Getcartacrupier()[0]);

            while (aj.GetPuntuacionCrupier() < 17)
            {
                aj.DaraCartaCrupier();
                dealerScore.Text = "Dealer score: " + aj.GetPuntuacionCrupier();
                DibujarCrupier(aj.Ultimacartacrupier);
            }

            if (aj.GetPuntuacionCrupier() > jugador.GetPuntiacion() && aj.GetPuntuacionCrupier() < 22)
            {
                resultLabel.Text = "Crupier gana!";
            }
            else
            {
                resultLabel.Text = "Jugador gana!";
            }

        }

        private void hitMeButton_Click(object sender, EventArgs e)
        {

            if (!jugador.Volar)
            {
                jugador.DarCarta(aj.CartasIniciales());
                playerScore.Text = "Score: " + jugador.GetPuntiacion();
                DibujarCartasJugador(jugador.Ultimacarta());

                if (jugador.Volar)
                {
                    hitMeButton.Enabled = false;
                    standButton.Enabled = false;
                    splitButton.Enabled = false;
                    resultLabel.Text = "Crupier gana!";
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "";
            hitMeButton.Enabled = true;
            standButton.Enabled = true;
            RemoverCartas(jugadorCartasToDisplay);
            RemoverCartas(crupiercartas);

            crupiercartaXPos = empezarcrupierXPos;
            jugadorCartaXPos = empezarXPos;
            List<Carta> cartasjugador = jugador.MostrarMano();
            aj.EmpezarNuevojuego();
            playerScore.Text = "Score: " + jugador.GetPuntiacion();
            dealerScore.Text = "";
            DibujarCartasJugador(cartasjugador[0]);
            DibujarCartasJugador(cartasjugador[1]);
            CartaCrupierNoMostrada(aj.CartaVisiblecrupier);

            if (cartasjugador[0].Valor == cartasjugador[1].Valor)
            {
                splitButton.Enabled = true;
            }
            if (jugador.GetPuntiacion() == 21)
            {
                resultLabel.Text = "Blackjack! Jugador gana!";
                hitMeButton.Enabled = false;
                standButton.Enabled = false;
            }
        }

        private void RemoverCartas(List<PictureBox> imagenescarta)
        {
            foreach (PictureBox box in imagenescarta)
            {
                this.Controls.Remove(box);
            }
        }
    }
}
