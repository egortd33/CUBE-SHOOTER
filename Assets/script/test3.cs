using UnityEngine;

public class test3 : MonoBehaviour
{
    private GameObject gameObjectA;



    public GameObject GameObjectB => gameObjectB;
    private GameObject gameObjectB;


    [SerializeField] private GameObject _gameObject;


    private void Start()
    {
        string NameDoorA = _gameObject.name;

        gameObjectB = gameObject;
    }

    public void OpenDoor() 
    {
        string NameDoorA = _gameObject.name;
    }

    public string NameDoor()
    {
        return _gameObject.name;
    }
}
