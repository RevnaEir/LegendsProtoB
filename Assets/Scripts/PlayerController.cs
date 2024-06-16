using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lateralMoveSpeed = 2f;
    public float boostMultiplier = 2f;
    public float boostDuration = 2f;
    public float boostCooldown = 5f;
    public float maxTurnAngle = 10f; // Small angle for slight directional change

    private float currentBoostTime = 0f;
    private float boostCooldownTime = 0f;
    private bool isBoosting = false;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded = true;

    private Vector3 initialForward; // To maintain the primary downhill direction

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        initialForward = transform.forward; // Capture the initial forward direction at start
    }
    public void StartSkiing()
    {
        SoundManager.Instance.PlaySoundEffect(SoundManager.SoundEffect.Skiing);
    }

    public void Boost()
    {
        SoundManager.Instance.PlaySoundEffect(SoundManager.SoundEffect.Whoosh);
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal") * -1;
            Vector3 lateralMovement = transform.right * horizontalInput * lateralMoveSpeed;
            rb.MovePosition(rb.position + lateralMovement * Time.deltaTime);

            // Calculate a slight rotation for visual feedback
            if (Mathf.Abs(horizontalInput) > 0)
            {
                float rotationAngle = horizontalInput * maxTurnAngle;
                Quaternion slightTurn = Quaternion.AngleAxis(rotationAngle, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(initialForward) * slightTurn, Time.deltaTime * 10);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(initialForward), Time.deltaTime * 10);
            }
        }


        HandleBoost();
        
        

        // Maintain constant forward movement in the initial downhill direction
        Vector3 forwardMovement = initialForward * (isBoosting ? moveSpeed * boostMultiplier : moveSpeed);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardMovement.z);

        // Update the animator
        float speed = rb.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void HandleBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space) && boostCooldownTime <= 0)
        {
            isBoosting = true;
            currentBoostTime = boostDuration;
            boostCooldownTime = boostCooldown;
        }

        if (isBoosting)
        {
            if (currentBoostTime > 0)
            {
                currentBoostTime -= Time.deltaTime;
            }
            else
            {
                isBoosting = false;
            }
        }

        if (boostCooldownTime > 0)
        {
            boostCooldownTime -= Time.deltaTime;
        }
    }

    public void QuickBoost()
    {
        boostCooldownTime = 0;
        if (boostCooldownTime <= 0)
        {
            isBoosting = true;
            currentBoostTime = boostDuration;
            boostCooldownTime = boostCooldown;
        }

        if (isBoosting)
        {
            if (currentBoostTime > 0)
            {
                currentBoostTime -= Time.deltaTime;
            }
            else
            {
                isBoosting = false;
            }
        }

        if (boostCooldownTime > 0)
        {
            boostCooldownTime -= Time.deltaTime;
        }

    }
}