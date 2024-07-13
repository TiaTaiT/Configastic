using Fluxor;

namespace Configastic.Store.CounterUseCase
{
    public class CounterReducers
    {
        [ReducerMethod]
        public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action) =>
           state with { CurrentCount = state.CurrentCount + action.Increment };
    }
}
