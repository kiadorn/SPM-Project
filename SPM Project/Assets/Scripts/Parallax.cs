using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    /*public Transform playerPosition;
    public float modifier1Y;
    public float modifier1X;
    public float z1;
    public float modifier2Y;
    public float modifier2X;
    public float z2;
    public float modifier3Y;
    public float modifier3X;
    public float z3;

    void Update () {
        transform.GetChild(0).transform.position = new Vector3(playerPosition.position.x * modifier1X/100, playerPosition.position.y * modifier1Y/100, z1);
        transform.GetChild(1).transform.position = new Vector3(playerPosition.position.x * modifier2X/100, playerPosition.position.y * modifier2Y/100, z2);
        transform.GetChild(2).transform.position = new Vector3(playerPosition.position.x * modifier3X/100, playerPosition.position.y * modifier3Y/100, z3);
    }*/

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;

            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
        
    }
}
