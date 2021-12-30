using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Quaternion targetAngle_0 = Quaternion.Euler(0,0,0);
    public Quaternion currentAngle;
    public GameObject rocketParent;
    private float rocketYonDrop;
    private bool stageDropped = false;
    private GameObject lastChild;


    void Untag()
    {
        this.tag = "droppedStage";
        m_rigidbody.constraints = RigidbodyConstraints.None;

        lastChild = rocketParent.transform.GetChild(rocketParent.transform.childCount - 2).gameObject;

        

        if(Mathf.Abs(this.transform.position.x - lastChild.transform.position.x) < 0.1)
        {
            this.transform.position = new Vector3(lastChild.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    void AddConstraints()
    {
        
        m_rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        Debug.Log("Constraints added");
    }

    void AddAllConstraints()
    {
        m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        this.tag = "Untagged";
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        rocketParent = GameObject.Find("Rocket");
        this.tag = "firstStage";
    }

    

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(this.tag == "firstStage")
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetAngle_0, 1f);
                this.transform.parent = rocketParent.transform;
                AddConstraints();
                rocketYonDrop = this.transform.position.y;
                stageDropped = true;

                Invoke("Untag", 3f);
                Invoke("AddAllConstraints", 5f);
               
            }      
        }

        if(stageDropped)
        {
            if (this.transform.position.y > rocketYonDrop)
            {
                this.transform.position = new Vector3(this.transform.position.x, rocketYonDrop, this.transform.position.z);
            }
        }
        
    }
}
