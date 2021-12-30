using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailedDrop : MonoBehaviour
{
    public GameObject level;
    public GameObject gameOver;
    public GameObject failZone;
    public GameObject rocket;
    private GameObject rocketLastChild;

    void failZoneUp()
    {
        rocketLastChild = rocket.transform.GetChild(rocket.transform.childCount - 1).gameObject;
        failZone.transform.position = new Vector3(rocketLastChild.transform.position.x, rocketLastChild.transform.position.y - 2f, rocketLastChild.transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("failZoneUp", 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("firstStage"))
        {
            level.SetActive(false);
            gameOver.SetActive(true);
        }
        

    }
}
