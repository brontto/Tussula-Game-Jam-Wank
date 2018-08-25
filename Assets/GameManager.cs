using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<Player> players = new List<Player>();

	public GameObject track;

	private BezierCurve bezierOuter;
	private BezierCurve bezierInner;


	[HideInInspector] public float trackLength;

	public static GameManager instance;

	public void AddPlayer(Player player) {
		players.Add (player);
	}

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		bezierOuter = track.GetComponent<BezierCurve> ();
		
		trackLength = bezierOuter.length;
		Debug.Log (trackLength);

		CreatePlayers ();
	}

	public Vector3 GetBezierPoint(float value) {
		value = Mathf.Lerp (0, 1, value);
		return bezierOuter.GetPointAt (value);

	}

	public Vector3 GetBezierPointOnLane(float value, int playerIndex = 0) {
		value = Mathf.Lerp (0, 1, value);

		Vector3 outerPoint = bezierOuter.GetPointAt (value);
		Vector3 innerPoint = bezierInner.GetPointAt (value);

		Vector3 distance = outerPoint - innerPoint;

		distance = distance / players.Count * playerIndex;

		return Vector3.zero;
	}

	void CreatePlayers() {

		foreach (Player player in players) {

			GameObject mount = (GameObject)Instantiate(player.GetMount(), Vector3.zero, Quaternion.identity);

			PlsyerController controller = mount.AddComponent<PlsyerController>();
			controller.PlayerInfo = player;

		}

	}





}
