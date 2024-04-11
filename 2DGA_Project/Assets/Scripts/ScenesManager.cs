using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is an example of a SceneManager
/// </summary>
/// 

public class ScenesManager : MonoBehaviour
{
    // creating a singleton instance that can be used in any script without instantiating it first
    public static ScenesManager Instance;

    [SerializeField]
    private Image progressBar;

    private float target;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;

        progressBar.fillAmount = 0;
    }

    // creating an enum for all Scenes that we want to use in our build
    // to access the scenes that are currently in the build go to "File > Build Settings > Scenes in Build"
    // the names in the enum have to match the names of your scenes
    public enum Scene { 
        UIScene,
        SpineSkinChange
    }

    public void LoadMenu() {
        // we use Unity's SceneManager class to load a scene by name or build index (can be found in build settings menu)
        SceneManager.LoadScene((int)Scene.UIScene);
        //SceneManager.LoadScene(Scene.UIScene.ToString());
    }

    public void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // unity can load scenes asynchronously
    // this allows us to load large scenes in the background while we show a loading screen or continue the game
    // Needs to be marked as "async" !!!
    public async void LoadSceneAsync() {
        target = 0;
        progressBar.fillAmount = 0;

        // starting the async download
        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        // disabling the automatic scene switch that would happen if the LoadSceneAsync func would finish
        scene.allowSceneActivation = false;

        do
        {
            // create an artificial small delay between incrementing the progress bar
            await Task.Delay(100); // DO NOT ADD THIS IN YOUR NORMAL CODE!!!!
            target = scene.progress;

            // we do this as long as the loading percentage of LoadSceneAsync is below 90%
        } while (scene.progress < 0.9f);

        // create an artificial delay before switching the scene
        await Task.Delay(1000); // DO NOT ADD THIS IN YOUR NORMAL CODE!!!!

        // enable unity to switch the scene after it finished loading
        scene.allowSceneActivation = true;
    }

    private void Update()
    {
        // fill the bar smoothly through the MoveTowards
        // interpolates between the current fillAmount and the newly calulcated loading percentage in the LoadSceneAsync func
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, Time.deltaTime * 3);
    }
}
