using System.Collections;
using UnityEngine;

public class MissionTriggerBehaviour : MonoBehaviour {
	public GameObject targetMission;
	public GameObject targetPlayer;

	void Update() {
		
	}

	void pollPlayerDist() {
		Vector3 playerPos, pos;
		RaycastHit hit;
	}

	void activateMission() {
		targetMission.SendMessage("activateMission", null, SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody.gameObject == targetPlayer) {
			targetMission.SendMessage("targetMarkerActivated", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	void OnCollisionStay(Collision collision) {
		if(collision.rigidbody.gameObject == targetPlayer) {
			targetMission.SendMessage("targetMarkerActivated", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}