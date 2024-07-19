
/* Unmerged change from project 'Configastic (net8.0-android)'
Before:
using Fluxor;
After:
using Configastic;
using Configastic.Components;
using Configastic.Components.Features;
using Configastic.Components.Features.Counter;
using Configastic.Components.Features.Counter.Store;
using Configastic.Components.Features.Counter.Store;
using Configastic.Components.Features.Counter.Store.CounterUseCase;
using Fluxor;
*/
using Fluxor;

namespace Configastic.Components.Features.Counter.Store
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
