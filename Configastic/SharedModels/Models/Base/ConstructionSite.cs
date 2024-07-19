namespace Configastic.SharedModels.Models.Base
{
    public class ConstructionSite
    {
        private List<Cabinet> _cabinets = [];

        public string Name { get; set; } = string.Empty;

        public string DirectoryPath { get; set; } = string.Empty;

        public List<Cabinet> GetAll()
        {
            return _cabinets;
        }

        public void SetConstructionSite(List<Cabinet> cabinets)
        {
            _cabinets = cabinets;
        }

        public void Add(Cabinet cabinet)
        {
            _cabinets.Add(cabinet);
        }
    }
}
