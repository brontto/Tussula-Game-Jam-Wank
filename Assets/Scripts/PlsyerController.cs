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

	private float position = 0;

	private GameManager gman;

	private bool isFinished = false;

	// Use this for initialization
	void Start () {
		gman = GameManager.instance;
		currentDirection = 0;
		t = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		HandleControls ();

		FollowBezier ();
	}

	void FollowBezier() {

		position += velocity / (gman.trackLength * 40);

		t.position = gman.GetBezierPointOnLane (position, (float)playerInfo.getIndex());

		//look to future location to rotate mounts properly
		t.LookAt(gman.GetBezierPointOnLane (position + 0.01f, (float)playerInfo.getIndex()));

		if (position > 1 && !isFinished) {
			playerFinished();
			position -= 1;
		}
		
	}

	private void playerFinished() {
		gman.FinishPlayer (playerInfo.getIndex ());
		isFinished = true;

	}

	private float calculateDistortion () {

		Vector3 currentPosition =  gman.GetBezierPointOnLane (position, (float)playerInfo.getIndex());
		Vector3 nextPosition = gman.GetBezierPointOnLane (position + 0.01f, (float)playerInfo.getIndex());
	
		return 0;
	}


	void HandleControls() {

		if (isFinished) {
			return;
		}

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

		velocity *= 1 - mountInfo.drag;

	}


}
