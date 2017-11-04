using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadAppButton : MonoBehaviour {

	public string appName;
	// Use this for initialization
	void Start () {		
		GetComponent<Button>().onClick.AddListener (() => {
			//SoundManager.Instance.PlaySFX ("pop");
			FakeOS.Instance.LoadApp(appName);
		});
	}
}