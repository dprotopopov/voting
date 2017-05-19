using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace voting
{
    /// <summary>
    /// ����� ��� �������� ����������� ������� �� �����
    /// </summary>
    public class StoredElection : ElectionAbstract, IDisposable
    {
        /// <summary>
        /// ����������� �������� ����������� ������� �� �����
        /// </summary>
        /// <param name="reader"></param>
        public StoredElection(TextReader reader)
        {
            var line = reader.ReadLine();
            if (line == null) throw new Exception("������������ ������");
            var votes = line.Split('\t').Select(int.Parse).ToList();
            if (!votes.Any()) throw new Exception("������������ ������");
            var rows = new List<IEnumerable<string>>();

            while ((line = reader.ReadLine())!= null)
            {
                var row = line.Split('\t');
                if (row.Count() != votes.Count()) throw new Exception("������������ ������");
                rows.Add(row);
            }
            if (!rows.Any()) throw new Exception("������������ ������");

            var candidates = rows.SelectMany(x => x).Distinct().ToList();
            candidates.Sort();
            Candidates = candidates;
            if (rows.Count != candidates.Count) throw new Exception("������������ ������");

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