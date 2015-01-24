using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scorebar : MonoBehaviour 
{
    private static Text scoreText;
    private static string defaultText;
    private static int defaultTextLength;

	// Use this for initialization
	void Start () 
    {
        scoreText = transform.FindChild("Text").GetComponent<Text>();
        defaultText = scoreText.text;
        defaultTextLength = defaultText.Length;
	}

    public static void SetScoreText(int value)
    {
        string valueString = value.ToString();
        int valueLength = valueString.Length;
        if (value >= 0 && valueLength <= defaultTextLength)
        {
            string zeroString = GetFilledString(defaultTextLength - valueLength, '0');
            scoreText.text = zeroString + valueString;
        }
    }

    private static string GetFilledString(int length, char character)
    {
        string filledString = "";
        for (int i = 0; i < length; i++)
        {
            filledString += character;
        }

        return filledString;
    }
}
