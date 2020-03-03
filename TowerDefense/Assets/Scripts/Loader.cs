using UnityEngine;

/// <summary>
/// Class for instantiation the instance of T type inside of T class 
/// </summary>
/// <typeparam name="T">Type of the inherited from the Loader class</typeparam>
public class Loader<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<T>();

            return instance;
        }
    }
}
