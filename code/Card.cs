/*
Card
By: Mason Lane
06-03-2021
This is a card object. it contains card information
we use a dictionary to make accessing & evaluating string values easier
*/
using System; 
using System.Collections.Generic;
namespace cardSpace { 
    class CardConstants
    {
        public static readonly  char[] SUITS = {'C','D','H','S'};   //typical suits
        public static readonly  Dictionary<int, string> VALUES = new Dictionary<int, string>{{0,"2"},{1,"3"},{2,"4"},   //now aces can be associated with low cards
                                                           {3,"5"},{4,"6"},{5,"7"},
                                                           {6,"8"},{7,"9"},{8,"10"},
                                                           {9,"J"},{10,"Q"},
                                                           {11,"K"},{12,"A"}};
    }
    class Card { 
        private string value;           //private, since cards should be kept hidden      
        private char suit;              

        public Card(string v, char s){  //a card cant exist without with suit/val
            value = v;                  
            suit = s;                   
        }


        public string getValue(){       
            return value;
        }

        public char getSuit(){
            return suit;
        }

        public void setSuit(char s){
            suit = s;
        }

        public void setValue(string v){
            value = v;
        }
    } 
} 