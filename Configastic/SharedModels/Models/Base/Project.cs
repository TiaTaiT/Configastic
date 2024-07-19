namespace Configastic.SharedModels.Models.Base
{
    public class Project
    {
        public List<ConstructionSite> SetProject { get; set; } = [];

        public string Name { get; set; } = string.Empty;

        public List<ConstructionSite> GetAll()
        {
            return SetProject;
        }

        public void Add(ConstructionSite constructionSite)
        {
            SetProject.Add(constructionSite);
        }
    }
}
