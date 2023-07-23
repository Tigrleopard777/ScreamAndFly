using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static Dictionary<string, LinkedList<GameObject>> poolsDictionary;
    private static Transform deactivatedObjectsParent;
    // Start is called before the first frame update
    public static void Init()
    {
        deactivatedObjectsParent = GameObject.Find("PoolOfCubes").transform;
        poolsDictionary = new Dictionary<string, LinkedList<GameObject>>();
    }

    public static GameObject GetGameObjectFromPool (GameObject prefab)
    {
        if(!poolsDictionary.ContainsKey(prefab.name))
        {
            poolsDictionary[prefab.name] = new LinkedList<GameObject>();
        }

        GameObject result;

        if (poolsDictionary[prefab.name].Count > 0)
        {
            result = poolsDictionary[prefab.name].First.Value;
            poolsDictionary[prefab.name].RemoveFirst();
            result.SetActive(true);
            return result;
        }

        result = GameObject.Instantiate(prefab);
        result.name = prefab.name;

        return result;
    }

    public static void PutGameObjectToPool(GameObject target)
    {
        poolsDictionary[target.name].AddLast(target);
        target.transform.parent = deactivatedObjectsParent;
        target.SetActive(false);
    }

}
