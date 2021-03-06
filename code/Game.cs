/*
PokerHands
By: Mason Lane
06-03-2021
This handles the majority of our card logic when playing.
When a user asks to play a round, we have our dealer class
create a new deck(s), shuffle, and then deal 5. It then 
checks each hand to determine a winner given the poker rules
*/
using System; 
using cardSpace;
using deckSpace;
using handSpace;
using dealerSpace;
using System.Collections.Generic; 
namespace pokerHandSpace { 
    class HandConstants{
        public static readonly  Dictionary<int, string> HANDS = new Dictionary<int, string>{{0,"High"},{1,"Pair"},{2,"Two Pairs"},  //likewise as below, but for associating a win type's value
                                                                                             {3,"Three Of A Kind"},{4,"Flush"}}; 
        public static readonly  Dictionary<string, int> SCORINGVALUES = new Dictionary<string, int>{{"2", 0},{"3", 1},{"4", 2},     //This is used to reference a card's inherent score
                                                                                                    {"5", 3},{"6", 4},{"7", 5},     //games can have unique score systems. low aces, etc.
                                                                                                    {"8", 6},{"9", 7},{"10", 8},
                                                                                                    {"J", 9},{"Q", 10},
                                                                                                    {"K", 11},{"A", 12}};                                                                                     
        public const int NUMPLAYERS = 2;    //incase we want to implement multiple players.       
    }
    class PokerHands { 
        private List<CardHand> players; //hands and dealer should be private
        private Dealer dealer;

        public PokerHands(){
        }


        //get new hands
        public void newHands(){
            players = new List<CardHand>();
            for(int i = 0; i<HandConstants.NUMPLAYERS; i++){
                players.Add(new CardHand(dealer.dealHand()));
            }
        }


        //plays one round to compare hands
        public void playRound(int decks){
            dealer = new Dealer(decks);     //new game = new deck. 
            dealer.shuffleDecks();          //shuffle 
            newHands();                     //deal hands

            Object[] result = compareHands();   //compare both hands and return the result
            string resultMessage = "";
            Console.WriteLine("_______________________________");
            Console.WriteLine("Player 1:");
            printHand(players[0]);
            Console.WriteLine("Player 2:");
            printHand(players[1]);
            Console.WriteLine("-------------------------------");

            if((int)result[0] == 0){                //result is 0? player1 wins. Append win type
                resultMessage = "Player 1 Wins"; 
                resultMessage = resultMessage + " - ";
            }
            else if((int)result[0] == 1){           //result is 1? player2 wins. Append win type
                resultMessage = "Player 2 Wins"; 
                resultMessage = resultMessage + " - ";
            }

            resultMessage = resultMessage + result[1];  //display results or error notifications
            Console.WriteLine(resultMessage); 
            Console.WriteLine("_______________________________");
            
        }


        //compare players' hands
        public Object[] compareHands(){
            List<int> hands = new List<int>();                  //each player's hand is equal to a win score of 0-4
            for(int i = 0; i<HandConstants.NUMPLAYERS; i++){
                hands.Add(checkPlay(players[i]));               //for each player, add their winstate to the list
            }
            if(hands[0] == hands[1]){                           //if player 1 and 2 have the same win states
                switch(hands[0]){                               //resolve based on the shared winstate 
                    case 0:
                        return resolveHighTie(players[0], players[1]);
                    case 1:
                        return resolvePairTie(players[0], players[1]);  
                    case 2:
                        return resolveTwoPairTie(players[0], players[1]);
                    case 3:
                        return resolveThreesTie(players[0], players[1]);
                    case 4:
                        return resolveHighTie(players[0], players[1]);      //we can simply default to high cards, since its a tied flush           
                }     
            }
            if(hands[0] > hands[1]){                                    //if player 1 has a higher winstate, they win. return their 0 and their winning hand string
                return new Object[]{0, HandConstants.HANDS[hands[0]]};
            }
            else if (hands[0] < hands[1]){                              //else do so for player 2
                return new Object[]{1,HandConstants.HANDS[hands[1]]};
            }
            else{
                return new Object[]{3,"[unexpected game error]"};       //exception handling
            }




        }


