using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TankBehaviour {

    private float _xDir = 0;
    private float _zDir = 0;

    protected override void Start()
    {
        base.Start();
    }

    public override void Behave(Tank tank)
    {
        _xDir = Input.GetAxisRaw("Horizontal");
        _zDir = Input.GetAxisRaw("Vertical");

        Aim(tank);

        if (_xDir != 0 || _zDir !=0)
        {
            Move(tank);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceMine(tank);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_shotOffCooldown && tank.BulletsAlive < tank.BulletLimit)
            {
                Shoot(tank);
            }
        }
    }

    protected override void Aim(Tank tank)
    {
        base.Aim(tank);
    }

    protected override void Shoot(Tank tank)
    {
        base.Shoot(tank);
    }

    protected override void Move(Tank tank)
    {
        transform.Translate(tank.Body.transform.forward * Time.deltaTime * tank.Speed);

        RotateBody(tank);
    }

    protected override void PlaceMine(Tank tank)
    {
        base.PlaceMine(tank);
    }

    protected override Vector3 GetTarget()
    {
        return Input.mousePosition;
    }

    private void RotateBody(Tank tank)
    {
        Vector3 moveDir = Quaternion.LookRotation(new Vector3(_xDir, 0, _zDir), Vector3.up).eulerAngles;
        Vector3 bodyDir = tank.Body.transform.rotation.eulerAngles;

        Vector3 newDir = Vector3.RotateTowards(bodyDir, moveDir - tank.Body.transform.position, tank.TurnSpeed * Time.deltaTime, tank.TurnSpeed);
        tank.Body.transform.rotation = Quaternion.Euler(0, newDir.y, 0);
        //Quaternion target = tank.Body.transform.rotation * Quaternion.AngleAxis(tank.TurnSpeed, moveDir);
        //tank.Body.transform.rotation = Quaternion.Lerp(tank.Body.transform.rotation, target, 1);

        //TODO: fix de random 360 draai die de tank doet als die van 270 naar 0 draait
    }

    protected override void RemoveCooldown()
    {
        base.RemoveCooldown();
    }
}
