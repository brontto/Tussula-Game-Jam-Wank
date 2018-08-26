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


    void Start() {
        topTextPos = topText.GetComponent<RectTransform>();
        midTextPos = midText.GetComponent<RectTransform>();
    }

    public UISEQUENCE UpdateMe(){

        if(Input.GetKeyDown(KeyCode.RightArrow)) playerCount++;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) playerCount--;

        if (playerCount <= 0) playerCount = maxPlayerCount;
        if (playerCount > maxPlayerCount) playerCount = 1;

        if (midText.text != "< " + playerCount.ToString() + " >") {
            midText.text = "< " + playerCount.ToString() + " >";
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            return UISEQUENCE.BUTTONSELECT;
            gameObject.SetActive(false);
        }
        return UISEQUENCE.START;
    }

    public int GetPlayerCount() {
        return playerCount;
    }
}
