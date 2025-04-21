using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.dao;
using HospitalManagementSystem.entity;

namespace HospitalManagementSystem.Main
{
    public class HospitalManagementApp
    {
        static void Main(string[] args)
        {
            HospitalServiceImpl service = new HospitalServiceImpl();
            Console.WriteLine("Welcome to the Appointment Management System");
            Console.Write("Login as \n1 Admin \n2 Patient: ");
            Console.WriteLine("Enter Your Choice = ");
            string userType = Console.ReadLine();

            switch(userType)
            {
                case "1":
                    Console.Write("Enter Admin Username: ");
                    string adminUser = Console.ReadLine();
                    Console.Write("Enter Admin Password: ");
                    string adminPass = Console.ReadLine();
                    if (adminUser == "admin" && adminPass == "admin123")
                    {
                        bool running = true;
                        while (running)
                        {
                            Console.WriteLine("\n--- Admin Menu ---");
                            Console.WriteLine("1. View Appointment by ID");
                            Console.WriteLine("2. View Appointments for Patient");
                            Console.WriteLine("3. View Appointments for Doctor");
                            Console.WriteLine("0. Logout");
                            Console.Write("Enter your choice: ");
                            string choice = Console.ReadLine();

                            switch (choice)
                            {
                                case "1":
                                    Console.Write("Enter Appointment ID: ");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    var appt = service.getAppointmentById(id);
                                    Console.WriteLine(appt != null ? appt.ToString() : "Appointment not found.");
                                    break;

                                case "2":
                                    Console.Write("Enter Patient ID: ");
                                    int patientId = Convert.ToInt32(Console.ReadLine());
                                    var patientAppointments = service.getAppointmentsForPatient(patientId);
                                    foreach (var p in patientAppointments)
                                        Console.WriteLine(p);
                                    break;

                                case "3":
                                    Console.Write("Enter Doctor ID: ");
                                    int doctorId = Convert.ToInt32(Console.ReadLine());
                                    var doctorAppointments = service.getAppointmentsForDoctor(doctorId);
                                    foreach (var d in doctorAppointments)
                                        Console.WriteLine(d);
                                    break;

                                case "0":
                                    running = false;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice.");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Admin credentials.");
                    }
                    break;
                case "2": // Patient
                    Console.Write("Enter your Patient ID: ");
                    int patId = Convert.ToInt32(Console.ReadLine());

                    bool loggedIn = true;
                    while (loggedIn)
                    {
                        Console.WriteLine("\n--- Patient Menu ---");
                        Console.WriteLine("1. Schedule Appointment");
                        Console.WriteLine("2. Update Appointment");
                        Console.WriteLine("3. Cancel Appointment");
                        Console.WriteLine("0. Logout");
                        Console.Write("Enter your choice: ");
                        string patientChoice = Console.ReadLine();

                        switch (patientChoice)
                        {
                            case "1":
                                Console.Write("Enter Doctor ID: ");
                                int docId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter Appointment Date (yyyy-MM-dd): ");
                                DateTime appDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Enter Description: ");
                                string desc = Console.ReadLine();

                                var newApp = new Appointments(0, patId, docId, appDate, desc);
                                if (service.scheduleAppointment(newApp))
                                    Console.WriteLine("Appointment scheduled.");
                                else
                                    Console.WriteLine("Failed to schedule appointment.");
                                break;

                            case "2":
                                Console.Write("Enter Appointment ID to update: ");
                                int updateId = Convert.ToInt32(Console.ReadLine());
                                var updateApp = service.getAppointmentById(updateId);

                                if (updateApp != null && updateApp.PatientID == patId)
                                {
                                    Console.Write("Enter new Date (yyyy-MM-dd): ");
                                    updateApp.AppointmentDate = DateTime.Parse(Console.ReadLine());
                                    Console.Write("Enter new Description: ");
                                    updateApp.Description = Console.ReadLine();

                                    if (service.updateAppointment(updateApp))
                                        Console.WriteLine("Appointment updated.");
                                    else
                                        Console.WriteLine("Update failed.");
                                }
                                else
                                {
                                    Console.WriteLine("Appointment not found or unauthorized access.");
                                }
                                break;

                            case "3":
                                Console.Write("Enter Appointment ID to cancel: ");
                                int cancelId = Convert.ToInt32(Console.ReadLine());

                                var cancelApp = service.getAppointmentById(cancelId);
                                if (cancelApp != null && cancelApp.PatientID == patId)
                                {
                                    if (service.cancelAppointment(cancelId))
                                        Console.WriteLine("Appointment cancelled.");
                                    else
                                        Console.WriteLine("Cancellation failed.");
                                }
                                else
                                {
                                    Console.WriteLine("Appointment not found or unauthorized.");
                                }
                                break;

                            case "0":
                                loggedIn = false;
                                break;

                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Invalid login type.");
                    break;
            }
        }
    }
}
