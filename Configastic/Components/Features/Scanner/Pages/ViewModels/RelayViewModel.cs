using Configastic.SharedModels.Models.BolidDevices.ElectricModules;
using MudBlazor;
using Color = MudBlazor.Color;

namespace Configastic.Components.Features.Scanner.Pages.ViewModels
{
    public class RelayViewModel
    {
        private readonly Relay _relay;

        public int Id { get; private set; }

        private bool _isToggled;
        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                _isToggled = value;
                Task.Run(() => RelayHandler());
            }
        }

        private async Task RelayHandler()
        {
            if (_isToggled)
            {
                await _relay.TurnOnAsync();
                return;
            }
            await _relay.TurnOffAsync();
        }

        public string Title { get; private set; }
        public Color Color 
        { 
            get => IsToggled ? Color.Secondary : Color.Surface; 
            set; 
        }
        public string Icon 
        { 
            get => IsToggled ? Icons.Material.Filled.Favorite : Icons.Material.Filled.AlarmOff; 
            private set; 
        }

        public RelayViewModel(Relay relay)
        {
            _relay = relay;
            Title = _relay.RelayIndex.ToString();
            Id = _relay.RelayIndex;
            Icon = Icons.Material.Filled.Favorite;
            Color = Color.Primary;
        }
    }
}
