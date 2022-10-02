using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameFlow _gameFlow;
        [SerializeField] private DayManager _dayManager;
        private PlayerStatusData _player;
        
        [Header("Text Display")]
        [SerializeField]  private TMP_Text _skillText;
        [SerializeField]  private TMP_Text _stressText;
        [SerializeField]  private TMP_Text _healthText;
        [SerializeField]  private TMP_Text _moneyText;
        [SerializeField]  private TMP_Text _bookText;
        [SerializeField]  private TMP_Text _foodText;
        [SerializeField] private TMP_Text _actionText;
        [SerializeField] private TMP_Text _dayText;
        
        [Header("Image Display")]
        [SerializeField] private GameObject _monologuePanel;
        [SerializeField] private TMP_Text _monologueText;

        private void OnEnable()
        {
            BaseObject.OnShowMonologue += UpdateMonologue;
        }

        private void OnDisable()
        {
            BaseObject.OnShowMonologue -= UpdateMonologue;
        }

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
            _actionText.text = Constants.Status.Action + _player.action.ToString();
            _dayText.text = Constants.Status.Day + _dayManager.amountDay.ToString();
        }
        private void SetMonologue()
        {
            _monologuePanel.GetComponent<Button>().onClick.AddListener(OnClickMonologue);
        }
        private void OnClickMonologue()
        {
            _monologuePanel.SetActive(false);
        }
        private void UpdateMonologue(string text)
        {
            _monologueText.text = text;
            _monologuePanel.SetActive(true);
            SetMonologue();
        }
        
    }
}