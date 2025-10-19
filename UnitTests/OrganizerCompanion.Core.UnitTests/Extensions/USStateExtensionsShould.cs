using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class USStateExtensionsShould
    {
        [Test, Category("Extensions")]
        public void GetName_WithValidStates_ShouldReturnCorrectNames()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(USStates.Alabama.GetName(), Is.EqualTo("Alabama"));
                Assert.That(USStates.Alaska.GetName(), Is.EqualTo("Alaska"));
                Assert.That(USStates.Arizona.GetName(), Is.EqualTo("Arizona"));
                Assert.That(USStates.Arkansas.GetName(), Is.EqualTo("Arkansas"));
                Assert.That(USStates.California.GetName(), Is.EqualTo("California"));
                Assert.That(USStates.Colorado.GetName(), Is.EqualTo("Colorado"));
                Assert.That(USStates.Connecticut.GetName(), Is.EqualTo("Connecticut"));
                Assert.That(USStates.Delaware.GetName(), Is.EqualTo("Delaware"));
                Assert.That(USStates.Florida.GetName(), Is.EqualTo("Florida"));
                Assert.That(USStates.Georgia.GetName(), Is.EqualTo("Georgia"));
                Assert.That(USStates.Hawaii.GetName(), Is.EqualTo("Hawaii"));
                Assert.That(USStates.Idaho.GetName(), Is.EqualTo("Idaho"));
                Assert.That(USStates.Illinois.GetName(), Is.EqualTo("Illinois"));
                Assert.That(USStates.Indiana.GetName(), Is.EqualTo("Indiana"));
                Assert.That(USStates.Iowa.GetName(), Is.EqualTo("Iowa"));
                Assert.That(USStates.Kansas.GetName(), Is.EqualTo("Kansas"));
                Assert.That(USStates.Kentucky.GetName(), Is.EqualTo("Kentucky"));
                Assert.That(USStates.Louisiana.GetName(), Is.EqualTo("Louisiana"));
                Assert.That(USStates.Maine.GetName(), Is.EqualTo("Maine"));
                Assert.That(USStates.Maryland.GetName(), Is.EqualTo("Maryland"));
                Assert.That(USStates.Massachusetts.GetName(), Is.EqualTo("Massachusetts"));
                Assert.That(USStates.Michigan.GetName(), Is.EqualTo("Michigan"));
                Assert.That(USStates.Minnesota.GetName(), Is.EqualTo("Minnesota"));
                Assert.That(USStates.Mississippi.GetName(), Is.EqualTo("Mississippi"));
                Assert.That(USStates.Missouri.GetName(), Is.EqualTo("Missouri"));
                Assert.That(USStates.Montana.GetName(), Is.EqualTo("Montana"));
                Assert.That(USStates.Nebraska.GetName(), Is.EqualTo("Nebraska"));
                Assert.That(USStates.Nevada.GetName(), Is.EqualTo("Nevada"));
                Assert.That(USStates.NewHampshire.GetName(), Is.EqualTo("New Hampshire"));
                Assert.That(USStates.NewJersey.GetName(), Is.EqualTo("New Jersey"));
                Assert.That(USStates.NewMexico.GetName(), Is.EqualTo("New Mexico"));
                Assert.That(USStates.NewYork.GetName(), Is.EqualTo("New York"));
                Assert.That(USStates.NorthCarolina.GetName(), Is.EqualTo("North Carolina"));
                Assert.That(USStates.NorthDakota.GetName(), Is.EqualTo("North Dakota"));
                Assert.That(USStates.Ohio.GetName(), Is.EqualTo("Ohio"));
                Assert.That(USStates.Oklahoma.GetName(), Is.EqualTo("Oklahoma"));
                Assert.That(USStates.Oregon.GetName(), Is.EqualTo("Oregon"));
                Assert.That(USStates.Pennsylvania.GetName(), Is.EqualTo("Pennsylvania"));
                Assert.That(USStates.RhodeIsland.GetName(), Is.EqualTo("Rhode Island"));
                Assert.That(USStates.SouthCarolina.GetName(), Is.EqualTo("South Carolina"));
                Assert.That(USStates.SouthDakota.GetName(), Is.EqualTo("South Dakota"));
                Assert.That(USStates.Tennessee.GetName(), Is.EqualTo("Tennessee"));
                Assert.That(USStates.Texas.GetName(), Is.EqualTo("Texas"));
                Assert.That(USStates.Utah.GetName(), Is.EqualTo("Utah"));
                Assert.That(USStates.Vermont.GetName(), Is.EqualTo("Vermont"));
                Assert.That(USStates.Virginia.GetName(), Is.EqualTo("Virginia"));
                Assert.That(USStates.Washington.GetName(), Is.EqualTo("Washington"));
                Assert.That(USStates.WestVirginia.GetName(), Is.EqualTo("West Virginia"));
                Assert.That(USStates.Wisconsin.GetName(), Is.EqualTo("Wisconsin"));
                Assert.That(USStates.Wyoming.GetName(), Is.EqualTo("Wyoming"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithInvalidState_ShouldReturnEnumName()
        {
            // Arrange
            var invalidState = (USStates)999;

            // Act
            var result = invalidState.GetName();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithValidStates_ShouldReturnCorrectAbbreviations()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(USStates.Alabama.GetAbbreviation(), Is.EqualTo("AL"));
                Assert.That(USStates.Alaska.GetAbbreviation(), Is.EqualTo("AK"));
                Assert.That(USStates.Arizona.GetAbbreviation(), Is.EqualTo("AZ"));
                Assert.That(USStates.Arkansas.GetAbbreviation(), Is.EqualTo("AR"));
                Assert.That(USStates.California.GetAbbreviation(), Is.EqualTo("CA"));
                Assert.That(USStates.Colorado.GetAbbreviation(), Is.EqualTo("CO"));
                Assert.That(USStates.Connecticut.GetAbbreviation(), Is.EqualTo("CT"));
                Assert.That(USStates.Delaware.GetAbbreviation(), Is.EqualTo("DE"));
                Assert.That(USStates.Florida.GetAbbreviation(), Is.EqualTo("FL"));
                Assert.That(USStates.Georgia.GetAbbreviation(), Is.EqualTo("GA"));
                Assert.That(USStates.Hawaii.GetAbbreviation(), Is.EqualTo("HI"));
                Assert.That(USStates.Idaho.GetAbbreviation(), Is.EqualTo("ID"));
                Assert.That(USStates.Illinois.GetAbbreviation(), Is.EqualTo("IL"));
                Assert.That(USStates.Indiana.GetAbbreviation(), Is.EqualTo("IN"));
                Assert.That(USStates.Iowa.GetAbbreviation(), Is.EqualTo("IA"));
                Assert.That(USStates.Kansas.GetAbbreviation(), Is.EqualTo("KS"));
                Assert.That(USStates.Kentucky.GetAbbreviation(), Is.EqualTo("KY"));
                Assert.That(USStates.Louisiana.GetAbbreviation(), Is.EqualTo("LA"));
                Assert.That(USStates.Maine.GetAbbreviation(), Is.EqualTo("ME"));
                Assert.That(USStates.Maryland.GetAbbreviation(), Is.EqualTo("MD"));
                Assert.That(USStates.Massachusetts.GetAbbreviation(), Is.EqualTo("MA"));
                Assert.That(USStates.Michigan.GetAbbreviation(), Is.EqualTo("MI"));
                Assert.That(USStates.Minnesota.GetAbbreviation(), Is.EqualTo("MN"));
                Assert.That(USStates.Mississippi.GetAbbreviation(), Is.EqualTo("MS"));
                Assert.That(USStates.Missouri.GetAbbreviation(), Is.EqualTo("MO"));
                Assert.That(USStates.Montana.GetAbbreviation(), Is.EqualTo("MT"));
                Assert.That(USStates.Nebraska.GetAbbreviation(), Is.EqualTo("NE"));
                Assert.That(USStates.Nevada.GetAbbreviation(), Is.EqualTo("NV"));
                Assert.That(USStates.NewHampshire.GetAbbreviation(), Is.EqualTo("NH"));
                Assert.That(USStates.NewJersey.GetAbbreviation(), Is.EqualTo("NJ"));
                Assert.That(USStates.NewMexico.GetAbbreviation(), Is.EqualTo("NM"));
                Assert.That(USStates.NewYork.GetAbbreviation(), Is.EqualTo("NY"));
                Assert.That(USStates.NorthCarolina.GetAbbreviation(), Is.EqualTo("NC"));
                Assert.That(USStates.NorthDakota.GetAbbreviation(), Is.EqualTo("ND"));
                Assert.That(USStates.Ohio.GetAbbreviation(), Is.EqualTo("OH"));
                Assert.That(USStates.Oklahoma.GetAbbreviation(), Is.EqualTo("OK"));
                Assert.That(USStates.Oregon.GetAbbreviation(), Is.EqualTo("OR"));
                Assert.That(USStates.Pennsylvania.GetAbbreviation(), Is.EqualTo("PA"));
                Assert.That(USStates.RhodeIsland.GetAbbreviation(), Is.EqualTo("RI"));
                Assert.That(USStates.SouthCarolina.GetAbbreviation(), Is.EqualTo("SC"));
                Assert.That(USStates.SouthDakota.GetAbbreviation(), Is.EqualTo("SD"));
                Assert.That(USStates.Tennessee.GetAbbreviation(), Is.EqualTo("TN"));
                Assert.That(USStates.Texas.GetAbbreviation(), Is.EqualTo("TX"));
                Assert.That(USStates.Utah.GetAbbreviation(), Is.EqualTo("UT"));
                Assert.That(USStates.Vermont.GetAbbreviation(), Is.EqualTo("VT"));
                Assert.That(USStates.Virginia.GetAbbreviation(), Is.EqualTo("VA"));
                Assert.That(USStates.Washington.GetAbbreviation(), Is.EqualTo("WA"));
                Assert.That(USStates.WestVirginia.GetAbbreviation(), Is.EqualTo("WV"));
                Assert.That(USStates.Wisconsin.GetAbbreviation(), Is.EqualTo("WI"));
                Assert.That(USStates.Wyoming.GetAbbreviation(), Is.EqualTo("WY"));
            });
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithInvalidState_ShouldReturnEmptyString()
        {
            // Arrange
            var invalidState = (USStates)999;

            // Act
            var result = invalidState.GetAbbreviation();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithValidStates_ShouldReturnCorrectStateModel()
        {
            // Arrange & Act
            var californiaModel = USStates.California.ToStateModel();
            var texasModel = USStates.Texas.ToStateModel();
            var newYorkModel = USStates.NewYork.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(californiaModel.Name, Is.EqualTo("California"));
                Assert.That(californiaModel.Abbreviation, Is.EqualTo("CA"));

                Assert.That(texasModel.Name, Is.EqualTo("Texas"));
                Assert.That(texasModel.Abbreviation, Is.EqualTo("TX"));

                Assert.That(newYorkModel.Name, Is.EqualTo("New York"));
                Assert.That(newYorkModel.Abbreviation, Is.EqualTo("NY"));
            });
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithAllStates_ShouldCreateValidStateModels()
        {
            // Arrange
            var allStates = new[]
            {
                USStates.Alabama, USStates.Alaska, USStates.Arizona, USStates.Arkansas, USStates.California,
                USStates.Colorado, USStates.Connecticut, USStates.Delaware, USStates.Florida, USStates.Georgia,
                USStates.Hawaii, USStates.Idaho, USStates.Illinois, USStates.Indiana, USStates.Iowa,
                USStates.Kansas, USStates.Kentucky, USStates.Louisiana, USStates.Maine, USStates.Maryland,
                USStates.Massachusetts, USStates.Michigan, USStates.Minnesota, USStates.Mississippi, USStates.Missouri,
                USStates.Montana, USStates.Nebraska, USStates.Nevada, USStates.NewHampshire, USStates.NewJersey,
                USStates.NewMexico, USStates.NewYork, USStates.NorthCarolina, USStates.NorthDakota, USStates.Ohio,
                USStates.Oklahoma, USStates.Oregon, USStates.Pennsylvania, USStates.RhodeIsland, USStates.SouthCarolina,
                USStates.SouthDakota, USStates.Tennessee, USStates.Texas, USStates.Utah, USStates.Vermont,
                USStates.Virginia, USStates.Washington, USStates.WestVirginia, USStates.Wisconsin, USStates.Wyoming
            };

            // Act & Assert
            foreach (var state in allStates)
      {
        var model = state.ToStateModel();
                
                Assert.That(model, Is.Not.Null);
        Assert.Multiple(() =>
        {
          Assert.That(model.Name, Is.Not.Null.And.Not.Empty);
          Assert.That(model.Abbreviation, Is.Not.Null.And.Not.Empty);
        });
        Assert.That(model.Abbreviation, Has.Length.EqualTo(2));
      }
    }

        [Test, Category("Extensions")]
        public void ToStateModel_WithInvalidState_ShouldReturnModelWithEnumNameAndEmptyAbbreviation()
        {
            // Arrange
            var invalidState = (USStates)999;

            // Act
            var result = invalidState.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo("999"));
                Assert.That(result.Abbreviation, Is.EqualTo(string.Empty));
            });
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithStatesContainingSpaces_ShouldHandleCorrectly()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                var newHampshireModel = USStates.NewHampshire.ToStateModel();
                Assert.That(newHampshireModel.Name, Is.EqualTo("New Hampshire"));
                Assert.That(newHampshireModel.Abbreviation, Is.EqualTo("NH"));

                var newJerseyModel = USStates.NewJersey.ToStateModel();
                Assert.That(newJerseyModel.Name, Is.EqualTo("New Jersey"));
                Assert.That(newJerseyModel.Abbreviation, Is.EqualTo("NJ"));

                var newMexicoModel = USStates.NewMexico.ToStateModel();
                Assert.That(newMexicoModel.Name, Is.EqualTo("New Mexico"));
                Assert.That(newMexicoModel.Abbreviation, Is.EqualTo("NM"));

                var newYorkModel = USStates.NewYork.ToStateModel();
                Assert.That(newYorkModel.Name, Is.EqualTo("New York"));
                Assert.That(newYorkModel.Abbreviation, Is.EqualTo("NY"));

                var northCarolinaModel = USStates.NorthCarolina.ToStateModel();
                Assert.That(northCarolinaModel.Name, Is.EqualTo("North Carolina"));
                Assert.That(northCarolinaModel.Abbreviation, Is.EqualTo("NC"));

                var northDakotaModel = USStates.NorthDakota.ToStateModel();
                Assert.That(northDakotaModel.Name, Is.EqualTo("North Dakota"));
                Assert.That(northDakotaModel.Abbreviation, Is.EqualTo("ND"));

                var rhodeIslandModel = USStates.RhodeIsland.ToStateModel();
                Assert.That(rhodeIslandModel.Name, Is.EqualTo("Rhode Island"));
                Assert.That(rhodeIslandModel.Abbreviation, Is.EqualTo("RI"));

                var southCarolinaModel = USStates.SouthCarolina.ToStateModel();
                Assert.That(southCarolinaModel.Name, Is.EqualTo("South Carolina"));
                Assert.That(southCarolinaModel.Abbreviation, Is.EqualTo("SC"));

                var southDakotaModel = USStates.SouthDakota.ToStateModel();
                Assert.That(southDakotaModel.Name, Is.EqualTo("South Dakota"));
                Assert.That(southDakotaModel.Abbreviation, Is.EqualTo("SD"));

                var westVirginiaModel = USStates.WestVirginia.ToStateModel();
                Assert.That(westVirginiaModel.Name, Is.EqualTo("West Virginia"));
                Assert.That(westVirginiaModel.Abbreviation, Is.EqualTo("WV"));
            });
        }
    }
}
