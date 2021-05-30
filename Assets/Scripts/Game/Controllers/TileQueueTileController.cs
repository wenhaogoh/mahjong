using UnityEngine;
using TMPro;

public class TileQueueTileController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private int count;
    void Start()
    {
        count = 2;
        countText.SetText(count.ToString());
    }
    void Update()
    {

    }
    public void DecreaseCount()
    {
        count--;
        countText.SetText(count.ToString());
    }
    public bool IsEmpty()
    {
        return count <= 0;
    }
}
