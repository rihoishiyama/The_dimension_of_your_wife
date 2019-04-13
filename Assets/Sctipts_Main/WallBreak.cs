using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WallBreak : MonoBehaviour {

	public Explodable _explodable;
    public bool isDestroy = false;

    private void OnTriggerEnter2D(Collider2D col2d)
    {
        if(col2d.tag == "Player")
        {
            isDestroy = true;
            _explodable.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(transform.position);
        }
    }


}
