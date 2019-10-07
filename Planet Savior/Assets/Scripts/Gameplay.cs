using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] GameObject peoplePrefab = null;
    [SerializeField] Transform planet = null;
    [SerializeField] float extraPlanetR = 1;
    [SerializeField] Transform[] obsticals = null;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator SpawnRoutine()
    {
        SpawnPeople();
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnPeople()
    {
        Vector3 spawnPosition = RandomPointInSphere(planet.localScale.x / 2 + extraPlanetR, planet.position);
        if (obsticals != null)
        {
            foreach (var obstical in obsticals)
            {
                if (IsPointOfSphere(spawnPosition, obstical.localScale.x / 2 + 1, obstical.position))
                {
                    return;
                }
            }
        }
        peoplePrefab.transform.position = spawnPosition;
        Instantiate(peoplePrefab);
    }

    private Vector3 RandomPointInSphere(float R, Vector3 rootPoint)
    {
        Vector3 position = Random.onUnitSphere + rootPoint;
        position = position * R;
        return position;
    }

    private bool IsPointOfSphere(Vector3 point, float R, Vector3 rootPoint)
    {
        bool xIn = false;
        bool yIn = false;
        bool zIn = false;

        if(point.x <= (rootPoint.x + R) && (rootPoint.x - R) <= point.x)
        {
            xIn = true;
        }

        if (point.y <= (rootPoint.y + R) && (rootPoint.y - R) <= point.y)
        {
            yIn = true;
        }

        if (point.z <= (rootPoint.z + R) && (rootPoint.z - R) <= point.z)
        {
            zIn = true;
        }


        return xIn && yIn && zIn;
    }
}
