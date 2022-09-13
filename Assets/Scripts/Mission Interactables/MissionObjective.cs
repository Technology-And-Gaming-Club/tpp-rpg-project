using System.Collections;
using UnityEngine;

abstract public class MissionObjective : MonoBehaviour {
	public bool started;
	public float progress;
	public bool passed;

	// Use this for initialization
	void Start() {
		started = false;
		passed = false;
		progress = 0;
	}

	// Update is called once per frame

	abstract public void play();
}