using UnityEngine;

namespace BumblePux.NoughtsAndCrosses
{
    public class GameManager : MonoBehaviour
    {
        public const string PLAYER_NOUGHT = "O";
        public const string PLAYER_CROSS = "X";

        [Header("References")]
        public GameHUD HUD;

        [Header("Game Settings")]
        public Color DefaultGridColor = Color.white;
        public Color MatchGridColor = Color.red;

        [Header("UI Settings")]
        public int GridTextSize = 120;

        [Header("Board References")]
        public GridSpace[] GridSpaces;

        private string currentPlayer;
        private bool hasGameTied;
        private int noughtsScore;
        private int crossesScore;

        private GridSpace[] matchedSpaces = new GridSpace[3];


        public void OnGridSpacePressed(GridSpace gridSpace)
        {
            gridSpace.SetSpace(currentPlayer);

            if (HasCurrentPlayerWon())
            {
                GameOver();
            }
            else if (NoRemainingSpaces())
            {
                hasGameTied = true;
                GameOver();
            }
            else
            {
                ChangeCurrentPlayer();
                HUD.SetCurrentPlayerTurnPanel(currentPlayer);
            }
        }

        private void Start()
        {
            ResetScores();
            SetupGame();
        }

        private void ResetScores()
        {
            noughtsScore = 0;
            crossesScore = 0;

            HUD.UpdateScoreDisplay(noughtsScore, crossesScore);
        }

        public void SetupGame()
        {
            hasGameTied = false;
            matchedSpaces = new GridSpace[3];

            ResetBoard();
            SetRandomStartingPlayer();

            HUD.HideRestartButtons();
            HUD.ResetTurnPanels();
            HUD.SetCurrentPlayerTurnPanel(currentPlayer);
        }

        private void ResetBoard()
        {
            foreach (var space in GridSpaces)
            {
                space.SetGameManager(this);
                space.SetTextSize(GridTextSize);
                space.SetColor(DefaultGridColor);
                space.ResetSpace();
            }
        }

        private void SetRandomStartingPlayer()
        {
            int coinFlip = Random.Range(0, 2);
            if (coinFlip == 1)
                currentPlayer = PLAYER_CROSS;
            else
                currentPlayer = PLAYER_NOUGHT;
        }

        private void ChangeCurrentPlayer()
        {
            if (currentPlayer == PLAYER_NOUGHT)
                currentPlayer = PLAYER_CROSS;
            else
                currentPlayer = PLAYER_NOUGHT;
        }

        private void GameOver()
        {
            foreach (var space in GridSpaces)
            {
                space.DisableSpace();
            }

            if (hasGameTied)
            {
                HUD.ShowTie();
            }
            else
            {
                HighlightMatchingSpaces();

                if (currentPlayer == PLAYER_NOUGHT)
                    noughtsScore++;
                else
                    crossesScore++;

                HUD.ShowWinner(currentPlayer);
                HUD.UpdateScoreDisplay(noughtsScore, crossesScore);
            }
        }

        private bool HasCurrentPlayerWon()
        {
            if (HasHorizontalMatch() || HasVerticalMatch() || HasDiagonalMatch())
                return true;
            else
                return false;
        }

        private bool HasHorizontalMatch()
        {
            // Top row
            if (GridSpaces[0].GetText() == currentPlayer && GridSpaces[1].GetText() == currentPlayer && GridSpaces[2].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[0];
                matchedSpaces[1] = GridSpaces[1];
                matchedSpaces[2] = GridSpaces[2];
                return true;
            }
            // Middle row
            else if (GridSpaces[3].GetText() == currentPlayer && GridSpaces[4].GetText() == currentPlayer && GridSpaces[5].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[3];
                matchedSpaces[1] = GridSpaces[4];
                matchedSpaces[2] = GridSpaces[5];
                return true;
            }
            // Bottom row
            else if (GridSpaces[6].GetText() == currentPlayer && GridSpaces[7].GetText() == currentPlayer && GridSpaces[8].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[6];
                matchedSpaces[1] = GridSpaces[7];
                matchedSpaces[2] = GridSpaces[8];
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HasVerticalMatch()
        {
            if (GridSpaces[0].GetText() == currentPlayer && GridSpaces[3].GetText() == currentPlayer && GridSpaces[6].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[0];
                matchedSpaces[1] = GridSpaces[3];
                matchedSpaces[2] = GridSpaces[6];
                return true;
            }
            else if (GridSpaces[1].GetText() == currentPlayer && GridSpaces[4].GetText() == currentPlayer && GridSpaces[7].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[1];
                matchedSpaces[1] = GridSpaces[4];
                matchedSpaces[2] = GridSpaces[7];
                return true;
            }
            else if (GridSpaces[2].GetText() == currentPlayer && GridSpaces[5].GetText() == currentPlayer && GridSpaces[8].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[2];
                matchedSpaces[1] = GridSpaces[5];
                matchedSpaces[2] = GridSpaces[8];
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HasDiagonalMatch()
        {
            if (GridSpaces[0].GetText() == currentPlayer && GridSpaces[4].GetText() == currentPlayer && GridSpaces[8].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[0];
                matchedSpaces[1] = GridSpaces[4];
                matchedSpaces[2] = GridSpaces[8];
                return true;
            }
            else if (GridSpaces[2].GetText() == currentPlayer && GridSpaces[4].GetText() == currentPlayer && GridSpaces[6].GetText() == currentPlayer)
            {
                matchedSpaces[0] = GridSpaces[2];
                matchedSpaces[1] = GridSpaces[4];
                matchedSpaces[2] = GridSpaces[6];
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool NoRemainingSpaces()
        {
            bool noSpaces = true;

            foreach (var space in GridSpaces)
            {
                if (space.IsEmpty())
                    noSpaces = false;
            }

            return noSpaces;
        }

        private void HighlightMatchingSpaces()
        {
            foreach (var space in matchedSpaces)
            {
                space.SetColor(MatchGridColor);
            }
        }
    }
}