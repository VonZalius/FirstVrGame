using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreZoneScript : MonoBehaviour
{
    public GameObject targetPrefab; // Objet assigné via l'Inspector
    public bool destroyOnScore = false; // Activer/désactiver la destruction
    public float destroyDelay = 0f; // Temps avant destruction

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant correspond bien au prefab assigné
        if (targetPrefab != null && IsSamePrefab(other.gameObject, targetPrefab))
        {
            // Ajoute 1 point au score
            ScoreScript.Instance.AddScore(1);

            // Vérifie si on doit détruire l'objet
            if (destroyOnScore)
            {
                Destroy(other.gameObject, destroyDelay);
            }
        }
    }

    private bool IsSamePrefab(GameObject obj, GameObject prefab)
    {
        // Vérifie si l'objet instancié provient bien du prefab assigné
        return obj.name.StartsWith(prefab.name);
    }
}





