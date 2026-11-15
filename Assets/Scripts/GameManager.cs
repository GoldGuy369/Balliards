using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public enum GameState { Ready, Moving, Upgrading, GameOver } //DIFFERENT STATES THE GAME CAN BE

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BallSpawner rack;
    public UpgradeMenu upgradeMenu;   //not yet implemented
    public Text hudText;
    public GameObject GameOverScreen;
    public int score = 0;
    public Text scoreDisplay;

    public int baseMoves = 5;
    public int targetScore = 7;
    public int targetIncrease = 5;
    public float combo=1f;
    public float compoMultiplyer = 0.3f;


    public Stats stats = new Stats();
    public GameState State { get; private set; }
    public int round = 1;
    public int roundScore;
    public int movesLeft;
    public int target;
    public int pottedThisMove;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        startRun();
        if (GameOverScreen) GameOverScreen.SetActive(false);
    }

    public void startRun()
    {
        stats = new Stats();
        round = 1;
        if (GameOverScreen) GameOverScreen.SetActive(false);
        startRound();
    }

    //set target score and amount of moves to use abilities witht
    public void startRound()
    {
        target = targetScore + (round - 1) * targetIncrease;
        roundScore = 0;
        movesLeft = baseMoves+stats.extraMoves;
        rack.Spawn();

        State = GameState.Ready;
        refreshHud();
    }

    //check if the player is available to take turn then set state to Moving(rolling balls action)
    public bool startMove()
    {
        if (State != GameState.Ready || movesLeft <= 0) return false;
        State = GameState.Moving;
        movesLeft--;
        pottedThisMove = 0;
        refreshHud();
        return true;

    }

    //wait for balls to stop to end the move - coroutine to stop and start rather than chekc every update frame
    public void endMove() { StartCoroutine(WaitForSettle()); }

    IEnumerator WaitForSettle()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        while (rack.checkMovement()) yield return null;
        //end round if 0 moves or 0 balls left else continue
        if (movesLeft <= 0 || rack.ballsInPlay == 0) endRound();
        else { State = GameState.Ready; refreshHud(); }
    }

    void endRound()
    {
        if (roundScore >= target)
        {
            State = GameState.Upgrading;
            upgradeMenu.Show();     // use whatever you named the method in UpgradeMenu
        }else
        {
            State = GameState.GameOver;
            if (GameOverScreen) GameOverScreen.SetActive(true);
        }
        refreshHud();
    }

    public void ballPotted(Ball ball)
    {
        //if ball isnt potted then add a combo if multiple potted this move and add to score and refresh hud to display new score
        if (ball.potted) return;
        ball.potBall();
        combo = 1f + compoMultiplyer * pottedThisMove;
        roundScore += Mathf.RoundToInt(ball.points * combo);
        pottedThisMove++;
        refreshHud();
    }

    public void UpgradePicked(Upgrade upgrade)
    {
        upgrade.applyUpgrade(stats);
        round++;
        startRound();
    }

    void refreshHud()
    {
        if (hudText)
            hudText.text = $"Round {round}, Moves {movesLeft}, Score {roundScore}/{target}";
    }
}

