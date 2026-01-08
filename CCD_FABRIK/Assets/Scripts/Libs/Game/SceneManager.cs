namespace Game
{
    public class SceneManager : UnityEngine.MonoBehaviour
    {
        public static string currentScene;

        private void Awake()
        {
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            DontDestroyOnLoad(gameObject);
        }

        public static void LoadScene(string sceneName)
        {
            currentScene = sceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        public static void ReloadScene() => LoadScene(currentScene);
    }
}