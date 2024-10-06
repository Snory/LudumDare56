using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LandZoneManager : MonoBehaviour
{
    [SerializeField]
    private List<LandZone> _landZones;

    [SerializeField]
    private GeneralEvent _gameWon;

    private void Awake()
    {
        _landZones = new List<LandZone>();
        var landZones = FindObjectsByType(typeof(LandZone), FindObjectsInactive.Include, FindObjectsSortMode.None);
        _landZones.AddRange(landZones.Select(l => (LandZone) l));
    }

    private void Start()
    {
        foreach (var landZone in _landZones)
        {
            landZone.LandzoneDeactivated.AddListener(CheckLandzone);
        }
    }

    public void CheckLandzone()
    {
        Debug.Log("Checking landzone");
        foreach (var landZone in _landZones)
        {
            if (landZone.IsLandzoneActive())
            {
                Debug.Log("Landzone is active");
                return;
            }
        }

        _gameWon.Raise();
    }
}
