using Configastic.SharedModels.Models.Utils;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class AddProjectAction
    {
        public ProjectHeader Project { get; }
        public AddProjectAction(ProjectHeader project) => Project = project;
    }

    public class ClearProjectsAction { }

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
