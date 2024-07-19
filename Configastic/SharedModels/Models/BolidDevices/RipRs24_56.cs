﻿
using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class RipRs24_56 : RipRs
    {
        public const int Code = 55;

        public RipRs24_56(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "РИП-24 исп.56";
            SupportedModels =
            [
                Model,
            ];
        }

        public override bool Setup(Action<int> updateProgressBar, int modelCode = 0)
        {
            return base.Setup(updateProgressBar, Code);
        }
    }
}
