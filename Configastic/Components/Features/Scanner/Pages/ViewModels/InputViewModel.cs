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

        public InputViewModel(Shleif input)
        {
            _input = input;
            InputIndex = _input.ShleifIndex;
        }
    }
}
