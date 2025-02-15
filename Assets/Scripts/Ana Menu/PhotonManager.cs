using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //Oyuncularýn Photon sunucusuna baðlanmasýný saðlar.
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();    
    }

    //Oyuncularýn lobiye baðlanmasýný saðlar.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
