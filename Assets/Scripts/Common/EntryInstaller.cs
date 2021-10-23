using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;

public class EntryInstaller : MonoInstaller
{
    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _sceneManager;
    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _dataManager;

    public override void InstallBindings()
    {
        Container.Bind<SceneSwitcher>().FromComponentsOn(_sceneManager).AsSingle();
        Container.Bind<DataManager>().FromComponentsOn(_dataManager).AsSingle();

    }
}
