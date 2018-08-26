using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles players' key binding selections and also which rider and mount are chosen.
//When timer goes to zero, go to next UISEQUENCE. Escape key goes back to previous sequence

//Refactor Timer to its own class
public class PlayerCharacterSelection : MonoBehaviour {
    private List<Player> players;
    private int playerCount = 0;
    private KeyCode[] keyBoard = new KeyCode[] { KeyCode.Backspace, KeyCode.Delete, KeyCode.Tab, KeyCode.Clear, KeyCode.Pause, KeyCode.Space, KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9, KeyCode.KeypadPeriod, KeyCode.KeypadPeriod, KeyCode.KeypadDivide, KeyCode.KeypadMultiply, KeyCode.KeypadMinus, KeyCode.KeypadPlus, KeyCode.KeypadEnter, KeyCode.KeypadEnter, KeyCode.KeypadEquals, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.Insert, KeyCode.Home, KeyCode.End, KeyCode.PageUp, KeyCode.PageDown, KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12, KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };
    private List<KeyCode> keysInUse;
    bool buttonSelectActive;

    [SerializeField]
    private PlayerSelectController playerSelectController;
    
    [SerializeField]
    private float countdown4start = 30;

    [SerializeField]
    private TimerPanel timer;

    private Coroutine co;
    private ResourceManager resourceManager;
  

    //Find resource manager from the scene
    private void FindResourceManager() {
        if (!resourceManager)  resourceManager = (ResourceManager)FindObjectOfType(typeof(ResourceManager));
    }

    void Start() {
        FindResourceManager();
    }

    void OnEnabled() {
        FindResourceManager();
    }

    void OnDisable() {
        KillMe();
    }


    //Use this to initialize the tabs
    public void Initialize(int _playerCount) {
        timer.Init(countdown4start);
        playerCount = _playerCount;
        buttonSelectActive = false;
        players = new List<Player>();
        keysInUse = new List<KeyCode>(playerCount*2);
        buttonSelectActive = false;
        FindResourceManager();
        playerSelectController.CreateNPlayers(playerCount, resourceManager);
        //Debug.Log(resourceManager);
        players.Clear();
        co = StartCoroutine(setButtons());
    }

    //Destroy everything before calling initialize again
    private void KillMe() {
        playerSelectController.ClearMe();
        gameObject.SetActive(false);
        keysInUse.Clear();
        StopCoroutine(co);
    }


    //Decrement timer and control the sequence flow
    public UISEQUENCE UpdateMe(){
        timer.UpdateMe();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            KillMe();
            return UISEQUENCE.START;
        }

        if (timer.OutOfTime()) {
            StopCoroutine(co);
            return UISEQUENCE.READY;
        }

        return UISEQUENCE.BUTTONSELECT;
    }

    //Set resources for each player here. 
    public List<Player> GetPlayers() {

        for (int i = 0; i < players.Count; i++) {
            Pair indx = playerSelectController.tabs[i].GetSelected();
            players[i].SetMount(resourceManager.mounts[indx.mount]);
            players[i].SetRider(resourceManager.riders[indx.rider]);
        }
        
        return players;
    }

    //Set buttons for each player
    private IEnumerator setButtons() {
        for (int i = 0; i < playerCount; i++) {
            players.Add(new Player(i));
            playerSelectController.SetTextLeft(i, "Select");

            while (true) {
                foreach (var item in keyBoard) {
                    if (!Input.GetKeyDown(item)) continue;
                    if (keysInUse.Contains(item)) continue;
                    
                    if (players[i].getLeftKey() == KeyCode.None) {
                        playerSelectController.SetLeftButton(i, item);
                        players[i].setLeftKey(item);
                        keysInUse.Add(item);
                        playerSelectController.SetTextRight(i, "Select");
                    } else if (players[i].getRightKey() == KeyCode.None) {
                        playerSelectController.SetRightButton(i, item);
                        players[i].setRightKey(item);
                        keysInUse.Add(item);
                    }
                }

                if (players[i].getLeftKey() != KeyCode.None && players[i].getRightKey() != KeyCode.None) {

                    break;
                }

                yield return null;
            }
            //Debug.Log(players[i].getKeyDetails());
        }
    }

   
    //Check if the key binding is already taken 
    //obsolete
    private bool checkIfUsed(KeyCode[] items, KeyCode i) {
        foreach (var item in items) {
            if (item == i) {
                return true;
            }
        }
        return false;
    }


}
