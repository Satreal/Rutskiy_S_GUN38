using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Mover_1 : MonoBehaviour
{
    [SerializeField]
    private Vector3 _start = new Vector3(-10, 0, 0);
    [SerializeField]
    private Vector3 _end = new Vector3(10, 0, 0);
    [SerializeField]
    private float _speed=1f;
    [SerializeField]
    private float _delay=1f;
    private Rigidbody _rigidbody;

    
    private IEnumerator Start()
    {
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody is null");
            yield break;
        }
        _rigidbody.position = _start;
        var finish = _end;
       
        while (true)
        {
            var step = _speed * Time.deltaTime;
            Vector3 target =Vector3.MoveTowards(_rigidbody.position, finish, step);
            _rigidbody.MovePosition(target);
            if (_rigidbody.position==finish)
            {
                yield return new WaitForSeconds(_delay);
                finish = finish == _end ? _start : _end;
            }
            yield return null;
        }


    }

  
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine( _start, _end);
        
    }
}

