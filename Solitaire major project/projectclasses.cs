using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace Solitaire_major_project
{
    public enum suits
    {
        hearts,diamonds,spades,clubs
    }
    class Card : PictureBox
    {
        private string[] names = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        private string[] suits = { "hearts", "diamonds", "spades", "clubs" };
        private Point originalpos;
        private bool dragging = false;
        public int value { get; set; }
        public suits suit { get; set; }

        public string GameLoc { get; set; }
       
        public bool faceup { get; set; }

        public string name()
        {
            return names[value - 1];
        }

        public Card(int v ,int s, bool isfaceup) : base()
        {
            value = v;
            suit = (suits)s;
            faceup = isfaceup;

            string imagefile="";
            if (faceup)
            {
                imagefile = "../../resources/" + names[value - 1] + "_of_" + suits[(int)suit] + ".png";
            }
            if (!faceup)
            {
                imagefile = "../../resources/" + "card_back_red" + ".png";
            }
            Image = Image.FromFile(imagefile);
            MouseMove += new MouseEventHandler(Card_MouseMove);
            MouseDown += new MouseEventHandler(Card_MouseDown);
            MouseUp += new MouseEventHandler(Card_MouseUp);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Margin = new Padding(0, 0, 0, 0);
            Width = 115;
            Height = 170;
        }

        public void flip()
        {
            faceup = !faceup;
            string imagefile = "";
            if (faceup)
            {

                imagefile = "../../resources/" + names[value - 1] + "_of_" + suits[(int)suit] + ".png";
            }
            if (!faceup)
            {
                imagefile = "../../resources/" + "card_back_red" + ".png";
            }
            Image = Image.FromFile(imagefile);
        }
        
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && faceup == true)
            {
                dragging = true;
                originalpos = this.Location;
                BringToFront();
            }
        }
        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Left += e.X - 57;
                this.Top += e.Y - 85;
            }
        }
        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                dragging = false;
                if(!validmove(this, e.Location))
                {
                    this.Location = originalpos; //resets position if move is invalid.
                }
                else
                {

                }
            }
        }
    }
    class deck
    {
        private Card[] cards = new Card[52];
        private int pos = 0;

        public deck()
        {
            for (int s = 0; s < 4; s++)
            {
                for (int v = 0; v < 13; v++)
                {
                    Card c = new Card(v+1,s,false);
                    cards[s * 13 + v] = c;
                }
            }
        }
        public Card Draw()
        {
            pos++;
            return cards[pos - 1];
        }
        public void Shuffle()
        {
            Random r = new Random();
            for (int i = 0; i < 200; i++)
            {
                /* shuffles cards into a random order */
                int ran = r.Next(0, 52);
                int ran2 = r.Next(0, 52);
                Card c1 = cards[ran];
                cards[ran] = cards[ran2];
                cards[ran2] = c1;
                
            }
       
        }
        
    }
    class acebox : PictureBox
    {
        public acebox()
        {
            /* this is the constructor for the acebox method which creates 1 acebox where the cards go in order 
             of value and suit i.e Ace of spades, 1 of spades and so on */

            string imagefile = "";

            imagefile = "../../resources/" + "acebox" + ".png";
            Image = Image.FromFile(imagefile);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Margin = new Padding(0, 0, 0, 0);
            Width = 115;
            Height = 170;
        }
        
    }
    class boxdeck 
    {
        //instantiates an array of aceboxes 
        private acebox[] aceboxes = new acebox[4];
        private int pos = 0;

        public acebox Draw()
        {
            //prints the ace boxes on screen side by side
            pos++;
            return aceboxes[pos - 1];
        }
    }
    

}
