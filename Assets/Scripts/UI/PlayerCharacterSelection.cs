using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCharacterSelection : MonoBehaviour {
    private List<Player> players;
    private int playerCount = 0;
    private KeyCode[] keyBoard = new KeyCode[] { KeyCode.Backspace, KeyCode.Delete, KeyCode.Tab, KeyCode.Clear, KeyCode.Pause, KeyCode.Space, KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9, KeyCode.KeypadPeriod, KeyCode.KeypadPeriod, KeyCode.KeypadDivide, KeyCode.KeypadMultiply, KeyCode.KeypadMinus, KeyCode.KeypadPlus, KeyCode.KeypadEnter, KeyCode.KeypadEnter, KeyCode.KeypadEquals, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.Insert, KeyCode.Home, KeyCode.End, KeyCode.PageUp, KeyCode.PageDown, KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12, KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };
    private KeyCode[] keysInUse;
    bool buttonSelectActive;

    [SerializeField]
    private PlayerSelectController playerSelectController;
    
    [SerializeField]
    private float countdown4start = 30;

    private float timer;
   
    private Coroutine co;
    private ResourceManager resourceManager;
    [SerializeField]
    private Text timerText;

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

    public void Initialize(int _playerCount) {
        timer = countdown4start;
        playerCount = _playerCount;
        buttonSelectActive = false;
        players = new List<Player>();
        keysInUse = new KeyCode[playerCount * 2];
        buttonSelectActive = false;
        FindResourceManager();
        playerSelectController.CreateNPlayers(playerCount, resourceManager);
        //Debug.Log(resourceManager);
        players.Clear();
        co = StartCoroutine(setButtons());
    }

    private void KillMe() {
        playerSelectController.ClearMe();
        gameObject.SetActive(false);
        StopCoroutine(co);
    }


    public UISEQUENCE UpdateMe(){
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("ESCAPE");
            KillMe();
            return UISEQUENCE.START;
        }

        timer -= Time.deltaTime;
        timerText.text = "Start in: " +((int)(timer)).ToString();
        if (timer <= 0.0f) {
            StopCoroutine(co);
            return UISEQUENCE.READY;
        }

        return UISEQUENCE.BUTTONSELECT;
    }



    public List<Player> GetPlayers() {

        for (int i = 0; i < players.Count; i++) {
            Pair indx = playerSelectController.tabs[i].GetSelected();
            players[i].SetMount(resourceManager.mounts[indx.mount]);
            players[i].SetRider(resourceManager.riders[indx.rider]);
        }
        
        return players;
    }



    private IEnumerator setButtons() {
        for (int i = 0; i < playerCount; i++) {
            players.Add(new Player(i));
            playerSelectController.SetTextLeft(i, "Select");

            while (true) {
                foreach (var item in keyBoard) {
                    if (Input.GetKeyDown(item) && players[i].getLeftKey() == KeyCode.None && checkIfUsed(keysInUse, item) == false) {
                        playerSelectController.SetLeftButton(i, item);
                        players[i].setLeftKey(item);
                        keysInUse[2 * i] = item;
                        playerSelectController.SetTextRight(i, "S. Right Button");
                    } else if (Input.GetKeyDown(item) && players[i].getRightKey() == KeyCode.None && item != players[i].getLeftKey() && checkIfUsed(keysInUse, item) == false) {
                        playerSelectController.SetRightButton(i, item);
                        players[i].setRightKey(item);
                        keysInUse[2 * i + 1] = item;
                    }
                }

                if (players[i].getLeftKey() != KeyCode.None && players[i].getRightKey() != KeyCode.None) {

                    break;
                }

                yield return null;
            }
            Debug.Log(players[i].getKeyDetails());
        }
    }

   
    private bool checkIfUsed(KeyCode[] items, KeyCode i) {
        foreach (var item in items) {
            if (item == i) {
                return true;
            }
        }
        return false;
    }


}
