using System;
using System.Collections.Generic;
using System.Linq;

namespace voting
{
    /// <summary>
    /// ����� ��� �������� ����������� ������� �� �����
    /// </summary>
    public class OnlineElection : ElectionAbstract, IDisposable, ISecondRound
    {
        public List<int> Votes { get; private set; } // ������ ���������� ������� ��������
        public List<List<int>> Ballots { get; private set; } // ������ ���������� �������� (������������)
        /// <summary>
        /// ����������� �������� ����������� ������� �� �����
        /// </summary>
        /// <param name="reader"></param>
        public OnlineElection()
        {
            Console.Write("������� ����� ����������:");
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine("������� ������ ����� ������� �������� (������ ����� ���������� ����������)");
            var line = Console.ReadLine();
            if (line == null) throw new Exception("������������ ������");
            var votes = line.Split('\t').Select(int.Parse).ToList();
            if (!votes.Any()) throw new Exception("������������ ������");
            Votes = votes;

            var rows = new List<IEnumerable<string>>();

            Console.WriteLine("������� ������ ������� �������� �� ���������� (������ ��� ���������� ����������)");
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0} �����:\t", i);
                line = Console.ReadLine();
                if (line == null) throw new Exception("������������ ������");
                var row = line.Split('\t');
                if (row.Count() != votes.Count()) throw new Exception("������������ ������");
                rows.Add(row);
            }
            if (!rows.Any()) throw new Exception("������������ ������");

            var candidates = rows.SelectMany(x => x).Distinct().ToList();
            candidates.Sort();
            Candidates = candidates;
            if (rows.Count != candidates.Count) throw new Exception("������������ ������");

            var ballots = new List<List<int>>();
            for (var i = 0; i < votes.Count; i++) ballots.Add(new List<int>());

            var matrix = new int[rows.Count, candidates.Count];
            int r = 0;
            foreach (var row in rows)
            {
                var index = 0;
                foreach (var c in row.Select(candidate => candidates.IndexOf(candidate)))
                {
                    ballots[index].Add(c);
                    matrix[r, c] += votes[index++];
                }
                r++;
            }
            Ballots = ballots;
            Matrix = matrix;
        }

        public int[,] SecondRoundMatrix(int first, int second)
        {
            var matrix2 = new int[2, 2];
            var votes = Votes;
            var ballots = Ballots;
            for (var i = 0; i < votes.Count; i++)
            {
                var index1 = ballots[i].IndexOf(first);
                var index2 = ballots[i].IndexOf(second);
                if (index1 < index2)
                {
                    matrix2[0, 0] += votes[i];
                    matrix2[1, 1] += votes[i];
                }
                else
                {
                    matrix2[0, 1] += votes[i];
                    matrix2[1, 0] += votes[i];
                }
            }
            return matrix2;
        }
        public void Dispose()
        {
        }
    }
}