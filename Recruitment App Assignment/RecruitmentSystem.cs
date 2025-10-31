using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Recruitment_App_Assignment.Data
{
    public class RecruitmentSystem
    {
        //holds all contractor objects 
        public List<Contractor> AllContractors { get; set; } = new List<Contractor>();

        //holds all jobs which the agency manages
        public List<Job> AllJobs { get; set; } = new List<Job>();

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
        public void AddJob(Job newJob)
        {
            AllJobs.Add(newJob);
        }

        //public void CompleteJob(Job jobToComplete)
        //{
        //    jobToComplete.IsCompleted = true;
        //    if (jobToComplete.AssignedWorker != null)
        //    {
        //        jobToComplete.AssignedWorker.IsAvailable = true;
        //    }
        //}

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

        //FindAll will go through all and find all true - can change to cost later
        
        //public List<Contractor> Search (string name)
        //{
        //    return contractor.FindAll((x)=> { return x.ToString().Contains(name);  });
        //}

        //Use above for SearchJobsbyCost #8
    }
}
