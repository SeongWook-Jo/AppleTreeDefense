using UnityEngine.SceneManagement;

public class GameManager
{
    public static void ChangeScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());
    }
}
