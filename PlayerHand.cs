using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoonProject
{
    internal class PlayerHand : Hand

    {
        // Derived class needs betting and player information
        // along with protected attributes in parent class
        private Player holder;
        private int bet;

        public PlayerHand(Player player)
        {
            bet = 0;
            // Hand is an aggregate of itself 
            // and the player that owns it
            // (used to funding the bets made)
            holder = player;
        }

        public bool PlaceBet(int amount)
        {
            bool betPlaced = false;
            int betIncrease = holder.CommitBet(amount);

            // Only update bet if there were funds
            if (betIncrease > 0)
            {
                bet += betIncrease;
                Console.WriteLine("Current bet value: £" + bet);
                betPlaced = true;
            }

            // Calling program will take care of if 
            // they can't place bet
            return betPlaced;
        }

        public int Payout()
        {
            // Update player's balance with original bet 
            // and same again for winning
            holder.UpdateBalance(bet * 2);
            // Confirm amount won for display in calling program
            return bet;
        }

        public int PayDouble()
        {
            // Update player's balance with original bet 
            // and double for Pontoon
            holder.UpdateBalance(bet * 3);
            // Confirm amount won for display in calling program
            return bet * 2;
        }
    }
}
