using System;
using System.Collections.Generic;
using System.Linq;

namespace voting
{
    // Класс для генерации итогов голосования
    public interface ISecondRound
    {
        int[,] SecondRoundMatrix(int first, int second);
    }

    public class RandomElection : ElectionAbstract, IDisposable, ISecondRound
    {
        public List<List<int>> Ballots { get; private set; } // Список бюллетеней
        public RandomElection(int total, Random random, Log log)
        {
            Total = total;
            Random = random;
            Log = log;
        }

        private int Total { get; set; }
        private Random Random { get; set; }
        private Log Log { get; set; }
        public void Dispose()
        {
        }

        public void Vote(List<string> candidates)
        {
            var count = candidates.Count();
            Candidates = candidates;
            var ballots = new List<List<int>>();
            var matrix = new int[count, count];
            for (var i = 0; i < Total; i++)
            {
                // Формируем случайный бюллетень
                var ballot = Enumerable.Range(0, count).ToList();
                for (var t = 0; t < Random.Next(count + count, count * count); t++)
                {
                    var a = Random.Next()%count;
                    var b = Random.Next()%count;
                    var x = ballot[a];
                    ballot[a] = ballot[b];
                    ballot[b] = x;
                }
                ballots.Add(ballot);
                // Подсчитываем голоса
                for (var r = 0; r < count; r++)
                {
                    matrix[r, ballot[r]]++;
                }
            }
            Ballots = ballots;
            Matrix = matrix;
        }
        public int[,] SecondRoundMatrix(int first, int second)
        {
            var matrix2 = new int[2, 2];
            var ballots = Ballots;
            for (var i = 0; i < ballots.Count; i++)
            {
                var index1 = ballots[i].IndexOf(first);
                var index2 = ballots[i].IndexOf(second);
                if (index1 < index2)
                {
                    matrix2[0, 0]++;
                    matrix2[1, 1]++;
                }
                else
                {
                    matrix2[0, 1]++;
                    matrix2[1, 0]++;
                }
            }
            return matrix2;
        }
    }
}