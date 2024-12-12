using Configastic.Components.Features.GoogleProjects.Models;
using Fluxor;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class ProjectHeaderFeature : Feature<ProjectHeaderState>
    {
        public override string GetName() => "ProjectHeaders";

        protected override ProjectHeaderState GetInitialState()
        {
            // Initialize with sample data or load from a service
            var sampleProjects = new List<ProjectHeader>
        {
            new ProjectHeader("001-Initial Design"),
            new ProjectHeader("002-Detailed Engineering"),
            new ProjectHeader("003-Manufacturing Prototype")
        };

            return new ProjectHeaderState(sampleProjects);
        }
    }
}
