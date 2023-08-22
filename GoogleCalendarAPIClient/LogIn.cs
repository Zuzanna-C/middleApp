using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCalendarAPIClient
{
    public class LogIn
    {
        string jsonFile = "cosmic-stacker-393910-30e34a3dff89.json";
        string calendarId = @"7eb1942465e026e68a53d42358f4531e3d0ceabf6db49846d979142e1498ad7a@group.calendar.google.com";

        public ServiceAccountCredential GoogleCalendarLogIn()
        {
            string[] Scopes = { CalendarService.Scope.Calendar };

            ServiceAccountCredential credential;

            using (var stream = new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                    new ServiceAccountCredential.Initializer(confg.ClientEmail)
                    {
                        Scopes = Scopes
                    }.FromPrivateKey(confg.PrivateKey));
            }
            return credential;
        }
    }
}
