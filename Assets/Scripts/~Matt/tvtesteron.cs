using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvtesteron : MonoBehaviour
{
    public Material tvVideoMat;
    public MeshRenderer tvRender;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            tvRender.material = tvVideoMat;       
        }
    }
}
