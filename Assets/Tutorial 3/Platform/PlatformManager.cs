using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {
	
	public Transform prefab;
	public int numCubes;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	public Booster booster;
	public Antigravity antigravity;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numCubes);
		for(int i = 0; i < numCubes; i++)
			objectQueue.Enqueue((Transform) Instantiate(prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		enabled = false;
	}
	
	void Update () {
		if(objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
			Recycle();
		}
	}
	
	private void GameStart() {
		nextPosition = startPosition;
		for(int i = 0; i < numCubes; i++) {
			Recycle();
		}
		enabled = true;
	}
	
	private void GameOver() {
		enabled = false;
	}
	
	private void Recycle() {
		float lvl = GameEventManager.getLevel() - 1;

		Vector3 scale = new Vector3(
			Random.Range(Mathf.Max(minSize.x - lvl * 0.2f, 1f), Mathf.Max(maxSize.x - lvl * 0.1f, 2f)),
			0.5f,
			1f);
		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		bool spawnBoost = booster.SpawnIfAvailable(position);
		if(!spawnBoost)
			antigravity.SpawnIfAvailable(position);

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		int materialIndex = Random.Range(0, materials.Length);
		o.renderer.material = materials[materialIndex];
		o.collider.material = physicMaterials[materialIndex];
		o.localRotation = new Quaternion (0f, 0f, 0f, 0f);
		o.gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
		o.gameObject.GetComponent<PlatformMover>().Recycle(minY - Random.Range(0, 2), maxY + Random.Range(0, 2));
		nextPosition += new Vector3(
			Random.Range(minGap.x + lvl * 0.5f, maxGap.x + lvl * 0.5f) + scale.x,
			Random.Range(minGap.y - lvl * 0.1f, maxGap.y + lvl * 0.1f),
			Random.Range(0, 0));
		if(nextPosition.y < minY) {
			nextPosition.y = minY + maxGap.y;
		} else if (nextPosition.y > maxY) {
			nextPosition.y = maxY - maxGap.y;
		}
		objectQueue.Enqueue(o);
	}	
}
