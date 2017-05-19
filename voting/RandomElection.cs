using System;
using System.Collections.Generic;
using System.Linq;

namespace voting
{
    // Класс для генерации итогов голосования
    public class RandomElection : ElectionAbstract, IDisposable
    {
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

        public void Vote(IEnumerable<string> candidates)
        {
            var count = candidates.Count();
            Candidates = candidates;
            var matrix = new int[count, count];
            for (var i = 0; i < Total; i++)
            {
                // Формируем случайный бюллетень
                var ballot = Enumerable.Range(0, count).ToArray();
                for (var t = 0; t < Random.Next(count + count, count * count); t++)
                {
                    var a = Random.Next()%count;
                    var b = Random.Next()%count;
                    var x = ballot[a];
                    ballot[a] = ballot[b];
                    ballot[b] = x;
                }
                // Подсчитываем голоса
                for (var r = 0; r < count; r++)
                {
                    matrix[r, ballot[r]]++;
                }
            }
            Matrix = matrix;
        }
    }
}