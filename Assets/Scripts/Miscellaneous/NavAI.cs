using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
    
public class NavAI : MonoBehaviour
{
    [SerializeField]
    private bool isBot;

    private GameObject player;
    private GameObject dest;
    private NavMeshAgent nav;

    void Start()
    {
        dest = GameObject.FindWithTag("RobotDest");
        player = PlayerManager.instance.gameObject;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isBot && MeleeAttackManager.instance.inCombat) {
            nav.destination = dest.transform.position;
        } else
        {
            nav.destination = player.transform.position;
        }
    }
}
