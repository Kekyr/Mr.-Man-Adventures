using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public TextMeshProUGUI numberOfCoins;

    [SerializeField] private float amountOfCoins=0;

    // Подсчёт собранных монет
    public void AddCoin()
    {
        amountOfCoins++;
        numberOfCoins.text = ((amountOfCoins / 100).ToString()).Replace(",", string.Empty);
    }
}
