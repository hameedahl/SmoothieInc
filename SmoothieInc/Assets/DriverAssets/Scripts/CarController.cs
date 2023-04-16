using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CarController : NetworkBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f;
    public float normalAccelerationFactor = 30.0f;
    float accelerationFactor = 30.0f;
    public float nitroAccelerationFactor = 40.0f;
    
    public float turnFactor = 3.5f;
    public float normalMaxSpeed = 20;
    float maxSpeed = 20;
    public float nitroMaxSpeed = 40;

    bool nitro = false;
    public int maxNitroTime = 60;
    int nitroTime = 0;
    public int maxNitroCooldownTime = 180;
    int nitroCooldownTime = 0;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        accelerationFactor = normalAccelerationFactor;
        maxSpeed = normalMaxSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            if(nitroCooldownTime <= 0)
            {
                nitro = true;
                nitroCooldownTime = maxNitroCooldownTime;
                nitroTime = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if(IsHost) 
        {
            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
        }
        if(nitro)
        {
            if(nitroTime < maxNitroTime)
            {
                nitroTime++;
                accelerationFactor = nitroAccelerationFactor;
                maxSpeed = nitroMaxSpeed;
            }
            else
            {
                nitro = false;
                accelerationFactor = normalAccelerationFactor;
                maxSpeed = normalMaxSpeed;
            }
        }
        else
        {
            if(nitroCooldownTime > 0)
            {
                nitroCooldownTime--;
            }
        }
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        if(rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if(accelerationInput == 0)
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        else
            rb.drag = 0;

        Vector2 efVector = transform.up * accelerationInput * accelerationFactor;

        rb.AddForce(efVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minTurnSpeed = (rb.velocity.magnitude / 8);
        minTurnSpeed = Mathf.Clamp01(minTurnSpeed);

        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if(velocityVsUp >= 0)
            rotationAngle -= steeringInput * turnFactor * minTurnSpeed;
        else
            rotationAngle -= -steeringInput * turnFactor * minTurnSpeed;

        rb.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
