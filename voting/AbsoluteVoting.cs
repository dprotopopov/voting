using System;
using System.Collections.Generic;
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
        ///     �������� [r,c] ������������� ����� ������� �������� �� ����� r ��������� c</param>
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface)
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

            var matrix2 = secondRoundInterface.SecondRoundMatrix(first, second);
            return (matrix2[0, 0] > matrix2[0, 1]) ? first : second;
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