using Fluxor;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class ProjectHeaderReducer
    {
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
