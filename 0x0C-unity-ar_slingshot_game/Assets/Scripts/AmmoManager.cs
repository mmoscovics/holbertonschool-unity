using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AmmoManager : MonoBehaviour
{
    public bool ready = false;
    public Rigidbody rb;
    public Vector2 lvelocity;
    public float power = 5f;
    public Vector2 direction;
    public Bounds bounds;
    public ARPlane plane;
    public Transform anchor;
    public Vector3 pVelocity;

    LineRenderer lRenderer;

    private Vector2 sPosition;
    private GameManager gm;
    //private bool aimed = false;

    void Start()
    {
        sPosition = this.transform.position;
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        plane = GameManager.sPlane;
        lRenderer = GetComponent<LineRenderer>();
        //bounds = GameManager.sPlane.GetComponent<MeshCollider>().bounds;
        //arc = GameObject.Find("Arc").GetComponentInChildren<LaunchArc>();
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (ready)
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        sPosition = new Vector2(touch.position.x, touch.position.y);
                        lRenderer.enabled = true;
                        break;
                    case TouchPhase.Ended:
                        rb.useGravity = true;
                        direction = new Vector2(touch.position.x, touch.position.y);
                        lvelocity = direction - sPosition;
                        rb.constraints = RigidbodyConstraints.None;
                        pVelocity = new Vector3(-lvelocity.x, -lvelocity.y, -lvelocity.y);
                        rb.AddForce(pVelocity);
                        rb.transform.parent = null;
                        lRenderer.enabled = false;
                        gm.UpdateAmmo();
                        ready = false;
                        break;
                }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Target")
        {
            other.gameObject.SetActive(false);
            gm.scoreNum += 1;
            gm.UpdateScore();
        }
    }
    // void LaunchAmmo()
    // {
    //     ready = false;
    //     rb.useGravity = true;
    //     rb.AddForce(direction * power);
    //     Debug.Log("Launch");
    // }
}
