using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.entity;

namespace HospitalManagementSystem.dao
{
    public interface IHospitalService
    {
        public Appointments getAppointmentById(int appointmentID);

        public List<Appointments> getAppointmentsForPatient(int patientId);

        public List<Appointments> getAppointmentsForDoctor(int doctorID);

        public bool scheduleAppointment(Appointments app);

        public bool updateAppointment(Appointments app);

        public bool cancelAppointment(int AppointmentId);
    }
}
