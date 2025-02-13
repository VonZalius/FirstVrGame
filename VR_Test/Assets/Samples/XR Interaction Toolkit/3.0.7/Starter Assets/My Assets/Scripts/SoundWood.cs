using UnityEngine;

public class WoodImpactSound : MonoBehaviour
{
    public AudioSource audioSource; // Référence à l'AudioSource

    [System.Serializable]
    public struct SoundRange
    {
        public float startTime; // Début du son
        public float stopTime;  // Fin du son
    }

    public SoundRange[] soundRanges; // Liste des plages sonores (max 3)

    [Header("Pitch Settings")]
    public float pitchMin = 0.9f; // Pitch minimum
    public float pitchMax = 1.1f; // Pitch maximum

    [Header("Distance Settings")]
    public float minDistance = 1.0f;  // Distance où le son est au volume maximum
    public float maxDistance = 20.0f; // Distance où le son devient inaudible

    [Header("Impact Volume Settings")]
    public float volumeMin = 0.2f;  // Volume minimum du son selon l'impact
    public float volumeMax = 1.0f;  // Volume maximum du son selon l'impact
    public float impactForceMultiplier = 0.1f; // Facteur d'influence de la force d'impact sur le volume

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource.clip == null)
        {
            Debug.LogError("Aucun AudioClip assigné à l'AudioSource !");
            return;
        }

        if (soundRanges.Length == 0)
        {
            Debug.LogError("Aucune plage de son définie !");
        }

        // Appliquer les paramètres de distance
        audioSource.spatialBlend = 1.0f; // Activer le son 3D
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // Son réaliste
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!audioSource.isPlaying && soundRanges.Length > 0) // Évite le spam sonore
        {
            // Choisir aléatoirement une plage de son
            int randomIndex = Random.Range(0, soundRanges.Length);
            SoundRange chosenRange = soundRanges[randomIndex];

            if (chosenRange.startTime < 0 || chosenRange.stopTime > audioSource.clip.length || chosenRange.startTime >= chosenRange.stopTime)
            {
                Debug.LogWarning("Plage de son invalide : " + chosenRange.startTime + " - " + chosenRange.stopTime);
                return;
            }

            // Modifier le pitch de manière aléatoire dans la plage définie
            audioSource.pitch = Random.Range(pitchMin, pitchMax);

            // Définir le volume selon la force de l'impact
            float impactForce = collision.relativeVelocity.magnitude; // Intensité de l'impact
            audioSource.volume = Mathf.Clamp(impactForce * impactForceMultiplier, volumeMin, volumeMax);

            // Jouer le son à la plage définie
            audioSource.time = chosenRange.startTime;
            audioSource.Play();

            // Arrêter le son après la durée définie
            Invoke("StopSound", chosenRange.stopTime - chosenRange.startTime);
        }
    }

    void StopSound()
    {
        audioSource.Stop();
    }
}

