using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour {

    [SerializeField]
    private float _destroyTime = 0.45f;

    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            collision.gameObject.GetComponent<Tank>().Destroy();
        }
        else if(collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<Bullet>().Explode();
        }
        else if(collision.gameObject.CompareTag("Destructable"))
        {
            Destroy(collision.gameObject);
        }
    }
}
