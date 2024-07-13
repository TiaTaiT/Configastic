using Fluxor;

namespace Configastic.Store.CounterUseCase
{
    public record CounterState
    {
        public int CurrentCount { get; init; }
    }

    public class CounterFeature : Feature<CounterState>
    {
        public override string GetName() => "Counter";

        protected override CounterState GetInitialState() => new CounterState { CurrentCount = 0 };
    }
}
