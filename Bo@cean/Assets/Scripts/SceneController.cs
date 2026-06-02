using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    
    public static SceneController instance;
    [SerializeField] Animator tansitionAnim;
    [SerializeField] GameObject congratulationPanel;
    private bool showCongratulation = false;
    private GUIStyle congratulationStyle;
    private GUIStyle congratulationShadowStyle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        Debug.Log("Next Level!");

        StartCoroutine(LoadLevel());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    void UnlockLevel()
    {
        if (SceneManager.GetActiveScene().name == "L4")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 4);
            PlayerPrefs.Save();
        }
        else if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }


    IEnumerator LoadLevel()
    {
        tansitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        UnlockLevel();

        if (SceneManager.GetActiveScene().name == "L4")
        {
            showCongratulation = true;
            
            Time.timeScale = 0;
            
            yield return new WaitForSecondsRealtime(3); 
            
            showCongratulation = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadSceneAsync(nextSceneIndex);
            }
            else
            {
                Debug.Log("No more scenes. Loading Main Menu.");

                SceneManager.LoadScene("Main Menu");
            }
        }

        tansitionAnim.SetTrigger("Start");

    }

    private void OnGUI()
    {
        if (showCongratulation)
        {
            if (congratulationStyle == null)
            {
                congratulationStyle = new GUIStyle();
                congratulationStyle.fontSize = 50;
                congratulationStyle.fontStyle = FontStyle.Bold;
                congratulationStyle.normal.textColor = Color.yellow;
                congratulationStyle.alignment = TextAnchor.MiddleCenter;

                congratulationShadowStyle = new GUIStyle(congratulationStyle);
                congratulationShadowStyle.normal.textColor = Color.black;
            }

            string text = "All Levels Completed! Congratulations!";
            float width = Screen.width;
            float height = 100f;
            float x = 0;
            float y = (Screen.height - height) * 0.5f;

            GUI.Label(new Rect(x + 2, y + 2, width, height), text, congratulationShadowStyle);
            GUI.Label(new Rect(x, y, width, height), text, congratulationStyle);
        }
    }
}
