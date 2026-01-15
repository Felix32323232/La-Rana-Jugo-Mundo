using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Tiempo")]
    public float maxTime = 10f;
    private float currentTime;

    [Header("Checkpoint")]
    private Vector3 lastCheckpointPosition;

    [Header("UI")]
    public TMP_Text timerText;

    private bool timerActive = false;

    void Start()
    {
        currentTime = maxTime;
        lastCheckpointPosition = transform.position;
        timerActive = false;
        UpdateUI();
    }

    void Update()
    {
        if (!timerActive) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            Respawn();
        }
        UpdateUI();
    }
    public void OnPlayerMoved()
    {
        if (!timerActive)
        {
            timerActive = true;
            currentTime = maxTime;
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
        currentTime = maxTime;
    }

    void Respawn()
    {
        timerActive = false;   
        currentTime = maxTime;   
        transform.position = lastCheckpointPosition;
    }
    void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(currentTime).ToString();
        }
    }
}
