using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HPtext;
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] TextMeshProUGUI DiamondText;

    [SerializeField] GameObject Player;

    [SerializeField] GameObject Diamond;

    int coinsScore = 20;

    Destroyable playerHP;
    Destroyable diamondHP;

    private void Awake()
    {
        playerHP = Player.GetComponent<Destroyable>();
        diamondHP = Diamond.GetComponent<Destroyable>();

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        CoinText.text = "COINS: " + coinsScore.ToString();
        HPtext.text = "HP: " + playerHP.GetHitPoints();
        DiamondText.text = "DP: " + diamondHP.GetHitPoints();
    }

    public void OnCoinCollected()
    {
        coinsScore += 1;
    }

    public string OnGameOverScore()
    {
        return coinsScore.ToString();
    }

    public bool SpendCoins(int amount)
    {
        if (amount > coinsScore)
        {
            return false;
        }
        coinsScore -= amount;
        return true;
    }
}
