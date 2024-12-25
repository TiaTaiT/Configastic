using Configastic.Services.Interfaces;
using Configastic.SharedModels.Models.Utils;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using System.Diagnostics;

namespace Configastic.Services.Services
{
    public class GoogleDriveSheetsLister : IGoogleDriveSheetsLister
    {
        private const string CredentialsFileName = "firm-capsule-441717-e2-2f90d66e4ff2.json";
        private readonly string credentialsPath = GetCredentialsPath();
        private static readonly string[] Scopes = { DriveService.Scope.DriveReadonly };
        private static readonly string ApplicationName = "Google Drive API .NET 8.0 Example";
        private readonly DriveService _service;
        public GoogleDriveSheetsLister()
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public async Task<IEnumerable<ProjectHeader>> ListAllSpreadsheetsAsync()
        {
            var request = _service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.spreadsheet'";
            request.Fields = "files(id, name)";

            CheckConnectionProfiles();
            CheckLanConnection();

            FileList result = await request.ExecuteAsync().ConfigureAwait(false);
            IList<Google.Apis.Drive.v3.Data.File> files = result.Files;

            var ids = new List<ProjectHeader>();

            if (files != null && files.Count > 0)
            {
                Console.WriteLine("Spreadsheets:");
                foreach (var file in files)
                {
                    ids.Add(new ProjectHeader(file.Name, file.Id));
                    Debug.WriteLine($"Name: {file.Name}, ID: {file.Id}");
                }
            }
            else
            {
                Debug.WriteLine("No spreadsheets found.");
            }
            return ids;
        }

        private static string GetCredentialsPath()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, CredentialsFileName);
            if (!System.IO.File.Exists(filePath))
            {
                // Copy the file from the embedded resource to AppDataDirectory
                using var stream = FileSystem.OpenAppPackageFileAsync(CredentialsFileName).Result;
                using var fileStream = System.IO.File.Create(filePath);
                stream.CopyTo(fileStream);
            }
            return filePath;
        }

        public void CheckConnectionProfiles()
        {
            var profiles = Connectivity.Current.ConnectionProfiles;

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Debug.WriteLine("Connected via WiFi.");
            }
            else if (profiles.Contains(ConnectionProfile.Cellular))
            {
                Debug.WriteLine("Connected via Cellular.");
            }
            else
            {
                Debug.WriteLine("No active connection profile detected.");
            }
        }

        public void CheckLanConnection()
        {
            var profiles = Connectivity.Current.ConnectionProfiles;

            if (profiles.Contains(ConnectionProfile.Ethernet))
            {
                Debug.WriteLine("Connected via Ethernet (LAN).");
            }
            else if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Debug.WriteLine("Internet access available (possibly via LAN).");
            }
            else
            {
                Debug.WriteLine("No LAN or Internet connection.");
            }
        }
    }
}
