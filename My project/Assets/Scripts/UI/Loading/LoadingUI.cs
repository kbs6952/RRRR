using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
     static string nextScene;            // ���� ������ �Ѿ ���̸�. static���� ������ ������ �ε�â�� ���� ���������� ���̱� ������.

    [SerializeField] private Image progressBar;

    private void Start()
    {
        // �ε� ���μ����� �����ؼ� �ش� ���μ����� �Ϸ�Ǹ� ���� ������ ��ȯ�Ѵ�. 
        StartCoroutine(LoadSceneProcess());
    }

  public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;

        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(0.3f); // ����

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;                 // ���� ������ �ڵ����� ���� ������ �̵��� ���ΰ�? true�̸� �ڵ��̵� false�� �̵��� ����
                                                                // �ε��߿� �ּ����� ��� �ð��� �ο���. (�ַ� ��Ƽ���� ����)

        float timer = 0f;

        while(!operation.isDone)
        {
            yield return null;      // �����Ӹ��� �Ʒ� ������ �ݿ�
            
            if(operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;                                    // Time.Scale�� ���Ӽ� �ð��� �����ų�� �ִ�. (������� ũ��Ƽ���̳� �߿��� ������ ������ �������� ȿ����)
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);               // �ε��� 90%���� 100%������ ���� ������������ ǥ��. (�׷��� �ε�â ���� �������� ������ ���°ſ���?)
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
