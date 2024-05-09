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
    private EvaluateNumber _evaluateNumber;
    
    public MainWindow()
    {
        InitializeComponent();

        
        CheckSettings checkSettings = new CheckSettings()
        {
            IsEnablePreviousNumbersChecked = settingEnablePreviousNumbers.IsChecked.Value,
            IsEnableConsecutiveNumbersChecked = settingEnableConsecutiveNumbers.IsChecked.Value,
            IsInternetConnectionEnabled = settingInternetConnection.IsChecked.Value
        };
        _evaluateNumber = new EvaluateNumber(checkSettings);
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

    private void ToggleVisibility(Visibility welcomeScreenVisibility, Visibility settingsPanelVisibility, Visibility numberRangePanelVisibility)
    {
        WelcomeScreen.Visibility = welcomeScreenVisibility;
        SettingsPanel.Visibility = settingsPanelVisibility;
        NumberRangePanel.Visibility = numberRangePanelVisibility;
    }

    private void btnRandSx_Click(object sender, RoutedEventArgs e)
    {
        if (WelcomeScreen.Visibility == Visibility.Visible || SettingsPanel.Visibility == Visibility.Visible)
        {
            ToggleVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible);
        }
        else
        {
            ToggleVisibility(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);
        }
    }

    private void btnSettings_Click(object sender, RoutedEventArgs e)
    {
        if (WelcomeScreen.Visibility == Visibility.Visible || NumberRangePanel.Visibility == Visibility.Visible)
        {
            ToggleVisibility(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);
        }
        else
        {
            ToggleVisibility(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);
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
    string numberText = txtNumber.Text;
    string minNumberText = txtMin.Text;
    string maxNumberText = txtMax.Text;

    // Set default values for empty fields
    numberText = string.IsNullOrWhiteSpace(numberText) ? "0" : numberText;
    minNumberText = string.IsNullOrWhiteSpace(minNumberText) ? "0" : minNumberText;
    maxNumberText = string.IsNullOrWhiteSpace(maxNumberText) ? "0" : maxNumberText;

    double number, minNumber, maxNumber;

    // Check if the inputs can be parsed to double or float, otherwise parse them as int
    if (double.TryParse(numberText, out number) && double.TryParse(minNumberText, out minNumber) && double.TryParse(maxNumberText, out maxNumber))
    {
        // Check if minNumber is smaller than maxNumber
        if (minNumber >= maxNumber)
        {
            // Display an error message to the user and highlight the faulty fields
            MessageBox.Show("The minimum number should be smaller than the maximum number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            txtMin.Background = Brushes.Red;
            txtMax.Background = Brushes.Red;
            return;
        }
        // Call the EvaluateRandomNumber method for double
        _evaluateNumber.EvaluateRandomNumber(number, minNumber, maxNumber);
    }
    else
    {
        // Parse the inputs as integers
        int intNumber, intMinNumber, intMaxNumber;
        if (int.TryParse(numberText, out intNumber) && int.TryParse(minNumberText, out intMinNumber) && int.TryParse(maxNumberText, out intMaxNumber))
        {
            // Check if minNumber is smaller than maxNumber
            if (intMinNumber >= intMaxNumber)
            {
                // Display an error message to the user and highlight the faulty fields
                MessageBox.Show("The minimum number should be smaller than the maximum number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMin.Background = Brushes.Red;
                txtMax.Background = Brushes.Red;
                return;
            }
            
            // Call the EvaluateRandomNumber method for int
            _evaluateNumber.EvaluateRandomNumber(intNumber, intMinNumber, intMaxNumber);
        }
        else
        {
            // Display an error message to the user and highlight the faulty fields
            MessageBox.Show("Please enter valid numbers.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            txtNumber.Background = Brushes.Red;
            txtMin.Background = Brushes.Red;
            txtMax.Background = Brushes.Red;
        }
    }

    // Clear the textboxes
    txtNumber.Text = "";
    txtMin.Text = "";
    txtMax.Text = "";

    // Clear the Grid
    RandomnessResults.Children.Clear();
    RandomnessResults.RowDefinitions.Clear();

    // Add the definitions of the results to the Grid
    if (settingEnablePreviousNumbers.IsChecked == true)
    {
        AddResultRow("Similarity with the previous numbers:");
    }
    if (settingEnableConsecutiveNumbers.IsChecked == true)
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