using DevExpress.Utils.Drawing;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchedulerProject {
    public partial class Form1 : Form {

        #region InitialDataConstants
        public static Random RandomInstance = new Random();
        public static string[] Users = new string[] {"Peter Dolan", "Ryan Fischer", "Richard Fisher", 
                                                 "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain", 
                                                 "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman", 
                                                 "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"};
        #endregion
        
        public Form1() {
            InitializeComponent();
            FillResources(schedulerDataStorage1, 5);
            InitAppointments();
            schedulerControl1.Start = DateTime.Now;
            UpdateControls();
        }

        #region InitialDataLoad
        void FillResources(SchedulerDataStorage storage, int count) {
                int cnt = Math.Min(count, Users.Length);
                for (int i = 1; i <= cnt; i++) {
                    var resource = storage.CreateResource(i);
                    resource.Caption = Users[i - 1];
                    storage.Resources.Add(resource);
                }
        }
        void InitAppointments() {
            this.schedulerDataStorage1.Appointments.Mappings.Start = "StartTime";
            this.schedulerDataStorage1.Appointments.Mappings.End = "EndTime";
            this.schedulerDataStorage1.Appointments.Mappings.Subject = "Subject";
            this.schedulerDataStorage1.Appointments.Mappings.AllDay = "AllDay";
            this.schedulerDataStorage1.Appointments.Mappings.Description = "Description";
            this.schedulerDataStorage1.Appointments.Mappings.Label = "Label";
            this.schedulerDataStorage1.Appointments.Mappings.Location = "Location";
            this.schedulerDataStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
            this.schedulerDataStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo";
            this.schedulerDataStorage1.Appointments.Mappings.ResourceId = "OwnerId";
            this.schedulerDataStorage1.Appointments.Mappings.Status = "Status";
            this.schedulerDataStorage1.Appointments.Mappings.Type = "EventType";

            CustomEventList eventList = new CustomEventList();
            GenerateEvents(eventList);
            this.schedulerDataStorage1.Appointments.DataSource = eventList;

        }
        void GenerateEvents(CustomEventList eventList) {
            int count = schedulerDataStorage1.Resources.Count;
            for (int i = 0; i < count; i++) {
                Resource resource = schedulerDataStorage1.Resources[i];
                string subjPrefix = resource.Caption + "'s ";
                eventList.Add(CreateEvent(eventList, subjPrefix + "meeting", resource.Id, 2, 5));
                eventList.Add(CreateEvent(eventList, subjPrefix + "travel", resource.Id, 3, 6));
                eventList.Add(CreateEvent(eventList, subjPrefix + "phone call", resource.Id, 0, 10));
            }
        }
        CustomEvent CreateEvent(CustomEventList eventList, string subject, object resourceId, int status, int label) {
            CustomEvent apt = new CustomEvent(eventList);
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            return apt;
        }
        #endregion
        #region Update Controls
        private void UpdateControls() {
            cbView.EditValue = schedulerControl1.ActiveViewType;
            rgrpGrouping.EditValue = schedulerControl1.GroupType;
        }
        private void rgrpGrouping_SelectedIndexChanged(object sender, System.EventArgs e) {
            schedulerControl1.GroupType = (SchedulerGroupType)rgrpGrouping.EditValue;
        }

        private void schedulerControl_ActiveViewChanged(object sender, System.EventArgs e) {
            cbView.EditValue = schedulerControl1.ActiveViewType;
        }

        private void cbView_SelectedIndexChanged(object sender, System.EventArgs e) {
            schedulerControl1.ActiveViewType = (SchedulerViewType)cbView.EditValue;
        }
        #endregion

        private void schedulerControl1_CustomDrawAppointmentBackground(object sender, CustomDrawObjectEventArgs e) {
            // Specify the ratio of a completed task to the entire task.
            double completenessRatio = 0.25;

            Rectangle bounds = CalculateEntireAppointmentBounds((AppointmentViewInfo)e.ObjectInfo);
            DrawBackGroundCore(e.Cache, bounds, completenessRatio);
            e.Handled = true;

        }

        Rectangle CalculateEntireAppointmentBounds(AppointmentViewInfo viewInfo) {
            int leftOffset = 0;
            int rightOffset = 0;
            double scale = viewInfo.Bounds.Width / viewInfo.Interval.Duration.TotalMilliseconds;
            if (!viewInfo.HasLeftBorder) {
                double hidden = (viewInfo.Interval.Start - viewInfo.AppointmentInterval.Start).TotalMilliseconds;
                leftOffset = (int)(hidden * scale);
            }
            if (!viewInfo.HasRightBorder) {
                double hidden = (viewInfo.AppointmentInterval.End - viewInfo.Interval.End).TotalMilliseconds;
                rightOffset = (int)(hidden * scale);
            }
            Rectangle bounds = viewInfo.Bounds;
            return Rectangle.FromLTRB(bounds.Left - leftOffset, bounds.Y, bounds.Right + rightOffset, bounds.Bottom);
        }
        void DrawBackGroundCore(GraphicsCache cache, Rectangle bounds, double completenessRatio) {
            Brush brush1 = new SolidBrush(Color.Green);
            Brush brush2 = new SolidBrush(Color.Orange);
            cache.FillRectangle(brush1, new Rectangle(bounds.X, bounds.Y, (int)(bounds.Width * completenessRatio), bounds.Height));
            cache.FillRectangle(brush2, new Rectangle(bounds.X + (int)(bounds.Width * completenessRatio), bounds.Y, (int)(bounds.Width * (1 - completenessRatio)), bounds.Height));
        }

    }
}