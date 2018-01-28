﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionHandler : MonoBehaviour {

    public float TransmissionSpeed = 3f;
    public float TransmissionDuration = 3f;
    public GameObject TransmissionPrefab;
    private CameraController controller;
    
    private void Start()
    {
        controller = FindObjectOfType<CameraController>();
    }

    public void SendTransmission(List<Symbols> symbols)
    {
        Transmission newTransmission = ObjectPooler.Instance.GetPooledObject(ObjectPooler.Pools.Transmission.ToString()).GetComponent<Transmission>();
        newTransmission.Activate(TransmissionSpeed, TransmissionDuration, symbols);
        controller.addTransmission();
        GameStats.Instance.SentTransmission();
    }
}
