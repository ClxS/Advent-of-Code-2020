namespace Common
{
    using System.Threading.Tasks;

    public interface ISolver
    {
        string Name { get; }

        Task SolveAsync();
    }
}
