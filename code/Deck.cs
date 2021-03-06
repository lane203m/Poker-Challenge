/*
CardDeck
By: Mason Lane
06-03-2021
This is a deck object. it contains many cards and is used for storage
*/
using System; 
using System.Collections.Generic;
using cardSpace;

namespace deckSpace { 
    class CardDeck { 
        private List<Card> deckCards;       //people should not be able to access the deck directly
        
        public CardDeck(){
            deckCards = generateCards();    //new card deck means fill with 52 cards
        }

        public char generateSuit(int n){
            return CardConstants.SUITS[n];  //from the card constants, get the char suit associated with this int
        }

        public string generateValue(int n){
            return CardConstants.VALUES[n]; //from the card constants, get the char value associated with this int
        }


        //generates cards of every value for every suit
        public List<Card> generateCards(){
            List<Card> cards = new List<Card>();

            for(int j = 0; j<CardConstants.SUITS.Length; j++)       //for every suit
            {
                char cardSuit = generateSuit(j);
                for(int k = 0; k<CardConstants.VALUES.Count ; k++)  //for every value of said suit
                {
                    string cardValue = generateValue(k);
                    cards.Add(new Card(cardValue, cardSuit));       //add a card of value and suit
                }
            }
            return cards;
        }


        //uses the fisher yates shuffle to mix our deck
        public void shuffleCards(){
            Random random = new Random((int)DateTime.Now.Ticks);    //random number with seed
            int n = deckCards.Count;                                
            while (n > 1) {  
                n--;  
                int k = random.Next(n + 1);                         //get random int               
                Card card = deckCards[k];                           //using int, get card
                deckCards[k] = deckCards[n];                        //swap cards
                deckCards[n] = card;  
            } 
        }

        public List<Card> getCards(){
            return deckCards;
        }

        public Card getCard(int i){
            return deckCards[i];
        }

        public void removeCard(int i){
            deckCards.RemoveAt(i);
        }

        public int countCards(){
            return deckCards.Count;
        }
    } 
} 