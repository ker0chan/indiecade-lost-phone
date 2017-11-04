using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeApp : AbstractApp {
	public override bool Back ()
	{
		//Tell the FakeOS that we're handling this Back action ourselves (and do nothing with it, muahaHAHAa!!1!!)
		return true;
	}
}
