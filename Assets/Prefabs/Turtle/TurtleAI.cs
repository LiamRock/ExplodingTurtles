using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform targetDestination;

    // Genetics
    [SerializeField] private float socialability = 1.0f;
    [SerializeField] private float satiability = 1.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float attractiveness = 1.0f;
    [SerializeField] private float attraction = 1.0f;
    [SerializeField] private float resistance = 1.0f;
    [SerializeField] private float immunity = 1.0f;
    [SerializeField] private float awareness = 1.0f;

    // Stats
    [SerializeField] private int fullness = 100;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;

        StartCoroutine("DepleteFullness");
    }

    private void Update() {
        navMeshAgent.destination = targetDestination.position;
    }

    // every 2 seconds perform the print()
    private IEnumerator DepleteFullness ()
    {
        fullness--;

        yield return new WaitForSeconds(1);

        StartCoroutine("DepleteFullness");
    }

    private void Eat(float satiation)
    {
        fullness = Mathf.Clamp(fullness + satiation, 0, 100);
    }

}
