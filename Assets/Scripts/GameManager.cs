using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject panel;
    public TMP_Text arrowCounter;
    public InputController inputController;
    public int totalEnemyCount = 3;
    public void YouWin()
    {
        StartCoroutine(WinPanel());
    }

    IEnumerator WinPanel()
    {
        yield return new WaitForSeconds(2f);
        inputController.enabled = false;
        panel.transform.GetChild(0).GetComponent<TMP_Text>().text = "you win";
        panel.SetActive(true);

    }

    IEnumerator LosePanel()
    {
        yield return new WaitForSeconds(2f);
        inputController.enabled = false;
        panel.transform.GetChild(0).GetComponent<TMP_Text>().text = "you lose";
        panel.SetActive(true);

    }
    public void YouLose()
    {
        StartCoroutine(LosePanel());
    }

    public void SetArrowText(int arrowCount)
    {
        arrowCounter.text = arrowCount.ToString();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
