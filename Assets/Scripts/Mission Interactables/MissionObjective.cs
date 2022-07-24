using System.Collections;
using UnityEngine;

public class MissionObjective : MonoBehaviour {
	public bool started;

	// Use this for initialization
	void Start() {
		started = false;
	}

	// Update is called once per frame
	void Update() {

	}

	public void play() {
		started = true;
	}
}