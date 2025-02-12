using UnityEngine;
using System.Collections.Generic; // Permet d'utiliser une liste

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab; // La balle à instancier
    public float launchForce = 10f; // Force du lancer
    public Vector3 launchDirection = Vector3.forward; // Direction du lancer
    public float launchInterval = 2f; // Temps entre chaque tir
    public int maxBalls = 10; // Nombre max de balles simultanées
    public float startDelay = 0f; // Délai avant le premier tir

    private Queue<GameObject> ballQueue = new Queue<GameObject>(); // Stocke les balles actives

    private void Start()
    {
        // Attendre "startDelay" secondes avant de commencer à lancer les balles
        InvokeRepeating(nameof(LaunchBall), startDelay, launchInterval);
    }

    private void LaunchBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball Prefab is not assigned!");
            return;
        }

        // Vérifier si on dépasse la limite de balles
        if (ballQueue.Count >= maxBalls)
        {
            // Détruire la balle la plus ancienne
            GameObject oldestBall = ballQueue.Dequeue();
            Destroy(oldestBall);
        }

        // Instancier la balle et la mettre comme enfant du Launcher
        GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);

        // Ajouter la nouvelle balle à la queue
        ballQueue.Enqueue(newBall);

        // Vérifier si la balle a un Rigidbody et appliquer la force
        Rigidbody rb = newBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("La balle doit avoir un Rigidbody !");
        }
    }
}



