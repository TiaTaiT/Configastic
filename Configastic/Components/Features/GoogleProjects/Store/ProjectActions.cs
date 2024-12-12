using Configastic.Components.Features.GoogleProjects.Models;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class CheckInProjectAction
    {
        public ProjectHeader Project { get; }
        public CheckInProjectAction(ProjectHeader project) => Project = project;
    }

    public class CheckOutProjectAction
    {
        public ProjectHeader Project { get; }
        public CheckOutProjectAction(ProjectHeader project) => Project = project;
    }
}
