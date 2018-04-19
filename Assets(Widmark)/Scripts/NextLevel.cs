using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    [SerializeField]
    private GameObject item;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (gameObject.CompareTag("Player"))
    //    {
    //        Application.LoadLevel("Lv2");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (item.CompareTag("Pickable"))
        {
            SceneManager.LoadScene("Lv2"); 
        }
    }
}
