using Configastic.SharedModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_Kdl2i_isp1 : C2000_Kdl2i
    {
        public new const int Code = 81;
        public C2000_Kdl2i_isp1(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-КДЛ-2И";
            SupportedModels =
            [
                Model,
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
