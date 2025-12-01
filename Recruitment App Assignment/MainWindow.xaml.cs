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

        private void RefreshAllLists() 
        {
            Jobs_ListBox.ItemsSource = null;
            Jobs_ListBox.ItemsSource = manager.AllJobs;
        }

        // CONTRACTOR MANAGEMENT

        // Handles logic for adding new Contracor, and input validation
        private void Button_AddContractor_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;

            if (string.IsNullOrEmpty(FirstNameBox.Text))
            {
                MessageBox.Show("First name cannot be empty.", "Input Error");
                return;
            }
            if (string.IsNullOrEmpty(LastNameBox.Text))
            {
                MessageBox.Show("Last name cannot be empty.", "Input Error");
                return;
            }
            if (HourlyRate_ComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please make a valid selection for Hourly Rate.", "Input Error.");
                return;
            }
            if (StartDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please make a valid selection for date.", "Input Error.");
                return;
            }
            Contractor newContractor = new Contractor();
            {
                newContractor.FirstName = FirstNameBox.Text;
                newContractor.LastName = LastNameBox.Text;
                newContractor.HourlyRate = (decimal)HourlyRate_ComboBox.SelectedItem;
                newContractor.StartDate = StartDatePicker.SelectedDate ?? DateTime.Today;
            }
            ;

            manager.AddContractor(newContractor);

            Contractors_ListBox.ItemsSource = manager.GetAllContractors();
            FirstNameBox.Clear();
            LastNameBox.Clear();
            HourlyRate_ComboBox.SelectedItem = null;
            StartDatePicker.SelectedDate = null;
            Contractors_ListBox.Items.Refresh();

        }

        // UPDATED: (DELETE?) Handled logic for loading Contractors to the list 
        //private void Button_Load_Contractors_Click(object sender, RoutedEventArgs e)
        //{
        //    Contractors_ListBox.ItemsSource = manager.GetAllContractors();
        //    //foreach (Contractor contractor in manager.GetAllContractors())
        //    //{
        //    //    Contractors_ListBox.Items.Add(contractor);
        //    //}
        //    Contractors_ListBox.Items.Refresh();
        //}

        // Handles logic for removing a Contractor
        private void Button_Remove_Contractors_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = Contractors_ListBox.SelectedItem;
            if (Contractors_ListBox.SelectedItem is Contractor selectedContractor)
            {
                manager.RemoveContractor(selectedContractor);
                Contractors_ListBox.ItemsSource = manager.GetAllContractors();
                Contractors_ListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a contractor to remove first.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning); ;
            }
        }

        //Handles Contractor Filter Combo Box 
        private void ContractorFilter_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((int)ContractorFilter_ComboBox.SelectedIndex == -1)
            {
                return;
            }
         
            string selectedFilter = (ContractorFilter_ComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            List<Contractor> filteredContractors = manager.FilterContractors(selectedFilter);
            Contractors_ListBox.ItemsSource = filteredContractors;
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
            
            bool isValidCost = decimal.TryParse(AgreedCostBox.Text, out agreedCost);

            if (!isValidCost || agreedCost <= 0)
            {
                MessageBox.Show("Please enter a valid number for the Agreed Cost.", "Input Error");
                return;
            }

            Job newJob = new Job();
            newJob.JobID = nextJobID++;
            newJob.JobTitle = jobTitle;
            newJob.AgreedCost = agreedCost;

            manager.AddJob(newJob);
            RefreshAllLists();
            JobTitle_TextBox.Clear();
            AgreedCostBox.Clear();

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
        // Searches and filters jobs by cost
        private void Button_SearchCost_Click(object sender, RoutedEventArgs e)
        {
            decimal minCost = 0m;
            decimal maxCost = 0m;

            bool isMinEntered = !string.IsNullOrWhiteSpace(MinCost_TextBox.Text);

            if(isMinEntered)
            {
                if (!decimal.TryParse(MinCost_TextBox.Text, out minCost) || minCost < 0)
                {
                   MessageBox.Show("Please enter a valid number for Minimum Cost.", "Input Error");
                   return;
                }
            }

                else 
                {
                    minCost = 0m;
                }
            bool isMaxEntered = !string.IsNullOrWhiteSpace(MaxCost_TextBox.Text);

                if (isMaxEntered)
                {
                    if (!decimal.TryParse(MaxCost_TextBox.Text, out maxCost) || maxCost < 0)
                    {
                       MessageBox.Show("Please enter a valid number for Minimum Cost.", "Input Error");
                       return;
                    }
                }
                else
                {
                    maxCost = decimal.MaxValue;
                }

            if (minCost > maxCost) 
            {
                MessageBox.Show("Minimum Cost cannot be greater than Maximum Cost.", "Input Error.");
                return;
            }
            
            List<Job> searchResults = manager.SearchJobsByCost(minCost, maxCost);

            SearchResults_ListBox.ItemsSource = searchResults;
        }

        //Handles Job Filter Combo Box 
        private void JobFilter_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JobFilter_ComboBox.SelectedIndex == -1)
            {
                return;
            }

            if (JobFilter_ComboBox.SelectedItem is int selectedIndex && selectedIndex == -1)
            {
                return;
            }
            string selectedFilter = (JobFilter_ComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            
            if (string.IsNullOrEmpty(selectedFilter))
            {
                return;
            }
            
            List<Job> filteredJobs = manager.FilterJobs(selectedFilter);
            Jobs_ListBox.ItemsSource = filteredJobs;
        }

        private void HourlyRate_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
