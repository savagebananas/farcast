using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(MonoBehaviour instance, string sceneName)
    {
        instance.StartCoroutine(transition(sceneName));
    }

    public static IEnumerator transition(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
