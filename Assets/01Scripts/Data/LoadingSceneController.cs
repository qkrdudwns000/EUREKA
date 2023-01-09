using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    private Image loadingBar;
    public static void LoadScend(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        // AsyncOperation 은 Async로 진행중인 씬작업의 진행도를 받아오는것.
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene); // 비동기방식. 씬 불러오는 작업 도중에 다른작업 병행이가능함.
        ao.allowSceneActivation = false; // 씬 작업이 끝나면 해당씬으로 바로이동할것인지. (false일경우에는 90%까지만 로드한상태로 기다리게됨.)

        float timer = 0f;
        while(!ao.isDone) // true 가될떄까지 반복문.
        {
            yield return null;

            if(ao.progress < 0.9f)
            {
                loadingBar.fillAmount = ao.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer/3);
                if(loadingBar.fillAmount >= 1f)
                {
                    ao.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
