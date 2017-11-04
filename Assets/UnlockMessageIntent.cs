using System;

public class UnlockMessageIntent : AbstractIntent
{
	public UnlockMessageIntent ()
	{
	}

	override public void Execute()
	{
		//Invisible action: unlocking the new content.
		//The intent can be called from anywhere, and doesn't have any link to the Messages app: we use the FakeOS singleton and its collection of existing apps to access it.
		((MessagesApp)FakeOS.Instance.existingApps ["messages"]).UnlockContent ();

		//Visible action: loading the app.
		//Since the content has been updated, the app will open with new messages in it.
		FakeOS.Instance.LoadApp ("messages");
	}
}
