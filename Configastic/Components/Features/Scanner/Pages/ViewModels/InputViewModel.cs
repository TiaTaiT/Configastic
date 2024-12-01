using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices.ElectricModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configastic.Components.Features.Scanner.Pages.ViewModels
{
    public class InputViewModel
    {
        private Shleif _input;

        public int InputIndex { get; set; }

        private IDispatcher? _dispatcher;
        private bool _isToggled;
        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                _isToggled = value;
                Task.Run(() => InputHandler());
            }
        }

        public byte AdcValue { get; private set; }

        private async Task InputHandler()
        {
            while (IsToggled)
            {
                
                AdcValue = await _input.GetShleifAdcValue();
            }
        }

        public InputViewModel(Shleif input)
        {
            _input = input;
            InputIndex = _input.ShleifIndex;
            _dispatcher = Dispatcher.GetForCurrentThread();
        }
    }
}
