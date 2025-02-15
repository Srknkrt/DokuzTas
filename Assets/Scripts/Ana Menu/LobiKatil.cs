using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobiKatil : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField odaAdi;

    //Katýl butonuna basýldýðýnda çalýþýr.
    public void OdayaKatil()
    {
        RoomOptions oda = new RoomOptions();

        if (!string.IsNullOrEmpty(odaAdi.text))
        {
            //Oda adýna göre odaya baðlanýr.
            PhotonNetwork.JoinRoom(odaAdi.text);
        }
    }

    public override void OnJoinedRoom()
    {
        //Clientin odaya baðlandýðý kýsým.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Oyun");
        }
    }
}
