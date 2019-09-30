using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaryManager : MonoBehaviour
{
    private BoxCollider2D managerBox;
    private Transform player;
    public GameObject boundary;
    private cameraFollow cam;

    // Start is called before the first frame update
    void Start()
    {
        managerBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject camera = GameObject.Find("Main Camera");
        cam = camera.GetComponent<cameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageBoundary();
    }

    //If player leaves a boundary, deactiveate it. When they enter the new boundary, activeate it
    void ManageBoundary()
    {
        if ((managerBox.bounds.min.x < player.position.x && player.position.x < managerBox.bounds.max.x) &&
            (managerBox.bounds.min.y < player.position.y && player.position.y < managerBox.bounds.max.y))
        {
            boundary.SetActive(true);
        }
        else
        {
            StartCoroutine(Fade());
            boundary.SetActive(false);
        }
            
    }

    //Fading between rooms, not done
    IEnumerator Fade()
    {
        cam.fade.color = Color.black;
        yield return new WaitForSeconds(1);
        cam.fade.color = Color.clear;
    }
}
