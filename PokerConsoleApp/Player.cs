using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsoleApp
{
    class Player
    {
        private Card card1;
        private Card card2;
        private Card[] hand;

        private int bigBlindStack;
        public Player(int BBStack)
        {
            setBigBlindStack(BBStack);
        }

        public void printHand()
        {
            Console.WriteLine(getCard1().printCardInfo());
            Console.WriteLine(getCard2().printCardInfo());
        }

        public Card getCard1()
        {
            return card1;
        }

        public Card getCard2()
        {
            return card2;
        }

        public int getBigBlindStack()
        {
            return bigBlindStack;
        }

        public Card[] getHand()
        {
            return hand;
        }

        public void setHand()
        {
            hand = new Card[2];
            hand[0] = getCard1();
            hand[1] = getCard2();
        }

        
        public void setBigBlindStack(int BBStack)
        {
            this.bigBlindStack = BBStack;
        }

        public void setCard1(Card card1)
        {
            this.card1 = card1;
        }

        public void setCard2(Card card2)
        {
            this.card2 = card2;
        }
    }
}
