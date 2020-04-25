using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnObject : MonoBehaviour //Base class for Spawn Object
{
    private SpawnPool mPool;
    public void Create(SpawnPool vPool)
    {
        mPool = vPool; //Remember our pool
        OnCreate(); //Call child's OnCreate
    }
    public void Reset(Vector3 vPosition, Quaternion vRotation)
    {
        OnReset(vPosition, vRotation);
    }
    protected abstract void OnCreate(); //Implemented in child to create object in first place
    protected abstract void OnReset(Vector3 vPosition, Quaternion vRotation); //Implemented in child to reset object when reused
    public abstract void Show(bool vShow = true); //Implemented in child
    public virtual void Remove()
    {
        mPool.PoolDestroy(this);        //Tell Pool we are done
    }

    public virtual bool CanReallocate { //Default is no re-allocation
        get {
            return false;
        }
    }

}
