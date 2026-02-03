using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame
{
    public class ScoreManager : MonoBehaviour
    {
        private int seconds;
        private int score;
        private int highscore;

        public UnityEvent OnScoreUpdate;
        public UnityEvent OnHighScoreUpdate;

        private void Start()
        {
            highscore = PlayerPrefs.GetInt("HighScore");
            OnHighScoreUpdate?.Invoke();
            GameManager.GetInstance().OnGameStart += OnGameStart;
        }

        public void OnGameStart()
        {
            score = 0;
            GameManager.GetInstance().uiManager.UpdateScore();
        }

        public void SetHighScore()
        {
            PlayerPrefs.SetInt("HighScore", highscore);
        }

        public int GetHighScore()
        {
            return highscore;
        }

        public string timer
        {
            get
            {
                return (Mathf.Round((float) seconds / 60.0f) + "Mins and " + seconds % 60 + " Seconds");
            }
            private set { }
        }

        public int GetScore()
        {
            return score;
        }

        public void IncrementScore()
        {
            score++;
            OnScoreUpdate?.Invoke();

            if (score > highscore)
            {
                highscore = score;
                OnHighScoreUpdate?.Invoke();
            }
        }


    }
}
