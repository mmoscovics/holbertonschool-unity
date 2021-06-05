using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArc : MonoBehaviour
{
    AmmoManager ammoManager;
    LineRenderer lineRenderer;

    public int nPoints = 20;
    public float distance = 0.2f;
    public LayerMask collidable;

    // Start is called before the first frame update
    void Start()
    {
        ammoManager = GetComponent<AmmoManager>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = nPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 sPosition = ammoManager.anchor.position;
        Vector3 sVelocity = ammoManager.pVelocity;

        for (float t = 0; t < nPoints; t += distance)
        {
            Vector3 nPoint = sPosition + t * sVelocity;
            nPoint.y = sPosition.y + sVelocity.y * t + Physics.gravity.y/2f * t * t;
            points.Add(nPoint);

            if(Physics.OverlapSphere(nPoint, 2, collidable).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        lineRenderer.SetPositions(points.ToArray());
    }
}
