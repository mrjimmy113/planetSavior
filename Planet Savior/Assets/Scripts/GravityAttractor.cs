using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = 9.8f;


    public void Attract(Rigidbody body, bool useGravity)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 localUp = body.transform.up;

        // Apply downwards gravity to body
        if(useGravity)
        {
            body.AddForce(gravityUp * -gravity);
        }
        // Allign bodies up axis with the centre of planet
        //body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;

        RotateBody(body);
    }
    void RotateBody(Rigidbody body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(body.transform.up, gravityUp) * body.rotation;
        body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, 50f * Time.deltaTime));
    }
}
