using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private BoxCollider2D box;
    private Camera cam;
    private float xRatio;
    private float yRatio;
    private float boxSizeX;
    private float boxSizeY;

    private Transform player;
    private GameObject bound;

    public Image fade;


    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //For fading between rooms... Not done yet
        fade.color = Color.clear;
    }

    void Update()
    {
        CalculateCameraBound();
        FollowPlayer();
    }

    //Makes sure the camera box is the right size for any ratio
    void CalculateCameraBound()
    {
        xRatio = (float)Screen.width / (float)Screen.height;
        yRatio = (float)Screen.height / (float)Screen.width;

        boxSizeX = (Mathf.Abs(cam.orthographicSize) * 2 * xRatio);
        boxSizeY = boxSizeX * yRatio;

        box.size = new Vector2(boxSizeX, boxSizeY);
    }
    
    //If a boundary is detected, keep the camera inside of it and the player centered as much as possible
    void FollowPlayer()
    {
        if (bound = GameObject.Find("boundary"))
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x, bound.GetComponent<BoxCollider2D>().bounds.min.x + box.size.x / 2, bound.GetComponent<BoxCollider2D>().bounds.max.x - box.size.x / 2),
                                             Mathf.Clamp(player.position.y, bound.GetComponent<BoxCollider2D>().bounds.min.y + box.size.y / 2, bound.GetComponent<BoxCollider2D>().bounds.max.y - box.size.y / 2),
                                             transform.position.z);
 
        }
    }
}

