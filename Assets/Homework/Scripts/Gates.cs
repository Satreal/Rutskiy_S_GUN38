using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private int _score; //счетчик голов
    [SerializeField, Range(1f,10f)]
    private int _maxScore;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Ball>(out var ball))
        {
            return;
        }
        else
        {
            _score++;
            Debug.Log($" Гол! счет теперь {_score}");
           
            Destroy(ball.gameObject);//уничтожаем забитый мяч
        }
        if(_score>=_maxScore)
        {
            Debug.Log("Поздравляю, игра окончена");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = true;
#endif

        }
    }







    // Start is called before the first frame update
    void Start()
    {
        _score = 0; //обнуляем счетчик голов

    }

    
}
