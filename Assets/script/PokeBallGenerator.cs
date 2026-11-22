using System.Collections;
using UnityEngine;

public class PokeBallGenerator : MonoBehaviour
{
    public GameObject pokeBallPrefab;
    public Animator trainerAnimator;
    public string throwTriggerName = "Throw";
    public Transform spawnPoint;

    [Header("發球時機與瞄準力道")]
    public float throwDelay = 0f; 
    public Camera targetCamera;
    public float throwPower = 2200f;
    public float extraUpward = 200f;

    void Start()
    {
        Application.targetFrameRate = 60;
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (trainerAnimator != null)
            {
                trainerAnimator.SetTrigger(throwTriggerName);
            }

            if (pokeBallPrefab != null)
            {
                Camera cam = targetCamera != null ? targetCamera : Camera.main;
                Vector3 force = cam != null
                    ? cam.ScreenPointToRay(Input.mousePosition).direction * throwPower + Vector3.up * extraUpward
                    : new Vector3(0f, extraUpward, throwPower);

                if (throwDelay <= 0f)
                {
                    SpawnAndShoot(force);
                }
                else
                {
                    StartCoroutine(ThrowAfterDelay(force));
                }
            }
        }
    }

    IEnumerator ThrowAfterDelay(Vector3 force)
    {
        yield return new WaitForSeconds(throwDelay);
        SpawnAndShoot(force);
    }

    void SpawnAndShoot(Vector3 force)
    {
        Vector3 startPos = spawnPoint != null ? spawnPoint.position : transform.position;
        GameObject ball = Instantiate(pokeBallPrefab, startPos, Quaternion.identity);

        PokeBallController ctrl = ball.GetComponent<PokeBallController>();
        if (ctrl != null)
        {
            ctrl.Shoot(force);
        }
    }
}