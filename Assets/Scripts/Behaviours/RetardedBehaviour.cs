using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetardedBehaviour : EnemyBehaviour {

    private GameObject _player;

    protected override void Start()
    {
        base.Start();
        _currentState = EnemyStates.WaitingForInput;
        _player = FindObjectOfType<Player>().gameObject;
    }

    public override void Behave(Tank tank)
    {
        switch (_currentState)
        {
            case EnemyStates.Idle:
                Aim(tank);
                if (_lookingRightAtTarget)
                {
                    //new target
                }
                break;
            case EnemyStates.WaitingForInput:
                StartCoroutine(WaitIdle());
                break;
            case EnemyStates.Moving:
                break;
            case EnemyStates.Aiming:
                Aim(tank);
                if (_lookingRightAtTarget)
                {
                    Shoot(tank);
                }
                break;
            default:
                break;
        }
    }

    protected override void Aim(Tank tank)
    {
        base.Aim(tank);
    }

    protected override void Move(Tank tank)
    {
        throw new System.NotImplementedException(); //retarded tanks don't move
    }

    protected override void PlaceMine(Tank tank)
    {
        base.PlaceMine(tank);
    }

    protected override void Shoot(Tank tank)
    {
        base.Shoot(tank);
        _currentState = EnemyStates.Idle;
    }

    protected override Vector3 GetTarget()
    {
        Vector3 target = _player.transform.position;

        switch (_currentState)
        {
            case EnemyStates.Idle:
                target = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                break;
            case EnemyStates.Aiming:
                target = _player.transform.position;
                break;
            default:
                break;
        }
        return target;
    }

    private IEnumerator WaitIdle()
    {
        _currentState = EnemyStates.Idle;
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        _currentState = EnemyStates.Aiming;
    }

    protected override void RemoveCooldown()
    {
        base.RemoveCooldown();
    }
}
