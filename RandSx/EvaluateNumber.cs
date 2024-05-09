using System.Net.Mime;

namespace RandSx;

public class EvaluateNumber
{
    private CheckSettings _checkSettings;
    
    private List<int> _previousNumbers = new List<int>();
    
    public EvaluateNumber(CheckSettings checkSettings)
    {
        _checkSettings = checkSettings;
    }
    
    public Double EvaluateRandomNumber(int RandomNumber, int MinNumber, int MaxNumber)
    {
        int score = 0;
        bool[] settings = _checkSettings.GetSettings();
        if (settings[0])
        {
            if (_previousNumbers.Count > 0)
            {
                foreach (var prevNum in _previousNumbers)
                {
                    score += prevNum % RandomNumber;
                }
                
            }
        }
        if (settings[1])
        {
            
            char[] charArray = RandomNumber.ToString().ToCharArray();
            for (int i = 0; i >= charArray.Length - 1; i--)
            {
                score += charArray[i] % charArray[i + 1];
            }
            
        }
        return Double.Parse(RandomNumber.ToString());
    }

    public Double EvaluateRandomNumber(Double RandomNumber, Double MinNumber, Double MaxNumber)
    {
        bool[] settings = _checkSettings.GetSettings();
        
        
        return RandomNumber;
    }
}