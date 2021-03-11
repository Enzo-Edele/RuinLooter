using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void Update()
    {
        LaunchProjectile();    
    }

    public virtual void LaunchProjectile(){
    }
}

