﻿using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class Signal20 : OrionDevice
    {
        public new const int Code = 1;
        public Signal20(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "Сигнал-20";
            SupportedModels =
            [
                Model
            ];
        }

        public override bool Setup(Action<int> updateProgressBar, int modelCode = 0)
        {
            return base.Setup(updateProgressBar, Code);
        }
    }
}
