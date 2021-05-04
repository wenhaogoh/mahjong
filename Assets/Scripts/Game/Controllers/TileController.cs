using UnityEngine;
using TMPro;

public class TileController : MonoBehaviour
{
    public Tile tile;
    public TextMeshProUGUI type;
    public TextMeshProUGUI value;
    // Start is called before the first frame update
    void Start()
    {
        type.SetText(tile.GetTileType().ToString());
        value.SetText(tile.GetValue().ToString());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        if (GameStateController.instance.gameState == GameStates.PLAYER_DISCARDING)
        {
            int index = transform.GetSiblingIndex();
            GameStateController.instance.DiscardPlayerTile(index);
        }
    }
}
