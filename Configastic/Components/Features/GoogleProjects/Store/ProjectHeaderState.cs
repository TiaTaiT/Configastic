using Configastic.Components.Features.GoogleProjects.Models;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class ProjectHeaderState
    {
        public List<ProjectHeader> Projects { get; }
        public ProjectHeader? CurrentCheckedInProject { get; }

        public ProjectHeaderState(List<ProjectHeader> projects, ProjectHeader? currentCheckedInProject = null)
        {
            Projects = projects;
            CurrentCheckedInProject = currentCheckedInProject;
        }
    }
}
