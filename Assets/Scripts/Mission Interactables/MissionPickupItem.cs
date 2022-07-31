using UnityEngine;

public class MissionPickupItem : MissionObjective {
	void Start() {
		passed = false;
	}

	void playerInteraction() {
		passed = true;
	}

	public override void play() {
		
 	}
}
