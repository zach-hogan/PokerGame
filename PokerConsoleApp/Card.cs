using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsoleApp
{
    class Card
    {
        private int cardVal;
        private int cardSuitAsInt;
        private String cardSuit;

        public Card(int cardVal, int cardSuitAsInt)
        {
            setCardVal(cardVal);
            setCardSuitAsInt(cardSuitAsInt);
            setCardSuit(getCardSuitAsInt());
        }

        public String printCardInfo()
        {
            return getCardVal() + " of " + getCardSuit();
        }

        public void setCardVal(int cardVal)
        {
            this.cardVal = cardVal;
        }
        public void setCardSuitAsInt(int cardSuitAsInt)
        {
            this.cardSuitAsInt = cardSuitAsInt;
        }
        
        public void setCardSuit(int cardSuitAsInt)
        {
            String cardSuit;
            if (cardSuitAsInt == 1)
            {
                cardSuit = "Clubs";
            }
            else if(cardSuitAsInt == 2)
            {
                cardSuit = "Diamonds";
            }
            else if (cardSuitAsInt == 3)
            {
                cardSuit = "Hearts";
            }
            else
            {
                cardSuit = "Spades";
            }
            this.cardSuit = cardSuit;
        }

        public int getCardVal()
        {
            return cardVal;
        }

        public int getCardSuitAsInt()
        {
            return cardSuitAsInt;
        }

        public String getCardSuit()
        {
            return cardSuit;
        }

    }
}
