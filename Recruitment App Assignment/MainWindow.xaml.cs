using Recruitment_App_Assignment.Data;
using System;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Recruitment_App_Assignment
{
    
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private RecruitmentSystem manager;

        // Provides list of hourly rates. 
        public List<decimal> StandardHourlyRates { get; } = new List<decimal>
            {
            00.00m,
            50.00m,
            60.00m,
            75.00m,
            100.00m
            };

        public MainWindow()
        {
            manager = new RecruitmentSystem();

            InitializeComponent();

            this.DataContext = this;
        }
        
        // CONTRACTOR MANAGEMENT

        // Handles logic for adding new Contracor, and input validation

        private void Button_AddContractor_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;

            if (string.IsNullOrEmpty(FirstNameBox.Text))
            {
                MessageBox.Show("Cannot be empty.", "Input Error");
                return;
            }
            if (string.IsNullOrEmpty(LastNameBox.Text))
            {
                MessageBox.Show("Cannot be empty.", "Input Error");
                return;
            }

            
            if (HourlyRate_ComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please make a valid selection.", "Input Error.");
                return;
            }
            
            if (StartDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please make a valid selection.", "Input Error.");
                return;
            }

            // TO DO: now that validation has been added new contractor object needs to be updated

            Contractor newContractor = new Contractor();
            newContractor.FirstName = FirstNameBox.Text;
            newContractor.LastName = LastNameBox.Text;
            newContractor.HourlyRate = (decimal)HourlyRate_ComboBox.SelectedItem;
            newContractor.StartDate = StartDatePicker.SelectedDate ?? DateTime.Today;
            manager.AddContractor(newContractor);
            //Contractors_ListData.Items.Refresh();
            Contractors_ListBox.ItemsSource = manager.GetAllContractors();
        }


        // Handles logic for loading Contractors to the list
        private void Button_Load_Contractors_Click(object sender, RoutedEventArgs e)
        {
            Contractors_ListBox.ItemsSource = manager.GetAllContractors();
            //foreach (Contractor contractor in manager.GetAllContractors())
            //{
            //    Contractors_ListBox.Items.Add(contractor);
            //}
        }

        // Handles logic for removing a Contractor
        private void Button_Remove_Contractors_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = Contractors_ListBox.SelectedItem;
            if (Contractors_ListBox.SelectedItem is Contractor selectedContractor)
            {
                manager.RemoveContractor(selectedContractor);
            }
            else
            {
                MessageBox.Show("Please select a contractor to remove first.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning); ;
            }
        }

        // JOB MANAGEMENT
        public List<Job> JobList = new List<Job>();

        private int nextJobID = 1;

        // Handles logic for adding new jobs, and input validation
        private void Button_AddJob_Click(object sender, RoutedEventArgs e)
        {
            string jobTitle = JobTitle_TextBox.Text.Trim();
            decimal agreedCost = 0m;

            if (string.IsNullOrEmpty(jobTitle))
            {
                MessageBox.Show("Job Title cannot be empty.", "Input Error");
                return;
            }
            //string costText = AgreedCostBox.Text.Trim();
            
            bool isValidCost = decimal.TryParse(AgreedCostBox.Text, out agreedCost);
            if (!isValidCost)
            {
                MessageBox.Show("Please enter a valid number.", "Input Error");
            }

            Job newJob = new Job();
            newJob.JobID = nextJobID++;
            newJob.JobTitle = jobTitle;
            newJob.AgreedCost = agreedCost;

            manager.AddJob(newJob);
            Jobs_ListBox.ItemsSource = manager.AllJobs;

        }

        // Shows selected job is complete or not
        private void Button_CompleteJob_Click(object sender, RoutedEventArgs e)
        {
            if (Jobs_ListBox.SelectedItem is Job selectedJob)
            {
                if (selectedJob.ContractorAssigned != null)
                {
                    manager.CompleteJob(selectedJob);
                    Jobs_ListBox.ItemsSource = manager.AllJobs;
                    Contractors_ListBox.ItemsSource = manager.AllContractors;
                    MessageBox.Show($"Job {selectedJob.JobID}: {selectedJob.JobTitle})marked as complete. Contractor {selectedJob.ContractorAssigned.FirstName} is now available.", "Job Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("This job does not have an assigned contractor.", "Assignment required", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a job to complete.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Handles assigning a selected contractor to a selected job
        // TO DO: Validation
        private void Button_AssignContractor_Click(object sender, RoutedEventArgs e)
        {
            if (Contractors_ListBox.SelectedItem is Contractor selectedContractor)
            {
                if (Jobs_ListBox.SelectedItem is Job selectedJob)
                {
                    manager.AssignContractorToJob(selectedContractor, selectedJob);
                    Contractors_ListBox.ItemsSource = manager.AllContractors;
                    Jobs_ListBox.ItemsSource = manager.AllJobs;
                    MessageBox.Show("Contractor Successfully Assigned.");

                }
            }

        }
        // Searches and filters jobs 
        // TO DO: validation
        private void Button_SearchCost_Click(object sender, RoutedEventArgs e)
        {
            decimal minCost = 0m;
            decimal maxCost = 0m;

            List<Job> searchResults = manager.SearchJobsByCost(minCost, maxCost);

            SearchResults_ListBox.ItemsSource = searchResults;
        }

        //private void AgreedCostBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
