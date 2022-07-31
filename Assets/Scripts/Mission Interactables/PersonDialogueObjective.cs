using System.Collections;
using UnityEngine;

public class PersonDialogueObjective : MissionObjective {
	public GameObject[] targetNPC;
	public GameObject targetPlayer;

	public string[] dialogues;
	// 0 -> player
	// 1-n -> NPC
	public int[] speakingObject;
	

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public override void play() {
		
	}
}