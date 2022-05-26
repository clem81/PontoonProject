using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoonProject
{
    internal class Hand
    {
        protected List<int> cardsFromDeck;

        public Hand()
        {
            cardsFromDeck = new List<int>();
        }

        public int HandValue(Deck deck)
        {
            int handValue = 0;
            int cardValue;
            bool aceUsed = false;

            foreach (int cardRef in cardsFromDeck)
            {
                cardValue = deck.GetCardValue(cardRef);

                // Court cards are limited to 10
                if (cardValue >= 10)
                {
                    handValue += 10;
                }
                // Ace used as 11 twice would make player bust
                // so use it as one
                else if (cardValue == 1 && aceUsed == true)
                {
                    handValue += 1;
                }
                // First time Ace drawn try to use it as 11
                else if (cardValue == 1 && aceUsed == false)
                {
                    handValue += 11;
                    aceUsed = true;
                }
                // All other cards count their face value
                else
                {
                    handValue += cardValue;
                }
            }

            // Try lowest value for an Ace if hand bust
            if (handValue > 21 && aceUsed == true)
            {
                handValue -= 10;
            }

            return handValue;
        }

        public void GetCard(Deck deck)
        {
            cardsFromDeck.Add(deck.DrawCard());
        }

        public void DisplayHand(Deck deck)
        {
            foreach (int cardRef in cardsFromDeck)
            {
                deck.DisplayCard(cardRef);
            }
        }

        public int CardsLeft()
        {
            return 5 - cardsFromDeck.Count();
        }
    }
}
