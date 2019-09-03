using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float _bulletSpeed = 0.0f;

    [SerializeField]
    private int _ricochets = 0;

    [SerializeField]
    private GameObject _richochetParticle;

    [SerializeField]
    private GameObject _explosionParticle;

    [SerializeField]
    private LayerMask _wallMask;

    [SerializeField]
    private LayerMask _bulletMask;

    [SerializeField]
    private LayerMask _tankMask;

    [SerializeField]
    private LayerMask _mineMask;

    private Tank _shooter;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _bulletSpeed);

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime *_bulletSpeed + .1f))
        {
            if (Physics.Raycast(ray, out hit, Time.deltaTime * _bulletSpeed + .1f, _wallMask))
            {
                Bounce(ray, hit);
            }
            else 
            if(Physics.Raycast(ray, out hit, Time.deltaTime * _bulletSpeed + .1f, _bulletMask))
            {
                Bullet hitBullet = hit.collider.gameObject.GetComponent<Bullet>();
                if (hitBullet != null)
                {
                    hitBullet.Explode();
                }
                Explode();
            }else 
            if (Physics.Raycast(ray, out hit, Time.deltaTime * _bulletSpeed + .1f, _tankMask))
            {
                hit.collider.gameObject.GetComponent<Tank>().Destroy();
                Explode();
            }
            else
            if (Physics.Raycast(ray, out hit, Time.deltaTime * _bulletSpeed + .1f, _mineMask))
            {
                hit.collider.gameObject.GetComponent<Mine>().Explode();
                Explode();
            }
        }
    }

    public void Explode()
    {
        _shooter.BulletsAlive--;
        Instantiate(_explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void GetShooter(Tank tank)
    {
        _shooter = tank;
    }

    private void Bounce(Ray ray, RaycastHit hit)
    {
        if (_ricochets == 0)
        {
            Explode();
            return;
        }

        Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
        float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, rot, 0);

        Instantiate(_richochetParticle, hit.point, Quaternion.LookRotation(hit.normal));

        _ricochets--;
    }
}
