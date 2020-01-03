using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Schiettent
{
    public class UpgradeButton : MonoBehaviour
    {
        public HandleGame Game;
        public HandlePlayer Player;
        public Image ImageObj;
        public Text TextObj;
        public Text HeaderText;
        public Button BuyButton;
        public Text ShopText;
        public string UpgradeName;
        private int UpgradeCost = 1;
        private int TimesBought = 0;
        private int UpgradeType = 0;
        private int GameCoins = 0;
        private bool Purchasable = true;
        private Color SoldOut = new Color(246, 200, 117, 255);

        private string UpgradeImprovement;
        private string CurrentStat;
        private float NewFloat;
        private int NewInt;

        // Use this for initialization
        void Start()
        {
            if (Player == null) { Debug.LogError("Missing reference: Player"); }
            if (UpgradeName == null) { Debug.LogError("Missing reference: UpgradeName"); }
            if (ImageObj == null) { Debug.LogError("Missing reference: ImageObj"); }
            if (TextObj == null) { Debug.LogError("Missing reference: TextObj"); }
            if (BuyButton == null) { Debug.LogError("Missing reference: BuyButton"); }
            if (ShopText == null) { Debug.LogError("Missing reference: ShopText"); }
            //ImageObj = gameObject.GetComponent<Image>();
            //TextObj = gameObject.transform.GetChild(0).GetComponent<Text>();
            //HeaderText = gameObject.transform.parent.GetComponentInChildren<Text>();
            //BuyButton = gameObject.GetComponent<Button>();
            SetFirstUpgrade();
        }

        void SetFirstUpgrade()
        {
            switch (UpgradeName)
            {
                case "Reload Faster":
                case "Shoot Faster":
                    UpgradeType = 1;
                    UpgradeCost = 45;
                    CurrentStat = "1.00s";
                    UpgradeImprovement = "-0.25";
                    NewFloat = 0.75f;

                    break;
                case "More Bullets":
                    UpgradeType = 2;
                    UpgradeCost = 65;
                    CurrentStat = "6";
                    UpgradeImprovement = "+ 2";
                    NewInt = 8;
                    break;
                case "More Clips":
                    UpgradeType = 3;
                    UpgradeCost = 80;
                    CurrentStat = "3";
                    UpgradeImprovement = " + 1";
                    NewInt = 4;
                    break;
            }
            HeaderText.text = UpgradeName + "\n\n" + CurrentStat + " " + UpgradeImprovement;
            TextObj.text = UpgradeCost + " coins";
        }

        public void BuyUpgrade()
        {
            if (Purchasable)
            {
                GameCoins = Game.m_totalCoins;
                if (GameCoins >= UpgradeCost)
                {
                    RemoveCost();
                    SetNewPlayerStats();
                    SetNewText();
                    SetNewBuyValue();
                    ShopText.text = GameCoins+"";
                }
            }
        }

        void RemoveCost()
        {
            GameCoins -= UpgradeCost;
            Game.m_totalCoins -= UpgradeCost;
        }

        void SetNewPlayerStats()
        {
            switch (UpgradeName)
            {
                case "Reload Faster":
                    Player.ChangePlayerRates("reload", NewFloat);
                    Debug.Log("Purchased reload upgrade. New Upgradecost: " + UpgradeCost + " currentstat: " + CurrentStat + " Upgradeimprovement: " + UpgradeImprovement + " newfloat: "  + NewFloat);
                    break;
                case "Shoot Faster":
                    Player.ChangePlayerRates("firerate", NewFloat);
                    Debug.Log("Purchased firerate upgrade. New Upgradecost: " + UpgradeCost + " currentstat: " + CurrentStat + " Upgradeimprovement: " + UpgradeImprovement + " newfloat: " + NewFloat);
                    break;
                case "More Bullets":
                    Player.ChangePlayerBulls("bullets", NewInt);
                    Debug.Log("Purchased bullets upgrade. New Upgradecost: " + UpgradeCost + " currentstat: " + CurrentStat + " Upgradeimprovement: " + UpgradeImprovement + " newfloat: " + NewInt);
                    break;
                case "More Clips":
                    Player.ChangePlayerBulls("clips", NewInt);
                    Debug.Log("Purchased clips upgrade. New Upgradecost: " + UpgradeCost + " currentstat: " + CurrentStat + " Upgradeimprovement: " + UpgradeImprovement + " newfloat: " + NewInt);
                    break;
                default:
                    break;
            }
        }

        void SetNewBuyValue()
        {
            switch (TimesBought)
            {
                case 0:
                    if (UpgradeType == 1) { UpgradeCost = 180; }
                    if (UpgradeType == 2) { UpgradeCost = 75; }
                    if (UpgradeType == 3) { UpgradeCost = 150; }
                    TimesBought++;
                    TextObj.text = UpgradeCost + " coins";
                    break;
                case 1:
                    if (UpgradeType == 1) { UpgradeCost = 360; }
                    if (UpgradeType == 2) { UpgradeCost = 250; }
                    if (UpgradeType == 3) { UpgradeCost = 300; }
                    TimesBought++;
                    TextObj.text = UpgradeCost + " coins";
                    break;
                case 2:
                    if (UpgradeType == 1) { UpgradeCost = 780; }
                    if (UpgradeType == 2) { UpgradeCost = 500; }
                    if (UpgradeType == 3) { UpgradeCost = 600; }
                    TimesBought++;
                    TextObj.text = UpgradeCost + " coins";
                    break;
                case 3:
                    LockUpgrade();
                    break;
                default:
                    break;
            }
        }

        void SetNewText()
        {
            switch (UpgradeName)
            {
                case "Reload Faster":
                case "Shoot Faster":

                    if (TimesBought == 0)
                    {
                        CurrentStat = "0.75s";
                        UpgradeImprovement = "-0.20s";
                        NewFloat = 0.55f;
                    }
                    if (TimesBought == 1)
                    {
                        CurrentStat = "0.55s";
                        UpgradeImprovement = "-0.15s";
                        NewFloat = 0.40f;
                    }
                    if (TimesBought == 2)
                    {
                        CurrentStat = "0.40s";
                        UpgradeImprovement = "-0.15s";
                        NewFloat = 0.25f;
                    }
                    if (TimesBought == 3)
                    {
                        CurrentStat = "0.25s";
                        UpgradeImprovement = "";
                    }
                    break;
                case "More Bullets":
                    if (TimesBought == 0)
                    {
                        CurrentStat = "8";
                        UpgradeImprovement = "+ 3";
                        NewInt = 11;
                    }
                    if (TimesBought == 1)
                    {
                        CurrentStat = "11";
                        UpgradeImprovement = "+ 4";
                        NewInt = 15;
                    }
                    if (TimesBought == 2)
                    {
                        CurrentStat = "15";
                        UpgradeImprovement = "+ 5";
                        NewInt = 20;
                    }
                    if (TimesBought == 3)
                    {
                        CurrentStat = "20";
                        UpgradeImprovement = "";
                    }
                    break;
                case "More Clips":
                    if (TimesBought == 0)
                    {
                        CurrentStat = "4";
                        UpgradeImprovement = " + 1";
                        NewInt = 4;
                    }
                    if (TimesBought == 1)
                    {
                        CurrentStat = "5";
                        UpgradeImprovement = " + 1";
                        NewInt = 5;
                    }
                    if (TimesBought == 2)
                    {
                        CurrentStat = "6";
                        UpgradeImprovement = " + 1";
                        NewInt = 6;
                    }
                    if (TimesBought == 3)
                    {
                        CurrentStat = "7";
                        UpgradeImprovement = "";
                    }
                    break;
            }
            HeaderText.text = UpgradeName + "\n\n" + CurrentStat + " " + UpgradeImprovement;
        }

        void LockUpgrade()
        {
            Purchasable = false;
            ImageObj.color = SoldOut;
            TextObj.text = "Max upgraded";
            BuyButton.interactable = false;
        }
    }
}