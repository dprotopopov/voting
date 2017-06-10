using System;
using System.Linq;

namespace voting
{
    public class MinSumVoting : VotingAbstract, IDisposable, IVoting
    {
        /// <summary>
        /// Нахождение индекса кандидата - победителя выборов
        /// </summary>
        /// <param name="matrix">Квадратная матрица голосов за кандидатов
        ///     Элементу [r,c] соответствует число голосов отданных за место r кандидату c</param>
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface)
        {
            Log.WriteLine("Расчёт значений сумм мест для кандидатов");
            var s = new int[matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    s[j] += (i + 1)*matrix[i, j];
                }
            }
            Log.WriteLine("Нахождение минимальной суммы мест");
            var t = s.Min();
            Log.WriteLine("Нахождение кандидата, набравшего минимальную сумму мест");
            for (var j = 0; j < s.Length; j++)
                if (s[j] == t)
                    return j;
            throw new Exception("Неизвестная ошибка");
        }

        public void Dispose()
        {
        }

        public MinSumVoting(Log log)
            : base(log)
        {
        }
    }
}