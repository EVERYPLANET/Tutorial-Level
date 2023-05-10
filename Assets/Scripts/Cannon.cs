using System;
using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Projection _projection;
    public bool isStill = true;
    private LineRenderer line;

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _force = 100;
    [SerializeField] private float _maxForce = 200;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private Transform _barrelPivot;
    [SerializeField] private float _rotateSpeed = 30;

    private PlayerController playerC;
    private Quaternion lookRotation;
    private Vector3 direction;

    public int ammo;
    public int maxAmmo = 5;
    public float cooldown = 5;
    private float timer = 0;

    private TextMeshProUGUI ammoCounter;

    private void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        line = GetComponent<LineRenderer>();
        ammoCounter = GameObject.FindGameObjectWithTag("Ammo Counter").GetComponent<TextMeshProUGUI>();
        playerC.addInventory("Ammo", 5);
    }

    private void Update()
    {
        ammo = checkAmmo();
        if (isStill && timer <= 0 && ammo > 0 && playerC.controlsActive)
        {
            line.enabled = true;
            HandleControls();
            _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force);
        }
        else
        {
            line.enabled = false;
        }

        timer -= Time.deltaTime;
        ammoCounter.text = ammo.ToString();
    }

    private void HandleControls()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        var facing = playerC.playerModel.transform.localScale.x;
        bool rightCondition = (facing >= 0) && ((angle <= -300 && angle >= -360) || (angle <= 0 && angle >= -80));
        bool leftCondition = (facing <= 0) && (angle >= -240 && angle <= -100);
        if (rightCondition || leftCondition)
        {
            _barrelPivot.transform.eulerAngles = (new Vector3(angle, 90, 0));
        }

        if (Input.GetKey(KeyCode.S) && _force >= 1)
        {
            _force -= 50 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W) && _force <= _maxForce)
        {
            _force += 50 * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            {
                var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);

                spawned.Init(_ballSpawn.forward * _force, false);
                useAmmo();
                timer = cooldown;
            }
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        var angle = Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        return (angle + 180) * -1;
    }

    private int checkAmmo()
    {
        int ammoCheck = 0;
        foreach (string item in playerC.inventory)
        {
            if (item == "Ammo")
            {
                ammoCheck++;
            }
        }

        if (ammoCheck > maxAmmo)
        {
            useAmmo();
        }

        if (ammoCheck != ammo)
        {
            return ammoCheck;
        }

        return ammo;
    }

    private void useAmmo()
    {
        playerC.inventory.Remove("Ammo");
    }

}