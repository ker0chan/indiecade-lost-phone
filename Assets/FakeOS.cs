using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The FakeOS class is central in the design of a lost phone game.
//This is a simplified version of the one we used in our games, keeping only the minimal features needed for this demo.
//It's mostly responsible for managing the stack of apps, and navigating through it.
//It also keeps track of the ingame time, which is useful for the (not included) ingame clock, which synchronized with real time with an offset.

public class FakeOS : MonoBehaviour {

	//Singleton pattern
	private static FakeOS instance;
	public static FakeOS Instance
	{
		get { return instance; }
	}
	void Awake() {
		instance = this;
	}
		
	//Dictionary of the apps for faster lookup
	public Dictionary<string, AbstractApp> existingApps;
	List<AbstractApp> stack;

	//The player's date IRL
	DateTime realDate;
	//The date in the fake phone
	DateTime gameDate;
	//Difference between player date and ingame date
	TimeSpan dateOffset;

	void Start() {
		existingApps = new Dictionary<string, AbstractApp>();
		//Find every GameObject under AppContainer that has an AbstractApp component that is enabled,
		// and add it to the list of existing apps.
		foreach (Transform t in GameObject.Find("AppContainer").transform) {
			AbstractApp abstractAppComponent = t.GetComponent<AbstractApp>();

			//Don't add elements that don't have an AbstractApp component
			if (abstractAppComponent == null)
				continue;

			//Don't add the elements that have a disabled AbstractApp
			if (!abstractAppComponent.enabled)
				continue;

			existingApps.Add(abstractAppComponent.name, abstractAppComponent);
		}

		stack = new List<AbstractApp>();

		realDate = DateTime.Now;
		gameDate = new System.DateTime(2017, 6, 6, 14, 0, 0);
		dateOffset = (realDate - gameDate);

		LoadApp("home");
	}

	//Loads a view/screen, scrolls the view to the bottom if scrollTo is set to 0.0f, scrolls to a given position otherwise, default value scrolls to 1.0f (top)
	public void LoadApp(string appName, bool openOnTop = true)
	{
		//Always compare lowercase names
		appName = appName.ToLower();

		AbstractApp app = existingApps[appName];

		//If the app is not on top of the stack
		if (GetCurrentAppName() != appName) {
			//Find if it's somewhere else in the stack
			int index = stack.FindLastIndex(a => a.name == app.name);
			if (index != -1) {
				//If yes: remove it so it can be added on top of the stack
				stack.RemoveAt(index);
			}

			//Add the app to the stack
			stack.Add(app);
		} else {
			//The app is already on top of the stack: make sure we're not putting it on the top layer again by accident :o
			openOnTop = false;
		}

		//Move the app on the top layer if needed
		if (openOnTop) {
			app.transform.SetAsLastSibling();
		}

		//If there is an app on the stack already, we'll need to remember it so we can hide it when the new app opens
		AbstractApp previousApp = null;
		if (stack.Count > 1) {
			previousApp = stack[stack.Count - 2];
		}

		//Resume the app (and animate it, if it's being moved to the top layer, meaning it wasn't already visible)
		app.ResumeApp();

		//Hide the previous app (if it exists) once the new one has finished its animation
		if (previousApp != null)
		{
			previousApp.PauseApp();
		}
	}

	#region Navigation bar shortcuts
	//Home button press handler
	public void PressHome()
	{
		//SoundManager.Instance.PlaySFX ("pop");
		Home ();
	}

	//Home button action
	public void Home()
	{
		if (stack.Count >= 2) {
			//Remove everything between the home app and the last app
			stack.RemoveRange (1, stack.Count - 2);
			//Hide the last app
			GetCurrentApp().PauseApp ();
			//And remove it from the stack
			stack.RemoveAt(stack.Count - 1);
		}
		//Reload the home app
		LoadApp ("home", false);
	}

	//Back button press handler
	public void PressBack()
	{
		//SoundManager.Instance.PlaySFX ("pop");
		Back ();
	}

	//Back button action
	public void Back()
	{

		if (GetCurrentApp ().Back ()) {
			return;
		}

		//if there's an app to close and an app to go back to
		if (stack.Count >= 2) {
			//Close it
			GetCurrentApp().PauseApp ();
			stack.RemoveAt(stack.Count - 1);
			//Load the previous app
			AbstractApp previous = stack[stack.Count - 1];
			LoadApp (previous.name, false);
			return;
		}

		//Otherwise... Enjoy the homescreen ¯\_(ツ)_/¯
	}
	#endregion

	//Computes the current ingame date
	public DateTime GetCurrentDate()
	{
		//current player date - date offset = current ingame date
		return (DateTime.Now - dateOffset);
	}

	//Returns the app on top of the stack
	public AbstractApp GetCurrentApp()
	{
		if (stack.Count > 0) {
			return stack[stack.Count - 1];
		} else {
			return null;
		}
	}
	//Returns the name of the app on top of the stack
	public string GetCurrentAppName()
	{
		if (stack.Count > 0) {
			return stack[stack.Count - 1].name;
		} else {
			return null;
		}
	}
}