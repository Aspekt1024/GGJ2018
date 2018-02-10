using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionHandler : MonoBehaviour {

    public float TransmissionSpeed = 3f;
    public float TransmissionDuration = 3f;
    public GameObject TransmissionPrefab;
    private CameraController controller;

    private int transmissionNumber;
    
    private void Start()
    {
        controller = FindObjectOfType<CameraController>();
        transmissionNumber = 0;
    }

    public void SendTransmission(List<Symbols> symbols)
    {
        transmissionNumber++;
        Transmission newTransmission = ObjectPooler.Instance.GetPooledObject(ObjectPooler.Pools.Transmission.ToString()).GetComponent<Transmission>();
        newTransmission.Activate(TransmissionSpeed, TransmissionDuration, symbols);
        controller.AddTransmission();

        newTransmission.SetPlanetsToReach(GameManager.Instance.GetUnlockedPlanets());

        GameUI.SetBatteryPercent((float) (GameStats.Instance.NumTransmissionsBeforeEnd -transmissionNumber) / GameStats.Instance.NumTransmissionsBeforeEnd);

        Logger.LogEntry entry = Logger.AddLog(symbols.ToArray());
        newTransmission.SetLogEntry(entry);
        GameStats.Instance.SentTransmission();
    }
}
