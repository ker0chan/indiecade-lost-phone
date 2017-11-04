using System;
using System.Collections;
using System.Collections.Generic;

public class GenericCSVLine
{
	//A collection of "column name -> string value" pairs
	public Dictionary<string, GenericCSVField> data = new Dictionary<string, GenericCSVField>();

	public GenericCSVLine ()
	{
	}

	//Add a new field in the collection, constructed from the given value
	public void AddField(string key, string value)
	{
		data.Add (key, new GenericCSVField (value));
	}

	//Return a single LocalizedField, that holds the values of multiple fields that have the same name
    public LocalizedCSVField GetLocalizedField(string key)
    {
		//Construct the new field
        LocalizedCSVField field = new LocalizedCSVField();

		//Fill it in with values from every supported language
        foreach(string locale in LocalizationManager.supportedLanguages)
        {
            if(data.ContainsKey(key+locale))
            {
				//French has weird punctuation rules! Let's enforce them here, shall we?
                if(locale == "Fr")
                {
                    //Replaces spaces by non-breaking spaces before punctuation markers
                    field.AddText(locale, data[key + locale].asString()
						.Replace(" ?", " ?")
						.Replace(" !", " !")
						.Replace(" :", " :")
					);
                } else
                {
                    field.AddText(locale, data[key + locale].asString());
                }                
            }
        }
        
        return field;
    }

	//Override the square brackets operator
	// myLine[someFieldName] returns the given field, or null if it doesn't exist
	public GenericCSVField this[string key]
	{
		get
		{
			if (data.ContainsKey (key)) {
				return data[key];
			} else {
				return null;
			}
		}
		set
		{
			data[key] = value;
		}
	}
}