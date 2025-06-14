using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } } // 🛠️ Ahora es accesible

    [SerializeField] TextMeshProUGUI displayBalance;

    private void Awake()
    {
        LoadMoney(); 
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        SaveMoney();
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        SaveMoney();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Money: " + currentBalance;
    }

    void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", currentBalance);
        PlayerPrefs.Save();
    }

    void LoadMoney()
    {
        currentBalance = PlayerPrefs.GetInt("Money", 20);
    }
}