        //get a player's hand worth
        public int checkPlay(CardHand hand){
            if(isFlush(hand) == true){                                  //check if flush, if yes, winstate is 4
                return 4;
            }
            else if(isThreeOfAKind(hand) != null ){                     //if three of a kind is returned, state is 3 
                return 3;
            }else if(isTwoPair(hand) != null){                          //etc etc
                return 2;
            }else if(isPair(hand) != null){
                return 1;
            }else{
                return 0;                                               //if nothing exists, then 0. Rely on High cards
            }
        }        


        //Discovers a winner for tied high cards
        public Object[] resolveHighTie(CardHand hand1, CardHand hand2){
            List<string> hand1Values = new List<string>();              //chars of each hand
            List<string> hand2Values = new List<string>();
            Object[] currentWinner;                                     //current winner
            int checkForHighTies = 4;
            string highMessage;
            int highestValue = -1;
            int temp;

            for(int i = 0; i < 5; i++){
                hand1Values.Add(hand1.getCard(i).getValue());           //get all card values for either player
                hand2Values.Add(hand2.getCard(i).getValue()); 
            }

            currentWinner = new Object[]{2, "Tie"};                     //assume tie

            while(checkForHighTies >= 0){                               //continue until all high ties are accounted for. if all values are identical, we truly tied 
                for(int i = 0; i < hand1Values.Count; i++){         
                    for(int j = 0; j < hand1Values.Count; j++){
                        if(HandConstants.SCORINGVALUES[hand1Values[i]] > HandConstants.SCORINGVALUES[hand2Values[i]]){      //if hand 1's current value is greater
                            temp = HandConstants.SCORINGVALUES[hand1Values[i]];
                            if(temp > highestValue){                                    //if hand1's value is greater than our past highest
                                highestValue = temp;
                                highMessage = "High: "+hand1Values[i];
                                currentWinner = new Object[]{0, highMessage};                   //set new current winner
                            }
                            if(temp == highestValue && (int)currentWinner[0] == 1){             //if hand1's value is equal to the past, but the current winner is hand2. We tie.
                                currentWinner = new Object[]{2, "Tie"};
                            }
                        }
                        else if(HandConstants.SCORINGVALUES[hand1Values[i]] < HandConstants.SCORINGVALUES[hand2Values[j]]){ //if hand 2's current value is greater
                            temp = HandConstants.SCORINGVALUES[hand2Values[j]];
                            if(temp > highestValue){                                    //if hand1's value is greater than our past highest
                                highestValue = temp;
                                highMessage = "High: "+hand2Values[j];
                                currentWinner = new Object[]{1, highMessage};
                            }
                            if(temp == highestValue && (int)currentWinner[0] == 0){     //if hand1's value is equal, but current winner is hand2. We tie.
                                currentWinner = new Object[]{2, "Tie"};
                            }
                        }
                    }
                }
                if((int)currentWinner[0] == 2){         //if we are tied
                    if(highestValue >= 0){              //and our highest value is not -1
                        hand1Values.Remove(CardConstants.VALUES[highestValue]); //remove the value of the highest card in each hand
                        hand2Values.Remove(CardConstants.VALUES[highestValue]); //this is so we can then compare the next highest for ties
                    }
                    highestValue = -1;      //reset
                    temp = -1;
                    checkForHighTies--;     //decrement. We loop for 5 cards worth. After which we would have a tie
                }else{
                    checkForHighTies = -1;  //we do not have a tie, escape the while loop (could also just break here)
                }
                
            }
            return currentWinner;      
        }


