using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.entity
{
    public class Appointments
    {
        private int _appointmentID;
        private int _patientID;
        private int _doctorID;
        private DateTime _appointmentDate;
        private string _description;

        public int  AppointmentID
        {
            get { return _appointmentID; }
            set { _appointmentID = value; }
        }

        public int PatientID
        {
            get { return _patientID; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid Patient ID.");
                _patientID = value;
            }
        }

        public int DoctorID
        {
            get { return _doctorID; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid Doctor ID.");
                _doctorID = value;
            }
        }

        public DateTime AppointmentDate
        {
            get { return _appointmentDate; }
            set
            {
                if (value < DateTime.Now.Date)
                    throw new ArgumentException("Appointment date cannot be in the past.");
                _appointmentDate = value;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Description cannot be empty.");
                _description = value;
            }
        }

        public Appointments(int patientID, int doctorID, DateTime appointmentDate, string description)
        {
            PatientID = patientID;
            DoctorID = doctorID;
            AppointmentDate = appointmentDate;
            Description = description;
        }

        public Appointments(int Appointmentid ,int patientID, int doctorID, DateTime appointmentDate, string description)
        {
            AppointmentID= Appointmentid;
            PatientID = patientID;
            DoctorID = doctorID;
            AppointmentDate = appointmentDate;
            Description = description;
        }
    }
}
