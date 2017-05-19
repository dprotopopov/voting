using System;
using System.Linq;

namespace voting
{
    /// <summary>
    /// ����� ����������� �����������.
    /// ��������� ��������, ��������� ������������ ����� ������� �� ������ �����.
    /// </summary>
    public class AbsoluteVoting : VotingAbstract, IDisposable, IVoting
    {
        /// <summary>
        /// ���������� ������� ��������� - ���������� �������
        /// </summary>
        /// <param name="matrix">���������� ������� ������� �� ����������
        /// �������� [r,c] ������������� ����� ������� �������� �� ����� r ��������� c</param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix)
        {
            Log.WriteLine("���������� ������������� ����� ������� �������� �� ������ �����");
            var s = matrix[0, 0];
            var total = matrix[0, 0];
            for (var j = 1; j < matrix.GetLength(1); j++)
            {
                s = Math.Max(s, matrix[0, j]);
                total += matrix[0, j];
            }
            Log.WriteLine("���������� ������� ���������, ���������� ������������ ����� �������");
            var first = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (matrix[0, j] == s)
                    first = j;
            if (s + s > total)
                return first;
            Log.WriteLine("��������������� ������������� ����� ������� �������� �� ������� ���������");
            s = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (j != first)
                {
                    s = Math.Max(s, matrix[0, j]);
                }
            Log.WriteLine("���������� ������� ���������, ���������� ������������ ����� �������");
            var second = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (j != first)
                    if (matrix[0, j] == s)
                        second = j;
            Log.WriteLine("���������� ������� ���� �����������");
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

        public void Dispose()
        {
        }

        public AbsoluteVoting(Log log)
            : base(log)
        {
        }
    }
}