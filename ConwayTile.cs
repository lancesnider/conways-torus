using UnityEngine;
using System.Collections;

public class ConwayTile : MonoBehaviour {

	public bool isOn = false;
	public Material materialOn;
	public Material materialOff;
	private Renderer rend; 
	private float updateTime = 0.3f;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start (){
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		OnOrOff ();
		InvokeRepeating("checkAllColliders", 0, updateTime);
	}
		

	void OnOrOff(){
		if (Random.Range (0, 16) == 1) {
			changeCellState (true);
		}
	}

	void changeCellState (bool newState) {
		if (!isOn && newState) {
			rend.sharedMaterial = materialOn;
		} else if(isOn && !newState) {
			rend.sharedMaterial = materialOff;
		}
		isOn = newState;
	}

	void checkAllColliders() {
		gameObject.GetComponent<Collider>().enabled = false;
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
		gameObject.GetComponent<Collider>().enabled = true;
		int activeNeighbors = 0;
		foreach(Collider neighbor in hitColliders){
			ConwayTile otherTile = neighbor.GetComponent<ConwayTile>();
			if(otherTile.isOn == true) 
				activeNeighbors++;
		}

		CheckRules (activeNeighbors);

	}

	void CheckRules(int activeNeighbors){
		if (isOn == true) {
			if (activeNeighbors <= 1 || activeNeighbors >= 4) {
				changeCellState (false);
			}
		} else {
			if (activeNeighbors == 3)
				changeCellState (true);
		}
	}

}
