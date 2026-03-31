using Modules.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        public event Action OnPlanetClicked
        {
            add => _planetButton.OnClick += value;
            remove => _planetButton.OnClick -= value;
        }
        public event Action OnPlanetHold
        {
            add => _planetButton.OnHold += value;
            remove => _planetButton.OnHold -= value;
        }
        public event Action<Vector2> OnGather;

        [SerializeField]
        private string _planetName;

        [SerializeField]
        private TMP_Text _incomeTime;

        [SerializeField]
        private Image _incomeProgressBar;

        [SerializeField]
        private GameObject _price;

        [SerializeField]
        private TMP_Text _priceText;

        [SerializeField]
        private GameObject _incomeProgress;

        [SerializeField]
        private GameObject _incomeIcon;

        [SerializeField]
        private GameObject _lockIcon;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private SmartButton _planetButton;

        public string GetName() => _planetName;

        public void SetIncomeTime(string time) => _incomeTime.text = time;

        public void SetIncomeProgressValue(int value) => _incomeProgressBar.fillAmount = value;

        public void SetPrice(string price) => _priceText.text = price;

        public void SetLocked(bool isLocked, Sprite icon)
        {
            _lockIcon.SetActive(isLocked);
            _price.SetActive(isLocked);

            _incomeProgress.SetActive(!isLocked);
            _incomeIcon.SetActive(!isLocked);

            _icon.sprite = icon;
        }

        public void SetIncomeReady(bool isReady)
        {
            _incomeIcon.SetActive(isReady);
            _incomeProgress.SetActive(!isReady);
        }

        public void GatherIncome()
        {
            OnGather?.Invoke(_incomeIcon.transform.position);
        }
    }
}