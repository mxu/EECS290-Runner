using UnityEngine;

public class PlatformMover : MonoBehaviour {

	public Material moving;

	private bool isRotating;
	private bool isMoving;
	private bool movingUp;
	private float minY, maxY;

	void Update () {
		if(isMoving) {
			transform.Translate(0f, 5f * Time.deltaTime * (movingUp ? 1f : -1f), 0f);

			if(transform.localPosition.y > maxY) {
				movingUp = false;
			}
			if(transform.localPosition.y < minY) {
				movingUp = true;
			}
		}
	}

	public void Recycle(float min, float max) {
		isMoving = Random.Range(0, 100) > 80;
		if (!isMoving) {
			isRotating = Random.Range (0, 100) > 80;
		}
		if(isMoving) {
			minY = min;
			maxY = max;
			//gameObject.renderer.material = moving;
		}
		else if (isRotating) {
			//gameObject.rigidbody.constraints = RigidbodyConstraints.None;  THIS SHIT DONE FUCKED UP
			//gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}

	}

	public bool getMoving () {
		return isMoving;
	}
}
