namespace voting
{
    public abstract class VotingAbstract
    {
        public VotingAbstract(Log log)
        {
            Log = log;
        }

        protected Log Log { get; private set; }
    }
}