using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavViewInsideTabViewTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavViewRootPage : Page
    {
        MainPage hostPage;
        TabView hostTabView;

        public NavViewRootPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hostPage = e.Parameter as MainPage;
            hostTabView = hostPage.RootTabView;

            for (int i = 0; i < 3; i++)
            {
                var newNavItem = new muxc.NavigationViewItem() { Icon = new SymbolIcon(Symbol.Placeholder), Content = "Sample Page " + (i+1), Tag = "SamplePage" + (i + 1) };

                newNavItem.ContextFlyout = GetContextFlyout(newNavItem);

                rootNavView.MenuItems.Add(newNavItem);
            }

            if (hostPage.RequestedPageTag == null)
            {
                rootNavView.SelectedItem = rootNavView.MenuItems[0];
            }
            else
            {
                //NavigateToTag(rootNavView, hostPage.RequestedPageTag);

                foreach (muxc.NavigationViewItem item in rootNavView.MenuItems)
                {
                    if (item.Tag.Equals(hostPage.RequestedPageTag))
                    {
                        rootNavView.SelectedItem = item;
                        break;
                    }
                }
            }


            base.OnNavigatedTo(e);
        }


        private MenuFlyout GetContextFlyout(muxc.NavigationViewItem newNavItem)
        {
            var contextFlyout = new MenuFlyout();

            var openInNewTabItem = new MenuFlyoutItem() { Icon = new SymbolIcon(Symbol.NewWindow), Text = "Open in new tab", Tag = newNavItem };

            openInNewTabItem.Click += OpenInNewTabItem_Click;

            contextFlyout.Items.Add(openInNewTabItem);

            return contextFlyout;
        }

        private void OpenInNewTabItem_Click(object sender, RoutedEventArgs e)
        {
            var requestedPage = (((sender as MenuFlyoutItem).Tag as muxc.NavigationViewItem).Tag as string);
            hostPage.CreatNewTabAndAddToItems(requestedPage);
        }

        private void NavigationView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            //if (hostTabView.SelectedItem != null)
            //{
            //    (hostTabView.SelectedItem as TabViewItem).Header = (sender.SelectedItem as muxc.NavigationViewItem).Content.ToString();
            //}


            var selectedItem = args.SelectedItem as muxc.NavigationViewItem;
            string selectedItemTag = ((string)selectedItem.Tag);
            NavigateToTag(sender, selectedItemTag);
        }

        private void NavigateToTag(muxc.NavigationView sender, string selectedItemTag)
        {

            sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
            string pageName = "NavViewInsideTabViewTester." + selectedItemTag;
            Type pageType = Type.GetType(pageName);

            rootFrame.Navigate(pageType);


        }
    }
}
