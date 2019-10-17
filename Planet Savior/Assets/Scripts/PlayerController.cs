using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] GameObject planet;
    [SerializeField] float upDownOffset = 0.1f;
    [SerializeField] float maxCapacity = 8f;
    float distanceToPlanet;
    

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
    }

    void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");
        if (turnRight) rotation = 1;
        if (turnLeft) rotation = -1;
        if (flyDown)
        {
            air.radius = Vector3.Distance(transform.position, planet.transform.position) - 1;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
   
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = rb.rotation * deltaRotation;
     
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));

        if (flyUp) air.radius += upDownOffset;

        

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



    public void TurnRight(bool isRight)
    {
        turnRight = isRight;
    }

    public void TurnLeft(bool isLeft)
    {
        turnLeft = isLeft;
    }

    public void FlyUp(bool isUp)
    {
        flyUp = isUp;
    }

    public void FlyDown(bool isDown)
    {
        if(isDown)
        {
            air.isTrigger = true;
            flyDown = true;
        }else
        {
            flyDown = false;
            air.isTrigger = false;
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

    #region Movement theo Force
    /*    public void TurnEngine()
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
       }*/
    /*  private void StablizeDistance()
  {
      float currentDistance = Vector3.Distance(transform.position, planet.transform.position);
      if(currentDistance > distanceToPlanet + distanceRange)
      {
          Vector3 gravityVector = (transform.position - planet.transform.position).normalized;
          rb.velocity = new Vector3(0, 0, 0);
          boosting = false;
          rb.AddForce(gravityVector * -stablizeForce);
      }


  }*/

    /*    private void Boosting()
        {
            if(boosting && (currentUpForce < maxUpForce))
            {
                Vector3 gravityVector = (transform.position - planet.transform.position).normalized;
                currentUpForce += engineForceOffset;
                rb.AddForce(gravityVector * currentUpForce);
            }
        }*/

    /*    private void Falling()
        {
            if(falling && (currentUpForce > 1f))
            {
                Vector3 gravityVector = (transform.position - planet.transform.position).normalized;
                currentUpForce -= engineForceOffset;
                rb.AddForce(gravityVector * currentUpForce);
            }
        }*/
    #endregion

}
