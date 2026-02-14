using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coffeeCount = 0;
    public int coffeeGoal = 5;

    public bool lightOn = true;

    [SerializeField] private GameObject winUI;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoffee()
    {
        coffeeCount++;
        Debug.Log("Coffee: " + coffeeCount);

        if (coffeeCount >= coffeeGoal)
        {
            Debug.Log("YOU WIN");
            if (winUI != null)
                winUI.SetActive(true);
        }
    }
    
    [SerializeField] private Light mainLight;

    public void TurnLightOff()
    {
        lightOn = false;

        if (mainLight != null)
            mainLight.enabled = false;
    }

    public void TurnLightOn()
    {
        lightOn = true;

        if (mainLight != null)
            mainLight.enabled = true;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainManu");
    }
}