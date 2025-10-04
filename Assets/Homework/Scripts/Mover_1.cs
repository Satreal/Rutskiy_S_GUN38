using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Mover_1 : MonoBehaviour
{
    [SerializeField]
    private  Vector3 _start = new Vector3(-10, 0, 0);
    [SerializeField]
    private Vector3 _end = new Vector3(10, 0, 0);
    [SerializeField]
    private float _speed=25f;
    [SerializeField]
    private float _delay=0.3f;
    private Rigidbody _rigidbody;
    private Vector3 position;

    private IEnumerator Start()
    {
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody is null");
            yield break;
        }
        
        Vector3 worldStart = position +transform.TransformDirection(_start);
        Vector3 worldEnd =position+ transform.TransformDirection(_end);
        _rigidbody.position = worldStart;
        var finish = worldEnd;
       
        while (true)
        {
            var step = _speed * Time.deltaTime;
            Vector3 target =Vector3.MoveTowards(_rigidbody.position, finish, step);
            _rigidbody.MovePosition(target);
            if (_rigidbody.position==finish)
            {
                yield return new WaitForSeconds(_delay);
                finish = finish == worldEnd ? worldStart : worldEnd;
            }
            yield return null;
        }


    }

  
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        position = transform.position;

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            return;
        }

        Vector3 baseposition = transform.position; 
        Gizmos.color = Color.yellow;
        Vector3 worldStart = baseposition + transform.TransformDirection(_start);
        Vector3 worldEnd = baseposition + transform.TransformDirection(_end);
        Gizmos.DrawLine(  worldStart, worldEnd);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(worldStart, 0.4f);
        Gizmos.DrawSphere(worldEnd, 0.4f);


    }
}

