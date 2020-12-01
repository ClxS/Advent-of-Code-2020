namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAsyncSolver
    {
        string Name { get; }

        Task SolveAsync();
    }
}
