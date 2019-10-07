using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float maxUpForce = 10f;
    [SerializeField] Transform planet;
    [SerializeField] float distanceRange = 1;
    [SerializeField] float stablizeForce = 0.1f;
    [SerializeField] float engineForceOffset = 0.1f;
    float distanceToPlanet;
    

    private float rotation;
    private Rigidbody rb;
    private GravityBody body;
    private UIScript ui;

    private bool turnRight = false;
    private bool turnLeft = false;
    private bool boosting = false;
    private bool falling = false;
    private float currentUpForce;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("Planet").GetComponent<Transform>();
        ui = GameObject.FindObjectOfType<UIScript>();
        distanceToPlanet = transform.position.y;
        body = GetComponent<GravityBody>();
        currentUpForce = maxUpForce;
    }

    void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");
        if (turnRight) rotation = 1;
        if (turnLeft) rotation = -1;
    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
   
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = rb.rotation * deltaRotation;
     
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));
        StablizeDistance();
        Boosting();
        Falling();
    }

    private void StablizeDistance()
    {
        float currentDistance = Vector3.Distance(transform.position, planet.position);
        if(currentDistance > distanceToPlanet + distanceRange)
        {
            Vector3 gravityVector = (transform.position - planet.position).normalized;
            rb.velocity = new Vector3(0, 0, 0);
            boosting = false;
            rb.AddForce(gravityVector * -stablizeForce);
        }
        
        
    }

    private void Boosting()
    {
        if(boosting && (currentUpForce < maxUpForce))
        {
            Vector3 gravityVector = (transform.position - planet.position).normalized;
            currentUpForce += engineForceOffset;
            rb.AddForce(gravityVector * currentUpForce);
        }
    }

    private void Falling()
    {
        if(falling && (currentUpForce > 1f))
        {
            Vector3 gravityVector = (transform.position - planet.position).normalized;
            currentUpForce -= engineForceOffset;
            rb.AddForce(gravityVector * currentUpForce);
        }
    }

    public void TurnRight(bool isRight)
    {
        turnRight = isRight;
    }

    public void TurnLeft(bool isLeft)
    {
        turnLeft = isLeft;
    }

    public void TurnEngine()
    {
        if(body.GetUseGravity())
        {
            body.SetUseGravity(false);
            boosting = true;
            falling = false;
            ui.SetEngineStatus(true);
        }else
        {
            body.SetUseGravity(true);
            falling = true;
            boosting = false;
            ui.SetEngineStatus(false);
        }
    }

}
