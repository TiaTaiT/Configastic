using Configastic.SharedModels.Models.Utils;
using Fluxor;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class ProjectHeaderReducer
    {
        [ReducerMethod]
        public static ProjectHeaderState ReduceAddProject(ProjectHeaderState state, AddProjectAction action)
        {
            if (state.Projects.Any(p => p.Url.Equals(action.Project.Url)))
                return state;
            var newProjects = new List<ProjectHeader>(state.Projects) { action.Project };
            return new ProjectHeaderState(newProjects, state.CurrentCheckedInProject);
        }

        [ReducerMethod]
        public static ProjectHeaderState ReduceClearProjects(ProjectHeaderState state, ClearProjectsAction action)
        {
            return new ProjectHeaderState(new List<ProjectHeader>(), null);
        }

        [ReducerMethod]
        public static ProjectHeaderState ReduceCheckInProject(ProjectHeaderState state, CheckInProjectAction action)
        {
            return new ProjectHeaderState(
                state.Projects,
                action.Project
            );
        }

        [ReducerMethod]
        public static ProjectHeaderState ReduceCheckOutProject(ProjectHeaderState state, CheckOutProjectAction action)
        {
            return new ProjectHeaderState(
                state.Projects,
                null
            );
        }
    }
}
