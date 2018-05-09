using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalRoomSceenChange : MonoBehaviour {

    public GameObject player;
    float sceneChangePoint = 96;


    void RespawnOnTrack()
    {
        if (player.transform.position.x < sceneChangePoint)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    private void Update()
    {
        RespawnOnTrack();
    }
}
