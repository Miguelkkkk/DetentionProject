using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlimeMovement : EnemyMovement
{
    private void Start()
    {
        agent.speed = Random.Range(4.1f, 4.3f);
    }
}