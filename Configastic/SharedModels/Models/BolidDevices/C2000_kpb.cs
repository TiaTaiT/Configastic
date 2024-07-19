﻿using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_kpb : OrionDevice
    {
        public const int Code = 15;
        public C2000_kpb(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-КПБ";
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