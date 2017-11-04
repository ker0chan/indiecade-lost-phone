using System;
using System.Collections.Generic;

public class LocalizedCSVField
{
	//A collection of "language -> text" pairs
    public Dictionary<string, string> fieldData = new Dictionary<string, string>();
    
	public LocalizedCSVField()
    {
    }

    //Copy constructor
    public LocalizedCSVField(LocalizedCSVField original)
    {
        this.fieldData = new Dictionary<string, string>(original.fieldData);
    }

	//Add a value to the field in a given language
    public void AddText(string locale, string value)
    {
        fieldData.Add(locale, value);
    }

	//Add a value to the field that will be identical for every language
    public void AddUniformText(string value)
    {
        foreach (string locale in LocalizationManager.supportedLanguages)
        {
            AddText(locale, value);
        }
    }

	//Get the value of the field in the given language
    string GetText(string locale)
    {
        if (fieldData.ContainsKey(locale))
        {
            return fieldData[locale];
        }
        else
        {
            return "";
        }
    }

	//Get the value of the field in the current language
    public string GetLocalizedText()
    {
        return GetText(LocalizationManager.Instance.currentLocale);
    }

	//Returns a single value in a locale that will never change
	//Useful for comparing if two lines have a value in common, regardless
	// of the current language that is set in the game.
    public string GetInvariantText()
    {
        return GetText("Fr");
    }

	//Replace a parameter string in a field ("$PLAYERNAME", "$CURRENTDATE", etc...)
    public LocalizedCSVField Replace(string oldValue, string newValue)
    {
        List<string> keys = new List<string>(fieldData.Keys);
        foreach (string key in keys)
        {
            fieldData[key] = fieldData[key].Replace(oldValue, newValue);
        }

        return this;
    }
}
