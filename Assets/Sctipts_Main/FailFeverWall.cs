using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailFeverWall : MonoBehaviour {

    public Explodable _explodable;
    [SerializeField] private WallBreak wallBreak;

    private void OnTriggerEnter2D(Collider2D col2d)
    {
        if (PlayerController.isFeverTouch && col2d.tag == "Player")
        {
            wallBreak.isDestroy = true;
            _explodable.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(transform.position);
        }
    }


}
