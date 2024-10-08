﻿using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_bi : OrionDevice
    {
        public const int Code = 10;
        public C2000_bi(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-БИ/БКИ";
            SupportedModels =
            [
                Model
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
