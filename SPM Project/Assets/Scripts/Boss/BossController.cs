using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller {

    public GameObject Hand;

	private void Update() {
        CurrentState.Update();
    }

}
