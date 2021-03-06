﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using B4B.Phone.Resources;

namespace B4B.Phone
{
    public partial class DetailsPage : PhoneApplicationPage
    {
       public static int index = -1;

        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    //we are currently using the list index to display data. This may conflict with reordering features/sort functionality
                    index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];
                }
            }
        }

        private void editScheme(object sender, RoutedEventArgs e)
        {
            if (index != -1) { 
                DataContext = App.ViewModel.Items[index];
                NavigationService.Navigate(new Uri("/Views/GradeScheme.xaml?selectedItem=" + DataContext, UriKind.Relative));
            }
        }
    }
}