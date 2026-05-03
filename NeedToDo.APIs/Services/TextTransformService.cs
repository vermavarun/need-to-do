using TextTransform;

public class TextTransformService
{
    public string TransformString(string text)
    {
        return Transform.AlternatingCase(text);
    }
}