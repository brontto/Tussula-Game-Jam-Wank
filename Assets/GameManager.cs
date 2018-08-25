using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<Player> players = new List<Player>();

	public GameObject track;

	private BezierCurve bezier;


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
		bezier = track.GetComponent<BezierCurve> ();
		
		trackLength = bezier.length;
		Debug.Log (trackLength);

		CreatePlayers ();
	}

	public Vector3 GetBezierPoint(float value) {
		value = Mathf.Lerp (0, 1, value);
		return bezier.GetPointAt (value);

	}

	void CreatePlayers() {

		foreach (Player player in players) {

			GameObject mount = (GameObject)Instantiate(player.GetMount(), Vector3.zero, Quaternion.identity);

			PlsyerController controller = mount.AddComponent<PlsyerController>();
			controller.PlayerInfo = player;

		}

	}





}
