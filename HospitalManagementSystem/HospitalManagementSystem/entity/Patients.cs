using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.Exceptions;

namespace HospitalManagementSystem.entity
{
    public class Patients
    {
        //Fields
        private int _patientID;
        private string _firstname;
        private string _lastname;
        private DateTime _dob;
        private string _gender;
        private string _phoneNumber;
        private string _address;

        private string[] Allowedgenders = { "Male", "Female", "Others" };

        //Properties
        public int PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        public string FirstName
        {
            get { return _firstname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InValidDataException("First name Should Not be Null");
                }
                _firstname = value;
            }
        }

        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        public DateTime Dob
        {
            get { return _dob; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new InValidDataException("Date of Birth Should not be Greater Than Today");
                }
                _dob = value;
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                if (!Allowedgenders.Contains(value))
                {
                    throw new InValidDataException("Gender Should Not be Null");
                }
                _gender = value;
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value.Length != 10 || !value.All(char.IsDigit))
                {
                    throw new InValidDataException("PhoneNumber Should Contain 10 numbers");
                }
                _phoneNumber = value;
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new InValidDataException("Address should not be Null");
                }
                _address = value;
            }
        }

        public Patients(string firstname,string lastname,DateTime dob ,string gender,string phonenumber,string address)
        {
            FirstName = firstname;
            LastName = lastname;
            Dob = dob;
            Gender = gender;
            PhoneNumber = PhoneNumber;
            Address = address;
        }

    }
}
