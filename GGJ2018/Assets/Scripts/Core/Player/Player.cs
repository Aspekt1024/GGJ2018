using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private TransmissionHandler transmission;

    private void Start()
    {
        transmission = GetComponent<TransmissionHandler>();
    }

    private void Update()
    {
    }

    public void SendTransmission(List<Symbols> symbols)
    {
        transmission.SendTransmission(symbols);
        SoundBites.Instance.PlayTransmissionSent();
    }
}
