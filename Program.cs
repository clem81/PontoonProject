using PontoonProject;

List<Player> players = new List<Player>();
List<PlayerHand> hands = new List<PlayerHand>();
Hand bankerHand = new Hand();
int numberPlayers, betAmount;
string drawAgain = "";

Deck cardDeck = new Deck();
// Shuffle the deck once created 
// ready for the game
cardDeck.Shuffle();

// Setup players
Console.Write("How many players will be playing? ");
numberPlayers = Int32.Parse(Console.ReadLine());

// Add the required number of players by name
// and create a hand for them
for (int i = 0; i < numberPlayers; i++)
{
    players.Add(new Player(i));
    hands.Add(new PlayerHand(players[i]));
}

// Let each player complete their hand in full
for (int currentPlayer = 0; currentPlayer < numberPlayers; currentPlayer++)
{
    Console.WriteLine("----------------------------");

    // Can't join in round if player doesn't have chips
    if (players[currentPlayer].HasFunds())
    {
        Console.WriteLine(players[currentPlayer].Name() + "'s first card...");
        hands[currentPlayer].GetCard(cardDeck);
        hands[currentPlayer].DisplayHand(cardDeck);
        Console.WriteLine("The value of hand is: " + hands[currentPlayer].HandValue(cardDeck));

        do
        {
            // Stop further play in hand if they run out of chips
            if (players[currentPlayer].HasFunds())
            {
                // Keep looping if the bet cannot be funded (not enough chips)
                do
                {
                    Console.WriteLine("You have: " + players[currentPlayer].Balance());
                    Console.Write("How much do you want to bet? ");
                    betAmount = Int32.Parse(Console.ReadLine());
                } while (!hands[currentPlayer].PlaceBet(betAmount));

                Console.WriteLine(players[currentPlayer].Name() + " draws and hand is...");
                hands[currentPlayer].GetCard(cardDeck);
                hands[currentPlayer].DisplayHand(cardDeck);
                Console.WriteLine("The value of hand is: " + hands[currentPlayer].HandValue(cardDeck));

                // As long as the player hasn't drawn 5 cars, has funds 
                // and the hand is less than 21 give them the choice
                // to draw another card
                if (hands[currentPlayer].CardsLeft() > 0 && hands[currentPlayer].HandValue(cardDeck) < 21 && players[currentPlayer].HasFunds())
                {
                    Console.WriteLine("Do you want to draw another card? (y)");
                    drawAgain = Console.ReadLine().ToLower();
                }
            }
            else
            {
                drawAgain = "n";
            }

        } while (drawAgain == "y" && hands[currentPlayer].CardsLeft() > 0 && hands[currentPlayer].HandValue(cardDeck) < 21);
    }
    else
    {
        Console.WriteLine(players[currentPlayer].Name() + " is broke and can't play.");
    }
    Console.WriteLine("-----------------------------");
    Console.WriteLine("Turn finished.");
}

// Banker's turn does not need to progress in the same
// way as the players and sticks when 16+
do
{
    bankerHand.GetCard(cardDeck);
} while (bankerHand.CardsLeft() > 0 && bankerHand.HandValue(cardDeck) < 16);

Console.WriteLine("-----------------------------");
Console.WriteLine("Banker plays.");
bankerHand.DisplayHand(cardDeck);
Console.WriteLine("The value of hand is: " + bankerHand.HandValue(cardDeck));
Console.WriteLine("-----------------------------");

// Banker Pontoon beats all other hands
if (bankerHand.CardsLeft() == 2 && bankerHand.HandValue(cardDeck) == 21)
{
    Console.WriteLine("Banker got Pontoon - no one wins!");
}
// otherwise check for other winning conditions
else
{
    for (int player = 0; player < numberPlayers; player++)
    {
        // Player has Pontoon
        if (hands[player].HandValue(cardDeck) == 21 && hands[player].CardsLeft() == 3)
        {
            Console.Write(players[player].Name() + " got Pontoon and won £");
            Console.WriteLine(hands[player].PayDouble() + "!");
        }
        // Player has five card trick (and banker doesn't)
        else if ((hands[player].CardsLeft() == 0 && hands[player].HandValue(cardDeck) <= 21) && !(bankerHand.CardsLeft() == 0 && bankerHand.HandValue(cardDeck) <= 21))
        {
            Console.Write(players[player].Name() + " got five card trick and the banker didn't so won £");
            Console.WriteLine(hands[player].PayDouble() + "!");
        }
        // Player hand out-scores banker's
        else if ((hands[player].HandValue(cardDeck) > bankerHand.HandValue(cardDeck)) && hands[player].HandValue(cardDeck) <= 21)
        {
            Console.Write(players[player].Name() + " won £");
            Console.WriteLine(hands[player].Payout() + "!");
        }

        // Update player balance
        Console.WriteLine(players[player].Name() + " now has: " + players[player].Balance());
        Console.WriteLine("-----------------------------");
    }
}

Console.Read();
