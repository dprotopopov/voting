using System;
using System.Collections.Generic;
using System.Linq;

namespace voting
{
    /// <summary>
    ///  ласс абсолютного голосовани€.
    /// ѕобеждает кандидат, набравший максимальное число голосов за первое место.
    /// </summary>
    public class AbsoluteVoting : VotingAbstract, IDisposable, IVoting
    {
        /// <summary>
        /// Ќахождение индекса кандидата - победител€ выборов
        /// </summary>
        /// <param name="matrix"> вадратна€ матрица голосов за кандидатов
        ///     Ёлементу [r,c] соответствует число голосов отданных за место r кандидату c</param>
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface)
        {
            Log.WriteLine("Ќахождение максимального числа голосов отданных за первое место");
            var s = matrix[0, 0];
            var total = matrix[0, 0];
            for (var j = 1; j < matrix.GetLength(1); j++)
            {
                s = Math.Max(s, matrix[0, j]);
                total += matrix[0, j];
            }
            Log.WriteLine("Ќахождение первого кандидата, набравшего максимальное число голосов");
            var first = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (matrix[0, j] == s)
                    first = j;
            if (s + s > total)
                return first;
            Log.WriteLine("Ќахождениечисла максимального числа голосов отданных за второго кандидата");
            s = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (j != first)
                {
                    s = Math.Max(s, matrix[0, j]);
                }
            Log.WriteLine("Ќахождение второго кандидата, набравшего максимальное число голосов");
            var second = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (j != first)
                    if (matrix[0, j] == s)
                        second = j;
            Log.WriteLine("ѕроведение второго тура голосовани€");

            var matrix2 = secondRoundInterface.SecondRoundMatrix(first, second);
            return (matrix2[0, 0] > matrix2[0, 1]) ? first : second;
        }
        public void Dispose()
        {
        }

        public AbsoluteVoting(Log log)
            : base(log)
        {
        }
    }
}