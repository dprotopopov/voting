using System.Collections.Generic;

namespace voting
{
    public abstract class ElectionAbstract
    {
        /// <summary>
        /// Список кандидатов
        /// </summary>
        public IEnumerable<string> Candidates { get; set; }

        /// <summary>
        /// Квадратная матрица голосов за кандидатов
        /// Элементу [r,c] соответствует число голосов отданных за место r кандидату c
        /// </summary>
        public int[,] Matrix { get; set; }
    }
}