using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    
    public static SceneController instance;
    [SerializeField] Animator tansitionAnim;
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


    IEnumerator LoadLevel()
    {
        tansitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if another scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes. Loading Main Menu.");

            SceneManager.LoadScene("Main Menu");
        }

        tansitionAnim.SetTrigger("Start");

    }
}
