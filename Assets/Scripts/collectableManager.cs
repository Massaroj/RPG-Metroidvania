using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableManager : MonoBehaviour
{
    public PickupType pickupType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch(pickupType)
            {
                case PickupType.fireAbility:
                    Debug.Log("Fire ability aqcuired");
                    collision.gameObject.GetComponent<movementJump>().fireEnabled = true;
                    collision.gameObject.GetComponent<movementJump>().iceEnabled = false;
                    Destroy(gameObject);
                    break;
                case PickupType.upgradePoint:
                    Debug.Log("Upgrade Point");
                    Destroy(gameObject);
                    break;
                case PickupType.iceAbility:
                    Debug.Log("Ice ability aqcuired");
                    collision.gameObject.GetComponent<movementJump>().iceEnabled = true;
                    collision.gameObject.GetComponent<movementJump>().fireEnabled = false;
                    Destroy(gameObject);
                    break;
                case PickupType.healthPickup:
                    Debug.Log("health up");
                    collision.gameObject.GetComponent<movementJump>().UI.GetComponent<UIManager>().updateHealth(collision.gameObject.GetComponent<movementJump>().health + 1);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public enum PickupType
    {
        upgradePoint,
        fireAbility,
        iceAbility,
        bossToken,
        healthPickup,
        swordUpgrade
    }
}
