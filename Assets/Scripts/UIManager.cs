using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UISEQUENCE{
    START, BUTTONSELECT, READY, END
}

public class UIManager : MonoBehaviour 
{
	
	private UISEQUENCE uiSequence = UISEQUENCE.START;

	


    
    
  

    [SerializeField]
    private PlayerNumberSelection playerNumberSelection;
    [SerializeField]
    private PlayerCharacterSelection playerCharacterSelection;




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
			case UISEQUENCE.START:
                if (!playerNumberSelection.gameObject.activeSelf) playerNumberSelection.gameObject.SetActive(true);
                uiSequence = playerNumberSelection.UpdateMe();
                break;
			case UISEQUENCE.BUTTONSELECT:
                if (!playerCharacterSelection.gameObject.activeSelf) {
                    playerNumberSelection.gameObject.SetActive(false);
                    playerCharacterSelection.gameObject.SetActive(true);
                    playerCharacterSelection.Initialize(playerNumberSelection.GetPlayerCount());
                }
                uiSequence = playerCharacterSelection.UpdateMe();
                break;
            case UISEQUENCE.READY:
                List<Player> players = playerCharacterSelection.GetPlayers();
                playerCharacterSelection.gameObject.SetActive(false);
                uiSequence = UISEQUENCE.END;
                break;
        }
	}


}