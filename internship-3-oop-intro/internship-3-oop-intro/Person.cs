using System;
using System.Collections.Generic;
using System.Text;

namespace internship_3_oop_intro
{
    public class Person
    {
        public Person(string firstName, string lastName, int oib, int phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
            PhoneNumber = phoneNumber;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OIB { get; set; }
        public int PhoneNumber { get; set; }
    }
}

