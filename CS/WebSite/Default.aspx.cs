using System;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;

public partial class _Default : System.Web.UI.Page {
    private int lastInsertedAppointmentId;

    protected void Page_Load(object sender, EventArgs e) {

        if(!IsPostBack) {
            ASPxScheduler1.Start = new DateTime(2008, 7, 12);
        }
    }

    protected void ASPxScheduler1_AppointmentRowInserting(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertingEventArgs e) {
        lastInsertedAppointmentId = GenerateNewAppointmentId();
        e.NewValues["ID"] = lastInsertedAppointmentId;
    }

    protected void ASPxScheduler1_AppointmentRowInserted(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs e) {
        e.KeyFieldValue = lastInsertedAppointmentId;
    }

    protected void ASPxScheduler1_AppointmentsInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e) {
        Appointment apt = (Appointment)e.Objects[0];
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        storage.SetAppointmentId(apt, lastInsertedAppointmentId);
    }

    private int GenerateNewAppointmentId() {
        int freeId = 0;

        for(int i = 0; i < ASPxScheduler1.Storage.Appointments.Count; i++) {
            Appointment apt = ASPxScheduler1.Storage.Appointments[i];

            int aptId = Convert.ToInt32(AppointmentIdHelper.GetAppointmentId(apt));

            if(aptId > freeId)
                freeId = aptId;

            if (apt.Type == AppointmentType.Pattern) {
               AppointmentBaseCollection exceptions = apt.GetExceptions();

               for (int j = 0; j < exceptions.Count; j++) {
                   aptId = Convert.ToInt32(AppointmentIdHelper.GetAppointmentId(exceptions[j]));

                   if (aptId > freeId)
                       freeId = aptId;
               }
            }
        }

        freeId++;
        return freeId;
    }
}