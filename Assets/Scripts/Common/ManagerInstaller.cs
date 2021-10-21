using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;

public class ManagerInstaller : MonoInstaller
{
    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _gameManager;

    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _dialogService;

    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _timeManager;

    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _sceneManager;

    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _bonusManager;

    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _dataManager;

    [BoxGroup("Installers")]
    [SerializeField]
    private GameObject _poolManager;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentsOn(_gameManager).AsSingle();
        Container.Bind<DialogService>().FromComponentsOn(_dialogService).AsSingle();
        Container.Bind<TimeManager>().FromComponentsOn(_timeManager).AsSingle();
        Container.Bind<SceneManager>().FromComponentsOn(_sceneManager).AsSingle();
        Container.Bind<BonusManager>().FromComponentsOn(_bonusManager).AsSingle();
        Container.Bind<DataManager>().FromComponentsOn(_dataManager).AsSingle();
        Container.Bind<PoolManager>().FromComponentsOn(_poolManager).AsSingle();
    }
}
