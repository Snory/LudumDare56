using UnityEngine;
using UnityEngine.Events;

public class LandZone : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    private bool _active;

    [SerializeField]
    private GameObject _tinyCreature;

    public UnityEvent LandzoneDeactivated;

    private void Awake()
    {
        _active = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer.material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_active)
        {
            return;
        }

        if (!other.CompareTag("Collector"))
        {
            return;
        }

        // Calculate the alignment of the up vector of the ship and the up vector of the landing zone
        float alignment = Vector3.Dot(other.transform.up, transform.up);

        // If the alignment is greater than 0.9, the ship is aligned with the landing zone
        if (alignment > 0.9f)
        {
            _renderer.material.color = Color.green;
            _active = false;
            _tinyCreature.SetActive(false);
            LandzoneDeactivated.Invoke();
        }
    }

    public bool IsLandzoneActive()
    {
        return _active;
    }
}
