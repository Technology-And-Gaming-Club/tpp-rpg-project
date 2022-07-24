using System.Collections;
using UnityEngine;

public class PositionTriggerBehaviour : MonoBehaviour {
	public int id;
	public bool active;
	public bool passed;
	public GameObject targetPlayer;

	float playerDist;

	// Use this for initialization
	void Start() {
		passed = false;
		active = false;
	}

	// Update is called once per frame
	void Update() {
		if(active) {
			pollPlayerDist();
			if(playerDist < transform.localScale.x) {
				active = true;
			}
		}
	}

	public void activate() {
		active = true;
	}

	void pollPlayerDist() {
		Vector3 playerPos, pos;
		playerPos = targetPlayer.transform.position - Vector3.up * targetPlayer.transform.position.y;
		pos = transform.position - Vector3.up * transform.position.y;

		playerDist = (playerPos - pos).magnitude;
	}
}