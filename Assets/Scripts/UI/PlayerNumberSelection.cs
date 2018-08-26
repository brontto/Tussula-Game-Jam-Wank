using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumberSelection : MonoBehaviour {
    private int playerCount = 1;
    [SerializeField]
    private int maxPlayerCount = 16;

    RectTransform topTextPos, midTextPos;
    public Text topText, midText;
    private bool update;

    void Start() {
        topTextPos = topText.GetComponent<RectTransform>();
        midTextPos = midText.GetComponent<RectTransform>();
        UpdateElements();
    }

    void OnEnabled() {
        UpdateElements();
    }


    private void UpdateElements() {
        if (playerCount <= 0) playerCount = maxPlayerCount;
        if (playerCount > maxPlayerCount) playerCount = 1;
        midText.text = "< " + playerCount.ToString() + " >";   
    }


    public UISEQUENCE UpdateMe(){

        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            playerCount++;
            UpdateElements();
        }
               
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            playerCount--;
            UpdateElements();
        }
        

        if (Input.GetKeyDown(KeyCode.Return)) {
            return UISEQUENCE.BUTTONSELECT;
        }
        return UISEQUENCE.START;
    }

    public int GetPlayerCount() {
        return playerCount;
    }
}
