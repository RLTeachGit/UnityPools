using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMarbleRealloc : PoolMarble
{

    PlayerCast mPlayerCast;

    PlayerCast PlayerCast {
        get {
            if(mPlayerCast==null)
            {
                mPlayerCast = FindObjectOfType<PlayerCast>(); //Lazy Getter
            }
            return mPlayerCast;
        }
    }

    public override bool CanReallocate {
        get {
            if (!PlayerCast.PlayerVisible) return true; //If player cant be seen reallocate
            return ((PlayerCast.PlayerPosition - transform.position).magnitude > PlayerCast.Radius); //If player is far away reallocate
        }
    }
}
