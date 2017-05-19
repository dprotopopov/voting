namespace voting
{
    public interface IVoting
    {
        /// <summary>
        /// ���������� ������� ��������� - ���������� �������
        /// </summary>
        /// <param name="matrix">���������� ������� ������� �� ����������
        /// �������� [r,c] ������������� ����� ������� �������� �� ����� r ��������� c</param>
        /// <returns></returns>
        int SelectWinner(int[,] matrix);
    }
}