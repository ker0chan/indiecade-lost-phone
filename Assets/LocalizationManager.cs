using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {
	
	//Singleton pattern
	private static LocalizationManager instance;
	public static LocalizationManager Instance
	{
		get { return instance; }
	}
	void Awake () {
		instance = this;
	}

	//The currently selected language
	//Could be set automatically by detecting the system locale,
	//or from a command line parameter when launching the game,
	//or from a configuration file...
	public string currentLocale;

	//The list of supported languages that we can expect to find while parsing the CSV files
	public static string[] supportedLanguages = { "Fr", "En" };
}
