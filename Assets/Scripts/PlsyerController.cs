using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlsyerController : MonoBehaviour {

	private Player playerInfo;
	public Player PlayerInfo {
		get {
			return playerInfo;
		}
		set {
			playerInfo = value;
			leftKey = playerInfo.getLeftKey();
			rightKey = playerInfo.getRightKey();	
			mountInfo = playerInfo.GetMount().GetComponent<MountData>();
		}
	}

	private KeyCode leftKey;
	private KeyCode rightKey;

	private int previousDirection = 0;
	private int currentDirection = 0;
	private float velocity = 0f;

	private MountData mountInfo;

	private Transform t;

	// Use this for initialization
	void Start () {
		currentDirection = 0;
		t = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		HandleControls ();

		velocity *= 1 - mountInfo.drag;
		t.Translate (Vector3.forward * velocity / 100f);
	}

	void HandleControls() {

		previousDirection = currentDirection;
		
		bool pressedButton = false;
		
		if (Input.GetKeyDown (leftKey)) {
			pressedButton = true;
			currentDirection = -1;
		}      
		
		if (Input.GetKeyDown (rightKey)) {
			pressedButton = true;
			currentDirection = 1;
		}    
		
		if (pressedButton) {
			
			if (currentDirection == previousDirection) {
				
				velocity = Mathf.Max(velocity - mountInfo.decelerationFromFailures,0);
				
			} else {

				velocity += mountInfo.acceleration;

			}
			
		}

	}


}
