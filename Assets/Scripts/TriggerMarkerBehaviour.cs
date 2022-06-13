using System.Collections;
using UnityEngine;

public class TriggerMarkerBehaviour : MonoBehaviour {
	public GameObject targetScript;
	public GameObject targetPlayer;

	void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody.gameObject == targetPlayer) {
			targetScript.SendMessage("targetMarkerActivated", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}