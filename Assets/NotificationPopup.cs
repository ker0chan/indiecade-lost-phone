using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationPopup : MonoBehaviour {

	public TextMeshProUGUI textComponent;
	public Button button; 

	public void Show(AbstractIntent intent, string text)
	{
		textComponent.text = text;

		//Remove any action that was given to the button before
		button.onClick.RemoveAllListeners ();
		//And give it a new action: executing the given intent
		button.onClick.AddListener (() => {
			intent.Execute();
			Close();
		}
		);

		gameObject.SetActive (true);
	}

	public void Close()
	{
		gameObject.SetActive (false);
	}
}
