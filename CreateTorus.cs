using UnityEngine;
using System.Collections;

public class CreateTorus : MonoBehaviour {

	public GameObject gameTile;
	public float TilesPerRing = 108; 
	public float RingRadius = 30; 


	// Use this for initialization
	void Start () {
		createRing (gameTile, TilesPerRing, RingRadius);
	}

	void createRing (GameObject item, float itemCount, float radius) {
		for (int i = 1; i <= itemCount; i++) {
			float currentAngle = 2 * Mathf.PI / itemCount * i;
			float newX = radius * Mathf.Cos (currentAngle);
			float newY = radius * Mathf.Sin (currentAngle);
			GameObject newItem = Instantiate(item, new Vector3 (newX, 0, newY), Quaternion.Euler (0, -360/itemCount * i, 0)) as GameObject;
			newItem.transform.parent = transform;
		}
	}
}
