using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandSx;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void btnKoFi_Click(object sender, RoutedEventArgs e)
    {
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "https://ko-fi.com/sxwxbxr",
            UseShellExecute = true
        };
        System.Diagnostics.Process.Start(psi);
    }
    
    private void btnRandSx_Click(object sender, RoutedEventArgs e)
    {
        if (WelcomeScreen.Visibility == Visibility.Visible)
        {
            WelcomeScreen.Visibility = Visibility.Collapsed;
            NumberRangePanel.Visibility = Visibility.Visible;
        }
        else if (SettingsPanel.Visibility == Visibility.Visible)
        {
            SettingsPanel.Visibility = Visibility.Collapsed;
            NumberRangePanel.Visibility = Visibility.Visible;
        }
        else
        {
            WelcomeScreen.Visibility = Visibility.Visible;
            NumberRangePanel.Visibility = Visibility.Collapsed;
        }

    }
    private void btnSettings_Click(object sender, RoutedEventArgs e)
    {
        if (WelcomeScreen.Visibility == Visibility.Visible)
        {
            WelcomeScreen.Visibility = Visibility.Collapsed;
            SettingsPanel.Visibility = Visibility.Visible;
        }
        else if (NumberRangePanel.Visibility == Visibility.Visible)
        {
            NumberRangePanel.Visibility = Visibility.Collapsed;
            SettingsPanel.Visibility = Visibility.Visible;
        }
        else
        {
            WelcomeScreen.Visibility = Visibility.Visible;
            SettingsPanel.Visibility = Visibility.Collapsed;
        }
    }
    
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        bool isValid = IsTextAllowed(e.Text);
        e.Handled = !isValid;
    }

    private static bool IsTextAllowed(string text)
    {
        Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        return !regex.IsMatch(text);
    }
    
    private void btnSubmit_Click(object sender, RoutedEventArgs e)
{
    // Get the values from the textboxes
    int number = int.Parse(txtNumber.Text);
    int minNumber = int.Parse(txtMin.Text);
    int maxNumber = int.Parse(txtMax.Text);

    // TODO: Submit the values for further processing

    // Clear the textboxes
    txtNumber.Text = "";
    txtMin.Text = "";
    txtMax.Text = "";

    // Clear the Grid
    RandomnessResults.Children.Clear();
    RandomnessResults.RowDefinitions.Clear();

    // Add the definitions of the results to the Grid
    if (chkMetric1.IsChecked == true)
    {
        AddResultRow("Time to input the number:");
    }
    if (chkMetric4.IsChecked == true)
    {
        AddResultRow("Similarity with the previous numbers:");
    }
    if (chkMetric5.IsChecked == true)
    {
        AddResultRow("Similarity between consecutive numbers:");
    }

    // Add a row for the overall score
    AddResultRow("Overall score:");
}

private void AddResultRow(string definition)
{
    // Create a new row for the Grid
    var rowDefinition = new RowDefinition();
    RandomnessResults.RowDefinitions.Add(rowDefinition);

    // Create a TextBlock for the definition and add it to the Grid
    var textBlock = new TextBlock
    {
        Text = definition,
        Foreground = new SolidColorBrush(Colors.Black),
        FontSize = 16
    };
    Grid.SetRow(textBlock, RandomnessResults.RowDefinitions.Count - 1);
    Grid.SetColumn(textBlock, 0);
    RandomnessResults.Children.Add(textBlock);

    // Create a TextBlock for the result and add it to the Grid
    var resultTextBlock = new TextBlock
    {
        Text = "", // The result will be set here
        Foreground = new SolidColorBrush(Colors.Black),
        FontSize = 16
    };
    Grid.SetRow(resultTextBlock, RandomnessResults.RowDefinitions.Count - 1);
    Grid.SetColumn(resultTextBlock, 1);
    RandomnessResults.Children.Add(resultTextBlock);
}
}