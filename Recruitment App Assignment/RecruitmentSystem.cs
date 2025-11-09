using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Recruitment_App_Assignment.Data

    // Manages all contractor and job data for the recruitment system
{
    public class RecruitmentSystem
    {
        //holds all contractor objects in system
        public List<Contractor> AllContractors { get; set; } = new List<Contractor>();

        //holds all job objects in system 
        public List<Job> AllJobs { get; set; } = new List<Job>();


        // Retrieves and returns List of contractors in the system
        public List<Contractor> GetAllContractors()
            { return AllContractors; }

        //add and remove contractors here
        public void RemoveContractor(Contractor oldContractor)
        {
            AllContractors.Remove(oldContractor);
        }
        public void AddContractor(Contractor Contractor)
        {
            AllContractors.Add(Contractor);   
        }

        // Adds jobs
        public void AddJob(Job newJob)
        {
            AllJobs.Add(newJob);
        }

        // Marks an assigned job as completed and then frees up 
        
        public void CompleteJob(Job jobToComplete)
        {
            jobToComplete.IsCompleted = true;
            if (jobToComplete.ContractorAssigned != null)
            {
                jobToComplete.ContractorAssigned.IsAssigned = false;
            }
        }
        // Assigns a contractor to a job.
        public void AssignContractorToJob(Contractor contractor, Job job)
        {
            job.ContractorAssigned = contractor;
            contractor.IsAssigned = true;
        }

        // TODO: Return contractors to list of jobs

        //public List<Contractor> GetAvailableContractors()
        //{
        //    List<Contractor> availableContractors = new List<Contractor>();

        //    foreach (Contractor contractor in AllContractors)
        //    { 
        //        if (contractor.IsAvailable == true) 
        //        {
        //            availableContractors.Add(contractor);
        //        }
        //    }
        //    return availableContractors; 
        //}

        //public List<Job> GetUnassignedJobs()
        //{
        //    List<Job> unassignedJobs = new List<Job>();

        //    foreach (Job job in AllJobs)
        //    {
        //        if(job.AssignedWorker == true)
        //        {  
        //            unassignedJobs.Add(job); 
        //        }
        //    }
        //    return unassignedJobs;
        //}
        //public Contractor Search (int id) 
        //{ 
        //    for (int i=0;i<AllContractors.Count;++i) 
        //    {
        //        if (AllContractors[i].ID == id) 
        //        { 
        //            return AllContractors[i];
        //        }
        //    }
        //    return null;    
        //}

        //FindAll will go through all and find all that are within the given range

        public List<Job> SearchJobsByCost(decimal minCost, decimal maxCost)
        {
            return AllJobs.FindAll((job) =>
            {
                return job.AgreedCost >= minCost && job.AgreedCost <= maxCost;
            });
        }
        
    }
}
