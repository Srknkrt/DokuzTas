using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobiManager : MonoBehaviourPunCallbacks
{
    public InputField lobiAdi;

    public void LobiKurKatil()
    {
        RoomOptions lobi = new RoomOptions();
        lobi.MaxPlayers = 2;

        if(!string.IsNullOrEmpty(lobiAdi.text))
        {
            PhotonNetwork.JoinOrCreateRoom(lobiAdi.text, lobi, TypedLobby.Default);
        }
    }
    
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Oyun");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Oyun");
        }
    }

}
