using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class MessagesApp : AbstractApp {

	public GameObject messageBlockPrefab;
	public List<Message> messages;
	public Transform appContent;

	// Use this for initialization
	void Start () {

		//V1: Manual creation
		/*
		messages = new List<Message>();
		messages.Add(new Message ("me", "hello", "sent", true));
		messages.Add(new Message ("you", "hi", "received", false));
		*/

		//V2: CSV Import
		messages = CSVImporter.Instance.ParseFile ("messenger", e => new Message (e));

		foreach (Message m in messages) {

			//V3: Unlockable content
			if (m.status == "waiting")
				continue;

			// /!\ Instantiate is sloooow! /!\
			// Use a pooling system instead!
			// #UnityTips
			GameObject entry = Instantiate (messageBlockPrefab);

			//Add the block in the hierarchy, under the Content object, in the LayoutGroup
			entry.transform.SetParent (appContent, false);

			//Fill in the component with the message data
			entry.GetComponent<MessageBlock>().Show (m);
		}
	}

	//Display all the "waiting" messages
	//V3: Unlockable content
	public void UnlockContent()
	{
		foreach (Message m in messages) {

			//Ignore the messages that are not "waiting", they have been added already.
			if (m.status != "waiting")
				continue;

			GameObject entry = Instantiate (messageBlockPrefab);
			entry.transform.SetParent (appContent, false);
			entry.GetComponent<MessageBlock>().Show (m);
		}
	}

	//V4: In a real game?
	// -You probably want to redraw the content of the app when something changes, like the language settings, or some other content unlock.
	//	Overriding the OnResume() method in this class, and using the isDirty flag to trigger a redraw the content can help.
	// -Destruction and Instanciation is not optimized at all - using a pooling system to disable all the messages blocks, then updating them with the content that we need is probably a safer way to go.
	// -The current implementation of UnlockContent() supposes that new content will only be unlocked once. Using a parameter to unlock multiple waves of messages at different times could be an improvement.

}
