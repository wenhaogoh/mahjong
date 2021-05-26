using UnityEngine;
using TMPro;

public class TileController : MonoBehaviour
{
    public Tile tile;
    public TextMeshProUGUI type;
    public TextMeshProUGUI value;
    public int index;
    void Start()
    {
        type.SetText(tile.GetTileType().ToString());
        value.SetText(tile.GetValue().ToString());
    }
    void Update()
    {

    }
    public void OnClick()
    {
        if (GameStateController.instance.gameState == GameStates.PLAYER0_DISCARDING)
        {
            GameStateController.instance.DiscardPlayerTile(index);
        }
    }
}
