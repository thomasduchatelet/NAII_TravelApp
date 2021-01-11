using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.UwpApp.View;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainNavigation : Page
    {
        public MainNavigation()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(TripsOverview));
        }

        private void NavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem selectedItem = (NavigationViewItem)args.InvokedItemContainer;

            var tag = selectedItem.Tag as string;
            var pageType =
                tag == "trips" ? typeof(TripsOverview) :
                tag == "categories" ? typeof(Categories) :
                tag == "covid" ? typeof(CovidOverview) :
                tag == "logout" ? typeof(Login) : null;
            
            if (tag == "logout")
            {
                Frame.Navigate(pageType);
            }else
            {
                if (pageType != null && pageType != contentFrame.CurrentSourcePageType)
                {
                    contentFrame.Navigate(pageType);
                }
            }

           
        }
    }
}
