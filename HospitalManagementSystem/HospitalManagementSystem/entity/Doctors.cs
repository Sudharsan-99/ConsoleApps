using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.Exceptions;

namespace HospitalManagementSystem.entity
{
    public class Doctors
    {
        private int _doctorID;
        private string _firstname;
        private string _lastname;
        private string _specialization;
        private string _phoneNumber;

        public int DoctorID
        {
            get { return _doctorID; }
            set { _doctorID = value; }
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

        public string Specialization
        {
            get { return _specialization; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InValidDataException("Specialization should not be null");
                }
                _specialization = value;
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

        public Doctors(string firstname,string lastname,string specialization,string phonenumber)
        {
            FirstName = firstname;
            LastName = lastname;
            Specialization = specialization;
            PhoneNumber = phonenumber;
        }

    }
}
