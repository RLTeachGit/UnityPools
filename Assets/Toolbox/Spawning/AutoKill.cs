using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour
{
    [SerializeField]
    float TimeToLive = 1.0f; //Set to Seconds to live

    void Start()
    {
        if(TimeToLive>0)
        {
            Invoke("SelfDestroy", TimeToLive); //Call function after set time
        }
    }

    void    SelfDestroy()
    {
        SpawnObject tSP = GetComponentInChildren<SpawnObject>();
        if (tSP != null)
        {
            tSP.Remove();
        }
        else
        {
            Destroy(gameObject, TimeToLive);
        }
    }
}
