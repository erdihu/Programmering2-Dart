using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Dart
{
    class Player
    {
        public string Name { get; private set; }
        private Turns t1, t2, t3;
        private List<Turns> _turns;

        /// <summary>
        /// Default cosntructor of Player. Sets player name and creates Turns list for the player.
        /// </summary>
        /// <param name="playerName"></param>
        public Player(string playerName)
        {
            Name = playerName;
            _turns = new List<Turns>();
        }

        /// <summary>
        /// Gets all the points of user and add to the list
        /// </summary>
        /// <param name="firstRound"></param>
        /// <param name="secondRound"></param>
        /// <param name="thirdRound"></param>
        public void AddTurn(int firstRound, int secondRound, int thirdRound)
        {
            t1 = new Turns(firstRound);
            t2 = new Turns(secondRound);
            t3 = new Turns(thirdRound);

            _turns.Add(t1);
            _turns.Add(t2);
            _turns.Add(t3);

        }

        /// <summary>
        /// Returns total points of a player
        /// </summary>
        /// <returns></returns>
        public int CalculatePoints()
        {
            return _turns.Sum(s => s.GetScore());
        }

        /// <summary>
        /// Print all scores of a player
        /// </summary>
        public void PrintTurns()
        {
            int round = 1;
            Console.WriteLine($"Scores for {Name}: ");

            for (int i = 0; i < _turns.Count; i++)
            {
                if (i % 3 == 0)
                {
                    Console.Write("\n");
                    Console.WriteLine($"Round {round}: ");
                    round++;
                }
                Console.Write($"{_turns[i].GetScore()} ");
            }
            //foreach (var score in _turns)
            //{
            //    while(_turns.)
            //    Console.Write($"{score.GetScore()} ");
            //}
        }
    }
}
