using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    [SerializeField]
    private float _detonationTime = 10f;

    [SerializeField]
    private GameObject _explosionEffect;

    private void Start()
    {
        Invoke("Explode", _detonationTime);
    }

    public void Explode()
    {
        Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Tank hitTank = collision.gameObject.GetComponent<Tank>();
        if (hitTank != null)
        {
            hitTank.Destroy();
            Explode();
        }
    }
}
