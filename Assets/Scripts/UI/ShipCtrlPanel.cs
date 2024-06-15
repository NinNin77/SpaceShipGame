using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipCtrlPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;

    private GameObject _obj;
    // SpaceshipÇÃèÓïÒ (ss=SpaceShip)
    private GameObject _spaceship;
    private HealthSystem _ssHealthSystem;
    private MainEngine _mainEngine;
    private SubEngine _subEngine;

    void Start()
    {
        _spaceship = GameObject.Find("Spaceship");
        _ssHealthSystem = _spaceship.GetComponent<HealthSystem>();
        _obj = GameObject.Find("mainEngine");
        _mainEngine = _obj.GetComponent<MainEngine>();
        _obj = GameObject.Find("subEngine");
        _subEngine = _obj.GetComponent<SubEngine>();
    }

    void Update()
    {
        // Health
        string health = _ssHealthSystem._health.ToString();
        _text.SetText($"Health: {health}");
        
    }
}
