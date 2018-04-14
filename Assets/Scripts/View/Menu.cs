﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FoxAndGeese;

public class Menu : MonoBehaviour {

	private PawnType humanPlayer = PawnType.Goose;
	public Slider slider;
	public const string confirmPlayerChosenNotification = "Menu.ConfirmPlayerChosenNotification";

	/** Called whenever the user changes the player on the slider */
	public void OnSliderChange() {
		int num = (int)slider.value;
		humanPlayer = num == 0 ? PawnType.Goose : PawnType.Fox;
		Debug.Log("Menu OnSliderChange cambiato in " + num + " " + humanPlayer);
	}

	/** Called when the user starts the game */
	public void ConfirmPlayerChosen() {
		Debug.Log("Menu ConfirmPlayer " + humanPlayer);
		this.PostNotification(confirmPlayerChosenNotification, humanPlayer);
	}

}
