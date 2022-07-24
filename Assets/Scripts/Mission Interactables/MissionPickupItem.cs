using UnityEngine;

public class MissionPickupItem : MonoBehaviour {
	public bool taken;

	void Start() {
		taken = false;
	}

	void playerInteraction() {
		taken = true;
	}
}
