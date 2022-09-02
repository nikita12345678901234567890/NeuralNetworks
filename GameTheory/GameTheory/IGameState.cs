namespace GameTheory
{
    public interface IGameState<T> where T : IGameState<T>
    {
        int Value { get; }
        bool XWin { get; }
        bool IsTie { get; }
        bool OWin { get; }
        bool IsTerminal { get; }
        T[] GetChildren();
    }
}