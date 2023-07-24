using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class CubeController : MonoBehaviour
{
    [SerializeField] GameObject CubePrefab;
    GameObject metalBall;
    GameObject activeCubes;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Init();
        metalBall = GameObject.Find("Metal ball");
        activeCubes = GameObject.Find("ActiveCubes");
        SpawnColumOnCenterWithTwoHole(metalBall.transform.position.z + 40);
        SpawnColumOnCenterWithTwoHole(metalBall.transform.position.z + 80);
        SpawnColumOnCenterWithTwoHole(metalBall.transform.position.z + 120);
        SpawnColumOnCenterWithTwoHole(metalBall.transform.position.z + 160);
        SpawnColumOnCenterWithTwoHole(metalBall.transform.position.z + 200);
    }
    public void SpawnColumOnCenterWithTwoHole(float zPosition)
    {
        int lvlOfHole = Random.Range(0, 10);
        for(int i = 0; i < 10; i++)
        {
            if (i != lvlOfHole && i != (lvlOfHole+1))
            {
                GameObject tempCube = PoolManager.GetGameObjectFromPool(CubePrefab);
                tempCube.transform.parent = activeCubes.transform;
                tempCube.transform.position = new Vector3(4.5f, i + 0.5f, zPosition);
            }
        }
    }    
    // Update is called once per frame
    void Update()
    {
        if (PoolManager.poolsDictionary[CubePrefab.name].Count >= 8)
        {
            SpawnColumOnCenterWithTwoHole(metalBall.transform.position.z + 200);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //MainCamera.GetComponent<AudioManager>().PlaySound(OnLostHpSound);
        if (other.gameObject.name == "Green Cube")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
