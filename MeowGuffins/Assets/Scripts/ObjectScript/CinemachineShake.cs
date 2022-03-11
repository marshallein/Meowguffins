using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtual;
    private float timer;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float duration, float intensity)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin
            = cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (cinemachineBasicMultiChannelPerlin != null)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            timer = duration;
        }
    }


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin
                    = cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                if (cinemachineBasicMultiChannelPerlin != null)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

                }
            }
        }
    }
}
