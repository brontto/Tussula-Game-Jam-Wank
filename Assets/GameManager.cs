using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text ranking;
	public Text winText;

	public Track[] tracks;

	public List<Player> players = new List<Player>();

	private List<int> finishedPlayers = new List<int> ();

	private BezierCurve bezierOuter;
	private BezierCurve bezierInner;

	private GameObject track;
	private GameObject trackBezierOuter;
	private GameObject trackBezierInner;

	private List<GameObject> playerGameobjects = new List<GameObject> ();

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

	//void Update() {

	//}

	public void Init() {

		ResetRace ();
		CreateTrack ();
		CreatePlayers ();

	}

	public void ResetRace() {
		finishedPlayers.Clear ();
		playerGameobjects.Clear ();
		winText.gameObject.SetActive (false);

	}

	public void FinishPlayer(int playerIndex)  {
		finishedPlayers.Add (playerIndex);

		if (finishedPlayers.Count >= players.Count) {
			StartCoroutine(FinishRace());
		}
	}

	private IEnumerator FinishRace() {
		string name = playerGameobjects[ finishedPlayers[0]].GetComponent<MountData>().name;

		winText.gameObject.SetActive (true);
		winText.text = name + " WINS !!";

		yield return new WaitForSeconds (2f);
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

			GameObject mountModel = (GameObject)Instantiate(player.GetMount(), Vector3.zero, Quaternion.identity);

			PlsyerController controller = mountModel.AddComponent<PlsyerController>();
			controller.PlayerInfo = player;

			GameObject riderModel = (GameObject)Instantiate(player.GetRider(), Vector3.zero, Quaternion.identity);
			riderModel.transform.parent = mountModel.transform;

			SpringJoint spring = riderModel.AddComponent<SpringJoint>();
			spring.connectedBody = mountModel.GetComponent<Rigidbody>();

			spring.spring = 1222f;

			playerGameobjects.Add(mountModel);


		}

	}

}

[System.Serializable]
public class Track {
	public GameObject trackPrefab;
	public GameObject trackOuterBezierPrefab;
	public GameObject trackInnerBezierPrefab;

}

