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
                for (int val = 1; val < 14; val++)
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
    }
}
