using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BumblePux.NoughtsAndCrosses
{
    public class GameHUD : MonoBehaviour
    {
        private const string TEXT_YOUR_TURN = "Your Turn";
        private const string TEXT_YOU_WON = "You Won!";
        private const string TEXT_TIED = "Tie!";

        [Header("External References")]
        //public GameManager GameManager;

        [Header("Turn Panels")]
        public GameObject NoughtsTurnPanel;
        public GameObject CrossesTurnPanel;

        [Header("Scores")]
        public TMP_Text NoughtsScoreText;
        public TMP_Text CrossesScoreText;

        [Header("Buttons")]
        public GameObject NoughtsRestartButton;
        public GameObject CrossesRestartButton;
        

        public void SetCurrentPlayerTurnPanel(string currentPlayer)
        {
            if (currentPlayer == GameManager.PLAYER_NOUGHT)
            {
                NoughtsTurnPanel.SetActive(true);
                CrossesTurnPanel.SetActive(false);
            }
            else
            {
                NoughtsTurnPanel.SetActive(false);
                CrossesTurnPanel.SetActive(true);
            }
        }

        public void HideRestartButtons()
        {
            NoughtsRestartButton.SetActive(false);
            CrossesRestartButton.SetActive(false);
        }

        public void ResetTurnPanels()
        {
            NoughtsTurnPanel.GetComponentInChildren<TMP_Text>().SetText(TEXT_YOUR_TURN);
            CrossesTurnPanel.GetComponentInChildren<TMP_Text>().SetText(TEXT_YOUR_TURN);
        }

        public void UpdateScoreDisplay(int noughtsScore, int crossesScore)
        {
            NoughtsScoreText.SetText(noughtsScore.ToString());
            CrossesScoreText.SetText(crossesScore.ToString());
        }

        public void ShowWinner(string currentPlayer)
        {
            if (currentPlayer == GameManager.PLAYER_NOUGHT)
            {
                NoughtsTurnPanel.GetComponentInChildren<TMP_Text>().SetText(TEXT_YOU_WON);
                NoughtsRestartButton.SetActive(true);
            }
            else
            {
                CrossesTurnPanel.GetComponentInChildren<TMP_Text>().SetText(TEXT_YOU_WON);
                CrossesRestartButton.SetActive(true);
            }
        }

        public void ShowTie()
        {
            NoughtsRestartButton.SetActive(true);
            NoughtsTurnPanel.SetActive(true);
            NoughtsTurnPanel.GetComponentInChildren<TMP_Text>().SetText(TEXT_TIED);

            CrossesRestartButton.SetActive(true);
            CrossesTurnPanel.SetActive(true);
            CrossesTurnPanel.GetComponentInChildren<TMP_Text>().SetText(TEXT_TIED);
        }
    }
}