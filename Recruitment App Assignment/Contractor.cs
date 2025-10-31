using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Initializes a new instance of the Contractor class
/// </summary>
class Contractor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public decimal HourlyRate { get; set; }

        public Contractor(string firstName, string lastName, DateTime startDate, decimal rate)
        {
            FirstName = firstName;
            LastName = lastName;
            StartDate = startDate;
            HourlyRate = rate;
        }
        

        public override string ToString()
        {
            return $"{FirstName} {LastName} (${HourlyRate}/hr)";
        }

    }
