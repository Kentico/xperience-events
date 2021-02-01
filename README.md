# Xperience Events

## Installation

1. Install the [Xperience.Events](https://www.nuget.org/packages/Xperience.Events) NuGet package in the CMS application
1. _(Optional)_ Install the [companion package](https://www.nuget.org/packages/Xperience.Core.Events) in your .NET Core application
1. Build the CMS application
1. In the __Page types__ application, configure the [scopes](https://docs.xperience.io/developing-websites/defining-website-content-structure/managing-page-types/limiting-the-pages-users-can-create#Limitingthepagesuserscancreate-Managingpagetypescopes) and [allowed types](https://docs.xperience.io/developing-websites/defining-website-content-structure/managing-page-types/limiting-the-pages-users-can-create#Limitingthepagesuserscancreate-Allowinguserstoplacecertainpagesunderapagetype) for the new `Xperience.Event` and `Xperience.EventCalendar` page types

## Creating a calendar

To create a new calendar of events, create an `Xperience.EventCalendar` page in your content tree. The _Text color_, _Background color_, and _Bullet color_ fields determine how events of this calendar appear on the live site:

![Calendar fields](/img/calendarfields.png)

_Text color_ defaults to `#000`. Under the __Attendees__ section you can control which information appears in an event's detailed popup (if you installed the __Xperience.Core.Events__ NuGet package):

![Detail popup](/img/detailpopup.png)

_Show event capacity and attendee count_ displays the "3 of 10" text from the above screenshot, and _Show event attendee names_ shows the list of names.

## Adding events to a calendar

To add events to a calendar, create `Xperience.Event` pages under the `Xperience.EventCalendar` page. Aside from basic information about the event, you can also configure registration for the event.

If _Requires registration_ is enabled, you can set an event capacity and  also allow further registration if the capacity is exceeded. You can view and manage current registrations from the new __Attendees__ tab:

![Attendee tab](/img/attendeetab.png)