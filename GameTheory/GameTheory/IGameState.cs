namespace GameTheory
{
    public interface IGameState<T> where T : IGameState<T>
    {
        int Value { get; }
        bool IsWin { get; }
        bool IsTie { get; }
        bool IsLoss { get; }
        bool IsTerminal { get; }
        T[] GetChildren();
    }
}