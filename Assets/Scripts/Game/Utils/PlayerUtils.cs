using System.Collections.Generic;

public class PlayerUtils
{
    public const int PLAYER0_ID = 0;
    public const int OPPONENT1_ID = 1;
    public const int OPPONENT2_ID = 2;
    public const int OPPONENT3_ID = 3;
    public static Player GetPlayer0()
    {
        return GetPlayer(PLAYER0_ID);
    }
    public static Player GetOpponent1()
    {
        return GetPlayer(OPPONENT1_ID);
    }
    public static Player GetOpponent2()
    {
        return GetPlayer(OPPONENT2_ID);
    }
    public static Player GetOpponent3()
    {
        return GetPlayer(OPPONENT3_ID);
    }
    public static Player GetPlayer(int id)
    {
        return new Player(id);
    }
    public static string GetPlayerTilesMessage(Player player)
    {
        List<Tile> flowerTiles = player.GetFlowerTiles().GetTiles();
        List<Tile> mainTiles = player.GetMainTiles().GetTiles();
        string flowerTilesMessage = string.Join(",", flowerTiles);
        string mainTilesMessage = string.Join(",", mainTiles);
        return "Player ID: " + player.GetId() + "\n"
                    + "Flower Tiles: [" + flowerTilesMessage + "]\n"
                    + "Main Tiles: [" + mainTilesMessage + "]";
    }
}