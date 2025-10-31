using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


    public class Job
    {
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public decimal AgreedCost { get; set; }
        public bool IsCompleted { get; set; }
        
        
    public Job(int jobID, string jobTitle, decimal agreedCost)
    {
        jobID = jobID;
        JobTitle = jobTitle;
        AgreedCost = agreedCost;
        IsCompleted = false;
    }
    public override string ToString()
    {
        return $"[{JobID}] {JobTitle} (${AgreedCost:N2})";
    }
}

