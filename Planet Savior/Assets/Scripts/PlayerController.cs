using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] GameObject planet;

    [SerializeField] float maxCapacity = 8f;


    [SerializeField] float maxFuel = 100f;
    private float currentFuel;
    [SerializeField] float fuelDecreaseRate = 1;

    [SerializeField] Light lightObject;
    [SerializeField] float maxLight = 45f;
    [SerializeField] float lightDecreaseRate = 0.0001f;
    private float lightDecrease;

    float distanceToPlanet;

    private Animator animator;
    

    private float rotation;
    private Rigidbody rb;
    private GravityBody body;
    private UIScript ui;
    private SphereCollider air;

    private bool turnRight = false;
    private bool turnLeft = false;
    private bool flyUp = false;
    private bool flyDown = false;
    private float currentUpForce;
    private float currentCapacity = 0;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindWithTag("Planet");
        air = planet.GetComponent<SphereCollider>();
        ui = GameObject.FindObjectOfType<UIScript>();
        distanceToPlanet = transform.position.y;
        body = GetComponent<GravityBody>();
        animator = GetComponentInChildren<Animator>();

        currentFuel = maxFuel;
        lightObject.spotAngle = maxLight;
        lightDecrease = lightDecreaseRate;
        
    }

    void Update()
    {
        rotation = 0;
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 2)
            {
                rotation = -1;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                rotation = 1;
            }
        }
        //rotation = Input.GetAxis("Horizontal");
        
        animator.SetFloat("turnOffset", rotation);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
   
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = rb.rotation * deltaRotation;
     
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));

        lightObject.spotAngle -= lightDecrease;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "People")
        {
            if(currentCapacity < maxCapacity)
            {
                currentCapacity++;
                ui.SetCapacity(currentCapacity);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            Debug.Log("Boom");
        }
    }

    public float GetCurrentCapacity()
    {
        return currentCapacity;
    }

    public void SetCurrentCapacity(float capacity)
    {
        this.currentCapacity = capacity;
        ui.SetCapacity(currentCapacity);
    }

    public void SetLightDecrease(float lightDecrease)
    {
        this.lightDecrease = lightDecrease;
    }

    public void ResetLightDecrease()
    {
        this.lightDecrease = lightDecreaseRate;
    }

    public void ReFillLight()
    {
        this.lightObject.spotAngle = this.maxLight;
    }


}
