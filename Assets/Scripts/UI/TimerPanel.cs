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
    

    public void Init(float _countdown4start) {
        countdown4start = _countdown4start;
        timer = countdown4start;
    }

    public void UpdateMe() {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return)) {
            timer -= 5;
            if (timer < 5) timer = 5 + 1;
        }

        

        timerTextField.text = timerText + " " + ((int)(timer)).ToString();
    }

    public bool OutOfTime() {
        return timer <= 0.0f; 
    }


}
