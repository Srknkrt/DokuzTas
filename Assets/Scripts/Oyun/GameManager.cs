using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject yesilTas;
    [SerializeField] private GameObject sariTas;

    //Oyun sahnesi a��ld���nda ta�lar� olu�turur.
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Oyun")
        {
            //�ok oyunculu modunda ta�lar� olu�turur.
            if (PlayerPrefs.GetString("Tur") == "Coklu")
            {
                Debug.Log("1");
                if (PhotonNetwork.IsConnected)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        for (int i = 8; i >= -8; i -= 2)
                        {
                            PhotonNetwork.Instantiate(yesilTas.name, new Vector3(-12f, 0.05f, i), Quaternion.identity);
                        }
                    }
                    else if (!PhotonNetwork.IsMasterClient)
                    {
                        for (int i = 8; i >= -8; i -= 2)
                        {
                            PhotonNetwork.Instantiate(sariTas.name, new Vector3(12f, 0.05f, i), Quaternion.identity);
                        }
                    }
                }
            }
            //Tek oyunculu modunda ta�lar� olu�turur.
            else if (PlayerPrefs.GetString("Tur") == "Tekli")
            {
                Debug.Log("2");
                for (int i = 8; i >= -8; i -= 2)
                {
                    Instantiate(yesilTas, new Vector3(-12f, 0.05f, i), Quaternion.identity);
                    Instantiate(sariTas, new Vector3(12f, 0.05f, i), Quaternion.identity);
                }
            }

            Deneme();//�imdilik var. Deneme yaparken h�zl� olsun diye
        }
    }

    //Oyun kapat�l�rken playerprefleri siler.
    void OnApplicationQuit()
    {
        Debug.Log("Oyun kapan�yor.");
        // Burada oyun kapanmadan �nce yap�lmas� gereken i�lemleri yazabilirsin.
        PlayerPrefs.DeleteKey("Tur");
    }
    void Deneme()
    {
        for (int i = 8; i >= -8; i -= 2)
        {
            Instantiate(yesilTas, new Vector3(-12f, 0.05f, i), Quaternion.identity);
            Instantiate(sariTas, new Vector3(12f, 0.05f, i), Quaternion.identity);
        }
    }
}
