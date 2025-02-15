using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject yesilTas;
    [SerializeField] private GameObject sariTas;

    //Oyun sahnesi açýldýðýnda taþlarý oluþturur.
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Oyun")
        {
            //Çok oyunculu modunda taþlarý oluþturur.
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
            //Tek oyunculu modunda taþlarý oluþturur.
            else if (PlayerPrefs.GetString("Tur") == "Tekli")
            {
                Debug.Log("2");
                for (int i = 8; i >= -8; i -= 2)
                {
                    Instantiate(yesilTas, new Vector3(-12f, 0.05f, i), Quaternion.identity);
                    Instantiate(sariTas, new Vector3(12f, 0.05f, i), Quaternion.identity);
                }
            }

            Deneme();//þimdilik var. Deneme yaparken hýzlý olsun diye
        }
    }

    //Oyun kapatýlýrken playerprefleri siler.
    void OnApplicationQuit()
    {
        Debug.Log("Oyun kapanýyor.");
        // Burada oyun kapanmadan önce yapýlmasý gereken iþlemleri yazabilirsin.
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
