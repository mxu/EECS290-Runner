using UnityEngine;

public class Antigravity : MonoBehaviour {
	
	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	
	void Start() {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	void Update() {
		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled) {
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider col) {
		if(col.name.Equals("Runner")) {
			Runner.AddAntigravity();
			gameObject.SetActive(false);
		}
	}
	
	public bool SpawnIfAvailable(Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range (0f, 100f)) {
			return false;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
		return true;
	}
	
	private void GameOver() {
		gameObject.SetActive(false);
	}
}
