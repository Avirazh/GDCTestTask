using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(WeaponHolder), typeof(TargetFinder))]
public class PlayerInput : MonoBehaviour, IMovable
{
    [SerializeField] public GameObject Weapon;
    [SerializeField] public LayerMask LayerMask;

    private Button _shootButton;
    private Button _inventoryButton;

    private Rigidbody2D _rigidbody;
    private Joystick _joystick;
    private UnitMovement _playerMovement;
    
    private WeaponHolder _weaponHolder;
    private TargetFinder _targetFinder;

    private PlayerConfig _playerConfig;

    public float Speed => _playerConfig.Speed;
    public Transform Transform => transform;


    [Inject]
    private void Construct(ButtonProvider buttonProvider, Joystick joystick, PlayerConfig playerConfig)
    {
        _shootButton = buttonProvider.ShootButton;
        _inventoryButton = buttonProvider.InventoryButton;
        _joystick = joystick;
        _playerConfig = playerConfig;
    }

    private void OnEnable()
    {
        _shootButton.onClick.AddListener(OnShootButtonPressed);
        _inventoryButton.onClick.AddListener(OnInventoryButtonPressed);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerMovement = new UnitMovement(transform, _rigidbody);

        _weaponHolder = GetComponent<WeaponHolder>();
        _targetFinder = GetComponent<TargetFinder>();

        _weaponHolder.SetDependencies(CreateWeapon().GetComponent<Weapon>(), LayerMask);
    }
    private void Update()
    {

    }
    void FixedUpdate()
    {
        _playerMovement.Move(new Vector2(_joystick.Horizontal, _joystick.Vertical), _playerConfig.Speed);
    }

    private void OnDisable()
    {
        _shootButton.onClick.RemoveAllListeners();
        _inventoryButton.onClick.RemoveAllListeners();
    }

    private void OnShootButtonPressed()
    {
        if (_targetFinder.TryGetTarget(out var target))
            _weaponHolder.Shoot(target);
        else
            _weaponHolder.ResetWeaponRotation();
    }
    private void OnInventoryButtonPressed() 
    {

    }
    private GameObject CreateWeapon()
    {
        return Instantiate(Weapon, transform);
    }
}
