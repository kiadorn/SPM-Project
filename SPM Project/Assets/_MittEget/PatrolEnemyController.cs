using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : Controller {

    private void Update()
    {
        CurrentState.Update();
    }

}
