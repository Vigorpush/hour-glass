using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}