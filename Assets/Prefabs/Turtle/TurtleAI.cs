using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform targetDestination;
    private SphereCollider range; // Vision range, affected by awareness genetics

    // Genetics
    [SerializeField] private float socialability = 1.0f;
    [SerializeField] private float satiability = 1.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float attractiveness = 1.0f;
    [SerializeField] private float attraction = 1.0f;
    [SerializeField] private float resistance = 1.0f;
    [SerializeField] private float immunity = 1.0f;
    public float awareness = 7.0f;

    // Stats
    [SerializeField] private int fullness = 100;
    [SerializeField] private GameObject food;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        range = GetComponent<SphereCollider>();
        navMeshAgent.speed = speed;
        navMeshAgent.destination = targetDestination.position;

        StartCoroutine("DepleteFullness");
    }

    public void SetDestination(Vector3 destination) 
    {
        navMeshAgent.destination = destination;
    }

    public void SetFood(GameObject food) 
    {
        this.food = food;
        navMeshAgent.destination = this.food.transform.position;
    }

    private void Update() 
    {
        if(food)
        {
            if (Vector3.Distance(transform.position, food.transform.position) <  0.5f)
            {
                Eat(10);
                Destroy(food);
                food = null;
            }
        }
        
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
        Debug.Log("Eating for " + satiation + " satiation");
        fullness = (int) Mathf.Clamp(fullness + satiation, 0, 100);

        navMeshAgent.destination = targetDestination.position;
    }
}
