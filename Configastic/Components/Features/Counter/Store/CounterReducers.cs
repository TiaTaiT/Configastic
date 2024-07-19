using Fluxor;

namespace Configastic.Components.Features.Counter.Store
{
    public class CounterReducers
    {
        [ReducerMethod]
        public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action) =>
           state with { CurrentCount = state.CurrentCount + action.Increment };
    }
}
