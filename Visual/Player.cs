using System.Collections.Generic;
using System.Linq;

namespace Visual
{
    public class Player
    {
        public string Name { get; }
        private Turns _t1, _t2, _t3;
        private readonly List<Turns> _turns;

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
            _t1 = new Turns(firstRound);
            _t2 = new Turns(secondRound);
            _t3 = new Turns(thirdRound);

            _turns.Add(_t1);
            _turns.Add(_t2);
            _turns.Add(_t3);

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
        public string PrintTurns()
        {
            string s;
            int round = 1;
            s = $"Scores for {Name}: ";

            for (int i = 0; i < _turns.Count; i++)
            {
                if (i % 3 == 0)
                {
                    s += "\n";
                    s += $"Round {round}: ";
                    round++;
                }
                s+= $"{_turns[i].GetScore()} ";
            }
            return s;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
