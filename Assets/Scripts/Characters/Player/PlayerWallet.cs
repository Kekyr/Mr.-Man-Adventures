using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public TextMeshProUGUI numberOfCoins;

    public float amountOfCoins=0;
    private string countOfCoins;
    // Подсчёт собранных монет

    private void Awake()
    {
        numberOfCoins = GameObject.FindGameObjectWithTag("NumberOfCoins").GetComponent<TextMeshProUGUI>();
    }
    public void AddCoin()
    {
        amountOfCoins++;
        countOfCoins= (amountOfCoins / 100).ToString();

        if (amountOfCoins % 10 == 0)
        {
            countOfCoins = countOfCoins[0].ToString() + countOfCoins[2].ToString()+"0";
        }
        else
        {
            countOfCoins = countOfCoins[0].ToString() + countOfCoins[2].ToString() + countOfCoins[3].ToString();
        }
        numberOfCoins.text = countOfCoins;
        
    }

    public void DeleteCoins(int amount)
    {
        amountOfCoins -= amount;
        countOfCoins= (amountOfCoins / 100).ToString();
        
        if (amountOfCoins % 10 == 0)
        {
            countOfCoins = countOfCoins[0].ToString() + countOfCoins[2].ToString() + "0";
        }
        else
        {
            countOfCoins = countOfCoins[0].ToString() + countOfCoins[2].ToString() + countOfCoins[3].ToString();
        }
        numberOfCoins.text = countOfCoins;

    }
}
