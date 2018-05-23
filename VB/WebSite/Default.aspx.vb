Imports System
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private lastInsertedAppointmentId As Integer

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		If (Not IsPostBack) Then
			ASPxScheduler1.Start = New DateTime(2008, 7, 12)
		End If
	End Sub

	Protected Sub ASPxScheduler1_AppointmentRowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertingEventArgs)
		lastInsertedAppointmentId = GenerateNewAppointmentId()
		e.NewValues("ID") = lastInsertedAppointmentId
	End Sub

	Protected Sub ASPxScheduler1_AppointmentRowInserted(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs)
		e.KeyFieldValue = lastInsertedAppointmentId
	End Sub

	Protected Sub ASPxScheduler1_AppointmentsInserted(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PersistentObjectsEventArgs)
		Dim apt As Appointment = CType(e.Objects(0), Appointment)
		Dim storage As ASPxSchedulerStorage = CType(sender, ASPxSchedulerStorage)
		storage.SetAppointmentId(apt, lastInsertedAppointmentId)
	End Sub

	Private Function GenerateNewAppointmentId() As Integer
		Dim freeId As Integer = 0

		For i As Integer = 0 To ASPxScheduler1.Storage.Appointments.Count - 1
			Dim apt As Appointment = ASPxScheduler1.Storage.Appointments(i)

			Dim aptId As Integer = Convert.ToInt32(AppointmentIdHelper.GetAppointmentId(apt))

			If aptId > freeId Then
				freeId = aptId
			End If

			If apt.Type = AppointmentType.Pattern Then
			   Dim exceptions As AppointmentBaseCollection = apt.GetExceptions()

			   For j As Integer = 0 To exceptions.Count - 1
				   aptId = Convert.ToInt32(AppointmentIdHelper.GetAppointmentId(exceptions(j)))

				   If aptId > freeId Then
					   freeId = aptId
				   End If
			   Next j
			End If
		Next i

		freeId += 1
		Return freeId
	End Function
End Class