using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{

    [SerializeField]
    Color Colour=Color.green;

    Collider mMyCollider;

    [SerializeField]
    float CareRadius=3.0f;

    public  bool    PlayerVisible {
        get {
            return  mPlayerVisible;
        }
    }

    public  Vector3 PlayerPosition {
        get {
            return mPlayerPosition;
        }
    }

    public  float Radius {
        get {
            return CareRadius;
        }
    }

    private void Start()
    {
        mMyCollider = GetComponent<Collider>();
    }

    bool mPlayerVisible=false;        //Can player been seen
    Vector3 mPlayerPosition=Vector3.zero;


    private void Update()
    {
        RaycastHit tHit;
        Ray tRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(tRay, out tHit))
        {
            if(tHit.collider == mMyCollider)
            {
                mPlayerPosition = tHit.point;
                mPlayerVisible = true;
                return;
            }
        }
        mPlayerVisible = false;
    }

    private void OnDrawGizmos()
    {
        if (PlayerVisible)
        {
            Gizmos.color = Colour;
            Gizmos.DrawSphere(mPlayerPosition, Radius);
        }
    }
}
