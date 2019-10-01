using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class dashAbility : MonoBehaviour
{

    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;
    public float dashSpeed = 2f;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //Switches between three different states of movement
        switch (dashState)
        {
            //Each case is a state in the dashing ability
            case DashState.Ready:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Working");
                    rb.AddForce(Vector2.right * 12000);
                    dashState = DashState.Dashing;
                }
                break;

            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    Debug.Log("Working pt 2");
                    dashTimer = maxDash;
                    dashState = DashState.Cooldown;
                }
                break;

            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    Debug.Log("Done");
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}