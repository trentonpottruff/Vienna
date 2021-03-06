﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

namespace Vienna {
	public class UIManager : MonoBehaviour {
		[SerializeField]
		GameObject vitals, pauseScreen, inventory, mainInventory, observedInventory;

		private InventoryScreen screen;

		private void Awake() {
			screen = inventory.GetComponentInParent<InventoryScreen>();
		}

		public void SetPauseScreen(bool newPaused) {
			vitals.SetActive(!newPaused);
			pauseScreen.SetActive(newPaused);
		}

		public bool ToggleInventory() {
			bool currentStatus = inventory.activeSelf;
			vitals.SetActive(currentStatus);
			inventory.SetActive(!currentStatus);
			if (!inventory.activeSelf) {
				observedInventory.SetActive(false);
				screen.Unobserve();
			}
			return inventory.activeSelf;
		}

		public void ObserveInventory(Inventory _inventory) {
			if (CanToggleInventory() && !inventory.activeSelf) {
				GameManager.singleton.Paused = ToggleInventory();
			}

			observedInventory.SetActive(true);
			screen.Observe(_inventory);
		}

		public bool PausedElsewhere() => (GameManager.singleton.Paused && !pauseScreen.activeSelf);
		public bool CanToggleInventory() => (GameManager.singleton.Paused && inventory.activeSelf) || (!GameManager.singleton.Paused && !inventory.activeSelf);
	}
}