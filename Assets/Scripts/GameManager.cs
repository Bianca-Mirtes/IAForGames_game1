using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startGame= false;
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        GameObject finalCanva = FindFirstObjectByType<PlayerController>().finalCanva;
        if (enemies.Length == 0 && startGame)
        {
            finalCanva.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.green;
            finalCanva.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Você Venceu!!!";
            finalCanva.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
