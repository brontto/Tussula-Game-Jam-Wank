using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	private int playerIndex;
	private KeyCode leftKey, rightKey;
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

	public void setLeftKey(KeyCode key)
	{
		leftKey = key;
		Debug.Log("Left key: " + leftKey);
	}

	public void setRightKey(KeyCode key)
	{
		rightKey = key;
		Debug.Log("Right key: " + rightKey);
	}
}