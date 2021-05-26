using UnityEngine;

public class TileActionController : MonoBehaviour
{
    public TileAction tileAction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        switch (GameStateController.instance.gameState)
        {
            case GameStates.OPPONENT1_OFFERING:
            case GameStates.OPPONENT2_OFFERING:
            case GameStates.OPPONENT3_OFFERING:
                GameStateController.instance.ProcessPlayerTileActionRequest(tileAction);
                break;
            default:
                GameStateController.instance.ExecutePlayerTileAction(tileAction);
                break;
        }
    }
}
