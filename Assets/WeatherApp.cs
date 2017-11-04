using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherApp : AbstractApp
{
	bool contentAlreadyUnlocked = false;

	public override void OnResume ()
	{
		//If the content has not yet been unlocked
		if (!contentAlreadyUnlocked) {

			//Unlock it ¯\_(ツ)_/¯
			contentAlreadyUnlocked = true;

			//Display a popup with this text, that will trigger this intent upon validation.
			NotificationManager.Instance.Notify (
				new UnlockMessageIntent (),
				"You have received new messages!"
			);

			//This is the simplest example we could find, but in a real game we'd probably need a more interesting action to trigger the popup.
			//Entering a password, clicking a specific button, or perhaps just a timed interaction...
		}

	}
}
