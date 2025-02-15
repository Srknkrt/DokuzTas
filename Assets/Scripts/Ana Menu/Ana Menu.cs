using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Start() 
    {
        panel.SetActive(false);
    }

    //Oyunu Ba�lat butonuna bas�ld���nda �al���r.
    public void OyunuBaslat()
    {
        SceneManager.LoadScene("Oyun");
        PlayerPrefs.SetString("Tur", "Tekli");
    }

    //�ok Oyunculu butonuna bas�ld���nda �al���r.
    public void CokOyunculu()
    {
        if(panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
        PlayerPrefs.SetString("Tur", "Coklu");
    }
}
