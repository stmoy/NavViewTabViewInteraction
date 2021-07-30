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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NavViewInsideTabViewTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public TabView RootTabView;

        public string RequestedPageTag = null;

        public MainPage()
        {
            this.InitializeComponent();

            RootTabView = rootTabView;
        }

        private void rootTab_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1; i++)
            {
                (sender as TabView).TabItems.Add(CreateNewTab(i));
            }

        }

        public void CreatNewTabAndAddToItems(string requestedPageTag)
        {
            RequestedPageTag = requestedPageTag;

            var newTab = CreateNewTab(0);
            rootTabView.TabItems.Add(newTab);
            rootTabView.SelectedItem = newTab;
        }

        private TabViewItem CreateNewTab(int index)
        {
            TabViewItem newItem = new TabViewItem();

            newItem.Header = "XCG";

            newItem.IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document };

            // The content of the tab is often a frame that contains a page, though it could be any UIElement.
            Frame frame = new Frame();

            frame.Navigate(typeof(NavViewRootPage), this);

            newItem.Content = frame;

            return newItem;
        }

        private void rootTab_AddTabButtonClick(TabView sender, object args)
        {
            var newItem = CreateNewTab(sender.TabItems.Count);
            sender.TabItems.Add(newItem);
            sender.SelectedItem = newItem;
        }

        private void rootTab_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            if (sender.TabItems.Count > 1)
                sender.TabItems.Remove(args.Tab);
        }
    }
}
