using System;
using System.Linq;

namespace voting
{
    public class MinSumVoting : VotingAbstract, IDisposable, IVoting
    {
        /// <summary>
        /// ���������� ������� ��������� - ���������� �������
        /// </summary>
        /// <param name="matrix">���������� ������� ������� �� ����������
        ///     �������� [r,c] ������������� ����� ������� �������� �� ����� r ��������� c</param>
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface)
        {
            Log.WriteLine("������ �������� ���� ���� ��� ����������");
            var s = new int[matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    s[j] += (i + 1)*matrix[i, j];
                }
            }
            Log.WriteLine("���������� ����������� ����� ����");
            var t = s.Min();
            Log.WriteLine("���������� ���������, ���������� ����������� ����� ����");
            for (var j = 0; j < s.Length; j++)
                if (s[j] == t)
                    return j;
            throw new Exception("����������� ������");
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