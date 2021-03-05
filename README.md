# Poker Hands

1. A poker deck contains 52 cards.  The deck is shuffled before play begins.

2. Each card has a suit which is one of 4 possible values: clubs, diamonds, hearts, or spades (denoted `C`, `D`, `H`, and `S` in the input data).

3. Each card also has a value which is one of 13 possible values: 2, 3, 4, 5, 6, 7, 8, 9, 10, jack, queen, king, ace (denoted `2`, `3`, `4`, `5`, `6`, `7`, `8`, `9`, `10`, `J`, `Q`, `K`, `A` in the input data).

4. For scoring purposes, the suits are unordered while the values are ordered as given above; 2 is lowest value, ace is highest value.

5. A poker hand consists of 5 cards dealt from the deck; hands are ranked by the following partial order from lowest to highest:

    - **HIGH CARD**...  Hands which do not fit any higher category are ranked by the value of their highest card.  If the highest cards have the same value, the hands are ranked by the next highest, and so on.
    - **PAIR**...  2 of the 5 cards in the hand have the same value.  Hands which both contain a pair are ranked by the value of the cards forming the pair.  If these values are the same, the hands are ranked by the values of the cards not forming the pair, in decreasing order.
    - **TWO PAIRS**...  The hand contains 2 different pairs.  Hands which both contain 2 pairs are ranked by the value of their highest pair.  Hands with the same highest pair are ranked by the value of their other pair.  If these values are the same the hands are ranked by the value of the remaining card.
    - **THREE OF A KIND**...  Three of the cards in the hand have the same value.  Hands which both contain three of a kind are ranked by the value of the 3 cards.
    - **FLUSH**...  Hand contains 5 cards of the same suit.  Hands which are both flushes are ranked using the rules for High Card.

6. Your task is to rank pairs of poker hands and to indicate which, if either, has a higher rank.  Examples follow (`input => output`):

   - `BLACK: 2H 3D 5S 9C KD WHITE: 2C 3H 4S 8C AH` => `WHITE WINS - high card: Ace`
   - `BLACK: 2H 4S 4C 3D 4H WHITE: 2S 8S AS QS 3S` => `WHITE WINS - flush`
   - `BLACK: 2H 3D 5S 9C KD WHITE: 2C 3H 4S 8C KH` => `BLACK WINS - high card: 9`
   - `BLACK: 2H 3D 5S 9C KD WHITE: 2D 3H 5C 9S KH` => `TIE`

7. Host your source code in a public Git repository.  Use C# to implement the algorithm.  You are encouraged to use [SOLID design principles](https://en.wikipedia.org/wiki/SOLID) and write automated tests.
