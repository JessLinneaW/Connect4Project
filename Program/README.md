# Connect4Project


The completed game program is contained in the Connect 4 Game.cs file directly above this ReadMe.md file, in the main Program file. This file contains detailed comments for each class and method.

The Individual Classes file within the main Program file contains individual files for each class in the Connect 4 Game program. These files do not contain notes.



Steps in Gameplay:

1. User enters how many named players they would like participating in the game.
   The user is then prompted to provide a name for each of their named players.
   For any remaining players of the 2 that are necessary to play the game, the program automatically creates unnamed players.
   All players are initiated with a team symbol, team colour, and the unnamed players are provided with their player number instead of a name.

2. A new connect 4 gameboard is created and the first player is prompted to select a column between 1-7.
   After player 1 has selected their column, their disk will be placed in the lowest available row of that column.
   Gameplay is now transfered to player 2 and they will repeat the same process.

3. Gameplay continues until either a winner is found, or the gameboard runs out of available spaces and a draw is declared.

4. The user then has the option to play again or exit the program.
