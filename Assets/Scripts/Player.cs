using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
	[SerializeField] private int playerIndex;
	[SerializeField] private KeyCode leftKey, rightKey;
	[SerializeField] private GameObject character, mount;

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

	public GameObject GetMount() {
		return mount;
	}
}