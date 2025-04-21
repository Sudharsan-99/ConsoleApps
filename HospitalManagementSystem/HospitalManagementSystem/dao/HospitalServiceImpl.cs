using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.util;

namespace HospitalManagementSystem.dao
{
    public class HospitalServiceImpl : IHospitalService
    {
        public Appointments getAppointmentById(int appointmentID)
        {
            Appointments appointments = null;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "SELECT * FROM Appointments WHERE AppointmentID = @appointmentID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                        //connection.Open(); 

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int appointmentId = Convert.ToInt32(reader["AppointmentID"]);
                                int patientID = Convert.ToInt32(reader["PatientID"]);
                                int doctorID = Convert.ToInt32(reader["DoctorID"]);
                                DateTime appointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                                string description = reader["Description"].ToString();

                                appointments = new Appointments(appointmentId, patientID, doctorID, appointmentDate, description);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -- " + ex.Message);
            }

            return appointments;
        }

        public List<Appointments> getAppointmentsForPatient(int patientId)
        {
            List<Appointments> appointmentsList = new List<Appointments>();

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "SELECT * FROM Appointments WHERE PatientID = @patientId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int appointmentId = Convert.ToInt32(reader["AppointmentID"]);
                                int doctorId = Convert.ToInt32(reader["DoctorID"]);
                                DateTime appointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                                string description = reader["Description"].ToString();

                                Appointments appointment = new Appointments(appointmentId, patientId, doctorId, appointmentDate, description);
                                appointmentsList.Add(appointment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving appointments: " + ex.Message);
            }

            return appointmentsList;
        }

        public List<Appointments> getAppointmentsForDoctor(int doctorID)
        {
            List<Appointments> appointmentsList = new List<Appointments>();

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "SELECT * FROM Appointments WHERE DoctorID = @doctorID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@doctorID", doctorID);
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int appointmentId = Convert.ToInt32(reader["AppointmentID"]);
                                int patientId = Convert.ToInt32(reader["PatientID"]);
                                DateTime appointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                                string description = reader["Description"].ToString();

                                Appointments appointment = new Appointments(appointmentId, patientId, doctorID, appointmentDate, description);
                                appointmentsList.Add(appointment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving doctor appointments: " + ex.Message);
            }

            return appointmentsList;
        }

        public bool scheduleAppointment(Appointments app)
        {
            bool isScheduled = false;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = @"INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Description)
                             VALUES (@patientID, @doctorID, @appointmentDate, @description)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", app.PatientID);
                        cmd.Parameters.AddWithValue("@doctorID", app.DoctorID);
                        cmd.Parameters.AddWithValue("@appointmentDate", app.AppointmentDate);
                        cmd.Parameters.AddWithValue("@description", app.Description ?? (object)DBNull.Value);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        isScheduled = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error scheduling appointment: " + ex.Message);
            }

            return isScheduled;
        }

        public bool updateAppointment(Appointments app)
        {
            bool isUpdated = false;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = @"UPDATE Appointments 
                             SET PatientID = @patientID,
                                 DoctorID = @doctorID,
                                 AppointmentDate = @appointmentDate,
                                 Description = @description
                             WHERE AppointmentID = @appointmentID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@appointmentID", app.AppointmentID);
                        cmd.Parameters.AddWithValue("@patientID", app.PatientID);
                        cmd.Parameters.AddWithValue("@doctorID", app.DoctorID);
                        cmd.Parameters.AddWithValue("@appointmentDate", app.AppointmentDate);
                        cmd.Parameters.AddWithValue("@description", app.Description ?? (object)DBNull.Value);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        isUpdated = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating appointment: " + ex.Message);
            }

            return isUpdated;
        }

        public bool cancelAppointment(int appointmentId)
        {
            bool isCancelled = false;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "DELETE FROM Appointments WHERE AppointmentID = @appointmentID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@appointmentID", appointmentId);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        isCancelled = rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error while cancelling appointment: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cancelling appointment: " + ex.Message);
            }

            return isCancelled;
        }

    }
}
