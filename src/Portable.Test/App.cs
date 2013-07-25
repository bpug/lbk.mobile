//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="App.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace PortableTest
{
    using Cirrious.CrossCore.IoC;
    using Cirrious.MvvmCross.ViewModels;

    using PortableTest.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            this.CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();

            this.RegisterAppStart<FirstViewModel>();
        }
    }
}