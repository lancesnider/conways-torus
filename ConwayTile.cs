using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConwayTile : MonoBehaviour {

	public bool isOn = false;
	public Material materialOn;
	public Material materialOff;
	private Renderer rend; 
	private float updateTime = 0.3f;
	private IEnumerator coroutine;
	public List<ConwayTile> neighbors = new List<ConwayTile>();
	private int activeNeighbors;
	private Collider thisCollider;

	void Start (){
		thisCollider = gameObject.GetComponent<Collider> ();
		rend = GetComponent<Renderer>();
		StartWithRandomTiles();
		findNeighbors ();
		InvokeRepeating("checkAllColliders", 0, updateTime);
		InvokeRepeating("CheckRules", 0.1f, updateTime);
	}

	void StartWithRandomTiles(){
		if (Random.Range (0, 6) == 1) {
			changeTileState (true);
		}
	}
		
	void changeTileState (bool newState) { 
		if (!isOn && newState) {
			rend.sharedMaterial = materialOn;
		} else if(isOn && !newState) {
			rend.sharedMaterial = materialOff;
		}
		isOn = newState;
	}

	void findNeighbors() {
		thisCollider.enabled = false;
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
		thisCollider.enabled = true;
		foreach(Collider collider in hitColliders){
			neighbors.Add(collider.GetComponent<ConwayTile> ());
		}
	}

	void checkAllColliders() {
		activeNeighbors = 0;
		foreach (ConwayTile neighbor in neighbors) {
			if (neighbor.isOn == true)
				activeNeighbors++;
		}
	}

	void CheckRules(){
		if (isOn == true) {
			if (activeNeighbors <= 1 || activeNeighbors >= 4) {
				changeTileState (false);
			}
		} else {
			if (activeNeighbors == 3)
				changeTileState (true);
		}
	}
}
