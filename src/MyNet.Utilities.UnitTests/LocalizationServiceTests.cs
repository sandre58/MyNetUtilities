// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using MyNet.Utilities.Localization;
using MyNet.Utilities.UnitTests.Data;
using Xunit;

namespace MyNet.Utilities.UnitTests
{
    public class LocalizationServiceTests
    {
        public LocalizationServiceTests() : base() => TranslationService.RegisterResources(nameof(DataResources), DataResources.ResourceManager);

        [Fact]
        public void CurrentCulture()
        {
            CultureInfoService.Current.SetCulture("fr-FR");
            var culture = TranslationService.Current.Culture;

            Assert.Equal(CultureInfo.CurrentCulture, culture);
        }

        [Fact]
        public void SetCulture()
        {
            CultureInfoService.Current.SetCulture("en-US");

            Assert.Equal("en-US", CultureInfo.CurrentCulture.Name);
        }

        [Fact]
        public void CultureNeutral()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttributes<NeutralResourcesLanguageAttribute>().FirstOrDefault();

            Assert.NotNull(attr);

            CultureInfoService.Current.SetCulture(attr.CultureName);

            Assert.Equal("Valeur Une", DataResources.Value1);
        }

        [Fact]
        public void CultureEn()
        {
            CultureInfoService.Current.SetCulture("en");

            Assert.Equal("Value One", DataResources.Value1);
        }

        [Fact]
        public void CultureEs()
        {
            CultureInfoService.Current.SetCulture("es-ES");

            Assert.Equal("Valor Uno", DataResources.Value1);
        }

        [Fact]
        public void GetString()
        {
            CultureInfoService.Current.SetCulture("fr-FR");

            Assert.Equal("Valeur Une", TranslationService.Current.Translate(nameof(DataResources.Value1)));
        }

        [Fact]
        public void GetStringEs()
        {
            CultureInfoService.Current.SetCulture("es-ES");

            Assert.Equal("Valor Uno", TranslationService.Current.Translate(nameof(DataResources.Value1)));
        }

        [Fact]
        public void GetStringItWithResources()
        {
            TranslationService.RegisterResources(nameof(OtherDataResources), OtherDataResources.ResourceManager);
            CultureInfoService.Current.SetCulture("it-IT");

            Assert.Equal("Valore Una", TranslationService.Current.Translate(nameof(DataResources.Value1)));
        }
    }
}
