using CMS;
using CMS.Core;
using CMS.DataEngine;
using Events;
using System.Linq;
using Xperience.Events;

[assembly: RegisterModule(typeof(EventsModule))]
namespace Xperience.Events
{
    public class EventsModule : Module
    {
        public EventsModule() : base(nameof(EventsModule)) { }

        protected override void OnInit()
        {
            base.OnInit();
            EventAttendeeInfo.TYPEINFO.Events.Insert.Before += Insert_Before;
        }

        private void Insert_Before(object sender, ObjectEventArgs e)
        {
            // Don't allow duplicate registrations to the same event
            var attendee = e.Object as EventAttendeeInfo;
            var existingAttendee = EventAttendeeInfoProvider.ProviderObject.Get()
                .WhereEquals("ContactID", attendee.ContactID)
                .WhereEquals("NodeID", attendee.NodeID)
                .TopN(1)
                .FirstOrDefault();

            if(existingAttendee != null)
            {
                e.Cancel();
                throw new System.Exception("Attendee already registered for event.");
            }
        }
    }
}