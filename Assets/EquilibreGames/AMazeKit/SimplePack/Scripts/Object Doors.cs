using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDoors : MonoBehaviour
{
    public enum DoorStatus { opened, opening, closed, closing };
    public DoorStatus currentStatus = DoorStatus.closed;
    public float closeDelay = 2.0f;
    public Vector3 movement;
    public float positionThreshold = 0.01f;

    private float speed = 1;
    private float objectDetectedTime = 0;
    private Vector3 closedPosition;
    private Vector3 openedPosition;
    bool ready = false;

    private Vector3 targetPosition;
    private DoorStatus targetStatus;
    private CameraTurn cameraTurn;

    private AudioSource mySound;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (currentStatus)
        {
            case DoorStatus.closing:
            case DoorStatus.opening: Moving(); break;
            case DoorStatus.opened: CheckAutoClose(); break;
        }
    }
    private void CheckAutoClose()
    {
        // Auto close when nothing is detected after closing delay
        if (objectDetectedTime + closeDelay < Time.time)
        {
            Close();
        }
    }
    private void Moving()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        if ((transform.position - targetPosition).sqrMagnitude < positionThreshold)
        {
            transform.position = targetPosition;
            currentStatus = targetStatus;
        }
    }
    public void Close()
    {
        if (currentStatus == DoorStatus.opened || currentStatus == DoorStatus.opening)
        {
            currentStatus = DoorStatus.closing;
            targetPosition = closedPosition;
            targetStatus = DoorStatus.closed;
            mySound.Play();
        }
    }
    public void Open()
    {
        if (currentStatus == DoorStatus.closed || currentStatus == DoorStatus.closing)
        {
            currentStatus = DoorStatus.opening;
            targetPosition = openedPosition;
            targetStatus = DoorStatus.opened;
            mySound.Play();

        }
    }
}
