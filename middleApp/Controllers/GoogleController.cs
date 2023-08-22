using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using middleApp.Models;

namespace middleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleController : ControllerBase
    {
        string jsonFile = "cosmic-stacker-393910-30e34a3dff89.json";
        string calendarId = @"7eb1942465e026e68a53d42358f4531e3d0ceabf6db49846d979142e1498ad7a@group.calendar.google.com";

        private ServiceAccountCredential GoogleCalendarLogIn()
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

        [HttpGet("upcoming_events")]
        public List<EventModel> GetUpcomingEventsInModel()
        {
            List<EventModel> eventsList = new List<EventModel>();
            ServiceAccountCredential credential = GoogleCalendarLogIn();

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            var calendar = service.Calendars.Get(calendarId).Execute();

            EventsResource.ListRequest listRequest = service.Events.List(calendarId);
            listRequest.TimeMin = DateTime.Now;
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 10;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = listRequest.Execute();

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTimeDateTimeOffset.ToString();
                    if (!String.IsNullOrEmpty(when))
                    {
                        eventsList.Add(
                            new EventModel
                            {
                                Title = eventItem.Summary,
                                StartTime = (DateTime)eventItem.Start.DateTime,
                                EndTime = (DateTime)eventItem.End.DateTime
                            });                      
                    }                    
                }
            }
            else
            {
                eventsList.Add(new EventModel
                {
                    Title = "empty",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now
                });
            }

            return eventsList;
        }

        [HttpPost("add_event")]
        public IActionResult AddEventToCalendar([FromBody] EventModel eventModel)
        {
            ServiceAccountCredential credential = GoogleCalendarLogIn();

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            try
            {
                Event newEvent = new Event
                {
                    Summary = eventModel.Title,
                    Start = new EventDateTime
                    {
                        DateTime = eventModel.StartTime,
                        TimeZone = "Europe/Warsaw",
                    },
                    End = new EventDateTime
                    {
                        DateTime = eventModel.EndTime,
                        TimeZone = "Europe/Warsaw",
                    }
                };
                EventsResource.InsertRequest insertRequest = service.Events.Insert(newEvent, calendarId);
                Event createdEvent = insertRequest.Execute();

                if (createdEvent != null)
                {
                    return Ok("Event added successfully!");
                }
                else
                {
                    return BadRequest("Failed to add the event.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding the event: {ex.Message}");
            }
        }

    }
}
