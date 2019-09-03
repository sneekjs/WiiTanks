using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBehaviour : MonoBehaviour {

    protected bool _shotOffCooldown = true;
    protected bool _lookingRightAtTarget = false;

    private Vector3 _target;
    private Camera _cam;

    protected virtual void Start()
    {
        _cam = FindObjectOfType<Camera>();
    }

    public abstract void Behave(Tank tank);

    protected virtual void Aim(Tank tank)
    {
        _target = GetTarget();

        Vector3 object_pos = _cam.WorldToScreenPoint(tank.Head.transform.position);
        _target.x = _target.x - object_pos.x;
        _target.y = _target.y - object_pos.y;
        float angle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0, -angle + 90f, 0));
        tank.Head.transform.rotation = Quaternion.Lerp(tank.Head.transform.rotation, lookRotation, tank.BarrelTurnSpeed * Time.deltaTime);

        _lookingRightAtTarget = (tank.Head.transform.rotation == lookRotation);
    }

    protected virtual void Shoot(Tank tank)
    {
        tank.BulletsAlive++;
        _shotOffCooldown = false;
        Invoke("RemoveCooldown", tank.GetFirerate());

        GameObject bulletGo = Instantiate(tank.Bullet, tank.BulletSpawn.transform.position, tank.BulletSpawn.transform.rotation);
        bulletGo.GetComponent<Bullet>().GetShooter(tank);
    }

    protected abstract void Move(Tank tank);

    protected virtual void PlaceMine(Tank tank)
    {
        if (tank.MineLimit > 0)
        {
            Instantiate(tank.Mine, transform.position, transform.rotation);
            tank.MineLimit--;
        }
    }

    protected abstract Vector3 GetTarget();
    
    protected virtual void RemoveCooldown()
    {
        _shotOffCooldown = true;
    }
}
