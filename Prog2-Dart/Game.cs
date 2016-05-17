using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Dart
{
    public class Game
    {
        private List<Player> _players = new List<Player>();
        private int userOutputInt;
        private bool isNumeric;
        private string userInput;
        private const int MAX_ARROW = 3;
        private bool continueGame = true;

        public Game()
        {
            //Empty constructor
        }
        /// <summary>
        /// Adds player by name
        /// </summary>
        /// <param name="playerName"></param>
        public void AddPlayer(string playerName)
        {
            _players.Add(new Player(playerName));
        }

        private void Parse(string userInput)
        {
            userInput = Console.ReadLine();
            isNumeric = int.TryParse(userInput, out userOutputInt);
        }
        public void PlayGame()
        {
            //Start by getting player names
            Console.WriteLine("******** Welcome to the virtual dart game. Each player has 3 arrows, use wisely! ********");
            Console.WriteLine("Possible numbers to aim: 20,1,18,4,13,6,10,15,2,17,3,19,7,16,8,11,14,9,12,5");

            Console.WriteLine("How many players?");
            Parse(userInput);

            //Input validation
            while (!isNumeric)
            {
                Console.WriteLine("Dart game needs numbers. Try again: ");
                Parse(userInput);
            }

            //Get player names
            for (int i = 0; i < userOutputInt; i++)
            {
                Console.WriteLine($"Name of Player {i + 1}:");
                _players.Add(new Player(Console.ReadLine()));
            }

            int[] score = new int[MAX_ARROW]; //Temporary score holding array

            //This is a controller for deciding whether to continue or stop the game
            while (continueGame)
            {
                //Iterate each player inside the list
                foreach (var player in _players)
                {
                    //Give MAX_ARROW rounds to each player, 3 in this case
                    for (int i = 0; i < MAX_ARROW; i++)
                    {
                        Console.WriteLine($"Player: {player.Name} - Throw your arrow #{i + 1}! Type your aimed number. Type R for random: ");
                        string input = Console.ReadLine();

                        if (input.Equals("R") || input.Equals("r")) //Give random
                        {
                            score[i] = Dartboard.HitPoint();
                        }
                        else
                        {
                            isNumeric = int.TryParse(input, out userOutputInt);
                            //Input validation
                            while (!isNumeric)
                            {
                                Console.WriteLine("Dart game needs numbers. Try again: ");
                                if (input.Equals("R") || input.Equals("r")) //Give random
                                {
                                    score[i] = Dartboard.HitPoint();
                                }
                                else
                                {
                                    Parse(input);                                 
                                }
                            }
                            //Check if the user input is available on the dartboard
                            if (Dartboard.IsNumberAvailable(userOutputInt))
                            {
                                score[i] = Dartboard.HitPoint(userOutputInt); //Give based on a probability   
                            }
                            else
                            {
                                Console.WriteLine("This number is not available on the dartboard. Try again: ");
                                Parse(input);
                                while (Dartboard.IsNumberAvailable(userOutputInt))
                                {
                                    Parse(input); 
                                }
                            }
                        }

                    }

                    //Add scores to user's turn list
                    player.AddTurn(score[0], score[1], score[2]);


                    //Control total points and stop the game if someone is over 301 pts
                    if (player.CalculatePoints() > 301)
                    {
                        Console.WriteLine($"{player.Name} won the game with {player.CalculatePoints()} points");
                        player.PrintTurns();
                        continueGame = false;
                        return;
                    }

                }

            }
        }
    }
}
