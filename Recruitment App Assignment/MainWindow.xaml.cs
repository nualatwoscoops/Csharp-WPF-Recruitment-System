using Recruitment_App_Assignment.Data;
using System;
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
        RecruitmentSystem manager;

        public MainWindow()
        {
            InitializeComponent();
            Contractors_ListBox.ItemsSource = manager.GetAllContractors();
            
        }
        public List<decimal> StandardHourlyRates { get; } = new List<decimal>
        {
            50.00m, 
            60.00m,
            75.00m,
            100.00m
        };

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
    }
}
