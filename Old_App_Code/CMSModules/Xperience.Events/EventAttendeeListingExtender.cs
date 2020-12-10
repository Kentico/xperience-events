using CMS.Base.Web.UI;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Xperience;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.UIControls;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace Xperience.Events
{
    public class EventAttendeeListingExtender : ControlExtender<UniGrid>
    {
        private Event Event { get; set; }

        public override void OnInit()
        {
            var nodeID = QueryHelper.GetInteger("nodeid", 0);
            var culture = QueryHelper.GetString("culture", SiteContext.CurrentSite.DefaultVisitorCulture);
            Event = DocumentHelper.GetDocuments<Event>()
                .WhereEquals("NodeID", nodeID)
                .Culture(culture)
                .OnCurrentSite()
                .TopN(1)
                .FirstOrDefault();

            if(!Event.EventRequiresRegistration)
            {
                Control.Visible = false;
                Control.HeaderActions.Visible = false;
                
                var heading = new LocalizedHeading();
                heading.Level = 4;
                heading.ResourceString = "xperience.events.registrationdisabled";
                Control.Parent.Controls.Add(heading);
            }
            else
            {
                Control.ShowObjectMenu = false;
                Control.OnAfterRetrieveData += Control_OnAfterRetrieveData;
            }
        }

        private System.Data.DataSet Control_OnAfterRetrieveData(System.Data.DataSet ds)
        {
            var attendeeCount = ds.Tables[0].Rows.Count;
            var capacity = Event.EventCapacity;

            var formatter = new NumberFormatInfo();
            formatter.NumberDecimalDigits = 1;
            var percentage = capacity > 0 ?
                Convert.ToDouble((Convert.ToDouble(attendeeCount) / Convert.ToDouble(capacity)) * 100) :
                0;
            Control.MessagesPlaceHolder.ShowInformation( $"<b>Capacity</b>: {capacity} <b>Attendees</b>: {attendeeCount} <b>Registration</b>: {percentage.ToString("N", formatter)}%", persistent: true);
            
            if(attendeeCount >= capacity && !Event.EventAllowOverCapacity)
            {
                Control.HeaderActions.Enabled = false;
            }

            return ds;
        }
    }
}