using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [InspectorName("Player Input")]

    [SerializeField] private Joystick _joystick;

    //[InspectorName("Game Buttons")]

    //[SerializeField] private Button _fireButton;
    //[SerializeField] private Button _inventoryButton;
    //[SerializeField] private Button _pauseButton;

    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_joystick);
    }
}
