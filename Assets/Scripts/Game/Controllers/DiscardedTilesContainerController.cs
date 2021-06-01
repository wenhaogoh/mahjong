using UnityEngine;
using System.Collections.Generic;

public class DiscardedTilesContainerController : MonoBehaviour
{
    public GameObject discardedTilePrefab;
    private const float VELOCITY = 500f;
    private const float ANGULAR_VELOCITY = 20f;
    private const int SPAWN_POINT = 214;
    private const int SCALE_COUNT = 3;
    private const float SCALE_VALUE = 0.05f;
    private readonly Vector3 PLAYER0_SPAWN_POINT = new Vector3(0, -SPAWN_POINT);
    private readonly Vector3 OPPONENT1_SPAWN_POINT = new Vector3(-SPAWN_POINT, 0);
    private readonly Vector3 OPPONENT2_SPAWN_POINT = new Vector3(0, SPAWN_POINT);
    private readonly Vector3 OPPONENT3_SPAWN_POINT = new Vector3(SPAWN_POINT, 0);
    private Queue<GameObject> scaleQueue = new Queue<GameObject>(SCALE_COUNT);
    void Start()
    {

    }
    void Update()
    {

    }
    public void DiscardTile(Tile discardedTile, int discardingPlayerId)
    {
        GameObject discardedTileGameObject;
        switch (discardingPlayerId)
        {
            case PlayerUtils.PLAYER0_ID:
                discardedTileGameObject = SpawnDiscardedTile(discardedTile, PLAYER0_SPAWN_POINT, new Vector2(0, VELOCITY), ANGULAR_VELOCITY, Quaternion.identity);
                break;
            case PlayerUtils.OPPONENT1_ID:
                discardedTileGameObject = SpawnDiscardedTile(discardedTile, OPPONENT1_SPAWN_POINT, new Vector2(VELOCITY, 0), ANGULAR_VELOCITY, Quaternion.Euler(new Vector3(0, 0, -90)));
                break;
            case PlayerUtils.OPPONENT2_ID:
                discardedTileGameObject = SpawnDiscardedTile(discardedTile, OPPONENT2_SPAWN_POINT, new Vector2(0, -VELOCITY), ANGULAR_VELOCITY, Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            case PlayerUtils.OPPONENT3_ID:
                discardedTileGameObject = SpawnDiscardedTile(discardedTile, OPPONENT3_SPAWN_POINT, new Vector2(-VELOCITY, 0), ANGULAR_VELOCITY, Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            default:
                discardedTileGameObject = null;
                Debug.LogError("No player with ID: " + discardingPlayerId + "!");
                break;
        }
        Scale();
        if (scaleQueue.Count == SCALE_COUNT)
        {
            scaleQueue.Dequeue();
        }
        scaleQueue.Enqueue(discardedTileGameObject);
    }
    public void RemoveLastDiscardedTile()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }
    public void RemoveAllDiscardedTiles()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    private GameObject SpawnDiscardedTile(Tile discardedTile, Vector3 spawnPoint, Vector2 velocity, float angularVelocity, Quaternion quaternion)
    {
        GameObject discardedTileObject = Instantiate(discardedTilePrefab, spawnPoint, quaternion);
        discardedTileObject.GetComponent<TileController>().SetTileContent(0, discardedTile, true);
        discardedTileObject.GetComponent<Rigidbody2D>().velocity = velocity;
        discardedTileObject.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
        discardedTileObject.transform.SetParent(this.transform, false);
        return discardedTileObject;
    }
    private void Scale()
    {
        foreach (GameObject discardedTileGameObject in scaleQueue)
        {
            if (discardedTileGameObject != null)
            {
                discardedTileGameObject.transform.localScale -= new Vector3(SCALE_VALUE, SCALE_VALUE);
            }
        }
    }
}
