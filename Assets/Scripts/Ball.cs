using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    //[SerializeField] private AudioSource _source;
    //[SerializeField] private AudioClip[] _clips;
    private bool _isGhost;

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        if (_isGhost) return;
        //Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        //_source.clip = _clips[Random.Range(0, _clips.Length)];
        //_source.Play();

        if (col.gameObject.CompareTag("Ball") || col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("PlayerModel"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col.collider, true);
        }
    }
}