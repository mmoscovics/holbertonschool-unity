using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.AI;

public class TargetManager : MonoBehaviour
{
    public float speed;
    public Bounds bounds;
    public Vector3 sPosition;
    public Vector3 hussle;
    public float velocity;
    public float distance;
    public static bool ready;


    void Start()
    {
        sPosition = transform.position;
        bounds = GameManager.sPlane.GetComponent<MeshCollider>().bounds;
        speed = Random.Range(0.1f, 0.2f);
        distance = Random.Range(0.1f, 0.7f);
        ready = false;
        //sPosition = bounds.center;
        //Vector3 rand = Random.insideUnitSphere;
        //rand.y = sPosition.y + 0.1f;
            //newTarget = Instantiate(target, sPosition, Quaternion.identity);
        //transform.position += rand;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
            MoveTarget();
    }

    void MoveTarget()
    {
        velocity += speed * Time.deltaTime;
        hussle = new Vector3(Mathf.Sin(velocity) * distance, bounds.center.y + 0.5f, Mathf.Cos(velocity) * distance);
        transform.position = sPosition + hussle;
    }
}
