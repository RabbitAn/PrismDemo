using Prism.Regions;
using System.Windows;

namespace PrismDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRegionManager regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            this.regionManager = regionManager;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            regionManager.RequestNavigate("MainRegion", "HomeView");
            NavigationListBox.SelectedIndex = 0;
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MenuToggleButton.IsChecked = false;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
      
        
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