        //discovers a winner for tied pair hands
        public Object[] resolvePairTie(CardHand hand1, CardHand hand2){
            string[] pairs = new string[2];
            pairs[0] = isPair(hand1);       //confirm is pair and remember results
            pairs[1] = isPair(hand2);

            Object[] currentWinner;            
            currentWinner = new Object[] {2, "Tie"};

            if(HandConstants.SCORINGVALUES[pairs[0]] > HandConstants.SCORINGVALUES[pairs[1]]){      //based on a pair value's score from 0 to 12, determine which pair is higher
                currentWinner = new Object[]{0, HandConstants.HANDS[1]};
            }
            else if(HandConstants.SCORINGVALUES[pairs[0]] < HandConstants.SCORINGVALUES[pairs[1]]){
                currentWinner = new Object[]{1, HandConstants.HANDS[1]};   
            }
            else{
                currentWinner = resolveHighTie(hand1, hand2);                                       //neither pair is higher. resort to highest card
            }
            return currentWinner;            
        }


        //discovers a winner for tied 2xpair hands
        public Object[] resolveTwoPairTie(CardHand hand1, CardHand hand2){
            List<List<string>> pairs = new List<List<string>>();
            pairs.Add(isTwoPair(hand1));        //confirm is two pairs and remember results
            pairs.Add(isTwoPair(hand2));
            int checkForTies = 2;
            Object[] currentWinner;            
            currentWinner = new Object[] {2, "Tie"};
            
            int highestValue = -1;
            int temp;
            while(checkForTies >= 0){
                for(int i = 0; i<pairs[0].Count; i++){           //for each combination of either player's pairs
                    for(int j = 0; j<pairs[1].Count; j++){
                        if(HandConstants.SCORINGVALUES[pairs[0][i]] > HandConstants.SCORINGVALUES[pairs[1][j]]){    //if player 1 has a higher pair, then player 1 is the current winner
                            temp = HandConstants.SCORINGVALUES[pairs[0][i]];
                            if(temp > highestValue){
                                highestValue = temp;
                                currentWinner = new Object[]{0, HandConstants.HANDS[2]};
                            }
                        }
                        else if(HandConstants.SCORINGVALUES[pairs[0][i]] < HandConstants.SCORINGVALUES[pairs[1][j]]){ //if player 2 has a higher pair, then player 2 is the current winner
                            temp = HandConstants.SCORINGVALUES[pairs[0][i]];
                            if(temp > highestValue){
                                highestValue = temp;
                                currentWinner = new Object[]{1, HandConstants.HANDS[2]};
                            }                        
                        }
                    }
                    
                }
                if((int)currentWinner[0] == 2){             //if we are tied
                    if(highestValue >= 0){                  //and our highest value is not -1
                        pairs[0].Remove(CardConstants.VALUES[highestValue]); //remove the value of the highest pair in each set of pairs
                        pairs[1].Remove(CardConstants.VALUES[highestValue]); //this is so we can then compare the next highest pair for ties
                    }
                    highestValue = -1;      //reset
                    temp = -1;
                    checkForTies--;                         //decrement. We loop for 2 sets worth. After which we would have a tie
                }else{
                    checkForTies = -1;                      //we do not have a tie, escape the while loop (could also just break here)
                }                
            }
            if((int)currentWinner[0] == 2){                     //if neither player has a value greater than the other, differ to high cards.
                currentWinner = resolveHighTie(hand1, hand2);
            }
            return currentWinner;
        }

        public Object[] resolveThreesTie(CardHand hand1, CardHand hand2){
            string[] Threes = new string[2];
            Threes[0] = isThreeOfAKind(hand1);              //confirm is threes and remember results
            Threes[1] = isThreeOfAKind(hand2);

            Object[] currentWinner;            
            currentWinner = new Object[] {2, "Tie"};

            if(HandConstants.SCORINGVALUES[Threes[0]] > HandConstants.SCORINGVALUES[Threes[1]]){        //if the value of player 1's threes hand is greater than player 2, then they win
                currentWinner = new Object[]{0, HandConstants.HANDS[3]};
            }
            else if(HandConstants.SCORINGVALUES[Threes[0]] < HandConstants.SCORINGVALUES[Threes[1]]){
                currentWinner = new Object[]{1, HandConstants.HANDS[3]};   
            }
            if((int)currentWinner[0] == 2){                                                             //if the values match, both players are tied. Difer to high cards for tie breaker
                currentWinner = resolveHighTie(hand1, hand2);                                       
            }
            return currentWinner;
        }     


