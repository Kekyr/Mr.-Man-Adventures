using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public TextMeshProUGUI numberOfCoins;

    public float amountOfCoins=0;

    // Подсчёт собранных монет

    private void Awake()
    {
        numberOfCoins = FindObjectOfType<TextMeshProUGUI>();
    }
    public void AddCoin()
    {
        amountOfCoins++;
        numberOfCoins.text = ((amountOfCoins / 100).ToString()).Replace(",", string.Empty);
        if(amountOfCoins%10==0)
        {
            numberOfCoins.text += "0";
        }
    }

    public void DeleteCoins(int amount)
    {
        amountOfCoins -= amount;
        numberOfCoins.text = ((amountOfCoins / 100).ToString()).Replace(",", string.Empty);
        if (amountOfCoins % 10 == 0)
        {
            numberOfCoins.text += "0";
        }

    }
}
