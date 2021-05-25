using System;
using UnityEngine;

public class Request : IComparable<Request>
{
    private TileAction tileAction;
    private Player requestingPlayer;

    public Request(TileAction tileAction, Player requestingPlayer)
    {
        this.tileAction = tileAction;
        this.requestingPlayer = requestingPlayer;
    }
    public bool IsEmpty()
    {
        return tileAction == null;
    }
    public TileAction GetTileAction()
    {
        return tileAction;
    }
    public Player GetRequestingPlayer()
    {
        return requestingPlayer;
    }
    public int CompareTo(Request obj)
    {
        if (obj.Equals(this)) return 0;

        if (obj.tileAction == null) return 1;

        if (this.tileAction == null) return -1;

        if (obj.tileAction.GetTileActionType() < this.tileAction.GetTileActionType())
        {
            return 1;
        }
        else if (obj.tileAction.GetTileActionType() > this.tileAction.GetTileActionType())
        {
            return -1;
        }
        else
        {
            int currentPlayerId = MapperUtils.MapGameStateToPlayerId(GameStateController.instance.gameState);
            int thisRequestingPlayerId = this.requestingPlayer.GetId();
            int objRequestingPlayerId = obj.requestingPlayer.GetId();
            if (thisRequestingPlayerId > currentPlayerId && objRequestingPlayerId > currentPlayerId
                || thisRequestingPlayerId < currentPlayerId && objRequestingPlayerId < currentPlayerId)
            {
                return objRequestingPlayerId.CompareTo(thisRequestingPlayerId);
            }
            else if (thisRequestingPlayerId > currentPlayerId && objRequestingPlayerId < currentPlayerId)
            {
                return 1;
            }
            else if (thisRequestingPlayerId < currentPlayerId && objRequestingPlayerId > currentPlayerId)
            {
                return -1;
            }
            else
            {
                throw new Exception("Offering player sent request!");
            }
        }
    }
}