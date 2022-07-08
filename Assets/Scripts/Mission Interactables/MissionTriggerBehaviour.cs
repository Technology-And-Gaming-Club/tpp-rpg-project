using System.Collections;
using UnityEngine;

public class MissionTriggerBehaviour : MonoBehaviour {
	public GameObject targetMission;
	public GameObject targetPlayer;
	public float markerRadius;
	float playerDist;

	void Update() {
		pollPlayerDist();

		if(playerDist < markerRadius) {
			targetMission.SendMessage("activateMission", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	void pollPlayerDist() {
		Vector3 playerPos, pos;
		playerPos = targetPlayer.transform.position - Vector3.up * targetPlayer.transform.position.y;
		pos = transform.position - Vector3.up * transform.position.y;

		playerDist = (playerPos - pos).magnitude;
	}
}