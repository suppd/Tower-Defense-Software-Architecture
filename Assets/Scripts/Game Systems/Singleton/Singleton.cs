using UnityEngine;
/// <summary>
/// SuperClass to implement singleton pattern into classes that need it
/// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        // Singleton instance.
        private static T instance;
        // Singleton instance getter.
        // If there is no instance, try find one in the scene.
        // If still there no instance found, then it creates a new one.
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject { name = typeof(T).Name };
                        instance = obj.AddComponent<T>();
                        //DontDestroyOnLoad(obj);
                    }
                }
                return instance;
            }
        }
        // If no singleton instance is found then assign this one as the only instance
        // else destroy it.
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                //DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
