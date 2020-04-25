using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class KillZone : MonoBehaviour
{


    [SerializeField]
    Bounds Bounds; //Can Be editied in IDE

    [SerializeField]
    bool HideInGame = true;  //Hide if playing, can be overridden in IDE

    [SerializeField]
    Color Colour = new Color(1.0f, 0, 0, 0.1f); //Default Gizmo colour


    //Draw Gizmo which show current spawning volume
    void OnDrawGizmosSelected()
    {
        if ((!Application.isPlaying) || (Application.isPlaying && !HideInGame)) //Only draw in editor or in game if not hidden
        {
            Gizmos.color = Colour;     //Draw spawning bounds in preset colour
            Gizmos.DrawCube(RelativeCenter, RelativeSize); //Make sure it take parent position into account
        }
    }

    Vector3 RelativeCenter {
        get {
            return transform.position + Bounds.center; //Take account of parent object origin
        }
    }

    Vector3 RelativeSize {
        get {
            Vector3 tRelSize = Bounds.size / 2;
            tRelSize.x *= transform.localScale.x;
            tRelSize.y *= transform.localScale.y;
            tRelSize.z *= transform.localScale.z;
            return tRelSize;
        }
    }

    BoxCollider mBC; //Cached BoxCollider

    private void Start()
    {
        if(Application.isPlaying) //Only call if playing, so its not created in edit
        {
            mBC = gameObject.AddComponent<BoxCollider>(); //Make & Add collider
            mBC.center = Bounds.center; //Take measurements from Bounds
            mBC.size = RelativeSize; //For some reason size is double
            mBC.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnObject tSP = other.GetComponentInChildren<SpawnObject>();
        if(tSP!=null)
        {
            tSP.Remove();  //If a Spawn item use Pool Methods to re-pool
        }
        else
        {
            Destroy(other.gameObject);      //if not Kill item directly
        }
    }
}
