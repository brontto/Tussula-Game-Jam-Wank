using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
//using static Cinemachine.CinemachineTargetGroup;

public class GameManager : MonoBehaviour {

	public bool skipUI = false;

	public GameObject winParticles;

	public GameObject[] fireWorks;

	public AudioManager audioManager;

	public Text ranking;
	public Text winText;

	public Track[] tracks;

	public List<Player> players = new List<Player>();

	private List<int> finishedPlayers = new List<int> ();

	private BezierCurve bezierOuter;
	private BezierCurve bezierInner;

	private GameObject trackGameObject;
	private GameObject trackBezierOuter;
	private GameObject trackBezierInner;

	private List<GameObject> playerGameobjects = new List<GameObject> ();

	[HideInInspector] public float trackLength;

	public static GameManager instance;

	public bool raceStarted = false;

	public void AddPlayer(Player player) {
		players.Add (player);
	}

	public void ResetPlayers() {
		players.Clear ();
	}

	public void AddPlayers(List<Player> p) {
		players = p;
	}

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
	//	Init ();

		if (skipUI) {
			Destroy(GameObject.Find("UI") );
			Init();
			
		}

	}

	float speakTime = 0;
	void Update() {

		if (Time.time > speakTime) {
			speakTime = Time.time + 0.25f;

			if (raceStarted && Random.Range(0,100) < 33 && !audioManager.isAnnouncerTalking()) {
				audioManager.PlayRandomWank();
			}

		}

	}

	public void Init() {

		ResetRace ();
		CreateTrack ();
		CreatePlayers ();

		StartCoroutine (StartWanking ());

	}

	IEnumerator StartWanking() {

		yield return new WaitForSeconds (1f);

		audioManager.PlayReadySetGo ();

		yield return new WaitForSeconds (3.8f);

		raceStarted = true;

	}

	public void ResetRace() {

		foreach (GameObject g in playerGameobjects) {
			Destroy(g);
		}

		Destroy (trackGameObject);
		Destroy (trackBezierOuter);
		Destroy (trackBezierInner);

		finishedPlayers.Clear ();
		playerGameobjects.Clear ();
		winText.gameObject.SetActive (false);
	
	}

	public void FinishPlayer(int playerIndex, Vector3 position)  {
		finishedPlayers.Add (playerIndex);

		if (finishedPlayers.Count >= players.Count) {
			audioManager.PlayDisappointedWank();
			raceStarted = false;



			StartCoroutine (FinishRace ());
		} else {

			GameObject win = (GameObject) Instantiate(winParticles, position, Quaternion.identity);
			win.transform.parent = trackGameObject.transform;

			audioManager.PlayFinishWank();
		}
	}

	private IEnumerator FinishRace() {
		string name = playerGameobjects[ finishedPlayers[0]].GetComponent<MountData>().name;

		winText.gameObject.SetActive (true);
		winText.text = name + " WINS !!";

		yield return new WaitForSeconds (5f);

		//particles

		int amount = 10;

		while (amount > 0) {

			GameObject g = (GameObject) Instantiate(fireWorks[Random.Range(0, fireWorks.Length)], GetBezierPointOnLane(Random.Range(0f,1f)),Quaternion.identity);
			g.transform.parent = trackGameObject.transform;
			amount--;

		}

		//anthermemmadsf
		int anthem = playerGameobjects [finishedPlayers [0]].GetComponentInChildren<CharacterData> ().anthem;

		audioManager.PlayAnthem (anthem);

		while (audioManager.isAnthemPlaying()) {
	
			yield return null;
		}

		yield return new WaitForSeconds (3f);

		//start new match
		Init ();
	}
	                           

	public void CreateTrack() {

		int n = Random.Range (0, tracks.Length);

		trackGameObject = (GameObject)Instantiate (tracks [n].trackPrefab, Vector3.zero, Quaternion.identity);
		trackBezierOuter = (GameObject)Instantiate (tracks [n].trackOuterBezierPrefab, Vector3.zero, Quaternion.identity);
		trackBezierInner = (GameObject)Instantiate (tracks [n].trackInnerBezierPrefab, Vector3.zero, Quaternion.identity);

		trackBezierOuter.transform.parent = trackGameObject.transform;
		trackBezierInner.transform.parent = trackGameObject.transform;

	//	trackGameObject.AddComponent<rotator> ();

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

	public Transform SearchHierarchyForBone(Transform current, string name)   
	{
		// check if the current bone is the bone we're looking for, if so return it
		if (current.name == name)
			return current;
		// search through child bones for the bone we're looking for
		for (int i = 0; i < current.childCount; ++i)
		{
			// the recursive step; repeat the search one step deeper in the hierarchy
			Transform found = SearchHierarchyForBone(current.GetChild(i), name);
			// a transform was returned by the search above that is not null,
			// it must be the bone we're looking for
			if (found != null)
				return found;
		}
		
		// bone with name was not found
		return null;
	}
	
	void CreatePlayers() {
		
		GameObject cine = GameObject.Find ("TargetGroup1");

		foreach (Player player in players) {

			//mount
			GameObject mountModel = (GameObject)Instantiate(player.GetMount(), Vector3.zero, Quaternion.identity);
			mountModel.transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);

			PlsyerController controller = mountModel.AddComponent<PlsyerController>();
			controller.PlayerInfo = player;

			//rider
			GameObject riderModel = (GameObject)Instantiate(player.GetRider(), Vector3.zero, Quaternion.identity);

			//find riderpelvis
			GameObject riderPelvis = SearchHierarchyForBone(riderModel.transform, "pelvis").gameObject;
		//	SpringJoint spring = riderPelvis.AddComponent<SpringJoint>();

			//find mountpelvis
			GameObject mountPelvis = SearchHierarchyForBone(mountModel.transform, "ass").gameObject;
		//	spring.connectedBody = mountPelvis.GetComponent<Rigidbody>();
		//	spring.spring = 522f;

			controller.mountAss = mountPelvis;
			controller.riderPelvis = riderPelvis;
			controller.mountAnimator = mountModel.GetComponent<Animator>();

			//dont put this to parent until recursive searches are done
			riderModel.transform.parent = mountModel.transform;

			playerGameobjects.Add(mountModel);

			cine.GetComponent<TargetGroupHandler>().AddToList(mountModel);

		}


	}

}

[System.Serializable]
public class Track {
	public GameObject trackPrefab;
	public GameObject trackOuterBezierPrefab;
	public GameObject trackInnerBezierPrefab;

}

