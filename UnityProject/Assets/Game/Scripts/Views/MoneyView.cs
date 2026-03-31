using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField]
        private float _animationTime = 1.0f;

        [SerializeField]
        private TMP_Text _moneyText;

        public void SetMoney(int value)
        {
            _moneyText.text = value.ToString();
        }

        public void SetMoney(int newValue, int prevValue)
        {
            DOTween.To(() => 
                prevValue,
                x => _moneyText.text = x.ToString(),
                newValue,
                _animationTime)
                .SetEase(Ease.OutCubic);
        }
    }
}
