using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [InspectorName("Player Input")]

    [SerializeField] private Joystick _joystick;

    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_joystick);

        Container.Bind<ButtonProvider>().FromComponentInHierarchy().AsSingle();
    }
}
