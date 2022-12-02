namespace MonteCarlo
{
    public interface IGameState<T> where T : IGameState<T>
    {
        int Value { get; set; }
        bool XWin { get; }
        bool IsTie { get; }
        bool OWin { get; }
        bool IsTerminal { get; }
        T[] GetChildren();

        List<T> Children { get; set; }

        T Parent { get; set; }

        //Monte carlo stuff:
        bool IsExpanded { get; set; }
        double UCT();
        int win { get; set; }
        int number { get; set; }
        const double C = 1.5;
    }
}