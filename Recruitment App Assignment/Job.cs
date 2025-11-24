using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// Represents a specific job within Recruitment system

public class Job
{
    public int JobID { get; set; }
    public string JobTitle { get; set; }
    public decimal AgreedCost { get; set; }
    public bool IsCompleted { get; set; }

    //added default parameterless constuctor 
    public Job() { }

    // Initialises default instance of job class
    public Job(int jobID, string jobTitle, decimal agreedCost)

    // Initialises parameterised instance of job class
    {
        this.JobID = jobID;
        this.JobTitle = jobTitle;
        this.AgreedCost = agreedCost;
        this.IsCompleted = false;
        this.ContractorAssigned = null;
    }

    public string JobInfo 
    {
        get
        {
            string cost = AgreedCost.ToString();
            string status = ContractorAssigned == null ? "Unassigned" : $"Assigned to {ContractorAssigned.FirstName}";
            return $"{JobTitle}, Cost: {cost}, Status: {status}";
        }
    
    }

    // Returns string to represent current job with ID, title and cost
    public override string ToString()
    {
        return $"[{JobID}] {JobTitle} (${AgreedCost:N2})";
    }

    public Contractor? ContractorAssigned { get; set; }
}

   

