using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCanons : MonoBehaviour
{
    [SerializeField, Tooltip("Canon distance, in meters")] private float maxDistance = 5;
    [Space]
    [Tooltip("Spread of canons in degrees, half the angle"), Range(0, 90)]
    [SerializeField] private float angleSpread = 15;
    [Tooltip("Spread of conons in meters"), Range(0, 1)]
    [SerializeField] private float positionSpread = 0.2f;
    [SerializeField] private int canonCount = 5;

    private void OnValidate()
    {
        angleSpread = Mathf.Max(0, angleSpread);
        positionSpread = Mathf.Max(0, positionSpread);
        canonCount = Mathf.Max(2, canonCount);
    }

    private Ray GetShootRay(float t)
    {
        float angle = Mathf.Lerp(-angleSpread, angleSpread, t);
        float offset = Mathf.Lerp(-positionSpread, positionSpread, t);

        Vector3 dir = GetDirectionForAngle(angle);
        Vector3 pos = transform.position - transform.right * offset;

        return new Ray(pos, dir);
    }

    private Vector3 GetDirectionForAngle(float angle)
    {
        return Quaternion.AngleAxis(angle, transform.forward) * transform.up;
    }

    private void OnDrawGizmos()
    {     
        float interval = 1f / (canonCount - 1);

        Ray lastRay = new Ray();

        for (int i = 0; i < canonCount; i++)
        {
            float t = interval * i;
            Ray ray = GetShootRay(t);

            Gizmos.color = i == 0 || i == canonCount - 1
                ? Color.red
                : Color.red * new Color(1, 1, 1, 0.3f);

            Gizmos.DrawRay(ray.origin, ray.direction * maxDistance);

            if(i != 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ray.GetPoint(maxDistance), lastRay.GetPoint(maxDistance));
                Gizmos.DrawLine(ray.origin, lastRay.origin);
            }

            lastRay = ray;
        }
    }
}
