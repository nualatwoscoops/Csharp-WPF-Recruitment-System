using Recruitment_App_Assignment.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using static System.Reflection.Metadata.BlobBuilder;

namespace TestProject2
{
    [TestClass]
    public sealed class testfile1
    {
        [TestMethod]
        public void TestNewContractor()
        {
            //Arrange
            var contractor = new Contractor();
            var recruitmentsystem = new RecruitmentSystem();

            //Act 
            recruitmentsystem.AddContractor(contractor);

            //Assert
            Assert.AreEqual(1, recruitmentsystem.AllContractors.Count);
        }
        
        public void TestRemoveContractor()
        {
            //Arrange
            var contractor = new Contractor();
            var recruitmentsystem = new RecruitmentSystem();
            
            recruitmentsystem.AddContractor(contractor);

            // Act
            recruitmentsystem.RemoveContractor(contractor);

            // Assert
            Assert.AreEqual(0, recruitmentsystem.AllContractors.Count);
        }
       
        //Checks if a job has been marked as completed/contractor IsAssigned = True/false
        public void TestJobComplete()
        {
            //Arrange
            
            var contractor1 = new Contractor("Bob", "Ross", DateTime.Now, 50.0m);
            contractor1.IsAssigned = true;

            var contractor2 = new Contractor("Darth", "Vadar", DateTime.Now, 60.0m);
            contractor2.IsAssigned = false;

            var job1 = new Job();
            job1.ContractorAssigned = contractor1;

            var job2 = new Job();
            job2.ContractorAssigned = contractor1;


            var recruitmentsystem = new RecruitmentSystem();
            recruitmentsystem.AllJobs.Add(job1);
            recruitmentsystem.AllJobs.Add(job2);

            //Act
            recruitmentsystem.CompleteJob(job1);

            //Assert
            Assert.AreEqual(false, contractor1.IsAssigned);
            Assert.AreEqual(true, contractor2.IsAssigned);

        }
        
        //checks if contractor assigns to correct job

        public void TestContractorAssigns()
        {
            //Arrange
            var contractor = new Contractor("Obi", "Wan" DateTime.Now, 100.0m);
            var job = new Job();

            var recruitmentsystem = new RecruitmentSystem();

            //Act
            recruitmentsystem.AssignContractorToJob(job, contractor);

            //Assert
            Assert.AreEqual(contractor, job.ContractorAssigned);
            Assert.AreEqual(true, contractor.IsAssigned);
                        
            }

        //tests if no contractor is assigned/null handles

        public void TestNoAssignedContractor()
        {
            //Arrange
            var job = new Job();
            var contractor = new Contractor();
            job.ContractorAssigned = null;
            job.ContractorAssigned = contractor;

            var recruitmentsystem = new RecruitmentSystem();

            //Act
            recruitmentsystem.CompleteJob(job);


            //Assert
            Assert.AreEqual(job.IsCompleted == true);
            Assert.AreEqual(contractor.IsAssigned == null);
        }
        //Checks that assigning a contractor doesn't complete a job

        public void TestAssignWontCompleteJob()
        {
            var job = new Job();
            var contractor = new Contractor();
            job.IsCompleted = false;

            var recruitmentsystem = new RecruitmentSystem();

            //Act
            recruitmentsystem.AssignContractorToJob();

            //Assert
            Assert.AreEqual(job.IsCompleted == false);
            Assert.AreEqual(contractor, job.ContractorAssigned);
            Assert.AreEqual(true, contractor.IsAssigned);

        }

        //checks if filtered jobs can correctly return a list of unassigned jobs

        public void TestFilteredJobsUnassignedJobs()
        {

            var job1 = new Job("Destroy All Jedi", 500);
            var job2 = new Job("Fix Xwing", 700);
            var job3 = new Job("Painting", 800);

            var contractor1 = new Contractor("Darth", "Vader", DateTime.Now, 100.0m);
            var contractor2 = new Contractor("Obi", "Wan", DateTime.Now, 100.0m);
            var contractor3 = new Contractor("Bob", "Ross", DateTime.Now, 50.0m);

            var recruitmentsystem = new RecruitmentSystem();

            recruitmentsystem.AssignContractorToJob(contractor1, job1);
            recruitmentsystem.AssignContractorToJob(contractor3, job3);

            recruitmentsystem.AllJobs.Add(job1);
            recruitmentsystem.AllJobs.Add(job2);
            recruitmentsystem.AllJobs.Add(job3);


            //Act
            var result = RecruitmentSystem.FilterJobs("Unassigned Jobs");

            //Assert
            Assert.IsTrue(2, result.Count);

        }

        //Checks if search cost method handles correctly 

        public void TestSearchJobsByCost()
        {
            var job = new Job("Destroy All Jedi", 500);
            var job2 = new Job("Fix Xwing", 700);
            var job3 = new Job("Breathwork", 800);

            var recruitmentsystem = new RecruitmentSystem();

            recruitmentsystem.AllJobs.Add(job);

            //Act 
            recruitmentsystem.SearchJobsByCost(minCost, 600 maxCost, 900);

            //Assert
            Assert.IsTrue(2, result.Count);

        }
    }
}
