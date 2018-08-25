using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionTab : MonoBehaviour {
    private int number = -1;
    [SerializeField]
    private Text nameField;
    [SerializeField]
    private Text rightKeyText;
    [SerializeField]
    private Text leftKeyText;
	
    public void Initialize(int number) {
        this.number = number;
        nameField.text = "P"+number.ToString();
    }



    public void SetLeftButton(string name) {
        leftKeyText.text = name;
    }

    public void SetRightButton(string name) {
        rightKeyText.text = name;
    }



    // Update is called once per frame
    void Update () {
		
	}
}
