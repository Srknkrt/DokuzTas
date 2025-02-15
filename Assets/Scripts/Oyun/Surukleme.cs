using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class Surukleme : MonoBehaviourPunCallbacks
{
    GameObject seciliNesne;
    GameObject eskiPoz;

    float bagUzaklik = 1.5f;

    List<GameObject> yuvaPozlari = new List<GameObject>();

    void Start()
    {
        eskiPoz = new GameObject("EskiPoz");
        YuvaPozBul();
    }
    
    //Yuvalarýn pozisyonlarýný listeye ekler.
    void YuvaPozBul()
    {
        yuvaPozlari.AddRange(GameObject.FindGameObjectsWithTag("Yuva"));
    }

    //Oyunun mekaniklerinin çalýþtýrýldýðý kýsým.
    void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
                MouseIlkTik();
            if (Input.GetMouseButton(0))
                MouseBasiliTut();
            if (Input.GetMouseButtonUp(0))
                MouseTikBirak();
        }
        catch (Exception) { }
    }

    //Mouse ile ilk týklandýðý zaman çalýþan fonksiyon. Seçili nesneye atama yapar
    void MouseIlkTik()
    {
        RaycastHit hit = CastRay();

        if (seciliNesne == null)
        {
            if (hit.collider != null)
            {
                //Çok oyunculu modda herkesin kendi taþýnýn hareket ettirilmesi saðlanýr.
                if (PlayerPrefs.GetString("Tur") == "Coklu")
                {
                    if ((hit.collider.CompareTag("BirinciOyuncu") && PhotonNetwork.IsMasterClient) || (hit.collider.CompareTag("IkinciOyuncu") && !PhotonNetwork.IsMasterClient))
                    //if(photonView.IsMine && (hit.collider.CompareTag("BirinciOyuncu") || hit.collider.CompareTag("IkinciOyuncu")))
                    {
                        seciliNesne = hit.collider.gameObject;
                        eskiPoz.transform.position = seciliNesne.transform.position;

                        Cursor.visible = false;
                    }
                }
                //Tek oyunculu modda taþýn hareket ettirilmesi saðlanýr.
                else
                {
                    if (hit.collider.CompareTag("BirinciOyuncu") || hit.collider.CompareTag("IkinciOyuncu"))
                    {
                        seciliNesne = hit.collider.gameObject;
                        eskiPoz.transform.position = seciliNesne.transform.position;

                        Cursor.visible = false;
                    }
                }
            }
        }
    }

    //Mousenin konumundan hangi nesneye týklandýðýný bulur.
    RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x,
                                                Input.mousePosition.y,
                                                Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x,
                                                 Input.mousePosition.y,
                                                 Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        //Debug.DrawRay(worldMousePosNear, worldMousePosFar - worldMousePosNear, Color.red);
        return hit;
    }

    //Seçili nesnenin hareket ettirilmesi saðlanýr.
    void MouseBasiliTut()
    {
        Vector3 worldPosition;

        if (seciliNesne != null)
        {
            worldPosition = PozBul();
            
            seciliNesne.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
        }
    }
    Vector3 PozBul()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, 
                                       Input.mousePosition.y, 
                                       Camera.main.WorldToScreenPoint(seciliNesne.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        return worldPosition;
    }

    //Seçili nesnenin uygun bir yere býrakýlmasýný saðlar.
    void MouseTikBirak()
    {
        if (seciliNesne != null)
        {
            MesafeHesapla();
            seciliNesne = null;
            Cursor.visible = true;
        }
    }

    //Seçili nesnenin yuvalara olan uzaklýðýný hesaplar. Eðer yakýnda yuva yoksa eski konumuna geri gönderir.
    void MesafeHesapla()
    {
        float mesafe;
        bool dogruPozMu = false;
        bool bosMu = true;
        GameObject nesne = null;

        foreach (GameObject yuva in yuvaPozlari)
        {
            mesafe = Vector3.Distance(seciliNesne.transform.position, yuva.transform.position);

            if(mesafe <= bagUzaklik)
            {
                nesne = yuva;
                dogruPozMu = true;
            }
        }

        if (dogruPozMu && bosMu && nesne != null)
        {
            seciliNesne.transform.position = nesne.transform.position;
        }
        else
        {
            seciliNesne.transform.position = eskiPoz.transform.position;
        }
    }
}
