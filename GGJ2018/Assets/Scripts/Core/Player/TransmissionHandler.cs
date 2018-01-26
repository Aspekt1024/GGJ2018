using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionHandler : MonoBehaviour {

    public float TransmissionSpeed = 1f;
    public Transform TransmissionsParentTf;
    public GameObject TransmissionPrefab;

    public List<Transmission> transmissions;
    
    private void Start()
    {
        transmissions = new List<Transmission>();
    }

    public void SendTransmission()
    {
        // TODO Pooler;
        Transmission newTransmission = Instantiate(TransmissionPrefab).GetComponent<Transmission>();
        newTransmission.transform.SetParent(TransmissionsParentTf);
        newTransmission.Activate(TransmissionSpeed);
        transmissions.Add(newTransmission);
    }
}
