using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //import TextMeshPro features
using UnityEngine.UI; //import UI features
using Entities;
public class MessageBlock : MonoBehaviour {

	public RectTransform backgroundTransform;
	public GameObject favoriteMarker;
	public TextMeshProUGUI textComponent;
	public TextMeshProUGUI nameComponent;
	public Image backgroundImage;

	public Color sentColor;
	public Color receivedColor;

	public void Show(Message m)
	{
		//Set the text
		textComponent.text = m.text;

		//Set the name
		nameComponent.text = "By: " + m.name;

		//Display the favorite marker depending on the value of "favorite"
		favoriteMarker.SetActive (m.favorite);

		//Set the color depending on the value of "status"
		backgroundImage.color = (m.status == "sent") ? sentColor : receivedColor;

		//Move the block left or right depending on the value of "status"
		Vector2 blockPosition = backgroundTransform.anchoredPosition;
		blockPosition.x = (m.status == "sent") ? 100 : -100;
		backgroundTransform.anchoredPosition = blockPosition;
	}
}
