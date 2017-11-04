using System;

namespace Entities
{
	public class Message
	{
		public string name;
		public string text;
		public string status;
		public bool favorite;

		//"Manual" constructor (V1 of MessagesApp)
		public Message(string name, string text, string status, bool favorite)
		{
			this.name = name;
			this.text = text;
			this.status = status;
			this.favorite = favorite;
		}

		//Construct from CSV data
		public Message (GenericCSVLine l)
		{
			//This is where we set the deserialization rules: what type should we parse each column as, etc...
			//Compound attributes could be managed here (constructing a DateTime from two columns, for example)
			text = l ["text"].asString ();
			name = l ["name"].asString ();
			status = l ["status"].asString ();
			favorite = l ["favorite"].asBool ();
		}
	}
}

