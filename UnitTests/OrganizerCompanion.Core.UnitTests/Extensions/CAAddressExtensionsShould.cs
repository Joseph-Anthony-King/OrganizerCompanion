using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class CAAddressExtensionsShould
    {
        [Test, Category("Extensions")]
        public void GetName_WithValidProvinces_ShouldReturnCorrectNames()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(CAProvinces.Alberta.GetName(), Is.EqualTo("Alberta"));
                Assert.That(CAProvinces.BritishColumbia.GetName(), Is.EqualTo("British Columbia"));
                Assert.That(CAProvinces.Manitoba.GetName(), Is.EqualTo("Manitoba"));
                Assert.That(CAProvinces.NewBrunswick.GetName(), Is.EqualTo("New Brunswick"));
                Assert.That(CAProvinces.NewfoundlandAndLabrador.GetName(), Is.EqualTo("Newfoundland and Labrador"));
                Assert.That(CAProvinces.NovaScotia.GetName(), Is.EqualTo("Nova Scotia"));
                Assert.That(CAProvinces.Ontario.GetName(), Is.EqualTo("Ontario"));
                Assert.That(CAProvinces.PrinceEdwardIsland.GetName(), Is.EqualTo("Prince Edward Island"));
                Assert.That(CAProvinces.Quebec.GetName(), Is.EqualTo("Quebec"));
                Assert.That(CAProvinces.Saskatchewan.GetName(), Is.EqualTo("Saskatchewan"));
                Assert.That(CAProvinces.NorthwestTerritories.GetName(), Is.EqualTo("Northwest Territories"));
                Assert.That(CAProvinces.Nunavut.GetName(), Is.EqualTo("Nunavut"));
                Assert.That(CAProvinces.Yukon.GetName(), Is.EqualTo("Yukon"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithInvalidProvince_ShouldReturnEnumName()
        {
            // Arrange
            var invalidProvince = (CAProvinces)999;

            // Act
            var result = invalidProvince.GetName();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithValidProvinces_ShouldReturnCorrectAbbreviations()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(CAProvinces.Alberta.GetAbbreviation(), Is.EqualTo("AB"));
                Assert.That(CAProvinces.BritishColumbia.GetAbbreviation(), Is.EqualTo("BC"));
                Assert.That(CAProvinces.Manitoba.GetAbbreviation(), Is.EqualTo("MB"));
                Assert.That(CAProvinces.NewBrunswick.GetAbbreviation(), Is.EqualTo("NB"));
                Assert.That(CAProvinces.NewfoundlandAndLabrador.GetAbbreviation(), Is.EqualTo("NL"));
                Assert.That(CAProvinces.NovaScotia.GetAbbreviation(), Is.EqualTo("NS"));
                Assert.That(CAProvinces.Ontario.GetAbbreviation(), Is.EqualTo("ON"));
                Assert.That(CAProvinces.PrinceEdwardIsland.GetAbbreviation(), Is.EqualTo("PE"));
                Assert.That(CAProvinces.Quebec.GetAbbreviation(), Is.EqualTo("QC"));
                Assert.That(CAProvinces.Saskatchewan.GetAbbreviation(), Is.EqualTo("SK"));
                Assert.That(CAProvinces.NorthwestTerritories.GetAbbreviation(), Is.EqualTo("NT"));
                Assert.That(CAProvinces.Nunavut.GetAbbreviation(), Is.EqualTo("NU"));
                Assert.That(CAProvinces.Yukon.GetAbbreviation(), Is.EqualTo("YT"));
            });
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithInvalidProvince_ShouldReturnEmptyString()
        {
            // Arrange
            var invalidProvince = (CAProvinces)999;

            // Act
            var result = invalidProvince.GetAbbreviation();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithValidProvinces_ShouldReturnCorrectStateModel()
        {
            // Arrange & Act
            var ontarioModel = CAProvinces.Ontario.ToStateModel();
            var quebecModel = CAProvinces.Quebec.ToStateModel();
            var britishColumbiaModel = CAProvinces.BritishColumbia.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ontarioModel.Name, Is.EqualTo("Ontario"));
                Assert.That(ontarioModel.Abbreviation, Is.EqualTo("ON"));

                Assert.That(quebecModel.Name, Is.EqualTo("Quebec"));
                Assert.That(quebecModel.Abbreviation, Is.EqualTo("QC"));

                Assert.That(britishColumbiaModel.Name, Is.EqualTo("British Columbia"));
                Assert.That(britishColumbiaModel.Abbreviation, Is.EqualTo("BC"));
            });
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithAllProvinces_ShouldCreateValidStateModels()
        {
            // Arrange
            var allProvinces = new[]
            {
                CAProvinces.Alberta, CAProvinces.BritishColumbia, CAProvinces.Manitoba, CAProvinces.NewBrunswick,
                CAProvinces.NewfoundlandAndLabrador, CAProvinces.NovaScotia, CAProvinces.Ontario, CAProvinces.PrinceEdwardIsland,
                CAProvinces.Quebec, CAProvinces.Saskatchewan, CAProvinces.NorthwestTerritories, CAProvinces.Nunavut, CAProvinces.Yukon
            };

            // Act & Assert
            foreach (var province in allProvinces)
            {
                var model = province.ToStateModel();
                
                Assert.That(model, Is.Not.Null);
                Assert.That(model.Name, Is.Not.Null.And.Not.Empty);
                Assert.That(model.Abbreviation, Is.Not.Null.And.Not.Empty);
                Assert.That(model.Abbreviation, Has.Length.EqualTo(2));
            }
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithInvalidProvince_ShouldReturnModelWithEnumNameAndEmptyAbbreviation()
        {
            // Arrange
            var invalidProvince = (CAProvinces)999;

            // Act
            var result = invalidProvince.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo("999"));
                Assert.That(result.Abbreviation, Is.EqualTo(string.Empty));
            });
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithProvincesContainingSpaces_ShouldHandleCorrectly()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                var britishColumbiaModel = CAProvinces.BritishColumbia.ToStateModel();
                Assert.That(britishColumbiaModel.Name, Is.EqualTo("British Columbia"));
                Assert.That(britishColumbiaModel.Abbreviation, Is.EqualTo("BC"));

                var newBrunswickModel = CAProvinces.NewBrunswick.ToStateModel();
                Assert.That(newBrunswickModel.Name, Is.EqualTo("New Brunswick"));
                Assert.That(newBrunswickModel.Abbreviation, Is.EqualTo("NB"));

                var newfoundlandAndLabradorModel = CAProvinces.NewfoundlandAndLabrador.ToStateModel();
                Assert.That(newfoundlandAndLabradorModel.Name, Is.EqualTo("Newfoundland and Labrador"));
                Assert.That(newfoundlandAndLabradorModel.Abbreviation, Is.EqualTo("NL"));

                var novaScotiaModel = CAProvinces.NovaScotia.ToStateModel();
                Assert.That(novaScotiaModel.Name, Is.EqualTo("Nova Scotia"));
                Assert.That(novaScotiaModel.Abbreviation, Is.EqualTo("NS"));

                var princeEdwardIslandModel = CAProvinces.PrinceEdwardIsland.ToStateModel();
                Assert.That(princeEdwardIslandModel.Name, Is.EqualTo("Prince Edward Island"));
                Assert.That(princeEdwardIslandModel.Abbreviation, Is.EqualTo("PE"));

                var northwestTerritoriesModel = CAProvinces.NorthwestTerritories.ToStateModel();
                Assert.That(northwestTerritoriesModel.Name, Is.EqualTo("Northwest Territories"));
                Assert.That(northwestTerritoriesModel.Abbreviation, Is.EqualTo("NT"));
            });
        }

        [Test, Category("Extensions")]
        public void ProvinceEnumAndDictionary_ShouldHaveSameCount()
        {
            // This test ensures all enum values are mapped in the dictionary
            // Arrange
            var enumValues = System.Enum.GetValues<CAProvinces>();
            
            // Act & Assert
            foreach (var province in enumValues)
            {
                // Both methods should return non-empty results
                var name = province.GetName();
                var abbreviation = province.GetAbbreviation();
                
                Assert.Multiple(() =>
                {
                    Assert.That(name, Is.Not.Null.And.Not.Empty, $"Name missing for {province}");
                    Assert.That(abbreviation, Is.Not.Null.And.Not.Empty, $"Abbreviation missing for {province}");
                    Assert.That(abbreviation.Length, Is.EqualTo(2), $"Abbreviation should be 2 chars for {province}");
                });
            }
        }

        [Test, Category("Extensions")]
        public void GetName_WithDifferentCallsToSameProvince_ShouldReturnConsistentResults()
        {
            // Arrange
            var province = CAProvinces.Ontario;

            // Act
            var name1 = province.GetName();
            var name2 = province.GetName();
            var name3 = province.GetName();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(name1, Is.EqualTo(name2));
                Assert.That(name2, Is.EqualTo(name3));
                Assert.That(name1, Is.EqualTo("Ontario"));
            });
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithDifferentCallsToSameProvince_ShouldReturnConsistentResults()
        {
            // Arrange
            var province = CAProvinces.Quebec;

            // Act
            var abbreviation1 = province.GetAbbreviation();
            var abbreviation2 = province.GetAbbreviation();
            var abbreviation3 = province.GetAbbreviation();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(abbreviation1, Is.EqualTo(abbreviation2));
                Assert.That(abbreviation2, Is.EqualTo(abbreviation3));
                Assert.That(abbreviation1, Is.EqualTo("QC"));
            });
        }
    }
}
