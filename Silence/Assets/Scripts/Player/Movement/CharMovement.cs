using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]

public class CharMovement : MonoBehaviour
{
    Rigidbody rb = null;

    public Vector3 forwardVector = Vector3.forward;
    public Vector3 rightVector = Vector3.right;

    Vector3 moveVals = Vector3.zero;
    Vector2 lookDir = Vector2.zero;


    public float speed = 5f;

    bool canMove = true;
    bool InputsDetected = false;

    PlayerInput input = null;
    GameObject camObj = null;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        camObj = Camera.main.gameObject.transform.parent.gameObject;
    }



    Vector3 moveDir = Vector3.zero;
    Vector3 jumpVal = Vector3.zero;
    void Update()
    {
        Vector2 stick = input.actions["Move"].ReadValue<Vector2>();

        switch (stick == Vector2.zero)
        {
            case true:
                InputsDetected = false;
                break;
            case false:
                InputsDetected = true;
                moveVals = ((stick.x * rightVector) + (stick.y * forwardVector)) * speed + storedVel;
                break;
        }

        moveDir = (stick.x * rightVector) + (stick.y * forwardVector);

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.15F);
        }
    }

    public void UIMoveInput(Vector2 value)
    {
        if (!InputsDetected)
        {
            Vector3 moveDir = (value.x * rightVector) + (value.y * forwardVector);
            if (moveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.15F);
            }
            moveVals = ((value.x * rightVector) + (value.y * forwardVector)) * speed + storedVel;
        }
    }
    void FixedUpdate()
    {
        MoveFunc();
    }


    float stoppingTimer = 0; //Time it takes to reach velocity(0)
    float stoppingDamper = 0f; //Float clamped between 1 and 3 to act as modifier for stopping time
    [SerializeField]
    float minDampingMod = 1, maxDampingMod = 3; //Values declaring how min and max values for damping modifier
    float maxSpeed = 10f;
    Vector3 storedVel = Vector3.zero;
    void MoveFunc()
    {
        moveVals += jumpVal;
        rb.velocity = moveVals;
        if (moveVals != Vector3.zero && canMove)
        {
            stoppingTimer = 0;   //Assign stopping Lerp timer to 0
            //rb.velocity = (new Vector3(moveVals.x, 0, moveVals.z )* speed + storedVel);     //Assign velocity = moveValues multiplied by speed. Add storedvelocity captured at end of fixed update
            stoppingDamper += Time.deltaTime;
            stoppingDamper = Mathf.Clamp(stoppingDamper, minDampingMod, maxDampingMod);   //Add to damping and limit to min and max values assigned in editor
        }
        else
        {
            stoppingTimer += Time.deltaTime / ((int)stoppingDamper / 2);   //Add to stoppingTimer by time/ half our damping value
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, rb.velocity.y, 0), stoppingTimer);   //set velocity to a value between velocity and 0, based on our StoppingTimer
        }
        storedVel = new Vector3(rb.velocity.x, 0, rb.velocity.z) * 0.1f; //Store a tenth of the rb's x,z velocity on this assignment

        //Clamp RB velocity so it never exceeds maximum speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            //rb.velocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0, rb.velocity.z), maxSpeed);
            
        }
    }
}
