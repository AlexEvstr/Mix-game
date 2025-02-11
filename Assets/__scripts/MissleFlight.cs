using UnityEngine;

public class MissleFlight : MonoBehaviour
{
    [SerializeField] private GameObject _badExplosion;
    [SerializeField] private GameObject _goodExplosion;

    private ButtonsController buttonsController;

    private float _speed = 10.0f;

    private CameraShake _cameraShake;
    private HeartsBehavior _heartsBehavior;
    private int _canEffect;

    private void Start()
    {
        buttonsController = GameObject.FindGameObjectWithTag("Manager").GetComponent<ButtonsController>();
        _cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        _heartsBehavior = GameObject.FindGameObjectWithTag("Controller").GetComponent<HeartsBehavior>();
        _canEffect = PlayerPrefs.GetInt("isEffectsEnabled", 1);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Construct"))
        {
            buttonsController.PlayExplosionSound();

            if (_canEffect == 1)
            {
                _cameraShake.Shake();
                GameObject badExplosion = Instantiate(_badExplosion);
                badExplosion.transform.position = transform.position;
                Destroy(badExplosion, 0.5f);
            }
            

            Destroy(gameObject);
            _heartsBehavior.LoveHeart();
        }

        else if (collision.gameObject.CompareTag("Border"))
        {
            if (_canEffect == 1)
            {
                GameObject goodExplosion = Instantiate(_goodExplosion);
                goodExplosion.transform.position = transform.position;
                Destroy(goodExplosion, 0.5f);
            }

            buttonsController.PlayGoodShotSound();     
            Destroy(gameObject);
            ScoreController.Score += 10;
        }
    }
}