using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Track[] tracks;

	public List<Player> players = new List<Player>();

	private List<int> finishedPlayers = new List<int> ();

	public GameObject trackOuter;
	public GameObject trackInner;

	private BezierCurve bezierOuter;
	private BezierCurve bezierInner;

	private GameObject track;
	private GameObject trackBezierOuter;
	private GameObject trackBezierInner;

	[HideInInspector] public float trackLength;

	public static GameManager instance;

	public void AddPlayer(Player player) {
		players.Add (player);
	}

	public void ResetPlayers() {
		players.Clear ();
	}

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		Init ();

	}

	public void Init() {

		ResetRace ();
		CreateTrack ();
		CreatePlayers ();

	}

	public void ResetRace() {
		finishedPlayers.Clear ();

	}

	public void FinishPlayer(int playerIndex)  {
		finishedPlayers.Add (playerIndex);

		if (finishedPlayers.Count >= players.Count) {
			FinishRace();
		}
	}

	private void FinishRace() {

	}
	                           

	public void CreateTrack() {

		int n = Random.Range (0, tracks.Length);

		track = (GameObject)Instantiate (tracks [n].trackPrefab, Vector3.zero, Quaternion.identity);
		trackBezierOuter = (GameObject)Instantiate (tracks [n].trackOuterBezierPrefab, Vector3.zero, Quaternion.identity);
		trackBezierInner = (GameObject)Instantiate (tracks [n].trackInnerBezierPrefab, Vector3.zero, Quaternion.identity);
		
		bezierOuter = trackBezierOuter.GetComponent<BezierCurve> ();
		bezierInner = trackBezierInner.GetComponent<BezierCurve> ();		

		trackLength = bezierOuter.length;
		
	}

	public Vector3 GetBezierPointOnLane(float value, float playerIndex = 0) {
		if (value > 1) {
			value -= 1;
		}
		if (value < 0) {
			value += 1;
		}
		
		Vector3 outerPoint = bezierOuter.GetPointAt (value);
		Vector3 innerPoint = bezierInner.GetPointAt (value);

		Vector3 distance = outerPoint - innerPoint;

		distance = distance / (players.Count -1) * playerIndex;

		return innerPoint + distance;
	}

	void CreatePlayers() {

		foreach (Player player in players) {

			GameObject mount = (GameObject)Instantiate(player.GetMount(), Vector3.zero, Quaternion.identity);

			PlsyerController controller = mount.AddComponent<PlsyerController>();
			controller.PlayerInfo = player;

		}

	}

}

[System.Serializable]
public class Track {
	public GameObject trackPrefab;
	public GameObject trackOuterBezierPrefab;
	public GameObject trackInnerBezierPrefab;

}

