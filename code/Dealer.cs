/*
Dealer
By: Mason Lane
06-03-2021
This handles the majority of our dealing logic. A dealer may draw, deal or shuffle cards from their decks
this is designed to support multiple decks incase we were to use this for other games.
*/
using System; 
using System.Collections.Generic;
using deckSpace;
using cardSpace;
using handSpace;

namespace dealerSpace { 
    class DealerConstants{
        public const int DEFAULTHANDSIZES = 5; //constant value. would change for other games
    }

    class Dealer { 

        private List<CardDeck> deck;    //a dealer may have multiple decks   
        private int handsize;
        public Dealer(){
            handsize = DealerConstants.DEFAULTHANDSIZES; //use default hand size
            deck = new List<CardDeck>();
            deck.Add(new CardDeck());    //add a new deck
        }

        public Dealer(int decks){
            handsize = DealerConstants.DEFAULTHANDSIZES;
            deck = new List<CardDeck>();
            for(int i = 0; i<decks; i++){
                deck.Add(new CardDeck()); //add a new deck for each 
            }
            
        }

        public Dealer(int decks, int size){     //example of how we'd include dealer in multi-deck-5+card games
            handsize = size;
            deck = new List<CardDeck>();
            for(int i = 0; i<decks; i++){
                deck.Add(new CardDeck());
            }
            
        }


        //draws a card & removes it from the deck
        public Card drawCard(){             
            Card card;
            for(int i = 0; i<deck.Count; i++){      //try to remove a card from either deck. othewise, null as error
                if(deck[i].countCards()>0){
                    card = deck[i].getCard(0);
                    deck[i].removeCard(0);
                    return card;
                }
            }
            return null;
        }


        //deals a number of cards
        public List<Card> dealCards(int numToDraw){
            List<Card> cards = new List<Card>();
            Card card;
            int capacity = 0;
            for(int i = 0; i<deck.Count; i++){
                capacity = capacity + deck[i].countCards();

            }
            if(capacity < numToDraw){
                    return null;               //check if all decks have enough cards to handle this deal
            }
            for(int i = 0; i<numToDraw; i++){   //draw the number of cards required
                card = drawCard();
                if(card != null){
                    cards.Add(card);             
                }
                else{
                    return null;                //if we still somehow dont have enough cards, send null error. we need a reshuffle/new deck
                }

            }
            return cards;
        }

        public List<Card> dealHand(){       //deal a hand
            List<Card> hand;
            hand = dealCards(handsize);
            return hand;
        }

        public void shuffleDecks(){         //shuffle all decks
            for(int i = 0; i<deck.Count; i++){
                deck[0].shuffleCards();
            }
        }
        
    } 
} 