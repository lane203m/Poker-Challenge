/*
PokerGame 
By: Mason Lane
06-03-2021
Runs a user-determined amount of rounds for a poker game. 
*/

using System; 
using pokerHandSpace;
  
namespace pokerHands { 
    class PokerGame { 
          
        // Main Method 
        public static int Main() { 
            PokerHands pokerHands = new PokerHands();  //start game with dealer, deck, hands
            Console.WriteLine("Enter how many rounds: ");
            
            int rounds; bool parseResult = int.TryParse(Console.ReadLine(), out rounds); //rounds to play
            
            if( parseResult == false || rounds <= 0){                   //user did not input an int
                Console.WriteLine("[Bad input, please restart]");   
                Console.ReadKey(); 
                return 1; 
            }

            for(int i = 0; i<rounds; i++){
                pokerHands.playRound(1);   //Play a round with one deck. Note, some casinos play with multiple decks. I attempted to work that in.
                Console.WriteLine();
            }

            Console.WriteLine("Rounds Finished, Press Any key");
            Console.ReadKey(); 
            return 0;
        } 
    } 
} 