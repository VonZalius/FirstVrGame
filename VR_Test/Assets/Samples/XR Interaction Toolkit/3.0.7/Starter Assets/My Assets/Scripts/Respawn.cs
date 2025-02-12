using UnityEngine;
using System.Collections.Generic;

public class CansManager : MonoBehaviour
{
    [Header("Prefab pour la recréation")]
    public GameObject canPrefab; // Le prefab que l'on utilise pour recréer les objets

    private List<Vector3> initialPositions = new List<Vector3>(); // Positions des objets
    private List<Quaternion> initialRotations = new List<Quaternion>(); // Rotations des objets
    private List<Vector3> initialScales = new List<Vector3>(); // Échelles des objets
    private List<GameObject> activeCans = new List<GameObject>(); // Liste des objets actuellement en jeu

    private void Start()
    {
        // Vérifie si un prefab est assigné
        if (canPrefab == null)
        {
            Debug.LogError("CansManager : Aucun prefab assigné à CanPrefab !");
            return;
        }

        // Enregistre tous les enfants actuels
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                initialPositions.Add(child.position);
                initialRotations.Add(child.rotation);
                initialScales.Add(child.localScale);
                activeCans.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        // Vérifie si tous les objets ont été détruits
        if (AllCopiesDestroyed())
        {
            RespawnCans();
        }
    }

    private bool AllCopiesDestroyed()
    {
        foreach (GameObject obj in activeCans)
        {
            if (obj != null)
                return false; // Au moins un objet existe encore
        }
        return true;
    }

    private void RespawnCans()
    {
        activeCans.Clear(); // Vide la liste des objets en jeu

        for (int i = 0; i < initialPositions.Count; i++)
        {
            // Instancie une nouvelle Can depuis le prefab assigné
            GameObject newCan = Instantiate(canPrefab, initialPositions[i], initialRotations[i]);

            // Applique les propriétés enregistrées
            newCan.transform.localScale = initialScales[i]; // Remet l'échelle d'origine
            newCan.transform.parent = transform; // Remet l'objet sous "Cans"

            activeCans.Add(newCan); // Ajoute à la liste des objets recréés
        }
    }
}



