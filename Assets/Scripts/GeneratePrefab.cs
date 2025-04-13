using UnityEngine;
using System.Collections.Generic;

public class GeneratePrefab : MonoBehaviour
{

    public GameObject GeneratedPrefab;

    [System.Serializable]
    public class PrefabData
    {
        public string name;
        public List<PrefabObject> obj;
    }
    
    [System.Serializable]
    public class PrefabObject
    {
        [SerializeField]
        public string prefab_name; // Updated to match JSON property
        public Vector3Wrapper scale;
        public Vector3Wrapper position;
    }

    [System.Serializable]
    public class Vector3Wrapper
    {
        public float x;
        public float y;
        public float z;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GeneratedPrefab = new GameObject();
        string json= @"
{
  ""name"": ""Gold_Arena"",
  ""obj"": [
    {
      ""prefab_name"": ""Gold_Cube"",
      ""scale"": {
        ""x"": 0.5,
        ""y"": 0.5,
        ""z"": 0.5
      },
      ""position"": {
        ""x"": 0,
        ""y"": 0,
        ""z"": 0
      }
    },
    {
      ""prefab_name"": ""Gold_Cube"",
      ""scale"": {
        ""x"": 0.5,
        ""y"": 0.5,
        ""z"": 0.5
      },
      ""position"": {
        ""x"": 5,
        ""y"": 0,
        ""z"": 0
      }
    },
    {
      ""prefab_name"": ""Gold_Cube"",
      ""scale"": {
        ""x"": 0.5,
        ""y"": 0.5,
        ""z"": 0.5
      },
      ""position"": {
        ""x"": 0,
        ""y"": 0,
        ""z"": 5
      }
    },
    {
      ""prefab_name"": ""Gold_Cube"",
      ""scale"": {
        ""x"": 0.5,
        ""y"": 0.5,
        ""z"": 0.5
      },
      ""position"": {
        ""x"": 5,
        ""y"": 0,
        ""z"": 5
      }
    }
  ]
}
";

        CreatePrefab(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePrefab(string json)
    {
        PrefabData newPrefabData = JsonUtility.FromJson<PrefabData>(json);
        GeneratedPrefab.name = newPrefabData.name;

        if (newPrefabData.obj == null)
        {
            Debug.LogError("Поле 'obj' в JSON не распарсилось: newPrefabData.obj == null");
            return;
        }

        foreach (PrefabObject obj in newPrefabData.obj)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{obj.prefab_name}");
            if (prefab == null)
            {
                Debug.LogError($"Prefab not found: {obj.prefab_name}");
                continue;
            }

            GameObject instance = Instantiate(prefab, GeneratedPrefab.transform);
            instance.transform.localPosition = new Vector3(obj.position.x, obj.position.y, obj.position.z);
            instance.transform.localScale = new Vector3(obj.scale.x, obj.scale.y, obj.scale.z);
        }
    }
}
