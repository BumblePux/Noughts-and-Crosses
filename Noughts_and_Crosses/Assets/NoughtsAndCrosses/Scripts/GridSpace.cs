using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.NoughtsAndCrosses
{
    public class GridSpace : MonoBehaviour
    {
        private Image image;
        private Button button;
        private TMP_Text text;

        private GameManager gameManager;

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
            text = GetComponentInChildren<TMP_Text>();
        }

        public void NotifyGameManager()
        {
            gameManager.OnGridSpacePressed(this);
        }

        public void ResetSpace()
        {
            text.SetText(string.Empty);
            button.interactable = true;
        }

        public void DisableSpace()
        {
            button.interactable = false;
        }

        public bool IsEmpty()
        {
            return button.interactable;
        }        

        public string GetText()
        {
            return text.text;
        }        

        public void SetGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void SetSpace(string currentPlayer)
        {
            button.interactable = false;
            text.SetText(currentPlayer);
        }

        public void SetTextSize(int size)
        {
            text.fontSize = size;
        }        

        public void SetColor(Color color)
        {
            image.color = color;
        }        
    }
}