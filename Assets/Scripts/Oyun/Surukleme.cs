using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class Surukleme : MonoBehaviourPunCallbacks
{
    GameObject selectedObject;
    GameObject oldPos;

    float snapDistance = 1.5f;

    List<GameObject> slotsPos = new List<GameObject>();

    void Start()
    {
        oldPos = new GameObject("OldPos");
        SlotsScan();
        
    }
    
    void SlotsScan()
    {
        slotsPos.AddRange(GameObject.FindGameObjectsWithTag("Slot"));
    }

    void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
                MouseClicked();
            if (Input.GetMouseButton(0))
                MouseHolded();
            if (Input.GetMouseButtonUp(0))
                MouseRelease();
        }
        catch (Exception) { }
    }
    void MouseClicked()
    {
        RaycastHit hit = CastRay();

        if (selectedObject == null)
        {
            
            if (hit.collider != null)
            {
                if(PlayerPrefs.GetString("Tur") == "Coklu")
                {
                    if (hit.collider.CompareTag("FirstPlayer") && PhotonNetwork.IsMasterClient
                    || hit.collider.CompareTag("SecondPlayer") && !PhotonNetwork.IsMasterClient)
                    //if (hit.collider.CompareTag("FirstPlayer") || hit.collider.CompareTag("SecondPlayer"))
                    {

                        selectedObject = hit.collider.gameObject;
                        oldPos.transform.position = selectedObject.transform.position;

                        Cursor.visible = false;
                    }
                }
                else
                {
                    if (hit.collider.CompareTag("FirstPlayer") || hit.collider.CompareTag("SecondPlayer"))
                    {

                        selectedObject = hit.collider.gameObject;
                        oldPos.transform.position = selectedObject.transform.position;

                        Cursor.visible = false;
                    }
                }
            }
        }
    }

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
    void MouseHolded()
    {
        Vector3 worldPosition;

        if (selectedObject != null)
        {
            worldPosition = FindPosition();
            
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);

        }
    }
    Vector3 FindPosition()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, 
                                       Input.mousePosition.y, 
                                       Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        return worldPosition;
    }

    void MouseRelease()
    {
        if (selectedObject != null)
        {
            CalculateDistance();
            selectedObject = null;
            Cursor.visible = true;
        }
    }

    void CalculateDistance()
    {
        float distance;
        bool isTruePos = false;
        bool isEmpty = true;
        GameObject obj = null;

        foreach (GameObject slot in slotsPos)
        {
            distance = Vector3.Distance(selectedObject.transform.position, slot.transform.position);

            if(distance <= snapDistance)
            {
                obj = slot;

                isTruePos = true;
            
            }
        }

        if (isTruePos && isEmpty && obj != null)
        {
            selectedObject.transform.position = obj.transform.position;
        }
        else
        {
            selectedObject.transform.position = oldPos.transform.position;
        }

    }


}
