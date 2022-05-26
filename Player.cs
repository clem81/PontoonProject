using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoonProject
{
    internal class Player
    {
        private string playerName;
        private int chips;

        // Create player
        public Player(int n)
        {
            chips = 1000;

            Console.Write("Player " + (n + 1) + ", what is your name? ");
            playerName = Console.ReadLine();
        }

        public string Balance()
        {
            return "£" + chips;
        }

        public string Name()
        {
            return playerName;
        }

        public int CommitBet(int amountBet)
        {
            if (amountBet > chips || amountBet <= 0)
            {
                Console.WriteLine("You don't have enough chips.");
                amountBet = 0;
            }
            else
            {
                chips -= amountBet;
                Console.WriteLine("Balance is now: " + Balance());
            }

            // Makes sure the calling program knows how
            // much has actually been committed to bet
            // even if it is zero!
            return amountBet;
        }

        public bool HasFunds()
        {
            bool canBet = true;

            if (chips == 0)
            {
                canBet = false;
            }

            return canBet;
        }

        public void UpdateBalance(int amountWon)
        {
            chips += amountWon;
        }
    }
}
