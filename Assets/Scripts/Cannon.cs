using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private Projection _projection;

    private void Update() {
        HandleControls();
        _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force);
    }
    

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _force = 20;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private Transform _barrelPivot;
    [SerializeField] private float _rotateSpeed = 30;
    
    private void HandleControls() {
        print(_barrelPivot.transform.rotation.x);
        if (Input.GetKey(KeyCode.S) && _barrelPivot.transform.rotation.x <= 0.35) _barrelPivot.Rotate(Vector3.right * _rotateSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.W) && _barrelPivot.transform.rotation.x >= -0.40) _barrelPivot.Rotate(Vector3.left * _rotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q) && _force >= 1)
        {
            _force -= 30 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E) && _force <= 100)
        {
            _force += 30 * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0)) {
            var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);

            spawned.Init(_ballSpawn.forward * _force, false);
        }
    }
    
}