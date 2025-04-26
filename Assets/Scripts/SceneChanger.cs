using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string NextSceneName;
    public void OnChangeScene()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}