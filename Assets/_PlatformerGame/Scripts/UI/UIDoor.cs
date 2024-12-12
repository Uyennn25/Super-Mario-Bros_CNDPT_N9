using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDoor : MonoBehaviour
{
    public Button buttonOpenDoor;

    private void Start()
    {
        buttonOpenDoor.onClick.AddListener(Open);
    }

    private void Open()
    {
        
    }
}
