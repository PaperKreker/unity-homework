using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopupView : MonoBehaviour
    {
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

        public void SetTitle(string title) => _titleText.text = title;

        public void SetAvatar(Sprite icon) => _avatarIcon.sprite = icon;

        public void SetPopulation(string population) => _populationText.text = population;
        //_populationText.text = $"Population: {population}";

        public void SetLevel(string level) => _levelText.text = level;
        //_levelText.text = $"Level: {current}/{max}";

        public void SetIncome(string income) => _incomeText.text = income;
        //_incomeText.text = $"Income: {income} / sec";
    }
}
