using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posChange : MonoBehaviour
{
    public TETConnect myTET;
    private float x, y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = myTET.TET.values.frame.avg.x;
        Debug.Log(x);
        gameObj.transform.localPosition = new Vector3(x,y,0);
        //y = myTET.frame.avg.y;
    }
}
