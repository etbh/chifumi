using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject mainScreen;
	public GameObject gameSettingsScreen;
	// No Tutorial screen yet !
	// Game settings screen toggle buttons (not very elegant, but...)
	public GameObject playerVsAi;
	public GameObject playerVsPlayer;
	public GameObject points;
	public GameObject rounds;
	private Button playerVsAiButton;
	private Button playerVsPlayerButton;
	private Button pointsButton;
	private Button roundsButton;

	public void Start() {
		playerVsAiButton = playerVsAi.GetComponent<Button>();
		playerVsPlayerButton = playerVsPlayer.GetComponent<Button>();
		pointsButton = points.GetComponent<Button>();
		roundsButton = rounds.GetComponent<Button>();
	}

	public void ShowGameSettings() {
		mainScreen.SetActive(false);
		gameSettingsScreen.SetActive(true);
	}

	// Toggle buttons functions - Invert the normal colors of buttons
	// to show what option is activated.

	public void ToggleButtons(Button activated, Button deactivated) {
		// The just-activated button has the default normal color.
		// Swap the colors to highlight the activated button and return the
		// other one to normal.
		ColorBlock deactivatedColors = deactivated.colors;
		ColorBlock activatedColors = activated.colors;
		activated.colors = deactivatedColors;
		deactivated.colors = activatedColors;
	}

	public void SetPlayerVsAI() {
		ToggleButtons(playerVsAiButton, playerVsPlayerButton);
	}

	public void SetPlayerVsPlayer() {
		ToggleButtons(playerVsPlayerButton, playerVsAiButton);
	}
	
	public void SetPoints() {
		ToggleButtons(pointsButton, roundsButton);
	}

	public void SetRounds() {
		ToggleButtons(roundsButton, pointsButton);
	}

	public void StartGame() {
		Application.LoadLevel("Game");
	}

	public void Update() {
		// Listen for ESCAPE input to go back one screen
		if (Input.GetKeyDown(KeyCode.Escape)) {
			mainScreen.SetActive(true);
			gameSettingsScreen.SetActive(false);
		}
	}
}
