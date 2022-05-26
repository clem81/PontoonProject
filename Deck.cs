using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoonProject
{
    internal class Deck
    {
        // Declare deck of cards
        // (initialise in constructor)
        private Card[] deck;
        private int topCard;

        public Deck()
        {
            deck = new Card[52];
            topCard = 0;

            // Loop through deck
            for (int i = 0; i < 52; i++)
            {
                // For each Card object, initialise 
                // with the loop's count variable,
                // (note this calls the class's constructor)
                deck[i] = new Card(i);
            }
        }

        // Display card in a user-friendly way
        public void DisplayCard(int n)
        {
            String cardRankName;
            int rankValue;

            // Determine the name of the card for the 
            // benefit of the user when playing
            rankValue = deck[n].Rank();

            switch (rankValue)
            {
                case 1:
                    cardRankName = "Ace";
                    break;
                case 11:
                    cardRankName = "Jack";
                    break;
                case 12:
                    cardRankName = "Queen";
                    break;
                case 13:
                    cardRankName = "King";
                    break;
                default:
                    cardRankName = rankValue.ToString();
                    break;
            }

            Console.WriteLine(cardRankName + " of " + deck[n].Suit());
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            Card swapCard = new Card();
            int swapRef;

            for (int i = 0; i < 52; i++)
            {
                // Generate and store a random number
                swapRef = rnd.Next(0, 52);

                // Swap cards in order to new random
                // postion
                swapCard = deck[i];
                deck[i] = deck[swapRef];
                deck[swapRef] = swapCard;
            }
        }

        public int GetCardValue(int cardNumber)
        {
            return deck[cardNumber].Rank();
        }

        public int DrawCard()
        {
            int drawCard = topCard;
            topCard += 1;

            // As a shuffle should only happen after
            // a Pontoon, the Deck should be reused -
            // wrap around from end back to start
            if (topCard > 51)
            {
                topCard = 0;
            }

            return drawCard;
        }
    }
}
