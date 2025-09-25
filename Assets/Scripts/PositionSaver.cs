using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace DefaultNamespace
{
    public class PositionSaver : MonoBehaviour
    {
        [Serializable]
        public struct Data
        {
            public Vector3 Position;
            public float Time;
        }

        [ReadOnly, SerializeField, Tooltip("для заполнения этого поля нужно воспользоваться контекстным меню в инспекторе и командой “Create File”")]
        private TextAsset _json;

        [SerializeField, HideInInspector]
        private List<Data> _records = new List<Data>();
        public List<Data> Records => _records;

        private void Awake()
        {
            //todo comment: Что будет, если в теле этого условия не сделать выход из метода?
            //предотвращает выполнение некорректного кода, т.е. убрав return - при значении null код продолжит выполняться и  выкинет ошибку NullReferenceException: Object reference not set to an instance of an object 
            if (_json == null)
            {
                gameObject.SetActive(false);
                Debug.LogError("Please, create TextAsset and add in field _json");
                return;
            }

            JsonUtility.FromJsonOverwrite(_json.text, this);
            //todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
            //позволяет избежать работу с nullевыми значениями и соответственно, предотвращает NullReferenceException. 
            if (_records == null)
            { _records = new List<Data>(10); }
        }

        private void OnDrawGizmos()
        {
            //todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
            // также позволяет избежать ошибки, связанные с отсутствием значения, т.е. гарантирует наличие хотя бы одного элемента
            if (_records == null || _records.Count == 0) return;
            var data = _records;
            var prev = data[0].Position;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(prev, 0.3f);
            //todo comment: Почему итерация начинается не с нулевого элемента?
            //нулевой элемент отработан в коде выше - var prev = data[0].Position
            for (int i = 1; i < data.Count; i++)
            {
                var curr = data[i].Position;
                Gizmos.DrawWireSphere(curr, 0.3f);
                Gizmos.DrawLine(prev, curr);
                prev = curr;
            }
        }

#if UNITY_EDITOR
		[ContextMenu("Create File")]
		private void CreateFile()
		{
			//todo comment: Что происходит в этой строке?
			//Создается новый файл
			var stream = File.Create(Path.Combine(Application.dataPath, "Path.txt"));
			//todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав) 
			// позволяет  пользоваться файлом  (смотреть, изменять)
			stream.Dispose();
			UnityEditor.AssetDatabase.Refresh();
			//В Unity можно искать объекты по их типу, для этого используется префикс "t:"
			//После нахождения, Юнити возвращает массив гуидов (которые в мета-файлах задаются, например)
			var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
			foreach (var guid in guids)
			{
				//Этой командой можно получить путь к ассету через его гуид
				var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
				//Этой командой можно загрузить сам ассет
				var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
				//todo comment: Для чего нужны эти проверки?
				// убеждает нас, что мы работаем именно с нашим нужным файлом, который не имеет nullзначение - т.е. успешно загружен
				if(asset != null && asset.name == "Path")
				{
					_json = asset;
					UnityEditor.EditorUtility.SetDirty(this);
					UnityEditor.AssetDatabase.SaveAssets();
					UnityEditor.AssetDatabase.Refresh();
					//todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
					//потому что мы нашли нужный нам файл, провели все необходимые с ним манипуляции, и дальнейший "прогон" по файлам не нужен
					return;
				}
			}
		}

		private void OnDestroy()
		{
			if(_json==null)
			{
			    return;
			}
            var serializedRecords = JsonUtility.ToJson(this,true);
            var path=UnityEditor.AssetDatabase.GetAssetPath(_json);
           
            path = Path.Combine(Application.dataPath.Replace("Assets",""),path);
            File.WriteAllText(path, serializedRecords);

            UnityEditor.EditorUtility.SetDirty(_json);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
           

           
            
		}
#endif
    }
}