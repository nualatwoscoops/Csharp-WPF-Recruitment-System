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
        [TestMethod]
        public void TestRemoveContractor()
        {
            // Arrange
            var contractor1 = new Contractor();
            var contractor2 = new Contractor();
            var recruitmentSystem = new RecruitmentSystem();

            // Act
            recruitmentSystem.AddContractor(contractor1);
            recruitmentSystem.AddContractor(contractor2);
            recruitmentSystem.RemoveContractor(contractor1);

            // Assert
            Assert.AreEqual(1, recruitmentSystem.AllContractors.Count);
        }
        [TestMethod]
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
        [TestMethod]
        //checks if contractor assigns to correct job

        public void TestContractorAssigns()
        {
            //Arrange
            var contractor = new Contractor("Obi", "Wan", DateTime.Now, 100.0m);
            var job = new Job();

            var recruitmentsystem = new RecruitmentSystem();

            //Act
            recruitmentsystem.AssignContractorToJob(contractor, job);

            //Assert
            Assert.AreEqual(contractor, job.ContractorAssigned);
            Assert.AreEqual(true, contractor.IsAssigned);
                        
            }
        [TestMethod]
        //tests if no contractor is assigned/null handles

        public void CompleteJob()
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
            Assert.IsTrue(job.IsCompleted == true);
            Assert.IsNull(contractor.IsAssigned);
        }
        [TestMethod]
        //Checks that assigning a contractor doesn't complete a job

        public void TestAssignWontCompleteJob()
        {
            //Arrange
            var job = new Job();
            var contractor = new Contractor("C3", "PO", DateTime.Now, 100.0m);
            job.IsCompleted = false;

            var recruitmentsystem = new RecruitmentSystem();

            //Act
            recruitmentsystem.AssignContractorToJob(contractor, job);

            //Assert
            Assert.IsTrue(job.IsCompleted == false);
            Assert.AreEqual(contractor, job.ContractorAssigned);
            Assert.AreEqual(true, contractor.IsAssigned);

        }
        [TestMethod]
        //checks if filtered jobs can correctly return a list of unassigned jobs

        public void TestFilteredJobsUnassignedJobs()
        {
            //Arrange
            var job1 = new Job(1, "Destroy All Jedi", 500);
            var job2 = new Job(2, "Fix Xwing", 700);
            var job3 = new Job(3, "Painting", 800);

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
            var result = recruitmentsystem.FilterJobs("Unassigned Jobs");

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Contains(job2));

        }
        [TestMethod]
        //Checks if search cost method handles correctly 

        public void TestSearchJobsByCost()
        {
            var job = new Job(1, "Destroy All Jedi", 500);
            var job2 = new Job(2, "Fix Xwing", 700);
            var job3 = new Job(3, "Breathwork", 800);

            var recruitmentsystem = new RecruitmentSystem();

            recruitmentsystem.AllJobs.Add(job);
            recruitmentsystem.AllJobs.Add(job2);
            recruitmentsystem.AllJobs.Add(job3);  

            //Act 
            var result = recruitmentsystem.SearchJobsByCost(600, 900);

            //Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(job2));
            Assert.IsTrue(result.Contains(job));

        }
    }
}
