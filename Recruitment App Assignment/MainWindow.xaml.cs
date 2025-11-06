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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecruitmentSystem manager;

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
        private void Button_AddContractor_Click(object sender, RoutedEventArgs e)
        {
            Contractor newContractor = new Contractor();
            newContractor.FirstName = FirstNameBox.Text;
            newContractor.LastName = LastNameBox.Text;
            newContractor.HourlyRate = (decimal)HourlyRate_ComboBox.SelectedItem;
            newContractor.StartDate = StartDatePicker.SelectedDate ?? DateTime.Today;
            manager.AddContractor(newContractor);
            //Contractors_ListData.Items.Refresh();
            Contractors_ListBox.ItemsSource = manager.GetAllContractors();
        }

        private void Button_Load_Contractors_Click(object sender, RoutedEventArgs e)
        {
            Contractors_ListBox.ItemsSource = manager.GetAllContractors();
            //foreach (Contractor contractor in manager.GetAllContractors())
            //{
            //    Contractors_ListBox.Items.Add(contractor);
            //}
        }

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

        public List<Job> JobList = new List<Job>();

        private int nextJobID = 1;
        private void Button_AddJob_Click(object sender, RoutedEventArgs e)
        {
            string jobTitle = JobTitleBox.Text.Trim();
            string costText = AgreedCostBox.Text.Trim();
            decimal agreedCost = 0;

            Job newJob = new Job();
            newJob.JobID = nextJobID++;
            newJob.JobTitle = jobTitle;
            newJob.AgreedCost = agreedCost;

            manager.AddJob(newJob);
            Jobs_ListBox.ItemsSource = manager.AllJobs;

        }

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

        private void Button_SearchCost_Click(object sender, RoutedEventArgs e)
        {
            decimal minCost = 0m;
            decimal maxCost = 0m;

            List<Job> searchResults = manager.SearchJobsByCost(minCost, maxCost);

            SearchResults_ListBox.ItemsSource = searchResults;
        }
    }
}
