using UnityEngine;

public class TileActionController : MonoBehaviour
{
    public TileAction tileAction;
    void Start()
    {

    }
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
                GameStateController.instance.ProcessPlayer0TileActionRequest(tileAction);
                break;
            default:
                GameStateController.instance.ExecutePlayer0TileAction(tileAction);
                break;
        }
    }
}
