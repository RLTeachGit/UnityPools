using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //Also run this code in editor, useful for dynamic editing
public class SpawnFromPool : MonoBehaviour
{
    [SerializeField]
    Bounds Bounds; //Can Be editied in IDE

    [SerializeField]
    bool HideInGame = true;  //Hide if playing, can be overridden in IDE

    [SerializeField]
    Color Colour = new Color(1.0f, 0, 0, 0.1f); //Default Gizmo colour

    [SerializeField]
    float SpawnTime = 20.0f; //Max SpawnTime

    [SerializeField]
    SpawnObject SpawnItem; //Set in IDE

    [SerializeField]
    SpawnPool SpawnPool; //Set in IDE

    [SerializeField]
    bool Spawning = true; //Set in IDE


    //Draw Gizmo which show current spawning volume
    void OnDrawGizmosSelected()
    {
        if ((!Application.isPlaying) || (Application.isPlaying && !HideInGame)) //Only draw in editor or in game if not hidden
        {
            Gizmos.color = Colour;     //Draw spawning bounds in preset colour
            Gizmos.DrawCube(SpawnCenter,RelativeSize); //Makse sure it take parent position into account
        }
    }

    Vector3 SpawnCenter {
        get {
            return transform.position + Bounds.center; //Take account of parent object origin
        }
    }

    Vector3 RandomSpawnPosition {
        get {
            Vector3 tRandomOffset = new Vector3(Random.Range(-Bounds.extents.x / 2, Bounds.extents.x / 2)
                                                , Random.Range(-Bounds.extents.y / 2, Bounds.extents.y / 2)
                                                , Random.Range(-Bounds.extents.z / 2, Bounds.extents.z / 2)); //Make relative random position
            return SpawnCenter + tRandomOffset; //Add center
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

    private void Start()
    {
        StartCoroutine(SpawnCoRoutine()); //Start CoRoutine
    }

    IEnumerator SpawnCoRoutine()
    {
        //Only run Spawing durng play
        while (Application.isPlaying) //Infinite loop, generally a BAD idea, but this one yields
        {
            yield return new WaitForSeconds(SpawnTime);
            if (SpawnItem != null && SpawnPool != null && Spawning)
            {
                SpawnPool.PoolSpawn(RandomSpawnPosition,Quaternion.identity);
            }
        }
    }
}
