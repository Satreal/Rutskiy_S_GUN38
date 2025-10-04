using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotate = new Vector3(0f,30f,0f);
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body == null)
        {
            Debug.Log($"Отсутствует Rigidbody на {gameObject.name}");
            yield break;
        }
        body.isKinematic = true;
        while (true)
        {
            Quaternion deltaAng = body.rotation * ( Quaternion.Euler(_rotate *  Time.deltaTime));
            body.MoveRotation(deltaAng);
            yield return null;
        }
        
    }

}
