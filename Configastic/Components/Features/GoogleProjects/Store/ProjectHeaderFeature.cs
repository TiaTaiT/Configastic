using Configastic.Services.Interfaces;
using Fluxor;

namespace Configastic.Components.Features.GoogleProjects.Store
{
    public class ProjectHeaderFeature : Feature<ProjectHeaderState>
    {
        private IGoogleDriveSheetsLister _googleDriveSheets;

        public ProjectHeaderFeature(IGoogleDriveSheetsLister googleDriveSheetsLister)
        {
            _googleDriveSheets = googleDriveSheetsLister;
        }

        public override string GetName() => "ProjectHeaders";

        protected override ProjectHeaderState GetInitialState()
        {
            // Initialize with sample data or load from a service
            
            return new ProjectHeaderState([]);
        }
    }
}
