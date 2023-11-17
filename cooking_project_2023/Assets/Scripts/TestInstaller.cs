using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LasyDI;

public class TestInstaller : MonoInstaller
{
    public TestInstaller()
    {
    }

    public override void OnInstall()
    {
        LasyContainer.Bind<A>().AsSingle();
        LasyContainer.Bind<B>().AsSingle();
        var obj = LasyContainer.GetObject<A>();
        obj.Test();
    }
}
