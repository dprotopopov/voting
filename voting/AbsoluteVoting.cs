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
        /// Ёлементу [r,c] соответствует число голосов отданных за место r кандидату c</param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix)
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
            Log.WriteLine("Ќахождениечисла саксимального чисоа голосов отданных за второго кандидата");
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
            using (var secondRound = new RandomElection(total, Program.m_random, Log))
            {
                var candidates = Enumerable.Range(0, 2).Select(x => string.Format("Bart-{0}", x)).ToList();
                secondRound.Vote(candidates);
                using (var relative = new RelativeVoting(Log))
                {
                    var id = relative.SelectWinner(secondRound.Matrix);
                    return id == 0 ? first : second;
                }
            }
        }
        public int SelectWinnerManual(List<string> canlidates, int[,] matrix)
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
            {
                Log.WriteLine("ѕервый кандидат набрал абсолютное большинство голосов");
                return first;               
            }
            Log.WriteLine("Ќахождение максимального числа голосов отданных за второе место");
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
            Console.WriteLine("ѕроведение второго тура голосовани€");
            Console.Write("”кажите число голосов за кандидата {0}:", canlidates[first]);
            var voices1 = int.Parse(Console.ReadLine());
            Console.Write("”кажите число голосов за кандидата {0}:", canlidates[second]);
            var voices2 = int.Parse(Console.ReadLine());
            Log.WriteLine(string.Format(" андидат {0} набрал {1} голосов", canlidates[first], voices1));
            Log.WriteLine(string.Format(" андидат {0} набрал {1} голосов", canlidates[second], voices2));

            return (voices1 >= voices2) ? first : second;
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