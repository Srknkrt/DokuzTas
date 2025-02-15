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

    //Olu�tur butonuna bas�ld���nda �al���r.
    public void OdaOlustur()
    {
        RoomOptions oda = new RoomOptions();
        oda.MaxPlayers = 2;

        if (!string.IsNullOrEmpty(odaAdi.text))
        {
            //Oda ad�na g�re oda olu�turuldu.
            PhotonNetwork.CreateRoom(odaAdi.text, oda, TypedLobby.Default);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Master�n odaya kat�ld��� k�s�m. Odaya rakip girene kadar bekler. Rakip girdikten sonra sahne de�i�ir.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Oyun");
        }
    }
}
