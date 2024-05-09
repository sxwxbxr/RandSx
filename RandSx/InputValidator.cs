namespace RandSx;

public class InputValidator
{
    public bool IsInputEmpty(string input)
    {
        return string.IsNullOrWhiteSpace(input);
    }

    public bool CanParseToDouble(string input, out double result)
    {
        return double.TryParse(input, out result);
    }
}