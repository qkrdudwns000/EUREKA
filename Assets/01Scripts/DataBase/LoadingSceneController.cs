using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    static bool isLoad;

    [SerializeField]
    private Image loadingBar;
    public static void LoadScend(string sceneName, bool _isLoad)
    {
        nextScene = sceneName;
        isLoad = _isLoad;
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isLoad)
            StartCoroutine(LoadSaveSceneProcess());
        else
            StartCoroutine(LoadSceneProcess());
    }

    public IEnumerator LoadSceneProcess()
    {
        Debug.Log("�����ӷε�");
        // AsyncOperation �� Async�� �������� ���۾��� ���൵�� �޾ƿ��°�.
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene); // �񵿱���. �� �ҷ����� �۾� ���߿� �ٸ��۾� �����̰�����.
        ao.allowSceneActivation = false; // �� �۾��� ������ �ش������ �ٷ��̵��Ұ�����. (false�ϰ�쿡�� 90%������ �ε��ѻ��·� ��ٸ��Ե�.)

        float timer = 0f;
        while(!ao.isDone) // true ���ɋ����� �ݺ���.
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
    IEnumerator LoadSaveSceneProcess()
    {
        Debug.Log("���̺�ε�");
        // AsyncOperation �� Async�� �������� ���۾��� ���൵�� �޾ƿ��°�.
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene); // �񵿱���. �� �ҷ����� �۾� ���߿� �ٸ��۾� �����̰�����.
        ao.allowSceneActivation = false; // �� �۾��� ������ �ش������ �ٷ��̵��Ұ�����. (false�ϰ�쿡�� 90%������ �ε��ѻ��·� ��ٸ��Ե�.)

        float timer = 0f;
        while (!ao.isDone) // true ���ɋ����� �ݺ���.
        {
            yield return null;

            if (ao.progress < 0.9f)
            {
                loadingBar.fillAmount = ao.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer / 3);
                if (loadingBar.fillAmount >= 1f)
                {
                    ao.allowSceneActivation = true;
                    SaveNLoad saveNLoad = FindObjectOfType<SaveNLoad>();
                    saveNLoad.LoadDatasCo();
                }
            }
        }
        
    }
}
