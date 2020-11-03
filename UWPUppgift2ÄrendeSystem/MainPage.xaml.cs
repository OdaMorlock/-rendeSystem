using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccess.Data;
using DataAccess.Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPUppgift2ÄrendeSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private IEnumerable<Issue> issues { get; set; }
        private int _customerId {get;set;}

        public MainPage()
        {
            this.InitializeComponent();
            LoadIssuesAsync().GetAwaiter();
        }


      private async Task LoadIssuesAsync()
        {
            issues = await SqliteContext.GetIssuesAsync();

             LoadActiveIssueAsync();
             LoadClosedIssueAsync();
        }

        

        private void LoadActiveIssueAsync()
        {
            LvActiveIssues.ItemsSource = issues.Where(i => i.Status != "closed").ToList(); 
        }

        private void LoadClosedIssueAsync()
        {
            LvClosedIssues.ItemsSource = issues.Where(i => i.Status == "closed").ToList();
        }

        private async void CreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            
           _customerId =  await SqliteContext.CreateCustomerAsync(new Customer {Name = "MArcus" });
        }

        private async void CreateCase_Click(object sender, RoutedEventArgs e)
        {
            await SqliteContext.CreateIssueAsync(
                new Issue
                {
                    Title = "Ett ärende",
                    Description = "Detta är ett ärende",
                    CustomerId = _customerId

                }

                );

           await  LoadIssuesAsync();
        }
    }
}
