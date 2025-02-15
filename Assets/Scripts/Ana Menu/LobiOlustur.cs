using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobiOlustur : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField odaAdi;

    //Oluþtur butonuna basýldýðýnda çalýþýr.
    public void OdaOlustur()
    {
        RoomOptions oda = new RoomOptions();
        oda.MaxPlayers = 2;

        if (!string.IsNullOrEmpty(odaAdi.text))
        {
            //Oda adýna göre oda oluþturuldu.
            PhotonNetwork.CreateRoom(odaAdi.text, oda, TypedLobby.Default);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Masterýn odaya katýldýðý kýsým. Odaya rakip girene kadar bekler. Rakip girdikten sonra sahne deðiþir.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Oyun");
        }
    }
}
