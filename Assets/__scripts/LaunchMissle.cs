using UnityEngine;

public class LaunchMissle : MonoBehaviour
{
    [SerializeField] private GameObject _missle;
    [SerializeField] private GameObject _plane;

    public void LaunchBehaviorr()
    {
        GameObject newMissle = Instantiate(_missle);
        newMissle.transform.rotation = _plane.transform.rotation;
        newMissle.transform.position = new Vector3(_plane.transform.position.x,
                                                   _plane.transform.position.y,
                                                   _plane.transform.position.z);
    }
}