using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float range;
    [SerializeField]
    private GameObject item;
    private bool isCarrying;
    // Use this for initialization
    void Start ()
    {
        item = this.gameObject;
        item.GetComponent<Rigidbody>().useGravity = true;
        isCarrying = false;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (Physics.Raycast(player.transform.position, Vector3.forward, range))
        {
           if (isCarrying == false && this.gameObject.tag == "Pickable")
            {
                Debug.Log("Player Entered");
                if (Input.GetButtonDown("PickUp"))
                {
                    Pickup();
                    isCarrying = true;                   
                }
            }
            else if (isCarrying == true)
            {
                Debug.Log(isCarrying);
                if (Input.GetButtonDown("PickUp"))
                {
                    Drop();
                    isCarrying = false;
                }
            }
        }
       
    }
    private void Pickup()
    {
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = hand.transform.position;
        item.transform.rotation = hand.transform.rotation;
        item.transform.SetParent(hand.transform);
    }
    private void Drop()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.SetParent(null);
        item.transform.position = hand.transform.position;
    }
   
}
