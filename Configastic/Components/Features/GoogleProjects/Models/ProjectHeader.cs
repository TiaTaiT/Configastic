namespace Configastic.Components.Features.GoogleProjects.Models
{
    public class ProjectHeader
    {
        public string Name { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public ProjectHeader(string fileName)
        {
            var nameParts = fileName.Split('-');
            if (nameParts.Length > 1)
            {
                ProjectNumber = nameParts[0].Trim();
                Name = nameParts[1].Trim();
            }
        }
    }
}