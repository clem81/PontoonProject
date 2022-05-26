using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoonProject
{
    internal class Card
    {
        // This identifies each card in relation 
        // to a whole deck
        private int cardReference;

        // Constructor for a card object needed 
        // during shuffle
        public Card()
        {
            cardReference = 0;
        }

        public Card(int n)
        {
            cardReference = n;
        }

        public int Rank()
        {
            // Use the full range of cardReference
            // to calculate an in-suit rank 1-13
            return 1 + cardReference % 13;
        }

        public String Suit()
        {
            string suitName = "";

            // Calculate suit based on groups of
            // 13 cards
            int suitValue = cardReference / 13;

            // Change the suitValue to an actual 
            // suit name
            switch (suitValue)
            {
                case 0:
                    suitName = "Hearts";
                    break;
                case 1:
                    suitName = "Clubs";
                    break;
                case 2:
                    suitName = "Diamonds";
                    break;
                case 3:
                    suitName = "Spades";
                    break;
            }

            return suitName;
        }
    }
}
