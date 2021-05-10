using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTransmittor : MonoBehaviour
{
    public Transform Target;
    public GameObject Camera;
    private CamPan CamFunct;

    private void Awake()
    {
        CamFunct = Camera.GetComponent<CamPan>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CamFunct.MoveCameratoZone(Target);
        }
    }
}
