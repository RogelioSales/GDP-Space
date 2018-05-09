using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDoor : MonoBehaviour
{
    public Text riddleDoor;
    public GameObject riddleText;
    public GameObject player;
    public GameObject item;
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
    private PickUp pickup;

    private AudioSource mySound;
    // Use this for initialization
    void Start()
    {
        pickup = item.GetComponent<PickUp>();
        riddleDoor.GetComponent<Text>();
        closedPosition = transform.position;
        openedPosition = closedPosition + movement;
        speed = movement.magnitude / closeDelay;
        mySound = GetComponent<AudioSource>();
    }
    void OnTriggerEnter()
    {
        riddleText.SetActive(true);
        riddleDoor.enabled = true;
        riddleDoor.text = "You Need A object to open this Door";
        if (player.tag == "player" && pickup.IsCarrying == true)
        {
            ready = true;
            Destroy(item);
            objectDetectedTime = Time.time;
            Open();

        }
        else
        {
            return;
        }
    }
    void OnTriggerStay()
    {
        riddleText.SetActive(true);
        riddleDoor.enabled = true;
        riddleDoor.text = "You Need A Object to open this Door";
        if (player.tag == "player" && pickup.IsCarrying == true)
        {
            ready = true;

            Destroy(item);
            objectDetectedTime = Time.time;
            Open();
        }
        else
        {
            return;
        }
    }
    void OnTriggerExit()
    {
        riddleText.SetActive(false);
        riddleDoor.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(pickup.IsCarrying);
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
