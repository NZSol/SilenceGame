using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UIMoveInput(Vector2 value)
    {
        lookDir = value;
        moveVals = new Vector3(value.x, 0, value.y);
    }

    public void MoveInput(CallbackContext context)
    {
        Vector2 stick = context.ReadValue<Vector2>();
        lookDir = stick;
        moveVals = new Vector3(stick.x, 0, stick.y);
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
        //Make character face direction moving in
        if (moveVals.magnitude != 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(lookDir.x, lookDir.y) * 180 / Mathf.PI, 0);
        }
        //Normalize value if magnitude > 1
        if (moveVals.magnitude > 1)
        {
            moveVals = moveVals.normalized;
        }

        if (moveVals != Vector3.zero && canMove)
        {

            stoppingTimer = 0;   //Assign stopping Lerp timer to 0
            rb.velocity = moveVals * speed + storedVel;     //Assign velocity = moveValues multiplied by speed. Add storedvelocity captured at end of fixed update
            stoppingDamper += Time.deltaTime;
            stoppingDamper = Mathf.Clamp(stoppingDamper, minDampingMod, maxDampingMod);   //Add to damping and limit to min and max values assigned in editor
        }
        else
        {
            stoppingTimer += Time.deltaTime / ((int)stoppingDamper / 2);   //Add to stoppingTimer by time/ half our damping value
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, stoppingTimer);   //set velocity to a value between velocity and 0, based on our StoppingTimer
        }
        storedVel = rb.velocity * 0.1f; //Store a tenth of the rb's velocity on this call

        //Clamp RB velocity so it never exceeds maximum speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
