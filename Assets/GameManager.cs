using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<Player> players = new List<Player>();

	public void AddPlayer(Player player) {
		players.Add (player);
	}

	// Use this for initialization
	void Start () {
		CreatePlayers ();
	}

	void CreatePlayers() {

		foreach (Player player in players) {

			GameObject mount = (GameObject)Instantiate(player.GetMount(), Vector3.zero, Quaternion.identity);

			PlsyerController controller = mount.AddComponent<PlsyerController>();
			controller.PlayerInfo = player;

		}

	}



}
