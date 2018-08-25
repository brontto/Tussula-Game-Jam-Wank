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
		}
	}

	private KeyCode leftKey;
	private KeyCode rightKey;

	private int previousDirection = 0;
	private int currentDirection = 0;
	private float velocity = 0f;

	// Use this for initialization
	void Start () {
		currentDirection = 0;
	}
	
	// Update is called once per frame
	void Update () {

		HandleControls ();
		
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
			
			if (currentDirection != previousDirection) {
				
			//	velocity = Mathf.Max(velocity - 
				
			}
			
		}

	}


}
