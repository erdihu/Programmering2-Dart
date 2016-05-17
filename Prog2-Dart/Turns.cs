using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Dart
{
    class Turns
    {
        //private int _remainingArrows = 3;
        private int _roundScore = 0;

        public Turns(int roundScore)
        {
            _roundScore = roundScore;
        }
        public int GetScore()
        {
            return _roundScore;
        }
    }
}
