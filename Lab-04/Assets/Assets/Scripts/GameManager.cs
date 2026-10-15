using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Cérebro do jogo: pontuação, vidas, slow-motion e telas de vitória/derrota.
// Crie um objeto vazio chamado "GameManager" na cena e adicione este script nele.
public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Won, Lost }

    public static GameManager Instance { get; private set; }

    [Header("Regras")]
    public int startingLives = 3;    // vidas no começo
    public int scoreToWin = 300;     // pontos necessários para vencer

    [Header("Slow-motion")]
    [Range(0.1f, 1f)] public float slowFactor = 0.4f;   // 0.4 = tudo a 40% da velocidade
    public float slowDuration = 5f;                     // duração em segundos
    public int scoreStepForSlowMotion = 200;            // dispara a cada N pontos (0 = desligado)

    public int Score { get; private set; }
    public int Lives { get; private set; }
    public GameState State { get; private set; }

    private float worldSpeed = 1f;
    private float slowTimeLeft;
    private int nextSlowScore;
    private Coroutine slowRoutine;

    private GUIStyle hud, titulo, subtitulo, botao;

    // Multiplicador de velocidade do "mundo" (fundo e inimigos).
    // É static para os outros scripts lerem com GameManager.WorldSpeed.
    // O jogador e os tiros NÃO usam esse valor, então ficam com vantagem.
    public static float WorldSpeed
    {
        get { return Instance != null ? Instance.worldSpeed : 1f; }
    }

    void Awake()
    {
        Instance = this;
        State = GameState.Playing;
        Lives = startingLives;
        nextSlowScore = scoreStepForSlowMotion;
        Time.timeScale = 1f;   // garante que o jogo comece rodando (importante após reiniciar)
    }

    void Update()
    {
        // Na tela final, R também reinicia.
        if (State != GameState.Playing && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void AddScore(int points)
    {
        if (State != GameState.Playing) return;

        Score += points;

        if (Score >= scoreToWin)
        {
            EndGame(GameState.Won);
            return;
        }

        if (scoreStepForSlowMotion > 0 && Score >= nextSlowScore)
        {
            ActivateSlowMotion();
            nextSlowScore = (Score / scoreStepForSlowMotion + 1) * scoreStepForSlowMotion;
        }
    }

    // Chamado pelo Enemy quando encosta na nave.
    public void LoseLife()
    {
        if (State != GameState.Playing) return;

        Lives--;

        if (Lives <= 0)
        {
            EndGame(GameState.Lost);
        }
    }

    public void ActivateSlowMotion()
    {
        // Se já estiver ativo, reinicia a contagem.
        if (slowRoutine != null) StopCoroutine(slowRoutine);
        slowRoutine = StartCoroutine(SlowMotionRoutine());
    }

    private IEnumerator SlowMotionRoutine()
    {
        worldSpeed = slowFactor;
        slowTimeLeft = slowDuration;

        while (slowTimeLeft > 0f)
        {
            slowTimeLeft -= Time.deltaTime;
            yield return null;   // espera o próximo frame
        }

        worldSpeed = 1f;
        slowRoutine = null;
    }

    private void EndGame(GameState resultado)
    {
        State = resultado;

        // timeScale = 0 CONGELA tudo (movimento, spawner, parallax), pois todos usam Time.deltaTime.
        // Aqui isso é o que queremos. Já no slow-motion não usamos timeScale, para o jogador continuar normal.
        Time.timeScale = 0f;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ---------------- Interface (OnGUI: não precisa de Canvas) ----------------

    void CriarEstilos()
    {
        if (hud != null) return;

        hud = new GUIStyle(GUI.skin.label);
        hud.fontSize = 28;
        hud.fontStyle = FontStyle.Bold;
        hud.normal.textColor = Color.white;

        titulo = new GUIStyle(GUI.skin.label);
        titulo.fontSize = 72;
        titulo.fontStyle = FontStyle.Bold;
        titulo.alignment = TextAnchor.MiddleCenter;

        subtitulo = new GUIStyle(GUI.skin.label);
        subtitulo.fontSize = 32;
        subtitulo.alignment = TextAnchor.MiddleCenter;
        subtitulo.normal.textColor = Color.white;

        botao = new GUIStyle(GUI.skin.button);
        botao.fontSize = 28;
    }

    void OnGUI()
    {
        CriarEstilos();

        GUI.Label(new Rect(20, 15, 500, 40), "Pontos: " + Score + " / " + scoreToWin, hud);
        GUI.Label(new Rect(20, 55, 500, 40), "Vidas: " + Lives, hud);

        if (State == GameState.Playing && worldSpeed < 1f)
        {
            GUI.Label(new Rect(20, 95, 500, 40), "SLOW-MOTION: " + slowTimeLeft.ToString("0.0") + "s", hud);
        }

        if (State != GameState.Playing)
        {
            DesenharTelaFinal();
        }
    }

    void DesenharTelaFinal()
    {
        // Fundo escuro semitransparente cobrindo a tela toda.
        GUI.color = new Color(0f, 0f, 0f, 0.75f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = Color.white;

        bool venceu = (State == GameState.Won);
        titulo.normal.textColor = venceu ? new Color(0.4f, 1f, 0.5f) : new Color(1f, 0.4f, 0.4f);

        float cx = Screen.width / 2f;
        float cy = Screen.height / 2f;

        GUI.Label(new Rect(0, cy - 130, Screen.width, 100), venceu ? "VITÓRIA!" : "DERROTA", titulo);
        GUI.Label(new Rect(0, cy - 30, Screen.width, 50), "Pontuação final: " + Score, subtitulo);

        if (GUI.Button(new Rect(cx - 140, cy + 50, 280, 60), "Jogar de novo (R)", botao))
        {
            Restart();
        }
    }
}
