using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Swing : MonoBehaviour
{
    public float delta = 1f;
    public float speed;
    public GameObject stage;
    public GameObject spawner;
    public GameObject cam;
    private Vector3 startPos;
    private float stapUp = 7f;
    //private bool stageOnCable = true;
    private GameObject rocketParent;
    private float rocketHeight;
    private int _score;
    public TMP_Text Score;
    public GameObject level;
    public GameObject winScreen;
    public GameObject[] stageArraySimple = new GameObject[4];
    public GameObject[] stageArrayMedium = new GameObject[4];
    private Scene currentScene;

    GameObject RandomStage()
    {
        if(_score > 5)
        {
            Debug.Log("above 5");
            return stageArrayMedium[Random.Range(0, stageArrayMedium.Length)];
        }
        else
        {
            return stageArraySimple[Random.Range(0, stageArraySimple.Length)];
        }
        
    }

    void SpawnStage()
    {
        rocketHeight = rocketParent.transform.GetChild(rocketParent.transform.childCount - 1).transform.position.y;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 90, 0), 1f);
        startPos = new Vector3(startPos.x, rocketHeight + stapUp, startPos.z);
        transform.position = startPos;
        cam.transform.position = new Vector3(cam.transform.position.x, rocketHeight + stapUp - 4, cam.transform.position.z);


        Vector3 conAnch = gameObject.GetComponent<HingeJoint>().connectedAnchor;
        gameObject.GetComponent<HingeJoint>().connectedAnchor = new Vector3(conAnch.x, rocketHeight + stapUp + 1, conAnch.z);

        _score = rocketParent.transform.childCount;

        Score.text = _score.ToString();

        
        if (!WinCheck())
        {
            GameObject InstStage;
            InstStage = (GameObject)Instantiate(RandomStage(), new Vector3(transform.position.x, startPos.y - 1.7f, transform.position.z), transform.rotation);
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = InstStage.GetComponent<Rigidbody>();
        }
        

        
        //stageOnCable = true;
    }

    bool WinCheck()
    {
        if (_score >= 10)
        {
            level.SetActive(false);
            winScreen.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "Level01")
        {
            speed = 1f;
        }
        else if(currentScene.name == "Level02")
        {
            speed = 3f;
        }

        GameObject InstStage;
        startPos = transform.position;
        InstStage = (GameObject) Instantiate(stage, new Vector3(transform.position.x, transform.position.y - 1.4f, transform.position.z), transform.rotation);
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = InstStage.GetComponent<Rigidbody>();
        rocketParent = GameObject.Find("Rocket");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(Mathf.Clamp(v.x, -1, 1), v.y, v.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Destroy(GetComponent<FixedJoint>());
            //stageOnCable = false;

            Invoke("SpawnStage", 2f);
           
        }

        /*if (Input.GetKeyDown(KeyCode.U) && !stageOnCable)
        {
            SpawnStage();
        }*/

    }
}
