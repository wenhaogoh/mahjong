using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerUtilsTests
{
    [Test]
    public void GetPlayer0()
    {
        Assert.AreEqual(PlayerUtils.PLAYER0_ID, PlayerUtils.GetPlayer0().GetId());
    }
    [Test]
    public void GetOpponent1()
    {
        Assert.AreEqual(PlayerUtils.OPPONENT1_ID, PlayerUtils.GetOpponent1().GetId());
    }
    [Test]
    public void GetOpponent2()
    {
        Assert.AreEqual(PlayerUtils.OPPONENT2_ID, PlayerUtils.GetOpponent2().GetId());
    }
    [Test]
    public void GetOpponent3()
    {
        Assert.AreEqual(PlayerUtils.OPPONENT3_ID, PlayerUtils.GetOpponent3().GetId());
    }
    [Test]
    public void GetPlayerTilesMessage()
    {
        Tile tile = TileUtils.GetRedDragonTile();
        string expectedMessage = "Player ID: " + PlayerUtils.PLAYER0_ID + "\n"
                                + "Flower Tiles: [" + tile + "]\n"
                                + "Main Tiles: [" + tile + "]";
        Player player = PlayerUtils.GetPlayer0();
        player.GetMainTiles().AddTile(tile);
        player.GetFlowerTiles().AddTile(tile);
        Assert.AreEqual(expectedMessage, PlayerUtils.GetPlayerTilesMessage(player));
    }
}
