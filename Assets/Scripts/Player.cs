using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	private int playerIndex;
	private KeyCode leftKey = KeyCode.None, rightKey = KeyCode.None;
	private GameObject character, mount;

	public Player(int index)
	{
		playerIndex = index;
		/* Debug.Log("Successfully created new player.\n" + "Index: " + playerIndex + "\nLeftKey: " + leftKey + "\nRightKey: " + rightKey); */
	}

	public int getIndex()
	{
		return playerIndex;
	}

	public KeyCode getLeftKey()
	{
		return leftKey;
	}

	public KeyCode getRightKey()
	{
		return rightKey;
	}

	public string getKeyDetails()
	{
		return "Index: " + playerIndex + ". LeftKey: " + leftKey + ". RightKey " + rightKey;
	}

	public void setLeftKey(KeyCode key)
	{
		leftKey = key;
	}

	public void setRightKey(KeyCode key)
	{
		rightKey = key;
	}
}