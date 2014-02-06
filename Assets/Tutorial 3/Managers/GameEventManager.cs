using UnityEngine;

public static class GameEventManager {
	public delegate void GameEvent();
	public static event GameEvent GameStart, GameOver, LevelUp, ToggleGravity;
	public static int levelSize = 20;

	private static int level;
	private static Color levelColor;
	private static bool gravity;
	
	public static void TriggerGameStart() {
		level = 1;
		gravity = true;
		levelColor = ColorManager.getRandomColor();
		if(GameStart != null) {
			GameStart();
		}
	}
	
	public static void TriggerGameOver() {
		if(GameOver != null) {
			GameOver();
		}
	}

	public static void TriggerLevelUp() {
		level++;
		levelColor = ColorManager.getRandomColor();
		if(LevelUp != null) {
			LevelUp();
		}
	}

	public static void TriggerToggleGravity(bool g) {
		gravity = g;
		if(ToggleGravity != null) {
			ToggleGravity();
		}
	}

	public static int getLevel() {
		return level;
	}

	public static Color getColor() {
		return levelColor;
	}

	public static bool getGravity() {
		return gravity;
	}
}
