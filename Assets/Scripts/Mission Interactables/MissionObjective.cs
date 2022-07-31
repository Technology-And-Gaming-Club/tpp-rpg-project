using System.Collections;
using UnityEngine;

abstract public class MissionObjective : MonoBehaviour {
	public bool started;
	public bool passed;

	// Use this for initialization
	void Start() {
		started = false;
	}

	// Update is called once per frame
	void Update() {

	}

	abstract public void play();
}