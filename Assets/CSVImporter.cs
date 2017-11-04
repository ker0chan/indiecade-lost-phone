using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class helps deserialize and cache CSV files.
//It is far from perfect from a technical standpoint, but has the benefit of being super flexible on the typing of objects:
//We're manipulating lists and collections of strings most of the time, resulting in loosely typed data.
//A total nightmare, unless you're prototyping and quickly iterating between different structures in your CSV files.

public class CSVImporter : MonoBehaviour {

	//Singleton pattern
	private static CSVImporter instance;
	public static CSVImporter Instance
	{
		get { return instance; }
	}
	void Awake () {
		instance = this;
	}

	[System.Serializable]
	public struct CSVFile
	{
		public string name;
		public string url;
		public TextAsset file;
		public string lastModified;
	}

	public CSVFile[] CSVFiles;

	public Dictionary<string, List<GenericCSVLine>> parsedFiles = new Dictionary<string, List<GenericCSVLine>>();
	string[] lineHeaders;
	List<GenericCSVLine> currentFile;

	//Parse the file as a collection of GenericCSVLine, cast into typed objects
	public List<T> ParseFile<T>(string identifier, Func<GenericCSVLine, T> constructor)
	{
		List<GenericCSVLine> genericList = ParseFile (identifier);
		List<T> typedList = new List<T> ();
		genericList.ForEach (i => {
			typedList.Add( constructor(i) );
		});
		return typedList;
	}	

	//Parse the file as a collection of GenericCSVLine
	public List<GenericCSVLine> ParseFile(string identifier)
	{
		if (parsedFiles.ContainsKey (identifier)) {
			return parsedFiles [identifier];
		}

		CSVFile CSVFile = new CSVFile();
		for (int i = 0; i < CSVFiles.Length; i++) {
			if (CSVFiles [i].name == identifier) {
				CSVFile = CSVFiles [i];
				break;
			}
		}
		currentFile = new List<GenericCSVLine> ();

		fgCSVReader.LoadFromString(CSVFile.file.text, new fgCSVReader.ReadLineDelegate(ParseLine));
		parsedFiles.Add (identifier, currentFile);

		return parsedFiles [identifier];
	}

	//Parse a single line, as a List of strings
	void ParseLine(int line_index, List<string> sourceLine)
	{
		if (line_index == 0) {
			lineHeaders = sourceLine.ToArray ();
			return;
		}

		GenericCSVLine line = new GenericCSVLine ();
		for (int i = 0; i < sourceLine.Count; i++)
		{
			line.AddField (lineHeaders [i], sourceLine [i]);
		}
		currentFile.Add (line);
	}
}
