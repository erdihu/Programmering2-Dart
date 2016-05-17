using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Dart
{
    public static class Dartboard
    {
        private static Random _r = new Random();
        private static readonly int[] _scoreBoard = {20,1,18,4,13,6,10,15,2,17,3,19,7,16,8,11,14,9,12,5};

        /// <summary>
        /// Gives random points which are available in _scoreBoard array
        /// </summary>
        /// <returns></returns>
        public static int HitPoint()
        {
            return GiveRandom();
        }

        /// <summary>
        /// Determines hitpoints based on probability given in software requirements
        /// </summary>
        /// <param name="playerAim"></param>
        /// <returns></returns>
        public static int HitPoint(int playerAim)
        {
            int random = _r.Next(0, 101);
            int index = Array.IndexOf(_scoreBoard, playerAim);

            if (random <= 60)
            {
                return playerAim;
            }
            else if (random > 60 && random <= 75) //Must handle the roundness of the dart board. Since there is no 0-1 element in the array 
            {
                if (index == 0)
                    return _scoreBoard[_scoreBoard.Length-1];
                return _scoreBoard[index - 1];
            }
            else if (random > 75 && random <= 90)
            {
                if (index == _scoreBoard.Length - 1)
                    return _scoreBoard[0];
                else
                {
                    return _scoreBoard[index + 1];
                }
            }
            else if (random > 90 && random <= 95)
            {
                return GiveRandom();
            }
            else
            {
                {
                    return 0; //Miss
                }
            }
        }

        /// <summary>
        /// Select random score from the score array
        /// </summary>
        /// <returns></returns>
        private static int GiveRandom()
        {
            return _scoreBoard[_r.Next(0, 20)];
        }

        //Helper method to check if the selected number is playable
        public static bool IsNumberAvailable(int number)
        {
            if (Array.IndexOf(_scoreBoard, number) != -1)
                return true;
            else
            {
                return false;
            }
            
        }
    }
}
