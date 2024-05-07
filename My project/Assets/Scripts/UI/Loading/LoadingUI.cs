using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
     static string nextScene;            // 다음 씬으로 넘어갈 씬이름. static으로 선언한 이유는 로딩창은 정말 여러곳에서 쓰이기 때문에.

    [SerializeField] private Image progressBar;

    private void Start()
    {
        // 로딩 프로세스를 진행해서 해당 프로세스가 완료되면 다음 씬으로 전환한다. 
        StartCoroutine(LoadSceneProcess());
    }

  public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;

        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(0.3f); // 이유

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;                 // 씬이 끝날때 자동으로 다음 씬으로 이동할 것인가? true이면 자동이동 false면 이동을 안함
                                                                // 로딩중에 최소한의 대기 시간을 부여함. (주로 멀티게임 때문)

        float timer = 0f;

        while(!operation.isDone)
        {
            yield return null;      // 프레임마다 아래 내용을 반영
            
            if(operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;                                    // Time.Scale로 게임속 시간을 변경시킬수 있다. (예를들면 크리티컬이나 중요한 순간에 게임이 느려지는 효과등)
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);               // 로딩바 90%에서 100%사이의 값을 선형보간으로 표시. (그래서 로딩창 거의 마지막엔 느리게 차는거였나?)
                if (progressBar.fillAmount >=1f)
                {
                    yield return new WaitForSeconds(1f);
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }

    }

}
