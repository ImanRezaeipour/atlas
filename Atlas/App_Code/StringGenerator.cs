using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StringBuilder
/// </summary>
public class StringGenerator
{
    private char[] SpecialCharacters = new char[] { '<', '>', ')', '(', '&' };
    public string CreateString(string str)
    {
        try
        {
            str = str.Replace("//", "/");
            string[] KeyCodeArray = str.Split('/');
            string RetString = string.Empty;
            for (int i = 0; i < KeyCodeArray.Length; i++)
            {
                if (KeyCodeArray[i] != string.Empty)
                {

                    ///ي = 1610  , ئ = 1574  , ی = 1740
                    if (KeyCodeArray[i] == "1610")
                        KeyCodeArray[i] = "1740";
                    ///ك = 1603 , ک = 1705
                    if (KeyCodeArray[i] == "1603")
                        KeyCodeArray[i] = "1705";
                    char character = Convert.ToChar(Convert.ToInt32(KeyCodeArray[i]));
                    if (!SpecialCharacters.Contains(character))
                        RetString += character;
                    else
                        RetString += string.Empty;
                }
            }
            return RetString.Trim() == string.Empty ? string.Empty : RetString;
        }
        catch
        {
            return string.Empty;
        }
    }

    public string CreateString(string str, StringGeneratorExceptionType Sget)
    {
        string RetString = string.Empty;
        string[] KeyCodeArray = null;
        switch (Sget)
        {
            case StringGeneratorExceptionType.ClientAttachments:
                char[] strCharArray = str.ToCharArray();
                for (int i = 0; i < strCharArray.Length; i++)
                {
                    if (!SpecialCharacters.Contains(strCharArray[i]))
                        RetString += strCharArray[i];
                    else
                        RetString += string.Empty;
                }
                break;
            case StringGeneratorExceptionType.ReportCondition:
            case StringGeneratorExceptionType.ConceptRuleManagement:
                str = str.Replace("//", "/");
                KeyCodeArray = str.Split('/');

                for (int i = 0; i < KeyCodeArray.Length; i++)
                {
                    if (KeyCodeArray[i] != string.Empty)
                    {
                        ///ي = 1610  , ئ = 1574  , ی = 1740
                        if (KeyCodeArray[i] == "1610")
                            KeyCodeArray[i] = "1740";
                        ///ك = 1603 , ک = 1705
                        if (KeyCodeArray[i] == "1603")
                            KeyCodeArray[i] = "1705";
                        char character = Convert.ToChar(Convert.ToInt32(KeyCodeArray[i]));
                        RetString += character;
                    }
                }
                break;
            case StringGeneratorExceptionType.Shifts:
                if (str == null)
                    str = string.Empty;
                str = str.Replace("//", "/");
                KeyCodeArray = str.Split('/');
                for (int i = 0; i < KeyCodeArray.Length; i++)
                {
                    if (KeyCodeArray[i] != string.Empty)
                    {

                        ///ي = 1610  , ئ = 1574  , ی = 1740
                        if (KeyCodeArray[i] == "1610")
                            KeyCodeArray[i] = "1740";
                        ///ك = 1603 , ک = 1705
                        if (KeyCodeArray[i] == "1603")
                            KeyCodeArray[i] = "1705";
                        char character = Convert.ToChar(Convert.ToInt32(KeyCodeArray[i]));
                        if (!SpecialCharacters.Where(x => x != '(' && x != ')').Contains(character))
                            RetString += character;
                        else
                            RetString += string.Empty;
                    }
                }
                break;
            default:
                break;
        }
        return RetString;
    }

}