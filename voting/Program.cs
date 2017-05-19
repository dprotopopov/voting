using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace voting
{
    internal static class Program
    {
        public static readonly Random m_random = new Random(); // Генератор псевдослучайных чисео

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            var numberOfTests = 100; // Количество тестов
            var logFileName = "voting.log"; // Имя лог-файла
            var minNumberOfCandidates = 2; // Минимальное количество кандидатов
            var maxNumberOfCandidates = 10; // Максимальное количество кандидатов
            var minNumberOfVoters = 100; // Минимальное количество выборщиков
            var maxNumberOfVoters = 1000; // Максимальное количество выборщиков

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == "--log") logFileName = args[++i];
                else if (args[i] == "--tests") numberOfTests = int.Parse(args[++i]);
                else if (args[i] == "--candidates")
                {
                    minNumberOfCandidates = int.Parse(args[++i]);
                    maxNumberOfCandidates = int.Parse(args[++i]);
                }
                else if (args[i] == "--voters")
                {
                    minNumberOfVoters = int.Parse(args[++i]);
                    maxNumberOfVoters = int.Parse(args[++i]);
                }
            }

            using (var writer = File.CreateText(logFileName)) // Создание файла и открытие потока записи
            using (var log = new Log(writer)) // Создание объекта логгирования
            {
                var result = new int[4, 4]; // Количество совпадений итогов голосований

                for (var t = 0; t < numberOfTests; t++)
                {
                    using (var firstRound = new RandomElection(m_random.Next(minNumberOfVoters, maxNumberOfVoters), m_random, log))
                    {
                        var candidates =
                            Enumerable.Range(0, m_random.Next(minNumberOfCandidates, maxNumberOfCandidates))
                                .Select(x => string.Format("Homer-{0}", x))
                                .ToList();
                        log.WriteLine(string.Format("Выборы из {0} кандидатов", candidates.Count));
                        firstRound.Vote(candidates);

                        // Вывод матрицы голосов
                        var sb = new StringBuilder();
                        for (var i = 0; i < candidates.Count; i++)
                        {
                            for (var j = 0; j < candidates.Count; j++)
                            {
                                sb.Append(firstRound.Matrix[i, j]);
                                sb.Append(',');
                            }
                            sb.Append(Environment.NewLine);
                        }
                        log.WriteLine(string.Format("Матрица голосования (строка=место,колонка=кандидат):{0}{1}", Environment.NewLine, sb.ToString()));

                        var list = new List<string>(); // Победители по различным методам

                        log.WriteLine("Нахождение победителя методом относительного большинства");
                        using (var voting = new RelativeVoting(log))
                            list.Add(candidates[voting.SelectWinner(firstRound.Matrix)]);
                        log.WriteLine("Нахождение победителя методом абсолютного большинства");
                        using (var voting = new AbsoluteVoting(log))
                            list.Add(candidates[voting.SelectWinner(firstRound.Matrix)]);
                        log.WriteLine("Нахождение победителя методом минимальной суммы мест");
                        using (var voting = new MinSumVoting(log))
                            list.Add(candidates[voting.SelectWinner(firstRound.Matrix)]);
                        using (var voting = new EthalonVoting(log))
                            list.Add(candidates[voting.SelectWinner(firstRound.Matrix)]);

                        log.WriteLine("Победитель:");
                        log.WriteLine(string.Format("Метод относительного большинства: {0}", list[0]));
                        log.WriteLine(string.Format("Метод абсолютного большинства   : {0}", list[1]));
                        log.WriteLine(string.Format("Метод минимальной суммы мест    : {0}", list[2]));
                        log.WriteLine(string.Format("Метод эталонов                  : {0}", list[3]));

                        // Накопление статистических итогов
                        for (var i = 0; i < list.Count; i++)
                        {
                            for (var j = 0; j < list.Count; j++)
                            {
                                if (list[i] == list[j]) result[i, j]++;
                            }
                        }
                    }
                }

                var p = new double[4, 4]; // Процент совпадения итогов голосования
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        p[i, j] = result[i, j]*100.0/numberOfTests;
                    }
                }

                log.WriteLine("Разультаты:");
                log.WriteLine(string.Format("Совпадение Относительного большинства и Абсолютного большинства : {0}%", p[0, 1]));
                log.WriteLine(string.Format("Совпадение Относительного большинства и Минимальной суммы мест  : {0}%", p[0, 2]));
                log.WriteLine(string.Format("Совпадение Абсолютного большинства и Минимальной суммы мест     : {0}%", p[1, 2]));
                log.WriteLine(string.Format("Совпадение Относительного большинства и Эталонов                : {0}%", p[0, 3]));
                log.WriteLine(string.Format("Совпадение Абсолютного большинства и Эталонов                   : {0}%", p[1, 3]));
                log.WriteLine(string.Format("Совпадение Минимальной суммы мест и Эталонов                    : {0}%", p[2, 3]));
            }
        }
    }
}