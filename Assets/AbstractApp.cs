using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractApp : MonoBehaviour
{
	//Name of the app, used by the FakeOS to identify this app
	private string _name = null;
	public string name{
		get { 
			if (_name == null) {
				//If the name hasn't been set yet: use the name of the containing GameObject, trimming the last 3 characters, in lowercase
				// ==> "MessagesApp" becomes "messages"
				_name = gameObject.name.Substring (0, gameObject.name.Length - 3).ToLower ();
			}
			return _name;
		}
		set { 
			_name = value;
		}
	}
		
	protected bool isPaused = true;
	protected bool isDirty = true;

	public virtual void ResumeApp ()
	{
		gameObject.SetActive (true);

		OnResume ();

		isPaused = false;
	}
	public virtual void PauseApp ()
	{
		
		gameObject.SetActive (false);

		OnPause ();

		isPaused = true;
	}
	public virtual bool Back()
	{
		PauseApp ();
		return false;
	}

	public virtual void OnResume()
	{
	}

	public virtual void OnPause()
	{
	}
		
	public virtual void SetDirty()
	{
		isDirty = true;
	}
}