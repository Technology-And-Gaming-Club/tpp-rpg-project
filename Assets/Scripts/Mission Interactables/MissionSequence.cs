// Dynamic Mission Sequence manager
using System.Collections;
using UnityEngine;

public class MissionSequence : MonoBehaviour {
	public string missionName;

	public MissionObjective[] objectives;
	public int currentObjective;
	public bool started;

	// Use this for initialization
	void Start() {
		currentObjective = 0;
		started = false;
	}

	// Update is called once per frame
	void Update() {
		if(started && !objectives[currentObjective].started) {
			objectives[currentObjective].play();
		}
	}

	public void activateMission() {
		started = true;
	}
}