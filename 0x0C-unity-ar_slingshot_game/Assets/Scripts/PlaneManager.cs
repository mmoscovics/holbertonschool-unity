using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

public class PlaneManager : MonoBehaviour
{
    public ARPlaneManager arPM;
    public ARRaycastManager arRM;

    public GameObject searching;
    public GameObject select;
    public GameObject startButton;
    public GameObject setupBG;
    private ARPlane sPlane;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        arPM = GetComponent<ARPlaneManager>();
        arRM = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arPM.trackables.count > 0)
        {
            searching.SetActive(false);
            if (!sPlane)
                select.SetActive(true);
        }

        if (Input.touchCount > 0)
            if (!sPlane && arRM.Raycast(Input.GetTouch(0).position, m_Hits))
            {
                sPlane = arPM.GetPlane(m_Hits[0].trackableId);
                DisablePlanes(sPlane.trackableId);
                setupBG.SetActive(false);
                startButton.SetActive(true);
            }
    }

    void DisablePlanes(TrackableId id)
    {
        arPM.enabled = false;
        foreach (var plane in arPM.trackables)
            if (plane.trackableId != id)
                plane.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        sPlane.GetComponent<MeshRenderer>().enabled = false;
        sPlane.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
        TargetSetUp();
    }

    void TargetSetUp()
    {
        Bounds bounds = sPlane.GetComponent<MeshCollider>().bounds;
    }
}
