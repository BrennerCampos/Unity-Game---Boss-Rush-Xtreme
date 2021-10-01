using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string levelToLoad;
    public GameObject loadingScreen;
    
    
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(LoadLevelAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator LoadLevelAsync()
    {
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);
        // gameObject.SetActive(false);
    }

}
