using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    Vector3 position;

    public GameObject wallPrefab;
    public GameObject minePrefab;
    public GameObject rocketPrefab;

    public LayerMask wallMask;

    public LineRenderer lineRenderer;

    public static float wallCooldown = 2.0f;
    public static float mineCooldown = 2.0f;
    public static float rocketCooldown = 1.0f;
    public static float grapplingHookCooldown = 1.0f;
    public static float dashCooldown = 1.0f;

    public static float grapplingHookDistance = 5.0f;

    public static float dashDistance = 3.0f;

    private float wallCooldownTimer = 0;
    private float mineCooldownTimer = 0;
    private float rocketCooldownTimer = 0;
    private float grapplingHookCooldownTimer = 0;
    public static float dashCooldownTimer = 0;

    void Start() {
        lineRenderer.enabled = false;
    }

    void FixedUpdate() {

    }

    void Update() {
        if (wallCooldownTimer > 0)
            wallCooldownTimer -= Time.deltaTime;

        if (mineCooldownTimer > 0)
            mineCooldownTimer -= Time.deltaTime;

        if (rocketCooldownTimer > 0)
            rocketCooldownTimer -= Time.deltaTime;

        if (grapplingHookCooldownTimer > 0)
            grapplingHookCooldownTimer -= Time.deltaTime;

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    public void onInput(InputDict dict) {
        if (dict.createMine)
            CreateMine();

        if (dict.createWall)
            CreateWall();

        if (dict.shootRocket)
            ShootRocket();

        if (dict.grapplingHook)
            GrapplingHook();

        if (dict.dash)
            Dash();

    }

    void CreateWall() {
        if (wallCooldownTimer <= 0) {
            position = GetComponent<Transform>().position;

            position = position - GetComponent<Transform>().forward;

            Instantiate(wallPrefab, position, GetComponent<Transform>().rotation);

            wallCooldownTimer = wallCooldown;
        }
    }

    void CreateMine() {
        if (mineCooldownTimer <= 0) {
            position = GetComponent<Transform>().position;

            //position = position - GetComponent<Transform>().forward;

            Instantiate(minePrefab, position, GetComponent<Transform>().rotation);

            mineCooldownTimer = mineCooldown;
        }
    }

    void ShootRocket() {
        if (rocketCooldownTimer <= 0) {
            position = GetComponent<Transform>().position + (GetComponent<Transform>().forward * 0.1f);

            Vector3 rot = GetComponent<Transform>().rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 90, rot.z + 90);

            GameObject rocket = Instantiate(rocketPrefab, new Vector3(position.x, 0.6f, position.z), Quaternion.Euler(rot));
            rocket.GetComponent<RocketController>().forwardDir = GetComponent<Transform>().forward;

            rocketCooldownTimer = rocketCooldown;
        }
    }

    void GrapplingHook() {
        if (grapplingHookCooldownTimer <= 0) {
            Transform transform = GetComponent<Transform>();

            position = transform.position;

            Vector3 dir = transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(position, dir, out hit, grapplingHookDistance, wallMask)) {

                Vector3 resultPos = dir * hit.distance;

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, position);
                lineRenderer.SetPosition(1, hit.point);

                GetComponent<PlayerController>().ChangeState(new GrappleState(GetComponent<PlayerController>(), hit.point));
            }

            grapplingHookCooldownTimer = grapplingHookCooldown;
        }
    }

    public void HideGrapple() {
        lineRenderer.enabled = false;
    }

    public void UpdateGrappleStartPos(Vector3 pos) {
        lineRenderer.SetPosition(0, pos);
    }

    void Dash() {
        if (dashCooldownTimer <= 0) {
            Transform transform = GetComponent<Transform>();

            position = transform.position;

            Vector3 dir = transform.forward;

            RaycastHit hit;

            Vector3 resultPos = dir * dashDistance + position;

            if (Physics.Raycast(position, dir, out hit, dashDistance, wallMask)) {
                resultPos = dir * hit.distance + position;
            }

            GetComponent<PlayerController>().ChangeState(new DashState(GetComponent<PlayerController>(), resultPos));

            dashCooldownTimer = dashCooldown;
        }
    }
}