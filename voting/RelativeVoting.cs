using System;

namespace voting
{
    /// <summary>
    ///     ����� �������������� �����������.
    ///     ��������� ��������, ��������� ������������ ����� ������� �� ������ �����.
    /// </summary>
    public class RelativeVoting : VotingAbstract, IDisposable, IVoting
    {
        public RelativeVoting(Log log)
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
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        public int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface)
        {
            Log.WriteLine("���������� ������������� ����� ������� �������� �� ������ �����");
            var s = matrix[0, 0];
            for (var j = 1; j < matrix.GetLength(1); j++)
            {
                s = Math.Max(s, matrix[0, j]);
            }
            Log.WriteLine("���������� ���������, ���������� ������������ ����� �������");
            for (var j = 0; j < matrix.GetLength(1); j++)
                if (matrix[0, j] == s)
                    return j;
            throw new Exception("����������� ������");
        }
    }
}