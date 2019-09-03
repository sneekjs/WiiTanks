using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : TankBehaviour {

    protected EnemyStates _currentState;

    protected override void Start()
    {
        base.Start();
    }

    public abstract override void Behave(Tank tank);

    protected  override void Aim(Tank tank)
    {
        base.Aim(tank);
    }

    protected abstract override void Move(Tank tank);

    protected override void PlaceMine(Tank tank)
    {
        base.PlaceMine(tank);
    }

    protected override void Shoot(Tank tank)
    {
        base.Shoot(tank);
    }

    protected abstract override Vector3 GetTarget();

    protected override void RemoveCooldown()
    {
        base.RemoveCooldown();
    }
}
