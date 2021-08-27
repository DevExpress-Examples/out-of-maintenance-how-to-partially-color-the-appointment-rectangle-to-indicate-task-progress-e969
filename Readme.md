<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128635764/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E969)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/PartiallyColoredAppointment/Form1.cs) (VB: [Form1.vb](./VB/PartiallyColoredAppointment/Form1.vb))
<!-- default file list end -->
# How to partially color the appointment rectangle to indicate task progress


<p>Scheduler appointments can represent tasks to be completed. If you are informed about the progress of a task, you may wish to indicate it with color. <br /> To accomplish this, handle the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerControl_CustomDrawAppointmentBackgroundtopic">CustomDrawAppointmentBackground</a> event. Use the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerCustomDrawObjectEventArgs_ObjectInfotopic">e.ObjectInfo</a> property to get access to the <a href="http://help.devexpress.com/#WindowsForms/clsDevExpressXtraSchedulerDrawingAppointmentViewInfotopic">AppointmentViewInfo</a> object, containing information about the appointment being displayed. The <a href="http://help.devexpress.com/#WindowsForms/DevExpressXtraSchedulerDrawingAppointmentViewInfo_InnerBoundstopic">InnerBounds</a> property provides the coordinates of the appointment body rectangle. <br />An appointment can extend beyond the visible area. To determine which part of the appointment is shown on the screen, the <strong>HasLeftBorder</strong> and <strong>HasRightBorder</strong> properties are evaluated. Using the <a href="http://help.devexpress.com/#WindowsForms/DevExpressXtraSchedulerDrawingAppointmentViewInfo_AppointmentIntervaltopic">AppointmentInterval</a> and <strong>Interval</strong> properties of the <strong>AppointmentViewInfo</strong> object,  the dimensions of hidden and visible parts can be calculated.<br />Note that you should set the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerAppointmentDisplayOptions_SnapToCellsModetopic">SnapToCellsMode</a> property to <strong>Never,</strong> to calculate the visible part of the appointment more accurately.<br />The SchedulerControl of the sample application is shown in the picture below.<br /><br /><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-partially-color-the-appointment-rectangle-to-indicate-task-progress-e969/13.1.4+/media/50b44e2a-d3be-11e4-80bf-00155d62480c.png"></p>
<p> </p>

<br/>


