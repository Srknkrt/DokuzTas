using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //Oyuncuların Photon sunucusuna bağlanmasını sağlar.
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();    
    }

    //Oyuncuların lobiye bağlanmasını sağlar.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
