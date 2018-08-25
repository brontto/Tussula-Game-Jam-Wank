using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour {
    public GameObject playerSelectionTab;
    private List<PlayerSelectionTab> tabs;

    // Use this for initialization
    void OnEnable() {
        if (!playerSelectionTab) Debug.Log("PlayerSelectionTab not set");
        tabs = new List<PlayerSelectionTab>();
    }


    public void CreateNPlayers(int nPlayers) {
        
        for (int i = 0; i< nPlayers; ++i) {
            GameObject obj = (GameObject)Instantiate(playerSelectionTab);
            obj.transform.SetParent(transform);
            obj.SetActive(true);
            PlayerSelectionTab tab = obj.GetComponent<PlayerSelectionTab>();
            tab.Initialize(i);
            tabs.Add(tab);
        }
    }

    public void SetLeftButton(int i, string name) {
        tabs[i].SetLeftButton(name);
    }

    public void SetRightButton(int i, string name) {
        tabs[i].SetRightButton(name);
    }



    // Update is called once per frame
    void Update () {
		
	}
}
