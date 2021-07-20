using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CamRotate : MonoBehaviour
{
    CharMovement movement = null;

    [SerializeField]
    GameObject Player = null;

    Vector3 targetRotation = Vector3.zero;

    float timer = 0;
    float timerSpeedModifier = 1.5f;
    bool startCount = false;

    bool inputsPrimed = true;

    void Start()
    {
        movement = Player.GetComponent<CharMovement>();
    }

    public void SetCamRotPos(CallbackContext context)
    {
        if (context.started && inputsPrimed)
        {
            timer = 0;
            inputsPrimed = false;
            NewCamRotation(1);
        }
        if (context.canceled)
        {
            timer = 0;
            inputsPrimed = true;
        }
    }
    public void SetCamRotNeg(CallbackContext context)
    {
        if (context.started && inputsPrimed)
        {
            timer = 0;
            inputsPrimed = false;
            NewCamRotation(-1);
        }
        if (context.canceled)
        {
            timer = 0;
            inputsPrimed = true;
        }
    }

    void NewCamRotation(int i)
    {
        if (i < 0)
        {
            targetRotation -= new Vector3(0,90,0);
        }
        else
        {
            targetRotation += new Vector3(0, 90, 0);
        }
        startCount = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement.forwardVector = transform.forward;
        movement.rightVector = transform.right;
        gameObject.transform.position = Player.transform.position;
        if (startCount)
        {
            timer += Time.deltaTime * timerSpeedModifier;
            if (timer >= 1)
            {
                startCount = false;
                timer = 0;
            }
        }
        gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), timer);
    }
}
