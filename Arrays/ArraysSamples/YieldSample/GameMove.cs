using System;
using System.Collections;

namespace Wrox.ProCSharp.Arrays
{
    public class GameMoves
    {
        private IEnumerator _cross;
        private IEnumerator _circle;

        public GameMoves()
        {
            _cross = Cross();
            _circle = Circle();
        }

        private int _move = 0;
        const int MaxMoves = 9;

        public IEnumerator Cross()
        {
            while (true)
            {
                Console.WriteLine($"Cross, move {_move}");
                if (++_move >= MaxMoves)
                    yield break;
                yield return _circle;
            }
        }

        public IEnumerator Circle()
        {
            while (true)
            {
                Console.WriteLine($"Circle, move {_move}");
                if (++_move >= MaxMoves)
                    yield break;
                yield return _cross;
            }
        }
    }
}