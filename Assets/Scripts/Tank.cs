using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    [SerializeField]
    private float _speed = 0.0f;

    [SerializeField]
    private float _turnSpeed = 0.0f;

    [SerializeField]
    private float _barrelTurnSpeed = 1f;

    [SerializeField]
    private TankBehaviour _behaviour;

    [SerializeField]
    private GameObject _head;

    [SerializeField]
    private GameObject _body;

    [SerializeField]
    private GameObject _bulletSpawn;

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private GameObject _mine;

    [SerializeField]
    private float _roundsPerMinute = 0.0f;

    [SerializeField]
    private int _bulletLimit = 1;

    [SerializeField]
    private int _mineLimit = 0;

    private int _bulletsAlive = 0;

    public GameObject Head
    {
        get{return _head;}
        set{_head = value;}
    }

    public GameObject Bullet
    {
        get { return _bullet; }
        set { _bullet = value; }
    }

    public GameObject BulletSpawn
    {
        get { return _bulletSpawn; } 
        set { _bulletSpawn = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public GameObject Body
    {
        get { return _body; }
        set { _body = value; }
    }

    public float TurnSpeed
    {
        get { return _turnSpeed; }
        set { _turnSpeed = value; }
    }

    public GameObject Mine
    {
        get { return _mine; }
        set { _mine = value; }
    }

    public int MineLimit
    {
        get { return _mineLimit; }
        set { _mineLimit = value; }
    }

    public float RoundsPerMinute
    {
        get { return _roundsPerMinute; }
        set { _roundsPerMinute = value; }
    }

    public int BulletLimit
    {
        get { return _bulletLimit; }
        set { _bulletLimit = value; }
    }

    public int BulletsAlive
    {
        get { return _bulletsAlive; }
        set { _bulletsAlive = value; }
    }

    public float BarrelTurnSpeed
    {
        get { return _barrelTurnSpeed; }
        set { _barrelTurnSpeed = value; }
    }

    private void Update()
    {
        _behaviour.Behave(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        //TODO: maak dit een fatsoenlijke destroy, miss via een destroyBehaviour
    }

    public float GetFirerate()
    {
        return 60f / RoundsPerMinute;
    }
}
