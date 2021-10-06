using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsoleApp
{
    class DealCards
    {
        private int pot;
        public DealCards(Card[] deck, Player[] players)
        {
            setPot(0);
            setPlayersActive(players);
            shuffleDeck(deck);
            int numCard = 0;
            for (int playerNum = 0; playerNum < players.Length; playerNum++)
            {
                players[playerNum].setCard1(deck[numCard]);
                players[playerNum].setCard2(deck[numCard + 1]);
                players[playerNum].setHand();
                numCard += 2;
            }
            for (int playerNum = 0; playerNum < players.Length; playerNum++)
            {
                Console.WriteLine("Player " + (playerNum + 1));
                players[playerNum].printHand();

            }
            Console.WriteLine();

            playerDecisions(players, getPot());
            Card[] communityCards = new Card[5];
            for (int i = 0; i < 5; i++)
            {
                communityCards[i] = deck[numCard + i];
                Console.WriteLine("Card " + (i + 1) + ": " + communityCards[i].printCardInfo());
                if (i > 1)
                {
                    playerDecisions(players, getPot());
                }
            }
        }

        public Card[] shuffleDeck(Card[] deck)
        {
            Card[] newDeck = deck;
            Random rnd = new Random();
            int randomNum;
            for (int i = 0; i < deck.Length; i++)
            {
                randomNum = rnd.Next(0, deck.Length - 1);
                Card tempCard = newDeck[i];
                newDeck[i] = deck[randomNum];
                deck[randomNum] = tempCard;
            }
            return newDeck;
        }

        public void setPlayersActive(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].setActive(true);
            }
        }

        public void playerDecisions(Player[] players, int PotSize)
        {
            int[] howMuchEachPlayerHasIn = new int[players.Length];

            for (int i = 0; i < howMuchEachPlayerHasIn.Length; i++)
            {
                howMuchEachPlayerHasIn[i] = 0;
            }

            int recentRaiser = 0;
            int potSize = PotSize;
            int isPotGood = 0;
            int numPlayersActive = 0;
            int playerNum = 0;
            bool someoneHasRaised = false;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].getActive())
                {
                    numPlayersActive++;
                }
            }

            while (isPotGood != numPlayersActive)
            {
                if (playerNum == players.Length)
                {
                    playerNum = 0;
                }
                if (!players[playerNum].getActive())
                {
                    playerNum++;
                    continue;
                }

                if (someoneHasRaised)
                {
                    Console.WriteLine("\nPot Size: " + potSize);
                    Console.WriteLine("Player " + (playerNum + 1) + ":");
                    Console.Write("Hand: ");
                    players[playerNum].printHand();
                    Console.WriteLine("Stack: " + players[playerNum].getBigBlindStack());
                    Console.WriteLine("Would you like to call " + (howMuchEachPlayerHasIn[recentRaiser] - howMuchEachPlayerHasIn[playerNum]) + " BB, fold, or raise? (c for Call, f for Fold, r for Raise)");
                    char decision = Console.ReadLine()[0];
                    if (decision == 'c')
                    {
                        potSize += howMuchEachPlayerHasIn[recentRaiser] - howMuchEachPlayerHasIn[playerNum];

                        players[playerNum].setBigBlindStack(players[playerNum].getBigBlindStack() - (howMuchEachPlayerHasIn[recentRaiser] - howMuchEachPlayerHasIn[playerNum]));
                        howMuchEachPlayerHasIn[playerNum] = howMuchEachPlayerHasIn[recentRaiser];
                        Console.WriteLine("Player " + (playerNum + 1) + " has called. They have " + howMuchEachPlayerHasIn[playerNum] + " in the pot");

                        isPotGood++;

                    }
                    else if (decision == 'f')
                    {
                        Console.WriteLine("Player " + (playerNum + 1) + " has folded.");
                        players[playerNum].setActive(false);
                        numPlayersActive--;
                    }
                    else
                    {
                        Console.WriteLine("What do you want to raise it to?");
                        int newToCall = int.Parse(Console.ReadLine());
                        while (newToCall < howMuchEachPlayerHasIn[recentRaiser])
                        {
                            Console.WriteLine("You must raise to a higher number than you're calling.");
                            Console.WriteLine("What do you want to raise it to?");
                            newToCall = int.Parse(Console.ReadLine());
                        }
                        players[playerNum].setBigBlindStack(players[playerNum].getBigBlindStack() - (newToCall - howMuchEachPlayerHasIn[recentRaiser]));
                        potSize += newToCall - howMuchEachPlayerHasIn[playerNum];
                        isPotGood = 1;
                        recentRaiser = playerNum;
                        howMuchEachPlayerHasIn[playerNum] = newToCall;
                        Console.WriteLine("Player " + (playerNum + 1) + " has called. They have " + howMuchEachPlayerHasIn[playerNum] + " in the pot");
                    }
                }
                else
                {
                    Console.WriteLine("\nPot Size: " + potSize);
                    Console.WriteLine("Player " + (playerNum + 1) + ":");
                    Console.Write("Hand: ");
                    players[playerNum].printHand();
                    Console.WriteLine("Stack: " + players[playerNum].getBigBlindStack());
                    Console.WriteLine("Would you like to check or raise? (c for check, r for Raise)");
                    char decision = Console.ReadLine()[0];
                    if (decision == 'r')
                    {
                        recentRaiser = playerNum;
                        Console.WriteLine("What do you want to raise it to?");
                        int newToCall = int.Parse(Console.ReadLine());
                        while (newToCall < 1)
                        {
                            Console.WriteLine("You must raise to a positive integer");
                            Console.WriteLine("What do you want to raise it to?");
                            newToCall = int.Parse(Console.ReadLine());
                        }
                        players[playerNum].setBigBlindStack(players[playerNum].getBigBlindStack() - newToCall);
                        someoneHasRaised = true;
                        potSize += newToCall;
                        isPotGood = 1;
                        howMuchEachPlayerHasIn[playerNum] = newToCall;
                        Console.WriteLine("Player " + (playerNum + 1) + " has called. They have " + howMuchEachPlayerHasIn[playerNum] + " in the pot");

                    }
                    else
                    {
                        Console.WriteLine("Player " + (playerNum + 1) + " has checked.");
                        isPotGood++;
                    }
                }
                playerNum++;


            }
            setPot(pot);
        }

        public void setPot(int pot)
        {
            this.pot = pot;
        }

        public int getPot()
        {
            return pot;
        }
    }
}
