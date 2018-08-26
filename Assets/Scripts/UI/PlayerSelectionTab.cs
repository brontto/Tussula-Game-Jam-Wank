using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pair {
    public Pair(int _rider, int _mount){
        rider = _rider;
        mount = _mount;
    }
    public int rider;
    public int mount;
}

public class PlayerSelectionTab : MonoBehaviour {
    private int number = -1;
    [SerializeField]
    private Text nameField;
    [SerializeField] private Text rightKeyText;
    [SerializeField] private Text leftKeyText;


    [SerializeField] private MeshFilter rightPreview;
    [SerializeField] private MeshFilter leftPreview;

    private ResourceManager manager;
    int riderNumber = 0;
    int mountNumber = 0;

    MeshFilter riderGraphics;
    GameObject mountGraphics;

    private KeyCode left = KeyCode.None;
    private KeyCode right = KeyCode.None;

    private List<KeyCode> lists;

    public void Initialize(int number, ResourceManager manager) {
        this.number = number;
        nameField.text = "P"+number.ToString();
        leftKeyText.text = "Wait...";
        rightKeyText.text = "Wait...";
        this.manager = manager;
    }

 


    public void SetTextLeft(string name) {
        leftKeyText.text = name;
    }

    public void SetTextRight(string name) {
        rightKeyText.text = name;
    }


    private void SetNextRider() {
        riderNumber++;
        if (riderNumber >= manager.riders.Count) riderNumber = 0;
        leftPreview.sharedMesh = manager.GetRider(riderNumber).GetComponent<MeshFilter>().sharedMesh;
    }


    private void SetNextMount() {
        mountNumber++;
        if (mountNumber >= manager.mounts.Count) mountNumber = 0;
        rightPreview.sharedMesh = manager.GetMount(mountNumber).GetComponent<MeshFilter>().sharedMesh;
    }


    public void SetLeftButton(KeyCode keycode) {
        leftKeyText.text = keycode.ToString();
        SetNextRider();
        left = keycode;
    }

    public void SetRightButton(KeyCode keycode) {
        rightKeyText.text = keycode.ToString();
        SetNextMount();
        right = keycode;
        //mountGraphics.transform.SetParent(transform);
    }

    public Pair GetSelected() {
        return new Pair(riderNumber, mountNumber);
    }

    // Change!
    void Update () {
		if(left!=KeyCode.None) {
           
            if (Input.GetKeyDown(left)) {
                SetNextRider();
            }
        }

        if (right != KeyCode.None) {
            
            if (Input.GetKeyDown(right)) {
                SetNextMount();
            }
        }
    }
}
