using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPan : MonoBehaviour
{
    public Transform StartPos;
    private Transform TargetPos;

    //Pan variables
    public float PanSpeed;
    private Transform lastLocation;
    private float percentageMoved = 0.0f;
    private float startTime;
    private float distance;

    private void Start()
    {
        startTime = Time.time;
        TargetPos = StartPos;
        distance = Vector3.Distance(StartPos.position, TargetPos.position);
    }


    private void Update()
    {
        float OverTime  = (Time.time - startTime) * PanSpeed;

        percentageMoved = OverTime / distance;

        this.transform.position = Vector3.Lerp(StartPos.position, TargetPos.position, percentageMoved);
    }

    public void MoveCameratoZone(Transform Position) {
        StartPos = TargetPos;
        TargetPos = Position;
        startTime = Time.time;
        distance = Vector3.Distance(StartPos.position, TargetPos.position);
    //transform.position = Position.position;

    }
   
}
