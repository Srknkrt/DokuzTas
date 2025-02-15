using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject yesilTas;
    public GameObject sariTas;
    void Start()
    {
        if(PlayerPrefs.GetString("Tur") == "Coklu")
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
            {
                for (int i = 8; i >= -8; i = i - 2)
                {
                    PhotonNetwork.Instantiate(yesilTas.name, new Vector3(-12f, 0.05f, i), Quaternion.identity);
                }
            }
            else if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
            {
                for (int i = 8; i >= -8; i = i - 2)
                {
                    PhotonNetwork.Instantiate(sariTas.name, new Vector3(12f, 0.05f, i), Quaternion.identity);
                }
            }
        }
        else
        {
            for (int i = 8; i >= -8; i = i - 2)
            {
                Instantiate(yesilTas, new Vector3(-12f, 0.05f, i), Quaternion.identity);
                Instantiate(sariTas, new Vector3(12f, 0.05f, i), Quaternion.identity);
            }
        }
    }

}
