using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    [SerializeField] private float _velocity = 10;
    [SerializeField] private float _torque = 5;
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Fire();
    }

    void Fire()
    {
        var distance = Mathf.Infinity;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Car"))
        {
            if (!go.GetComponent<PhotonView>().isMine)
            {
                var diff = (go.transform.position - transform.position).sqrMagnitude;
                if (diff < distance)
                {
                    distance = diff;
                    _target = go.transform;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (_target == null || _rigidbody == null)
            return;

        //missiles forward momentum
        _rigidbody.velocity = transform.forward * _velocity;


        var targetRotation = Quaternion.LookRotation(_target.position - transform.position);

        _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, _torque));
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Car") && !col.gameObject.GetComponent<PhotonView>().isMine)
        {
            Destroy(gameObject);
        }
    }
}