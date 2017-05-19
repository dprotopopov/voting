using System;
using System.Linq;

namespace voting
{
    /// <summary>
    ///     ����� ����������� ������� ��������.
    ///     ��������� ��������, ��������� ������������ ����� ������� �� ������ �����.
    /// </summary>
    public class EthalonVoting : VotingAbstract, IDisposable, IVoting
    {
        public EthalonVoting(Log log)
            : base(log)
        {
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     ���������� ������� ��������� - ���������� �������
        /// </summary>
        /// <param name="matrix">
        ///     ���������� ������� ������� �� ����������
        ///     �������� [r,c] ������������� ����� ������� �������� �� ����� r ��������� c
        /// </param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix)
        {
            Log.WriteLine("���������� ������ ����� ������� �������� �� ������ �����");
            var s = matrix[0, 0];
            for (var j = 1; j < matrix.GetLength(1); j++)
            {
                s += matrix[0, j];
            }
            Log.WriteLine("������� ������� ���������� �� ������� �������");
            var l = new double[matrix.GetLength(1)];
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                l[j] = (s - matrix[0, j]) * (s - matrix[0, j]);
                for (var i = 1; i < matrix.GetLength(0); i++)
                {
                    l[j] += matrix[i, j]*matrix[i, j];
                }
                l[j] = Math.Sqrt(l[j]);
            }
            Log.WriteLine("���������� ���������, ���������� � ������� �������");
            var d = l.Min();
            for (var j = 0; j < l.Length; j++)
                if (l[j] == d)
                    return j;
            throw new Exception("����������� ������");
        }
    }
}