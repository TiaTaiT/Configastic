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
            new ProjectHeader("003-Manufacturing Prototype"),
            new ProjectHeader("004-Manufacturing Prototype"),
            new ProjectHeader("005-Manufacturing Prototype"),
            new ProjectHeader("006-Manufacturing Prototype"),
            new ProjectHeader("007-Manufacturing Prototype"),
            new ProjectHeader("008-Manufacturing Prototype"),
            new ProjectHeader("009-Manufacturing Prototype"),
            new ProjectHeader("010-Manufacturing Prototype"),
            new ProjectHeader("011-Manufacturing Prototype"),
            new ProjectHeader("012-Manufacturing Prototype"),
            new ProjectHeader("013-Manufacturing Prototype"),
            new ProjectHeader("014-Manufacturing Prototype"),
            new ProjectHeader("015-Manufacturing Prototype"),
            new ProjectHeader("016-Manufacturing Prototype"),
            new ProjectHeader("017-Manufacturing Prototype"),
            new ProjectHeader("018-Manufacturing Prototype"),
            new ProjectHeader("019-Manufacturing Prototype"),
            new ProjectHeader("020-Manufacturing Prototype"),
            new ProjectHeader("021-Manufacturing Prototype"),
            new ProjectHeader("022-Manufacturing Prototype"),
            new ProjectHeader("023-Manufacturing Prototype"),
            new ProjectHeader("024-Manufacturing Prototype"),
            new ProjectHeader("025-Manufacturing Prototype"),
            new ProjectHeader("026-Manufacturing Prototype"),
            new ProjectHeader("027-Manufacturing Prototype"),
            new ProjectHeader("028-Manufacturing Prototype"),
            new ProjectHeader("029-Manufacturing Prototype"),
            new ProjectHeader("030-Manufacturing Prototype"),
            new ProjectHeader("031-Manufacturing Prototype"),
            new ProjectHeader("032-Manufacturing Prototype"),
            new ProjectHeader("033-Manufacturing Prototype"),
        };

            return new ProjectHeaderState(sampleProjects);
        }
    }
}
