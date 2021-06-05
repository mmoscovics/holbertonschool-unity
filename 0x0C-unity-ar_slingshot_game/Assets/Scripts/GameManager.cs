using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ARPlaneManager arPM;
    public ARRaycastManager arRM;
    public GameObject searching;
    public GameObject select;
    public GameObject startButton;
    public GameObject paButton;
    public GameObject rButton;
    public GameObject qButton;
    public GameObject sBG;
    public GameObject sT;
    public int scoreNum;
    public GameObject aBG;
    public GameObject aT;
    public int ammoNum;
    public GameObject setupBG;
    public GameObject target;
    public int targetCount = 5;
    public GameObject newTarget;
    public List<GameObject> targets;
    public GameObject ammo;
    public GameObject setAmmo;
    public static ARPlane sPlane;
    public Vector3 sPosition;
    public Vector3 cPosition;
    public Bounds bounds;

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
                TargetSetup();
                Debug.Log("Selected Plane");
            }
        if (setAmmo && setAmmo.transform.position.y < cPosition.y)
        {
            Destroy(setAmmo);
            AmmoSetup();
        }
        if (ammoNum == 0)
        {
            Destroy(setAmmo);
            paButton.SetActive(true);
        }
    }

    void DisablePlanes(TrackableId id)
    {
        arPM.enabled = false;
        foreach (var plane in arPM.trackables)
            if (plane.trackableId != id)
                plane.gameObject.SetActive(false);
        Debug.Log("Disable Planes");
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        paButton.SetActive(false);
        qButton.SetActive(true);
        rButton.SetActive(true);
        sBG.SetActive(true);
        aBG.SetActive(true);
        sPlane.GetComponent<MeshRenderer>().enabled = false;
        sPlane.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
        sPosition = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 0.5f);
        TargetManager.ready = true;
        AmmoSetup();
        Debug.Log("Game Start");
    }

    public void ReplayGame()
    {
        Destroy(setAmmo);
        foreach (var target in targets)
            Destroy(target);
        scoreNum = 0;
        sT.GetComponent<Text>().text = scoreNum.ToString();
        ammoNum = 7;
        aT.GetComponent<Text>().text = ammoNum.ToString();
        TargetSetup();
        TargetManager.ready = true;
        StartGame();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void TargetSetup()
    {
        bounds = sPlane.GetComponent<MeshCollider>().bounds;
        for (int i = 0; i < targetCount; i++)
        {
            cPosition = bounds.center;
            Vector3 rand = Random.insideUnitSphere;
            rand.y = cPosition.y + 0.55f;
            newTarget = Instantiate(target, cPosition, Quaternion.identity);
            newTarget.transform.position += rand;
            targets.Add(newTarget);
            Debug.Log("Spawned Target");
        }
        Debug.Log("Targets Setup");
    }

    void AmmoSetup()
    {
        //Vector3 sPosition = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 0.5f);
        setAmmo = Instantiate(ammo, Camera.main.ScreenToWorldPoint(sPosition), Quaternion.identity);
        setAmmo.GetComponent<AmmoManager>().enabled = true;
        Debug.Log("Ammo Setup");
    }

    public void UpdateAmmo()
    {
        ammoNum -= 1;
        aT.GetComponent<Text>().text = ammoNum.ToString();
    }

    public void UpdateScore()
    {
        sT.GetComponent<Text>().text = scoreNum.ToString();
    }
}
