namespace TextTransform;

public class Transform
{
    public static string AlternatingCase(string input)
    {
        var result = new char[input.Length];
        int caseIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsLetter(input[i]))
            {
                result[i] = caseIndex % 2 == 0
                    ? char.ToLower(input[i])
                    : char.ToUpper(input[i]);
                caseIndex++;
            }
            else
            {
                result[i] = input[i];
            }
        }

        return new string(result);
    }
}