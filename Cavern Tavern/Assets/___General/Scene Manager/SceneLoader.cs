using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private static GameObject blackScreen;

    private void Start()
    {
        blackScreen = GameObject.Find("Black Screen");
    }

    public static void LoadScene(MonoBehaviour instance, string sceneName)
    {
        instance.StartCoroutine(transition(sceneName));
        blackScreen.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public static IEnumerator transition(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        blackScreen.GetComponent<Animator>().SetTrigger("FadeIn");
    }
}
