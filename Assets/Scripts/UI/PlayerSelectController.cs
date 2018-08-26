using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour {
    public GameObject playerSelectionTab;
    public List<PlayerSelectionTab> tabs = new List<PlayerSelectionTab>();

    // Use this for initialization
    void OnEnable() {
        if (!playerSelectionTab) Debug.Log("PlayerSelectionTab not set");
    }


    public void CreateNPlayers(int nPlayers,ResourceManager manager) {
        for (int i = 0; i< nPlayers; ++i) {
            GameObject obj = (GameObject)Instantiate(playerSelectionTab, transform,false);
            obj.SetActive(true);
            PlayerSelectionTab tab = obj.GetComponent<PlayerSelectionTab>();
            tab.Initialize(i, manager);
            tabs.Add(tab);
        }
    }

    public void ClearMe() {
        foreach(PlayerSelectionTab tab in tabs) {
            Destroy(tab.gameObject);
        }
        tabs.Clear();
    }



    public void SetTextLeft(int i, string text) {
        tabs[i].SetTextLeft(text);
    }

    public void SetTextRight(int i, string text) {
        tabs[i].SetTextRight(text);
    }


    public void SetLeftButton(int i, KeyCode keycode) {
        tabs[i].SetLeftButton(keycode);
    }

    public void SetRightButton(int i, KeyCode keycode) {
        tabs[i].SetRightButton(keycode);
    }




}
