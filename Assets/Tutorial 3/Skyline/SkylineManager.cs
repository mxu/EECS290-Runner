using UnityEngine;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {

	public Transform prefab;
	public int numCubes;
	public float recycleOffset;
	public float greyOffset;
	public float hueOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.LevelUp += LevelUp;
		objectQueue = new Queue<Transform>(numCubes);
		for(int i = 0; i < numCubes; i++) {
			objectQueue.Enqueue((Transform)Instantiate(prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		}
		enabled = false;
	}
	
	void Update () {
		if(objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
			Recycle();
		}
	}
	
	private void GameStart() {
		setColors();
		nextPosition = startPosition;
		for(int i = 0; i < numCubes; i++) {
			Recycle();
		}
		enabled = true;
	}
	
	private void GameOver() {
		enabled = false;
	}

	private void LevelUp() {
		setColors();
	}

	private void setColors() {
		Color levelColor = ColorManager.offsetHue(ColorManager.offsetColor(GameEventManager.getColor(), greyOffset), hueOffset);
		foreach(Transform t in objectQueue)
			t.gameObject.renderer.material.color = levelColor;
	}
	
	private void Recycle() {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));
		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);
	}	
}
