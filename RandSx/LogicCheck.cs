using System.Net.Mime;

namespace RandSx;

public class LogicCheck
{
    public Double EvaluateRandomNumber(int RandomNumber, int MinNumber, int MaxNumber)
    {

        return Double.Parse(RandomNumber.ToString());
    }

    public Double EvaluateRandomNumber(Double RandomNumber, Double MinNumber, Double MaxNumber)
    {
        return RandomNumber;
    }
    
    public Double EvaluateRandomNumber(float RandomNumber, float MinNumber, float MaxNumber)
    {
        return RandomNumber;
    }
}