using UnityEngine;
using System;

//A bare container for a string value, and all the methods to parse it as... "something else".

public class GenericCSVField
{
	public string value;

    public GenericCSVField(string value)
	{
		this.value = value;
	}

	public DateTime asDateTime()
	{
		try
		{
			return DateTime.Parse(value);
		} catch(System.SystemException e) {
			Debug.LogError ("Couldn't parse " + value + " as date");
			return DateTime.Now;
		}

	}
	public string asString()
	{
		return value;
	}
	public int asInt()
	{
		try
		{
			return Int32.Parse (value);
		} catch(System.SystemException e) {
			Debug.LogError ("Couldn't parse " + value + " as int");
			return 0;
		}
	}
	public bool asBool()
	{
		if (value == "0") return false;
		if (value == "1") return true;

		Debug.LogError ("Couldn't parse " + value + " as bool");
		return false;
	}

	public static System.DateTime dateTimeStringToDateTime(string date, string time)
	{
		try
		{
			//Messy, fast date parsing
			//DateTime.Parse creates tons of garbage, but support many different formats and conversions
			//Since we have complete control over the source format, use a tailor made solution
			char[] charsToTrim = {'\t', ' '};
			date = date.Trim (charsToTrim);
			string[] dateElements = date.Split ('-');

			time = time.Trim (charsToTrim);
			char[] timeSplitters = {':', ' '};
			string[] timeElements = time.Split (timeSplitters);

			return new System.DateTime (int.Parse (dateElements [0]),
				int.Parse (dateElements [1]),
				int.Parse (dateElements [2]),
				int.Parse (timeElements [0]),
				int.Parse (timeElements [1]),
				0);
		} catch(System.SystemException e)
		{
			Debug.LogError ("Couldn't parse " + date + " " + time + " as date, returning current date.");
			return System.DateTime.Now;
		}
	}
	public static System.DateTime dateStringToDateTime(string date)
	{
		try
		{
			//Messy, fast date parsing
			//DateTime.Parse creates tons of garbage, but support many different formats and conversions
			//Since we have complete control over the source format, use a tailor made solution
			char[] charsToTrim = {'\t', ' '};
			date = date.Trim (charsToTrim);
			string[] dateElements = date.Split ('-');
			return new System.DateTime (int.Parse (dateElements [0]),
				int.Parse (dateElements [1]),
				int.Parse (dateElements [2]),
				0, 0, 0);
		} catch (System.SystemException e) {
			Debug.LogError ("Couldn't parse " + date + " as date, returning current date.");
			return System.DateTime.Now;
		}
	}
}