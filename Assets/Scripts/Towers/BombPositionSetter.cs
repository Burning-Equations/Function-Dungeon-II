using TMPro;
using UnityEngine;
using Events.GameEvents.Typed;
using Utils;

namespace Towers
{
    public class BombPositionSetter : MonoBehaviour
    {
        [Header("Tower variables")] 
        [SerializeField] private TowerVariables bombTowerVariables;

        [Header("GUI References")] 
        [SerializeField] private TextMeshProUGUI xAnswer;
        [SerializeField] private TextMeshProUGUI yAnswer;

        [Header("Events")] 
        [SerializeField] private Vector2GameEvent bombCoordinatesSet;
        [SerializeField] private GameObjectGameEvent onBombTowerPlaced;

        private Vector2 _answer;
        private GameObject _bombTower;

        private void Awake()
        {
            onBombTowerPlaced.AddListener(SetBombTower);
        }

        /// <summary>
        /// Retrieve bomb tower from event
        /// </summary>
        /// <param name="bombTower"> Bomb tower retrieved from event</param>
        private void SetBombTower(GameObject bombTower)
        {
            _bombTower = bombTower;
        }

        /// <summary>
        /// Check if typed coordinates are in range of tower and invoke the event to send coordinates back to tower
        /// </summary>
        public void OnConfirmButtonClicked()
        {
            _answer = new Vector2(
                float.Parse(StringExtensions.CleanUpDecimalOnlyString(xAnswer.text)),
                float.Parse(StringExtensions.CleanUpDecimalOnlyString(yAnswer.text))
            );

            if (
                _answer.x < _bombTower.transform.position.x - bombTowerVariables.Range ||
                _answer.x > _bombTower.transform.position.x + bombTowerVariables.Range ||
                _answer.y < _bombTower.transform.position.x - bombTowerVariables.Range ||
                _answer.y > _bombTower.transform.position.x + bombTowerVariables.Range
            )
                return;

            bombCoordinatesSet.Invoke(_answer);
        }
    }
}