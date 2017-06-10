using System;

namespace voting
{
    /// <summary>
    ///      ласс относительного голосовани€.
    ///     ѕобеждает кандидат, набравший максимальное число голосов за первое место.
    /// </summary>
    public class RelativeVoting : VotingAbstract, IDisposable, IVoting
    {
        public RelativeVoting(Log log)
            : base(log)
        {
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     Ќахождение индекса кандидата - победител€ выборов
        /// </summary>
        /// <param name="matrix">
        ///      вадратна€ матрица голосов за кандидатов
        ///     Ёлементу [r,c] соответствует число голосов отданных за место r кандидату c
        /// </param>
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface)
        {
            Log.WriteLine("Ќахождение максимального числа голосов отданных за первое место");
            var s = matrix[0, 0];
            for (var j = 1; j < matrix.GetLength(1); j++)
            {
                s = Math.Max(s, matrix[0, j]);
            }
            Log.WriteLine("Ќахождение кандидата, набравшего максимальное число голосов");
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (matrix[0, j] == s)
                    return j;
            throw new Exception("Ќеизвестна€ ошибка");
        }
    }
}