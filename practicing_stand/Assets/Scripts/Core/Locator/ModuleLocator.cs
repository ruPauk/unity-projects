using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModuleLocator
{
    public static void AddModule<T>(T module)
        where T: IModule
    {
        ModuleImplementation<T>.Module = module;
    }

    public static T GetModule<T>()
        where T : IModule
    {
        return ModuleImplementation<T>.Module;
    }

    private static class ModuleImplementation<T>
        where T : IModule
    {
        public static T Module;
    }
}
