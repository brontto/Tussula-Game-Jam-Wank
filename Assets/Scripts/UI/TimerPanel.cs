using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Customize panel behaviour
public class TimerPanel : MonoBehaviour {

    private float countdown4start = 30.1f;
    private float timer;


    [SerializeField]
    private Text timerTextField;
    [SerializeField]
    private string timerText = "Wank in: ";
    [SerializeField]
    private string endText = "Break a leg";
    [SerializeField]
    private float timerFontSpeed = Mathf.PI/8.0f;
    [SerializeField]
    private float timerSizeAmplitude = 5;


    private int fontSize;

    public void Init(float _countdown4start) {
        countdown4start = _countdown4start;
        timer = countdown4start;
        fontSize = timerTextField.fontSize;
    }

    public void UpdateMe() {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return)) {
            timer -= 5;
            if (timer < 5) timer = 5.5f;
        }

        if(timer<=5) {
            timerTextField.fontSize = (int)(fontSize + Mathf.Sin((timer-5.0f)* timerFontSpeed) * timerSizeAmplitude);
        }


        timerTextField.text = timerText + " " + ((int)(timer)).ToString();
    }

    public bool OutOfTime() {
        if(timer<=0) timerTextField.text = endText;
        return timer <= 0.0f; 
    }


}
