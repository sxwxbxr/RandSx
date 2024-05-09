namespace RandSx;

public class CheckSettings
{
    public bool IsEnablePreviousNumbersChecked { get; set; }
    public bool IsEnableConsecutiveNumbersChecked { get; set; }
    
    public bool IsInternetConnectionEnabled { get; set; }
    
    public bool[] GetSettings()
    {
        return new bool[]
        {
            IsEnablePreviousNumbersChecked,
            IsEnableConsecutiveNumbersChecked,
            IsInternetConnectionEnabled
        };
    }
}