﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Player : MonoBehaviour {
	public bool paused = false;

	private UIManager uiManager;
	private Controls controls;

	private void Awake() {
		uiManager = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<UIManager>();
		controls = new Controls();
		controls.UI.Pause.performed += Pause_performed;
	}

	private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		paused = !paused;
		uiManager.TogglePauseScreen(paused);
	}
}