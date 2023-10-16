using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] Transform house;
    [SerializeField] AudioSource[] constructionNoises;
    [SerializeField] ParticleSystem dust;

    [Header("Parameters")]
    [SerializeField] float houseRaiseSpeed = 0.1f;

    private Vector3 houseConstructionTarget;
    private bool isAudioStart;
    private bool isConstructionDone;
    
    void Start()
    {
        this.isConstructionDone = false;
        this.houseConstructionTarget = new Vector3(this.house.position.x, -7.01f, this.house.position.z);
        this.isAudioStart = true;

        foreach (AudioSource noise in this.constructionNoises) {
            noise.Play();
        }
    }

    void Update()
    {
        UpdateHousePosition();

        if (this.isAudioStart) {
            UpdateAudio(0.2f);
            this.isAudioStart = false;
        }

        if (this.isConstructionDone) {
            UpdateAudio(0);
            this.dust.Stop();
        }
    }

    private void UpdateHousePosition()
    {
        this.house.position = Vector3.MoveTowards(
            this.house.position,
            this.houseConstructionTarget,
            this.houseRaiseSpeed * Time.deltaTime
        );

        if (Vector3.Distance(this.house.transform.position, this.houseConstructionTarget) < 0.01f) {
            this.isConstructionDone = true;
        }
    }

    private void UpdateAudio(float targetVolume)
    {
        foreach (AudioSource noise in this.constructionNoises) {
            StartCoroutine(FadeAudio(noise, 1, targetVolume));
        }
    }

    private static IEnumerator Wait1()
    {
        yield return new WaitForSeconds(1);
    }

    private static IEnumerator FadeAudio(AudioSource audio, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audio.volume;
        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audio.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }
}
