using UnityEngine;

public class Runner : MonoBehaviour {

	public ParticleSystem boostEmitter;
	public static float distanceTraveled;
	public float acceleration;
	public Vector3 boostVelocity, jumpVelocity, speedVelocity;
	public float maxSpeed;
	public float gameOverY;
	public float greyOffset;
	public float hueOffset;

	private bool touchingPlatform;
	private Vector3 startPosition;
	private int nextLevel;
	private static int boosts;
	private static float antigravity;
	
	void Start() {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.LevelUp += LevelUp;
		GameEventManager.ToggleGravity += ToggleGravity;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	void Update () {
		if(touchingPlatform) {
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
		if(Input.GetButtonDown("Jump") && touchingPlatform) {
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			touchingPlatform = false;
		}
		if(Input.GetButtonDown("Boost") && boosts > 0) {
			rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
			boostEmitter.Play();
			GUIManager.SetBoosts(--boosts);
		}
		if(Input.GetButtonDown("Speed Up") && rigidbody.velocity.x < maxSpeed) {
			rigidbody.AddForce(speedVelocity, ForceMode.VelocityChange);
		}
		if(Input.GetButtonDown("Slow Down") && rigidbody.velocity.x > speedVelocity.x) {
			rigidbody.AddForce(-speedVelocity, ForceMode.VelocityChange);
		}
		if(Input.GetButtonUp("Toggle Gravity") && antigravity > 0f) {
			GameEventManager.TriggerToggleGravity(!GameEventManager.getGravity());
			GameEventManager.TriggerLevelUp();
		}

		if(!GameEventManager.getGravity()) {
			antigravity -= Time.deltaTime;
			if(antigravity < 0f) {
				antigravity = 0f;
				GameEventManager.TriggerToggleGravity(true);
			}
			GUIManager.SetAntigravity(antigravity);
		}

		distanceTraveled = transform.localPosition.x;
		if(distanceTraveled > nextLevel) {
			nextLevel += GameEventManager.levelSize;
			GameEventManager.TriggerLevelUp();
		}
		GUIManager.SetDistance(distanceTraveled);
		GUIManager.SetSpeed(this.transform.rigidbody.velocity.x);

		if(transform.localPosition.y < gameOverY) {
			GameEventManager.TriggerGameOver();
		}
	}
	
	public static void Boost() {
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}

	public static void AddAntigravity() {
		antigravity += 10;
		GUIManager.SetAntigravity(antigravity);
	}
	
	void OnCollisionEnter() {
		touchingPlatform = true;
	}
	
	void OnCollisionExit() {
		touchingPlatform = false;
		this.transform.parent = null; // BECOMES BATMAN
	}

	void OnCollisionStay(Collision col) { // JOHN TOLD ME TO
		if (col.transform.GetComponent<PlatformMover>().getMoving()) {
			this.transform.parent = col.transform;
		}
	}

	private void GameStart() {
		setSkyColor();
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		antigravity = 100f;
		GUIManager.SetAntigravity(antigravity);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		GUIManager.SetSpeed(this.transform.rigidbody.velocity.x);
		nextLevel = GameEventManager.levelSize;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	private void GameOver() {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}

	private void LevelUp() {
		setSkyColor();
	}

	private void ToggleGravity() {
		rigidbody.useGravity = GameEventManager.getGravity();
		renderer.material.color = GameEventManager.getGravity() ? Color.white : Color.black;
	}

	private void setSkyColor() {
		Camera.allCameras[0].backgroundColor = ColorManager.offsetHue(ColorManager.offsetColor(GameEventManager.getColor(), -160f), hueOffset);
	}
}
