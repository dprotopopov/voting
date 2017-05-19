using System;
using System.IO;

namespace voting
{
    /// <summary>
    /// Класс ведения лога в текстовом файле
    /// </summary>
    public class Log: IDisposable
    {
        protected TextWriter Writer { get; private set; }

        public Log(TextWriter writer)
        {
            Writer = writer;
        }

        public void WriteLine(string s)
        {
            Writer.WriteLine("{0}\t{1}",DateTime.Now,s);
        }
        public void Dispose()
        {
        }
    }
}