using System.Threading.Tasks;

namespace Gdot.Care.Common.Interface
{
    public interface ISqlCommand<TOutput, in TInput>
    {
        Task<TOutput> ExecuteAsync(TInput input1);
    }

    /// <summary>
    ///     Up to two input parameter support for case like 1st param cachekey, 2nd param for input class
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <typeparam name="TInput1"></typeparam>
    /// <typeparam name="TInput2"></typeparam>
    public interface ISqlCommand<TOutput, in TInput1, in TInput2>
    {
        Task<TOutput> ExecuteAsync(TInput1 input1, TInput2 input2);
    }
    public interface ISqlCommand<TOutput>
    {
        Task<TOutput> ExecuteAsync();
    }
    
}
