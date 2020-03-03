using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// For restart buttons in case of victory or defeat
/// </summary>
public class Restart : MonoBehaviour
{   
    public void LoadMainScene()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
