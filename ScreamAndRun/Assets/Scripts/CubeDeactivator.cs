using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDeactivator : MonoBehaviour
{
    GameObject metalBall;
    private void Start()
    {
        metalBall = GameObject.Find("Metal ball");
    }
    // Update is called once per frame

    void Update()
    {
        if((this.transform.position.z + 0.5) < metalBall.transform.position.z)
        {
            PoolManager.PutGameObjectToPool(this.gameObject);
            BallController.points++;
        }
    }
}
