using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    [SerializeField]
    GameObject BasePrefab;

    [SerializeField]
    int Count = 10; //How big this pool is

    List<SpawnObject> UnusedPool;  //Items free to use

    List<SpawnObject> UsedPool;  //Used Items

    // Start is called before the first frame update
    void Start()
    {
        SpawnObject tSpawnObject = BasePrefab != null ? BasePrefab.GetComponent<SpawnObject>() : null;
        Debug.AssertFormat(tSpawnObject != null, "No BasePrefab or SpawnObject");

        UnusedPool = new List<SpawnObject>();  //Make a Pool of SpawnObjects
        UsedPool = new List<SpawnObject>();  //Items in use

        for (int i = 0; i < Count; i++) //Make pool of object ready for use
        {
            GameObject tGO = Instantiate(BasePrefab, Vector3.zero, Quaternion.identity);
            SpawnObject tSO = tGO.GetComponent<SpawnObject>();      //Get the GO's SpawnObject & Store in Pool
            tSO.name = "Pool object:" + i;
            UnusedPool.Add(tSO);      //Add to pool
            tSO.transform.SetParent(transform);
            tSO.Create(this); //Link SpawnObject back to pool
            tSO.Show(false);    //Don't show until its used
        }
    }

    SpawnObject FindFreeObject() {
        if (UnusedPool.Count > 0)
        {
            SpawnObject tSO = UnusedPool[0];
            Debug.LogFormat("SpawnObject {0} allocated from pool", tSO);
            UnusedPool.Remove(tSO);
            return tSO;      //Get first one from pool if we have fee ones
        }

        foreach(SpawnObject tSO in UsedPool) //If none free then check for ones which can be deallocated
        {
            if(tSO.CanReallocate) //Find a volunteer
            {
                PoolDestroy(tSO);   //Remove it, gets added back to pool
                Debug.LogFormat("Reusing SpawnObject {0} allocated from pool", tSO.name);
                return tSO;     //Offer it up as free now
            }
        }
        Debug.LogFormat("Out of objects");
        return null; //Still none
    }

    public  SpawnObject    PoolSpawn(Vector3 vPosition, Quaternion vRotation)
    {
        SpawnObject tSO = FindFreeObject();      //Get first free one from pool
        if (tSO!=null) //Do we have items to use
        {
            tSO.transform.SetParent(null);  //Unparent
            UsedPool.Add(tSO);                  //Add it to used Pool
            tSO.Reset(vPosition, vRotation);              //Reset to defaults
            return tSO;     //Success
        }
        Debug.LogFormat("Pool empty cannot allocate SpawnObject");
        return null;    //Pool Deleted
    }

    public  void    PoolDestroy(SpawnObject vSO)
    {
        Debug.AssertFormat(vSO != null,"Invalid Object");


        if(UsedPool.Remove(vSO))
        {
            vSO.Show(false);        //Turn Off
            vSO.transform.SetParent(transform); //Parent to pool
            UnusedPool.Add(vSO);    //Add back to Unused Pool
        }
        else
        {
            Debug.LogFormat("Object {0} not in Used Pool", vSO.name);
        }
    }
}
