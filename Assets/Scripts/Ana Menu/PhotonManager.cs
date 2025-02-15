using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //Oyuncular�n Photon sunucusuna ba�lanmas�n� sa�lar.
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();    
    }

    //Oyuncular�n lobiye ba�lanmas�n� sa�lar.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
