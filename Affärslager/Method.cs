
using Datalager;
using Entitetlager;
using System;
using System.Collections.Generic;

namespace PatientManagementAffärslager
{
    //The main class representing the patient management system
    public class PatientManagement
    {
        //Properity to store the currently logged-in employee
        public Employee LoggedInEmployee { get; private set; }
        //Instance of unitofwork for managing data access operations
        private UnitOfWork unitOfWork;

        //Constructor initializing the PatientManagement instans
        public PatientManagement()
        {
            //Initialize UnitOfWork
            unitOfWork = new UnitOfWork();
            unitOfWork.FillDocktorVisits();
        }
        //Method to log in an employee

        public bool LogInEmployee(int employeeID, string password)
        {
            //Retrieve employee from the reposiyory based on the employee ID
            Employee employee = unitOfWork.EmployeeRepository.FirstOrDefault(e => e.EmployeeID == employeeID);
            //Check if the employee exists and the provided password is correct
            if (employee != null && employee.VerifyPassword(password))
            {
                //Set the logged-in employee and return true
                LoggedInEmployee = employee;
                return true;
            }
            //If login fails, set LoggedInEmployee to null and return salse
            LoggedInEmployee = null;
            return false;
        }
        //Method to register a new patient
        public void RegisterPatient(Patient patient)
        {
            // Add the patient to the repoditory and save changes
            unitOfWork.PatientRepository.Add(patient);
            unitOfWork.Save();
        }

        // Method to update patient information
        public void UpdatePatientInformation(Patient updatedPatient)
        {

            //  Check if an employee is logged in
            if (LoggedInEmployee == null)
            {
                throw new ApplicationException("There isn't anyone logged in.");
            }

            // Check if the logged-in employee is authorized to update patient information
            if (LoggedInEmployee.EmployeeID != updatedPatient.PatientNumber)
            {
                throw new ApplicationException("You can only update patient information as an employee.");
            }

            // Retrieve the existing patient based on patient number

            Patient existingPatient = unitOfWork.PatientRepository.FirstOrDefault(p => p.PatientNumber == updatedPatient.PatientNumber);

            // Check if the patient exists

            if (existingPatient != null)
            {
                // Update patient information and save changes

                existingPatient.Name = updatedPatient.Name;
                existingPatient.PersonalNumber = updatedPatient.PersonalNumber;
                existingPatient.Address = updatedPatient.Address;
                existingPatient.PhoneNumber = updatedPatient.PhoneNumber;
                existingPatient.EmailAddress = updatedPatient.EmailAddress;


                unitOfWork.PatientRepository.Update(existingPatient);
                unitOfWork.Save();
            }
            else
            {
                // Throw an exception if the patient is not found
                throw new ApplicationException("Patient not found.");
            }
        }


        // Method to book a doctor visit for the logged-in patient
        public void BookDoctorVisit(DoctorVisit visit)
        {
            // Check if an employee is logged in
            if (LoggedInEmployee == null)
            {
                throw new ApplicationException("There isn't anyone logged in.");
            }

            // Set the patient number for the doctor visit and add it to the repository
            visit.PatientNumber = LoggedInEmployee.EmployeeID;
            unitOfWork.DoctorVisitRepository.Add(visit);
            unitOfWork.Save();
        }

        public void CancelDoctorVisit(int visitNo)
        {
            // Check if an employee is logged in
            if (LoggedInEmployee == null)
            {
                throw new ApplicationException("There isn't anyone logged in.");
            }

            // Set the patient number for the doctor visit and add it to the repository
            DoctorVisit visit = unitOfWork.DoctorVisitRepository.FirstOrDefault(e => e.VisitNumber == visitNo);

            unitOfWork.DoctorVisitRepository.Remove(visit);
            unitOfWork.Save();
        }

        public DoctorVisit ViewDoctorVisit(int visitNo)
        {
            // Check if an employee is logged in
            if (LoggedInEmployee == null)
            {
                throw new ApplicationException("There isn't anyone logged in.");
            }

            DoctorVisit visit = unitOfWork.DoctorVisitRepository.FirstOrDefault(e => e.VisitNumber == visitNo);

            // Check if visit exists
            if (visit == null)
            {
                throw new ApplicationException("There isn't any visits booked with this number");
            }

            return visit;
        }




        // Method to register a diagnosis for the logged-in patient
        public void RegisterDiagnosis(Diagnosis diagnosis)
        {
            // Check if an employee is logged in
            if (LoggedInEmployee == null)
            {
                throw new ApplicationException("There isn't anyone logged in.");
            }

            // Set the patient number for the diagnosis and add it to the repository
            diagnosis.PatientNumber = LoggedInEmployee.EmployeeID;
            unitOfWork.DiagnosisRepository.Add(diagnosis);
            unitOfWork.Save();
        }
        // Method to prescribe treatment for the logged-in patient
        public void PrescribeTreatment(Treatment treatment)
        {
            // Check if an employee is logged in
            if (LoggedInEmployee == null)
            {
                throw new ApplicationException("There isn't anyone logged in.");
            }

            // Set the patient number for the treatment and add it to the repository
            treatment.PatientNumber = LoggedInEmployee.EmployeeID;
            unitOfWork.TreatmentRepository.Add(treatment);
            unitOfWork.Save();
        }


    }
}
