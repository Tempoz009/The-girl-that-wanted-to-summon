using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplayScript : MonoBehaviour
{
    [Header("TextForWaveDisplay")]
    [SerializeField] private Text _WaveDisplayText;

    void Update()
    {
        _WaveDisplayText.text = $"Current WAVE: {playerScript.currentWave}";
    }
}
