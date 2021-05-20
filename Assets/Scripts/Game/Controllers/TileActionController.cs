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
        bool isOpponentOffering = GameStateController.instance.gameState == GameStates.OPPONENT1_OFFERING
                               || GameStateController.instance.gameState == GameStates.OPPONENT2_OFFERING
                               || GameStateController.instance.gameState == GameStates.OPPONENT3_OFFERING;
        if (isOpponentOffering)
        {
            GameStateController.instance.ProcessPlayerTileActionRequest(tileAction);
        }
        else
        {
            GameStateController.instance.ExecutePlayerTileAction(tileAction);
        }
    }
}
