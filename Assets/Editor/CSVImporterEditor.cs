using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;

[CustomEditor(typeof(CSVImporter))]
public class CSVImporterEditor : Editor
{
	CSVImporter targetImporter;
	SerializedProperty CSVFiles;


	private static GUIContent
	downloadButtonContent = new GUIContent("UPDATE", "Download CSV");

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		CSVFiles = serializedObject.FindProperty("CSVFiles");

		//CSV Files inspector
		EditorGUILayout.PropertyField (CSVFiles);
		EditorGUI.indentLevel += 1;
		if (CSVFiles.isExpanded)
		{
			//Size
			EditorGUILayout.PropertyField(CSVFiles.FindPropertyRelative("Array.size"));

			for (int i = 0; i < CSVFiles.arraySize; i++) {
				SerializedProperty CSVFile = CSVFiles.GetArrayElementAtIndex (i);

				//File
				EditorGUILayout.PropertyField (CSVFile);
				if (CSVFile.isExpanded) {
					//Name
					SerializedProperty nameProperty = CSVFile.FindPropertyRelative ("name");
					nameProperty.stringValue = EditorGUILayout.TextField ("Name", nameProperty.stringValue);

					//Text asset
					SerializedProperty fileProperty = CSVFile.FindPropertyRelative ("file");
					EditorGUILayout.PropertyField (fileProperty);

					UnityEngine.Object textAsset = (UnityEngine.Object)fileProperty.objectReferenceValue;
					//Modification date
					DateTime lastModified = System.IO.File.GetLastWriteTime(AssetDatabase.GetAssetPath(textAsset));
					EditorGUILayout.LabelField ("Last modified:", lastModified.ToString());

					//URL + Download button
					EditorGUILayout.BeginHorizontal();
					SerializedProperty urlProperty = CSVFile.FindPropertyRelative ("url");
					urlProperty.stringValue = EditorGUILayout.TextField ("Url", urlProperty.stringValue);
					if (urlProperty.stringValue != "") {
						if (GUILayout.Button (downloadButtonContent, EditorStyles.miniButton, GUILayout.Width (60f))) {
							Download (urlProperty.stringValue, textAsset);
						}
					}

					EditorGUILayout.EndHorizontal();	
				}
			}
		}
		EditorGUI.indentLevel -= 1;

		serializedObject.ApplyModifiedProperties();

		// Show default inspector property editor
		//DrawDefaultInspector ();
	}

	bool Download(string url, UnityEngine.Object asset)
	{
		Debug.Log ("Sending request...");

		float timeOut = Time.realtimeSinceStartup + 10.0f;
		using(UnityWebRequest www = UnityWebRequest.Get(url)) {
			www.Send();

			while (www.responseCode < 1) {
				//do something, or nothing while blocking
				if (Time.realtimeSinceStartup > timeOut) {
					break;
				}
			}
			if(!(www.responseCode == 200)) {
				Debug.LogError("Error when requesting CSV file (responseCode:"+www.responseCode+")");
				Debug.LogError(www.error);
				return false;
			}
			else {
				string filepath = AssetDatabase.GetAssetPath(asset);
				System.IO.File.WriteAllText (filepath, www.downloadHandler.text);
				Undo.RecordObject(asset, "Downloaded CSV from distant file");
				Debug.LogError("Downloaded CSV from distant file");
				return true;
			}
		}
	}
}