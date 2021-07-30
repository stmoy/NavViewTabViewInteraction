using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NavViewInsideTabViewTester
{
    public sealed partial class MainPage : Page
    {
        public List<string> pages = new List<string>()
        {
            "SamplePage1",
            "SamplePage2",
            "SamplePage3"
        };


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

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();
                var splitText = sender.Text.ToLower().Split(" ");
                foreach (var page in pages)
                {
                    var found = splitText.All((key) =>
                    {
                        return page.ToLower().Contains(key);
                    });
                    if (found)
                    {
                        suitableItems.Add(page);
                    }
                }
                if (suitableItems.Count == 0)
                {
                    suitableItems.Add("No results found");
                }
                sender.ItemsSource = suitableItems;
            }

        }

        // TODO: Add the QuerySubmitted into the Xaml Controls Gallery - the ASB sample doesn't have the C# code...
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!args.ChosenSuggestion.Equals("No results found"))
            {
                CreatNewTabAndAddToItems(args.ChosenSuggestion.ToString());
            }
        }
    }
}
