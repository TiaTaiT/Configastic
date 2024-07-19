using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_Kdl2i : C2000_Kdl
    {
        public const int Code = 41;
        public C2000_Kdl2i(IPort port) : base(port)
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
