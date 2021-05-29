using System;
using UnityEngine;

public class DiscardedTilesContainerController : MonoBehaviour
{
    public GameObject discardedTilePrefab;
    private const float VELOCITY = 500f;
    private const float ANGULAR_VELOCITY = 20f;
    private const int SPAWN_POINT = 214;
    private Vector3 PLAYER0_SPAWN_POINT = new Vector3(0, -SPAWN_POINT);
    private Vector3 OPPONENT1_SPAWN_POINT = new Vector3(-SPAWN_POINT, 0);
    private Vector3 OPPONENT2_SPAWN_POINT = new Vector3(0, SPAWN_POINT);
    private Vector3 OPPONENT3_SPAWN_POINT = new Vector3(SPAWN_POINT, 0);
    void Start()
    {

    }
    void Update()
    {

    }
    public void DiscardTile(Tile discardedTile, int discardingPlayerId)
    {
        switch (discardingPlayerId)
        {
            case PlayerUtils.PLAYER0_ID:
                SpawnTile(discardedTile, PLAYER0_SPAWN_POINT, new Vector2(0, VELOCITY), ANGULAR_VELOCITY, Quaternion.identity);
                break;
            case PlayerUtils.OPPONENT1_ID:
                SpawnTile(discardedTile, OPPONENT1_SPAWN_POINT, new Vector2(VELOCITY, 0), ANGULAR_VELOCITY, Quaternion.Euler(new Vector3(0, 0, -90)));
                break;
            case PlayerUtils.OPPONENT2_ID:
                SpawnTile(discardedTile, OPPONENT2_SPAWN_POINT, new Vector2(0, -VELOCITY), ANGULAR_VELOCITY, Quaternion.Euler(new Vector3(0, 0, 180)));
                break;
            case PlayerUtils.OPPONENT3_ID:
                SpawnTile(discardedTile, OPPONENT3_SPAWN_POINT, new Vector2(-VELOCITY, 0), ANGULAR_VELOCITY, Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            default:
                Debug.LogError("No player with ID: " + discardingPlayerId + "!");
                break;
        }
    }
    private void SpawnTile(Tile discardedTile, Vector3 spawnPoint, Vector2 velocity, float angularVelocity, Quaternion quaternion)
    {
        GameObject discardedTileObject = Instantiate(discardedTilePrefab, spawnPoint, quaternion);
        discardedTileObject.GetComponent<TileController>().tile = discardedTile;
        discardedTileObject.GetComponent<TileController>().showTileContent = true;
        discardedTileObject.GetComponent<Rigidbody2D>().velocity = velocity;
        discardedTileObject.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
        discardedTileObject.transform.SetParent(this.transform, false);
    }
}
