using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField]  private TMP_Text _skillText;
        [SerializeField]  private TMP_Text _stressText;
        [SerializeField]  private TMP_Text _healthText;
        [SerializeField]  private TMP_Text _moneyText;
        [SerializeField]  private TMP_Text _bookText;
        [SerializeField]  private TMP_Text _foodText;
        [SerializeField] private TMP_Text _actionText;
        [SerializeField] private TMP_Text _dayText;
        
        [SerializeField] private GameFlow _gameFlow;
        [SerializeField] private DayManager _dayManager;
        private PlayerStatusData _player;
        private void Start()
        {
            _player = PlayerStatusData.instance;
        }

        private void Update()
        {
            UpdatePlayerStatus();
        }
        private void UpdatePlayerStatus()
        {
            _skillText.text = Constants.Status.Skill + _player.skill.ToString();
            _stressText.text = Constants.Status.Stress + _player.stress.ToString();
            _healthText.text = Constants.Status.Health + _player.health.ToString();
            _moneyText.text = Constants.Status.Money + _player.money.ToString();
            _bookText.text = Constants.Status.Book + _player.book.ToString();
            _foodText.text = Constants.Status.Food + _player.food.ToString();
            _actionText.text = Constants.Status.Action + _gameFlow._amountInteractables.ToString();
            _dayText.text = Constants.Status.Day + _dayManager.amountDay.ToString();
        }
    }
}