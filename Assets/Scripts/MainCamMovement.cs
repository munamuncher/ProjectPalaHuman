using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 camPos;
    private float maxX = 12f;
    private float minX = -12f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        if (player == null)
        {
            Debug.Log("MainCam.cs - Awake() - player 참조 실패");
        }
    }

    private void LateUpdate()
    {
        camPos = new Vector3(player.position.x, 0.52f, 0f);
        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        transform.position = camPos;
    }
}
