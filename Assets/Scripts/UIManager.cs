using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Text topText, midText;
	private string uiSequence = "start";
	private int playerCount = 1;
	private bool buttonSelectActive = false;
	public Player[] players;
	private KeyCode[] keyBoard = new KeyCode[] { KeyCode.Backspace, KeyCode.Delete, KeyCode.Tab, KeyCode.Clear, KeyCode.Pause, KeyCode.Escape, KeyCode.Space, KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9, KeyCode.KeypadPeriod, KeyCode.KeypadPeriod, KeyCode.KeypadDivide, KeyCode.KeypadMultiply, KeyCode.KeypadMinus, KeyCode.KeypadPlus, KeyCode.KeypadEnter, KeyCode.KeypadEnter, KeyCode.KeypadEquals, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.Insert, KeyCode.Home, KeyCode.End, KeyCode.PageUp, KeyCode.PageDown, KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12, KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };
	private KeyCode[] keysInUse;
	RectTransform topTextPos, midTextPos;
    public GameObject playerSelectUI;
    private PlayerSelectController playerSelectController;

	void Start()
	{
		topTextPos = topText.GetComponent<RectTransform>();
		midTextPos = midText.GetComponent<RectTransform>();
        playerSelectUI.SetActive(false);
	}



	void FixedUpdate()
	{
		switch(uiSequence)
		{
			case "start":
				playersSelect();
				break;
			case "buttonSelect":
				if (!buttonSelectActive)
				{
					buttonSelectActive = true;
                    playerSelectUI.SetActive(true);
                    playerSelectController = playerSelectUI.GetComponent<PlayerSelectController>();
                    playerSelectController.CreateNPlayers(playerCount);

                    StartCoroutine(setButtons());
				}
				break;
		}
	}

	void playersSelect()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) && playerCount < 16)
		{
			playerCount++;
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) && playerCount > 1)
		{
			playerCount--;
		}

		if (midText.text != "< " + playerCount.ToString() + " >")
		{
			midText.text = "< " + playerCount.ToString() + " >";
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			players = new Player[playerCount];
			keysInUse = new KeyCode[playerCount*2];
			uiSequence = "buttonSelect";
		}
	}

	IEnumerator setButtons()
	{
	
		for (int i = 0; i < playerCount; i++)
		{
			players[i] = new Player(i);

			while (true)
			{
				foreach(var item in keyBoard)
				{
					if (Input.GetKeyDown(item) && players[i].getLeftKey() == KeyCode.None && checkIfUsed(keysInUse, item) == false)
					{
                        playerSelectController.SetLeftButton(i, item.ToString());
                        players[i].setLeftKey(item);
						keysInUse[2*i] = item;
					}
					else if (Input.GetKeyDown(item) && players[i].getRightKey() == KeyCode.None && item != players[i].getLeftKey() && checkIfUsed(keysInUse, item) == false)
					{
                        playerSelectController.SetRightButton(i, item.ToString());
                        players[i].setRightKey(item);
						keysInUse[2*i+1] = item;
					}
				}

				if (players[i].getLeftKey() != KeyCode.None && players[i].getRightKey() != KeyCode.None)
				{

					break;
				}
				
				yield return null;
			}
			Debug.Log(players[i].getKeyDetails());
		}
	}
	bool checkIfUsed(KeyCode[] items, KeyCode i)
	{
		foreach (var item in items)
		{
			if (item == i)
			{
				return true;
			}
		}
		return false;
	}
}