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
			mountInfo = new MountData();

			//purkkaaa
			mountInfo.drag = 0.015f;
			mountInfo.acceleration = 0.1f;
			mountInfo.decelerationFromFailures = 0.2f;
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

	public GameObject rider;
	public GameObject riderPelvis;
	public GameObject mountAss;

	// Use this for initialization
	void Start () {
		gman = GameManager.instance;
		currentDirection = 0;
		t = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		FollowBezier ();

		if (!gman.raceStarted) {
			return;
		}

		HandleControls ();
			
	}

	void FollowBezier() {

		//lowly slowdown all the time
		velocity *= 1 - mountInfo.drag;

		position += velocity / (gman.trackLength * 40);

		t.position = gman.GetBezierPointOnLane (position, (float)playerInfo.getIndex());

		riderPelvis.transform.position = mountAss.transform.position;

		//look to future location to rotate mounts properly
		t.LookAt(gman.GetBezierPointOnLane (position + 0.01f, (float)playerInfo.getIndex()));

		if (position > 1 && !isFinished) {
			playerFinished();
			position -= 1;
		}
		
	}

	private void playerFinished() {
		gman.FinishPlayer (playerInfo.getIndex (), transform.position);
		isFinished = true;

	}

	private float calculateDistortion () {

		Vector3 currentPosition =  gman.GetBezierPointOnLane (position, (float)playerInfo.getIndex());
		Vector3 nextPosition = gman.GetBezierPointOnLane (position + 0.01f, (float)playerInfo.getIndex());
	
		return 0;
	}

	public float getPosition() {
		return position;
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

				Rigidbody[] rigids = riderPelvis.GetComponentsInChildren<Rigidbody>();
				foreach(Rigidbody r in rigids) {
					r.AddForce (r.transform.right * currentDirection * r.mass * 600);
					velocity += mountInfo.acceleration;
				}

		

			}
			
		}



	}


}
