using UnityEngine;
using TMPro;

public class TileController : MonoBehaviour
{
    public Tile tile;
    public TextMeshProUGUI type;
    public TextMeshProUGUI value;
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
            int index = transform.GetSiblingIndex();
            GameStateController.instance.DiscardPlayerTile(index);
        }
    }
}
