using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopupView : MonoBehaviour
    {
        public event UnityAction OnCloseClicked
        { 
            add => _closeButton.onClick.AddListener(value);
            remove => _closeButton.onClick.RemoveListener(value);
        }

        public event UnityAction OnUpgradeClicked
        {
            add => _upgradeButton.onClick.AddListener(value);
            remove => _upgradeButton.onClick.RemoveListener(value);
        }

        [SerializeField]
        private TMP_Text _titleText;

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private Image _avatarIcon;

        [SerializeField]
        private TMP_Text _populationText;

        [SerializeField]
        private TMP_Text _levelText;

        [SerializeField]
        private TMP_Text _incomeText;

        [SerializeField]
        private Button _upgradeButton;

        [SerializeField]
        private GameObject _maxLevelPanel;

        [SerializeField]
        private TMP_Text _upgradePrice;

        public void SetTitle(string title) => _titleText.text = title;

        public void SetAvatar(Sprite icon) => _avatarIcon.sprite = icon;

        public void SetPopulation(string population) => _populationText.text = population;

        public void SetLevel(string level) => _levelText.text = level;

        public void SetIncome(string income) => _incomeText.text = income;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void SetUpgradeButton(string text, bool canUpgrade)
        {
            _upgradeButton.interactable = canUpgrade;
            _upgradePrice.text = text;
        }

        public void SetMaxLevel(bool isMaxLevel)
        {
            _upgradeButton.gameObject.SetActive(!isMaxLevel);
            _maxLevelPanel.SetActive(isMaxLevel);
        }
    }
}
