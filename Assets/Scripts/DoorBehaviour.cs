using System.Collections;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {
	public enum states { open, closed };

	[Header("Animation")]
	public bool inAnim;
	public float animTime;
	public float animDuration;
	public float closeAngle;
	public float openAngle;

	[Header("States")]
	public states state;
	public states target;

	void Start() {
		state = states.closed;
		target = states.closed;
		inAnim = false;
	}

	void FixedUpdate() {
		if(state != target) {
			inAnim = true;
			if(target == states.open) {
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Time.fixedDeltaTime / animDuration * new Vector3(0, (openAngle - closeAngle), 0));
				animTime += Time.fixedDeltaTime;

				if(animTime > animDuration) {
					state = states.open;
					inAnim = false;
					animTime = 0;
				}
			} else {
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Time.fixedDeltaTime / animDuration * new Vector3(0, (closeAngle - openAngle), 0));
				animTime += Time.fixedDeltaTime;

				if(animTime > animDuration) {
					state = states.closed;
					inAnim = false;
					animTime = 0;
				}
			}
		}
	}

	void playerInteraction() {
		if(inAnim) {
			return;
		}
		if(state == target) {
			if(state == states.open) {
				target = states.closed;
			} else {
				target = states.open;
			}
		}
	}
}