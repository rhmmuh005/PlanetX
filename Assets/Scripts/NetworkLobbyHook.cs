using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {
    public string name = "";
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        PlayerID localPlayer = gamePlayer.GetComponent<PlayerID>();
        BillboardName nameplate = gamePlayer.GetComponent<BillboardName>();

        localPlayer.PlayerName = lobby.playerName;
        name = localPlayer.PlayerName;
        nameplate.cc = lobby.playerColor;
    }
}
