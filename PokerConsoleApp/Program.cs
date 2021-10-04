using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Card[] deckOfCards = new Card[52];
            int cardCount = 0;
            for(int suit = 1; suit < 5; suit++)
            {
                for (int val = 2; val < 15; val++)
                {
                    deckOfCards[cardCount] = new Card(val, suit);
                    cardCount++;
                }
            }


            int startingAmount = 50;

            Console.WriteLine("How many players are playing Poker");
            int numOfPlayers = int.Parse(Console.ReadLine());
            Player[] players = new Player[numOfPlayers] ;
            for (int playerNum = 0; playerNum < numOfPlayers; playerNum++)
            {
                players[playerNum] = new Player(startingAmount);
            }

            dealCards(deckOfCards, players);

            

            Thread.Sleep(20000);
        }

        static void dealCards(Card[] deck, Player[] players)
        {
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


            Card[] communityCards = new Card[5];
            for (int i = 0; i < 5; i++)
            {
                communityCards[i] = deck[numCard + i];
                if (i < 3)
                {

                }
            }

            


            Console.WriteLine("Community Cards:");
            printDeck(communityCards);


        }

        static Card[] shuffleDeck(Card[] deck)
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

        static void printDeck(Card[] deck)
        {
            for(int i = 0; i < deck.Length; i++)
            {
                Console.WriteLine(deck[i].getCardVal() + " of " + deck[i].getCardSuit());
            }
        }

        static void setPlayersActive(Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].setActive(true);
            }
        }

        static void goThroughPlayerDecision(Player[] players)
        {
            int activePlayersNum = 0;
            bool someoneHasRaised = false;
            int raiseNum = 0;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].getActive())
                {
                    if (someoneHasRaised)
                    {
                        Console.WriteLine("Would you like to call, fold, or raise? (C for Call, F for Fold, R for Raise)");
                        char decision = Console.ReadLine()[0];
                        if (decision == 'C')
                        {
                            players[i].setBigBlindStack(players[i].getBigBlindStack() - raiseNum);
                        }
                        else if(decision == 'F')
                        {
                            players[i].setActive(false);
                        }
                        else
                        {
                            players[i].setBigBlindStack(players[i].getBigBlindStack() - raiseNum);
                            Console.WriteLine("How much would you like to raise for?");
                            raiseNum = int.Parse(Console.ReadLine());
                            players[i].setBigBlindStack(players[i].getBigBlindStack() - raiseNum);
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Would you like to check or raise? (C for check, R for Raise)");
                        char decision = Console.ReadLine()[0];
                        if (decision == 'R')
                        {
                            Console.WriteLine("How much would you like to raise for?");
                            raiseNum = int.Parse(Console.ReadLine());
                            players[i].setBigBlindStack(players[i].getBigBlindStack() - raiseNum);
                        }
                        else
                        {
                            Console.WriteLine("Player " + (i + 1) + " has checked.");
                        }
                    }
                }
            }

        }
    }
}
