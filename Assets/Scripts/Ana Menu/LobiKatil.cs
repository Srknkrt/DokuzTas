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

    //Kat�l butonuna bas�ld���nda �al���r.
    public void OdayaKatil()
    {
        RoomOptions oda = new RoomOptions();

        if (!string.IsNullOrEmpty(odaAdi.text))
        {
            //Oda ad�na g�re odaya ba�lan�r.
            PhotonNetwork.JoinRoom(odaAdi.text);
        }
    }

    public override void OnJoinedRoom()
    {
        //Clientin odaya ba�land��� k�s�m.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Oyun");
        }
    }
}
