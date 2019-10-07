using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    GravityAttractor planet;
    Rigidbody rb;

    [SerializeField] bool useGravity = true;

    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rb = GetComponent<Rigidbody>();

        // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        // Allow this body to be influenced by planet's gravity
        planet.Attract(rb,useGravity);
    }

    public void SetUseGravity(bool gravity)
    {
        useGravity = gravity;
    }

    public bool GetUseGravity()
    {
        return useGravity;
    }
}
