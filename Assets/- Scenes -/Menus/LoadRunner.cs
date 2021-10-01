using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadRunner : MonoBehaviour
{

    public Button testSceneButton;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {

        Button button = testSceneButton.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        LoadingScenes.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(sceneToLoad);
    }
}
