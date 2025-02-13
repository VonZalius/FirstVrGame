using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    public float startTime = 0f; // Seconde où le son doit commencer
    public bool stopSound = false; // Doit-on arrêter le son ?
    public float stopTime = 10f; // Seconde où le son doit s'arrêter si stopSound est activé

    private AudioSource ambianceAudio;

    void Start()
    {
        ambianceAudio = GetComponent<AudioSource>();

        if (ambianceAudio.clip == null)
        {
            Debug.LogError("Aucun AudioClip assigné à l'AudioSource !");
            return;
        }

        // S'assurer que startTime ne dépasse pas la durée du clip
        startTime = Mathf.Clamp(startTime, 0f, ambianceAudio.clip.length);

        // Démarrer la lecture en décalé avec PlayScheduled()
        ambianceAudio.PlayScheduled(AudioSettings.dspTime + 0.1);
        ambianceAudio.time = startTime; // Définir le point de départ après lancement

        // Vérification de lecture
        StartCoroutine(VerifyStartTime());

        // Arrêter le son à stopTime si nécessaire
        if (stopSound)
        {
            stopTime = Mathf.Clamp(stopTime, startTime, ambianceAudio.clip.length);
            Invoke("StopSound", stopTime - startTime);
        }
    }

    private System.Collections.IEnumerator VerifyStartTime()
    {
        yield return new WaitUntil(() => ambianceAudio.isPlaying);
        ambianceAudio.time = startTime;
    }

    void StopSound()
    {
        ambianceAudio.Stop();
    }
}



