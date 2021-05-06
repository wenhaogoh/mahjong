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
        GameStateController.instance.ExecutePlayerAction(tileAction);
    }
}
