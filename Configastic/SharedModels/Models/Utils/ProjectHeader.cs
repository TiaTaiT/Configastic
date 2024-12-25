namespace Configastic.SharedModels.Models.Utils
{
    public class ProjectHeader
    {
        public string Name { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string Url {  get; set; } = string.Empty;

        public ProjectHeader(string fileName, string url)
        {
            var nameParts = fileName.Split('-');
            Url = url;
            if (nameParts.Length > 1)
            {
                ProjectNumber = nameParts[0].Trim();
                Name = nameParts[1].Trim();
            }
        }
    }
}