﻿using AutoBogus;
using System.Collections.Generic;

namespace Systore.Tests.Common.Builders
{
    public class GenerictBuilder<T> where T : class
    {
        private readonly T _instance;

        public GenerictBuilder()
        {
            _instance = GetAutoFaker().Generate();
        }

        private AutoFaker<T> GetAutoFaker()
        {
            return new AutoFaker<T>("pt_BR");
        }

        public T BuildAuto() => _instance;

        public IEnumerable<T> BuildListAuto(int count) => GetAutoFaker().GenerateLazy(count);
    }
}
