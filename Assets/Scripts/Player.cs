using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
	[SerializeField] private int playerIndex;
	[SerializeField] private KeyCode leftKey = KeyCode.None, rightKey = KeyCode.None;
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

	public GameObject GetMount() {
		return mount;
	}

	public GameObject GetRider() {
		return character;
	}

    public void SetMount(GameObject gameObject) {
        mount = gameObject;
    }

    public void SetRider(GameObject gameObject) {
        character = gameObject;
    }

}