        //returns the char value of a pair if any. null otherwise
        public string isPair(CardHand hand){    
            List<string> values = new List<string>();
            for(int i = 0; i < 5; i++){
                values.Add(hand.getCard(i).getValue());     //get all values from the hand
            }  
            for(int i = 0; i<5; i++){
                for(int j = 0; j<5 && j!=i; j++){
                    if (values[i] == values[j]){
                        return values[i];                   //if any one value matches, we pair.
                    }
                }
            } 
            
            return null;
                 
            
        }

        //returns the char value of two pairs if any. null otherwise
        public List<string> isTwoPair(CardHand hand){
            List<string> pairs = new List<string>();
            List<string> values = new List<string>();

            for(int i = 0; i < 5; i++){
                values.Add(hand.getCard(i).getValue());
            }
            int j = 4;
            for(int i = 0; i<j; i++){
                if(values[i] == values[i+1]){
                    pairs.Add(values[i]);                   //find the first pair if any. remove the detected cards
                    values.RemoveAt(i);
                    values.RemoveAt(i);
                    j = j-2;
                    break;
                }
                else{
                    pairs.Add(null);
                } 
            }
            for(int i = 0; i<j; i++){
                if(values[i] == values[i+1]){
                    pairs.Add(values[i]);                   //find the next pair
                    break;
                }
                else{
                    pairs.Add(null);
                } 
            }
            if(pairs[0] == null || pairs[1] == null){       //if either one of the two pairs could not be found. We do not have two pairs
                return null;
            }
            return pairs; 
        }


        //returns the char value of a threes hand if any. null otherwise
        public string isThreeOfAKind(CardHand hand){        
            string[] values = new string[5];
            for(int i = 0; i < 5; i++){
                values[i] = hand.getCard(i).getValue();        //get all hand values
            }

            for(int i = 0; i<3; i++){
                if(values[i] == values[i+1] && values[i+1] == values[i+2]){ //see if theres a threes hand amongst side-by-side values
                    return values[i];
                } 
            }

            //These check for threes that are not all side-by-side. So 
            if(values[3] == values[4] && values[4] == values[0]){   //[xbcxx]
                return values[3];
            } 
            if(values[0] == values[1] && values[1] == values[4]){   //[xxcdx]
                return values[1];
            } 
            if(values[0] == values[2] && values[2] == values[4]){   //[xbxdx]
                return values[0];
            }
            if(values[1] == values[3] && values[3] == values[4]){   //[axcxx]
                return values[1];
            } 
            if(values[3] == values[1] && values[1] == values[0]){   //[xxcxe]
                return values[3];
            }
            if(values[0] == values[2] && values[2] == values[3]){   //[xbxxe]
                return values[0];
            }  
            if(values[4] == values[2] && values[2] == values[1]){   //[axxdx]
                return values[4];
            } 
            return null;  //else we do not have threes 
        }


        //simply determines if a flush exists or not. Hands which are both flushes are ranked using the rules for High Card, so we dont care about value
        public bool isFlush(CardHand hand){
            Card card = hand.getCard(0);
            Card newCard;

            for(int i = 1; i<hand.getSize(); i++) {
                newCard = hand.getCard(i);
                if(card.getSuit() == newCard.getSuit()){    //if every single card is the same suit, we will return true
                    card = newCard;
                }
                else{
                    return false;
                }
            }  
            return true;
        }


        //prints a player's hand
        public void printHand(CardHand hand){
            Card card;
            string message = "";
            for(int i = 0; i<hand.getSize(); i++) {         //iterate through the hand's size
                card = hand.getCard(i);                     //get a card
                message = message + card.getValue() + card.getSuit() + " ";  //append card's value and suit

            }
            Console.WriteLine(message);
        }

    } 
} 