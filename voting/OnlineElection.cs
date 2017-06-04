using System;
using System.Collections.Generic;
using System.Linq;

namespace voting
{
    /// <summary>
    /// Класс для загрузки результатов выборов из файла
    /// </summary>
    public class OnlineElection : ElectionAbstract, IDisposable
    {
        /// <summary>
        /// Конструктор загрузки результатов выборов из файла
        /// </summary>
        /// <param name="reader"></param>
        public OnlineElection()
        {
            Console.Write("Укажите число кандидатов:");
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите строку числа голосов коалиций (строка чисел разделённых табуляцией)");
            var line = Console.ReadLine();
            if (line == null) throw new Exception("Неправильные данные");
            var votes = line.Split('\t').Select(int.Parse).ToList();
            if (!votes.Any()) throw new Exception("Неправильные данные");
            var rows = new List<IEnumerable<string>>();

            Console.WriteLine("Введите строки голосов коалиций за кандидатов (строки имён разделённых табуляцией)");
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0} место:\t", i);
                line = Console.ReadLine();
                if (line == null) throw new Exception("Неправильные данные");
                var row = line.Split('\t');
                if (row.Count() != votes.Count()) throw new Exception("Неправильные данные");
                rows.Add(row);
            }
            if (!rows.Any()) throw new Exception("Неправильные данные");

            var candidates = rows.SelectMany(x => x).Distinct().ToList();
            candidates.Sort();
            Candidates = candidates;
            if (rows.Count != candidates.Count) throw new Exception("Неправильные данные");

            var matrix = new int[rows.Count, candidates.Count];
            int r = 0;
            foreach (var row in rows)
            {
                var index = 0;
                foreach (var c in row.Select(candidate => candidates.IndexOf(candidate)))
                {
                    matrix[r, c] += votes[index++];
                }
                r++;
            }
            Matrix = matrix;
        }

        public void Dispose()
        {
        }
    }
}