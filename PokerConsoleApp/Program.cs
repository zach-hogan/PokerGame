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

            DealCards dealCards = new DealCards(deckOfCards,players);
            Thread.Sleep(20000);
        }
    }
}
