using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddContractor(Contractor newContractor)
        {
            AllContractors.Add(newContractor);   
        }
        public void AddJob(Job newJob)
        {
            AllJobs.Add(newJob);
        }

        public Contractor Search (int id) 
        { 
            for (int i=0;i<AllContractors.Count;++i) 
            {
                if (AllContractors[i].ID == id) 
                { 
                    return AllContractors[i];
                }
            }
            return null;    
        }
    }
}
