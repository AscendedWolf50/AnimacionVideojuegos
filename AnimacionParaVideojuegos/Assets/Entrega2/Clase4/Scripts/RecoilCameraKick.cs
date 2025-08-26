using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RecoilCameraKick : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] cameras;
    CinemachineBasicMultiChannelPerlin[] perlins;
    float[] baseAmplitude;


    private void Awake()
    {
        perlins = new CinemachineBasicMultiChannelPerlin[cameras.Length];
        baseAmplitude = new float[cameras.Length];
        for (int i = 0; i < cameras.Length; i++)
        {
            if (!cameras[i]) continue;
            perlins[i] = cameras[i].GetComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlins[i]) baseAmplitude[i] = perlins[i].AmplitudeGain;
        }
    }

    public void Kick(float strength, float peak, float recover)
    {
        StopAllCoroutines();
        StartCoroutine(KickRoutine(strength, peak, recover));
    }

    IEnumerator KickRoutine(float strength, float peak, float recover)
    {
        //Pico
        float t = 0f;
        while (t < peak)
        {
            t += Time.deltaTime;
            float k = t / Mathf.Max(0.0001f, peak);
            for(int i=0; i<perlins.Length; i++)
                if (perlins[i]) perlins[i].AmplitudeGain = Mathf.Lerp(baseAmplitude[i], baseAmplitude[i]+ strength, k);
            yield return null;
        }
        //Recover
        t = 0f;
        while(t < recover)
        {
            t += Time.deltaTime;
            float k = t / Mathf.Max(0.0001f, peak);
            for (int i = 0; i < perlins.Length; i++)
                if (perlins[i]) perlins[i].AmplitudeGain = Mathf.Lerp(baseAmplitude[i], baseAmplitude[i] + strength, k);
            yield return null;
        }

        for(int i=0; i<perlins.Length;i++)
            if (perlins[i]) 
                perlins[i].AmplitudeGain = baseAmplitude[i];

    }
}

