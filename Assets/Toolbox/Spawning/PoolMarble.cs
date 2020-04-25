using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMarble : SpawnObject
{
    MeshRenderer mMR; //Cache Mesh Renderer
    Rigidbody mRB;

    protected override void OnCreate()
    {
        mMR = GetComponent<MeshRenderer>();
        mRB = GetComponent<Rigidbody>();
        Debug.Assert(mMR != null && mRB !=null);
    }

    protected override void OnReset(Vector3 vPosition, Quaternion vRotation)
    {
        transform.position = vPosition;
        transform.rotation = vRotation;
        Show();
    }

    public override void Show(bool vShow = true)
    {
        mMR.enabled = vShow;
        mRB.detectCollisions = vShow;
        mRB.isKinematic = !vShow;
    }

    public override void Remove()
    {
        base.Remove();   //Call Base to take care of moving back to Unused Pool
    }
}
