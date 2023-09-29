
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs; //풀링을 적용할 오브젝트들을 담는 배열

    public List<GameObject>[] pools { get; private set; } 

    public void PoolSetting()
    {
        for(int i = 0; i < 5; i++)
        {
            Prefabs[i] = GameManager.instance.heroDatas[i].snowMan.gameObject;
        }
        pools =new List<GameObject>[Prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    public GameObject GetPools(int index)
    {
        GameObject selectedObject = null;
        foreach (GameObject pulling in pools[index])
        {
            if (!pulling.activeSelf)
            {
                selectedObject = pulling;
                selectedObject.SetActive(true);
                break;
            }
        }

        if (!selectedObject)
        {
            //Instantiate(object,vector3,Quaternion,parent)
            selectedObject = Instantiate(Prefabs[index],transform);
            pools[index].Add(selectedObject);
        }

        return selectedObject;
    }
}
