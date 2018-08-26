using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Possible states
public enum UISEQUENCE{
    START, BUTTONSELECT, READY, END
}

public class UIManager : MonoBehaviour 
{
	
    //Start from Start state
	private UISEQUENCE uiSequence = UISEQUENCE.START;

	
    //Set these correct panels with correct components
    [SerializeField]
    private PlayerNumberSelection playerNumberSelection;
    [SerializeField]
    private PlayerCharacterSelection playerCharacterSelection;

    float timer = 0;

    //deactivate panels, let sequences control them
    void Start()
	{
        playerNumberSelection.gameObject.SetActive(false);
        playerCharacterSelection.gameObject.SetActive(false);
        
	}


  
    //Why this needs to be fixed?
    void Update()
	{
        

        switch (uiSequence)
		{
            //Sequence of how many players we want
			case UISEQUENCE.START:
                if (!playerNumberSelection.gameObject.activeSelf) playerNumberSelection.gameObject.SetActive(true);
                uiSequence = playerNumberSelection.UpdateMe();
                break;
            //Select buttons for each player
			case UISEQUENCE.BUTTONSELECT:
                if (!playerCharacterSelection.gameObject.activeSelf) {
                    playerNumberSelection.gameObject.SetActive(false);
                    playerCharacterSelection.gameObject.SetActive(true);
                    playerCharacterSelection.Initialize(playerNumberSelection.GetPlayerCount());
                }
                uiSequence = playerCharacterSelection.UpdateMe();
                timer = 0.0f;
                break;
            //finalize before starting the game
            case UISEQUENCE.READY:
                timer += Time.deltaTime;
                if (timer < 3) break;
                List<Player> players = playerCharacterSelection.GetPlayers();
             //   playerCharacterSelection.gameObject.SetActive(false);
                Debug.Log("number of players:" + players.Count);

			transform.Find("UICamera").gameObject.SetActive(false);

			//start race with selected players
		//	GameManager.instance.ResetPlayers();

			GameManager.instance.AddPlayers(players);
			
			GameManager.instance.Init();

                uiSequence = UISEQUENCE.END;
                break;
        }
	}


}