namespace Player
{
    public interface IEntityInputSource
    {
        public float Direction { get; }
        public bool Jump { get; }

        public void ResetOneTimeActions();
    }
}