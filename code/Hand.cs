/*
CardHand
By: Mason Lane
06-03-2021
This is a hand object. it contains many cards and simply keeps track of a given hand
*/
using System; 
using System.Collections.Generic;
using cardSpace;
namespace handSpace {
    class CardHand { 
        private List<Card> handCards;           //a hand of cards. Should be private

        public CardHand(List<Card> dealtCards){
            handCards = new List<Card>();
            handCards = dealtCards;             //a hand must be dealt cards. it should not create its own cards          
        }


        public Card getCard(int i){
            return handCards[i];
        }
        

        public void setCard(Card card, int i){  //if you *must* set a card for some reason
            handCards[i] = card;
        }

        public void addCard(Card card){
            handCards.Add(card);            //say we play blackjack or go fish and the player needs to pickup
        }
        public void removeCard(int i){      
            handCards.RemoveAt(i);
        }
        public Card playCard(int i){        //play a card on the table, remove said card from hand.
            Card card = getCard(i);
            removeCard(i);
            return card;
        }

        public int getSize(){
            return handCards.Count;         //how big is a hand?
        }

        

    } 
} 