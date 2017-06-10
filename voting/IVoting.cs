namespace voting
{
    public interface IVoting
    {
        /// <summary>
        /// Нахождение индекса кандидата - победителя выборов
        /// </summary>
        /// <param name="matrix">Квадратная матрица голосов за кандидатов
        ///     Элементу [r,c] соответствует число голосов отданных за место r кандидату c</param>
        /// <param name="secondRoundInterface"></param>
        /// <returns></returns>
        int SelectWinner(int[,] matrix, ISecondRound secondRoundInterface);
    }
}