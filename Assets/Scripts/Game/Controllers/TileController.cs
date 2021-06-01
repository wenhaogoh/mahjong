using UnityEngine;
using TMPro;

public class TileController : MonoBehaviour
{
    public TextMeshProUGUI type;
    public TextMeshProUGUI value;
    public Tile tile;
    public int index;
    void Start()
    {

    }
    void Update()
    {

    }
    public void SetTileContent(int index, Tile tile, bool showTileContent)
    {
        this.index = index;
        this.tile = tile;
        if (showTileContent)
        {
            type.SetText(tile.GetTileType().ToString());
            value.SetText(tile.GetValue().ToString());
        }
        else
        {
            type.SetText("");
            value.SetText("");
        }
    }
    public void OnClick()
    {
        if (GameStateController.instance.gameState == GameStates.PLAYER0_DISCARDING)
        {
            GameStateController.instance.DiscardPlayer0Tile(index);
        }
    }
}
