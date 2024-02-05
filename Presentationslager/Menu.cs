using Entitetlager;
using PatientManagementAffärslager;
using System;
using System.Linq;
using System.Reflection.Metadata;
using Presentationslager;
using Datalager;
using System.ComponentModel.Design;

namespace PatientManagementConsole
{
    public class Program
    {
        static PatientManagement patientManagement = new PatientManagement();
        static Employee loggedInEmployee;


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Patient Management System!");

            while (true)
            {
                Console.WriteLine("\n══════════════════════════════════════");
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Log In");
                Console.WriteLine("2. Exit");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Enter your choice (1-2): ");



                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            LogIn();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void LogIn()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("Log In:");
            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                bool loggedIn = patientManagement.LogInEmployee(employeeId, password);

                if (loggedIn)
                {
                    Console.WriteLine($"Logged in successfully as Employee ID: {employeeId}.");

                    DisplayMainMenu();
                }
                else
                {
                    Console.WriteLine("Invalid login credentials. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Employee ID.");
            }
        }

        static void DisplayMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n══════════════════════════════════════");
                Console.WriteLine("Patient Management Menu:");
                Console.WriteLine("1. Register Patient");
                Console.WriteLine("2. Update Patient Information");
                Console.WriteLine("3. Book Doctor Visit");
                Console.WriteLine("4. Register Diagnosis");
                Console.WriteLine("5. Prescribe Treatment");
                Console.WriteLine("6. Cancel Doctor Visit");
                Console.WriteLine("7. View Doctor Appointment");

                Console.WriteLine("8. Log Out");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Enter your choice (1-8): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            RegisterPatient();
                            break;
                        case 2:
                            UpdatePatientInformation();
                            break;
                        case 3:
                            BookDoctorVisit();
                            break;
                        case 4:
                            RegisterDiagnosis();
                            break;
                        case 5:
                            PrescribeTreatment();
                            break;
                        case 6:
                            CancelDoctorVisit();
                            break;
                        case 7:
                            ViewDoctorAppointment();
                            break;
                        case 8:
                            Console.WriteLine("Logged out.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private static void ViewDoctorAppointment()
        {

            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("Option 7: View Doctor Appointment");

            Console.Write("Enter Visit Number: ");
            int visitNo = int.Parse(Console.ReadLine());

            try
            {

                var visit = patientManagement.ViewDoctorVisit(visitNo);
                Console.WriteLine("Visit Number: " + visit.VisitNumber);
                Console.WriteLine("Patient Number: " + visit.PatientNumber);
                Console.WriteLine("Booked date: " + visit.VisitDateTime);
                Console.WriteLine("Visit purpose: " + visit.Purpose);
                Console.WriteLine("Doctor Employee number: " + visit.DoctorEmployeeNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }



        }

        private static void CancelDoctorVisit()
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("Option 6: Cancel Doctor Visit");
            Console.Write("Enter Visit Number: ");
            int visitNo = int.Parse(Console.ReadLine());
            var visit = patientManagement.ViewDoctorVisit(visitNo);

            try
            {

                patientManagement.CancelDoctorVisit(visitNo);
                Console.WriteLine("Doctor visit canceled");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        static void RegisterPatient()
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("Option 1: Register Patient");


            Console.Write("Enter Patient Number: ");
            int patientNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Patient Personal Number: ");
            string personalNumber = Console.ReadLine();

            Console.Write("Enter Patient Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Patient Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Patient Email Address: ");
            string emailAddress = Console.ReadLine();

            Patient patient = new Patient
            {
                PatientNumber = patientNumber,
                Name = name,
                PersonalNumber = personalNumber,
                Address = address,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress
            };

            try
            {

                patientManagement.RegisterPatient(patient);

                Console.WriteLine("Patient registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        static void UpdatePatientInformation()
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("Option 2: Update Patient Info");

            Console.Write("Enter Patient Number: ");
            int patientNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Updated Patient Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Updated Patient Personal Number: ");
            string personalNumber = Console.ReadLine();

            Console.Write("Enter Updated Patient Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Updated Patient Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Updated Patient Email Address: ");
            string emailAddress = Console.ReadLine();


            Patient updatedPatient = new Patient
            {
                PatientNumber = patientNumber,
                Name = name,
                PersonalNumber = personalNumber,
                Address = address,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress
            };

            try
            {

                patientManagement.UpdatePatientInformation(updatedPatient);

                Console.WriteLine("Patient information updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        static void BookDoctorVisit()
        {
            Console.WriteLine("Option 3: Book Doctor Visit");
            int VisitNumber = Userinput.UserInt(" Enter visit number: ");
            int PatientNumber = Userinput.UserInt(" Enter Patient number: ");
            string VisitDateTime = Userinput.UserString("Enter date for doctor appointment according to: (YYYY-MM-DD: ");
            string Purpose = Userinput.UserString(" Enter visit purpose:");
            int DoctorEmployeeNumber = Userinput.UserInt(" Enter Doctor Employee Number: ");
            Console.WriteLine(" Doctor visit is booked! ");
        }




        static void RegisterDiagnosis()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine(" Option 4: Register Diagnosis ");

            Console.Write("Enter Patient Number: ");
            int patientNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Diagnosis Description: ");
            string diagnosisDescription = Console.ReadLine();

            Console.Write("Enter Diagnosis Date (YYYY-MM-DD): ");
            DateTime diagnosisDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Treatment Proposal: ");
            string treatmentProposal = Console.ReadLine();


            Diagnosis diagnosis = new Diagnosis
            {
                PatientNumber = patientNumber,
                DiagnosisDescription = diagnosisDescription,
                DiagnosisDate = diagnosisDate,
                TreatmentProposal = treatmentProposal
            };

            try
            {

                patientManagement.RegisterDiagnosis(diagnosis);

                Console.WriteLine("Diagnosis registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static void PrescribeTreatment()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("Option 5: Register Treatment");

            Console.Write("Enter Patient Number: ");
            int patientNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Medication: ");
            string medication = Console.ReadLine();

            Console.Write("Enter Dosage: ");
            string dosage = Console.ReadLine();


            Treatment treatment = new Treatment
            {
                PatientNumber = patientNumber,
                Medication = medication,
                Dosage = dosage,
                PrescriptionDate = DateTime.Now
            };

            try
            {

                patientManagement.PrescribeTreatment(treatment);

                Console.WriteLine("Treatment prescribed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}


