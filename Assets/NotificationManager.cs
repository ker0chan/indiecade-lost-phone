using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotificationManager : MonoBehaviour {

	//Singleton pattern
	private static NotificationManager instance;
	public static NotificationManager Instance
	{
		get { return instance; }
	}
	public void Awake()
	{
		instance = this;
	}

	public NotificationPopup popup;

	public void Notify(AbstractIntent intent, string contentKey)
	{
		popup.Show (intent, contentKey);
	}
}