using UnityEngine;

public class GUIManager : MonoBehaviour {

	public GUIText boostsText, distanceText, gameOverText, instructionsText, runnerText, speedText, antigravityText, levelText;
	private static GUIManager instance;
	
	void Start() {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.LevelUp += LevelUp;
		gameOverText.enabled = false;
		levelText.enabled = false;
	}

	void LevelUp ()
	{
		SetLevel(GameEventManager.getLevel());
	}
	
	void Update() {
		if(Input.GetButtonDown("Boost")) GameEventManager.TriggerGameStart();
	}
	
	public static void SetBoosts(int boosts) {
		instance.boostsText.text = "Boosts: " + boosts.ToString();
	}

	public static void SetAntigravity(float antigravity) {
		instance.antigravityText.text = "Antigravity: " + antigravity.ToString("f0") + "s";
	}
	
	public static void SetDistance(float distance) {
		instance.distanceText.text = "Distance: " + distance.ToString("f0") + "m";
	}
	
	public static void SetSpeed(float speed) {
		instance.speedText.text = "Speed: " + speed.ToString("f0") + "m/s";
	}

	public static void SetLevel(int level) {
		instance.levelText.text = "Level: " + level.ToString();
	}
	
	private void GameStart() {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		levelText.enabled = true;
		enabled = false;
	}
	
	private void GameOver() {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}
}
