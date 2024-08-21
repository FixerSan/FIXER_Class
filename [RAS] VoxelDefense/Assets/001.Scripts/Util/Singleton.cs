using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find($"@{typeof(T).Name}");
                if (go == null)
                {
                    go = new GameObject($"@{typeof(T).Name}");
                    instance = go.AddComponent<T>();
                }

                else instance = go.GetComponent<T>();

                GameObject system = GameObject.Find("@System");
                if (system == null)
                {
                    system = new GameObject("@System");
                    DontDestroyOnLoad(go);
                }
                go.transform.SetParent(system.transform);
            }
            return instance;
        }
    }
}