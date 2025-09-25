using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class StateExtensionsShould
    {
        [Test, Category("Extensions")]
        public void GetName_WithValidStates_ShouldReturnCorrectNames()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(States.Alabama.GetName(), Is.EqualTo("Alabama"));
                Assert.That(States.Alaska.GetName(), Is.EqualTo("Alaska"));
                Assert.That(States.Arizona.GetName(), Is.EqualTo("Arizona"));
                Assert.That(States.Arkansas.GetName(), Is.EqualTo("Arkansas"));
                Assert.That(States.California.GetName(), Is.EqualTo("California"));
                Assert.That(States.Colorado.GetName(), Is.EqualTo("Colorado"));
                Assert.That(States.Connecticut.GetName(), Is.EqualTo("Connecticut"));
                Assert.That(States.Delaware.GetName(), Is.EqualTo("Delaware"));
                Assert.That(States.Florida.GetName(), Is.EqualTo("Florida"));
                Assert.That(States.Georgia.GetName(), Is.EqualTo("Georgia"));
                Assert.That(States.Hawaii.GetName(), Is.EqualTo("Hawaii"));
                Assert.That(States.Idaho.GetName(), Is.EqualTo("Idaho"));
                Assert.That(States.Illinois.GetName(), Is.EqualTo("Illinois"));
                Assert.That(States.Indiana.GetName(), Is.EqualTo("Indiana"));
                Assert.That(States.Iowa.GetName(), Is.EqualTo("Iowa"));
                Assert.That(States.Kansas.GetName(), Is.EqualTo("Kansas"));
                Assert.That(States.Kentucky.GetName(), Is.EqualTo("Kentucky"));
                Assert.That(States.Louisiana.GetName(), Is.EqualTo("Louisiana"));
                Assert.That(States.Maine.GetName(), Is.EqualTo("Maine"));
                Assert.That(States.Maryland.GetName(), Is.EqualTo("Maryland"));
                Assert.That(States.Massachusetts.GetName(), Is.EqualTo("Massachusetts"));
                Assert.That(States.Michigan.GetName(), Is.EqualTo("Michigan"));
                Assert.That(States.Minnesota.GetName(), Is.EqualTo("Minnesota"));
                Assert.That(States.Mississippi.GetName(), Is.EqualTo("Mississippi"));
                Assert.That(States.Missouri.GetName(), Is.EqualTo("Missouri"));
                Assert.That(States.Montana.GetName(), Is.EqualTo("Montana"));
                Assert.That(States.Nebraska.GetName(), Is.EqualTo("Nebraska"));
                Assert.That(States.Nevada.GetName(), Is.EqualTo("Nevada"));
                Assert.That(States.NewHampshire.GetName(), Is.EqualTo("New Hampshire"));
                Assert.That(States.NewJersey.GetName(), Is.EqualTo("New Jersey"));
                Assert.That(States.NewMexico.GetName(), Is.EqualTo("New Mexico"));
                Assert.That(States.NewYork.GetName(), Is.EqualTo("New York"));
                Assert.That(States.NorthCarolina.GetName(), Is.EqualTo("North Carolina"));
                Assert.That(States.NorthDakota.GetName(), Is.EqualTo("North Dakota"));
                Assert.That(States.Ohio.GetName(), Is.EqualTo("Ohio"));
                Assert.That(States.Oklahoma.GetName(), Is.EqualTo("Oklahoma"));
                Assert.That(States.Oregon.GetName(), Is.EqualTo("Oregon"));
                Assert.That(States.Pennsylvania.GetName(), Is.EqualTo("Pennsylvania"));
                Assert.That(States.RhodeIsland.GetName(), Is.EqualTo("Rhode Island"));
                Assert.That(States.SouthCarolina.GetName(), Is.EqualTo("South Carolina"));
                Assert.That(States.SouthDakota.GetName(), Is.EqualTo("South Dakota"));
                Assert.That(States.Tennessee.GetName(), Is.EqualTo("Tennessee"));
                Assert.That(States.Texas.GetName(), Is.EqualTo("Texas"));
                Assert.That(States.Utah.GetName(), Is.EqualTo("Utah"));
                Assert.That(States.Vermont.GetName(), Is.EqualTo("Vermont"));
                Assert.That(States.Virginia.GetName(), Is.EqualTo("Virginia"));
                Assert.That(States.Washington.GetName(), Is.EqualTo("Washington"));
                Assert.That(States.WestVirginia.GetName(), Is.EqualTo("West Virginia"));
                Assert.That(States.Wisconsin.GetName(), Is.EqualTo("Wisconsin"));
                Assert.That(States.Wyoming.GetName(), Is.EqualTo("Wyoming"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithInvalidState_ShouldReturnEnumName()
        {
            // Arrange
            var invalidState = (States)999;

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
                Assert.That(States.Alabama.GetAbbreviation(), Is.EqualTo("AL"));
                Assert.That(States.Alaska.GetAbbreviation(), Is.EqualTo("AK"));
                Assert.That(States.Arizona.GetAbbreviation(), Is.EqualTo("AZ"));
                Assert.That(States.Arkansas.GetAbbreviation(), Is.EqualTo("AR"));
                Assert.That(States.California.GetAbbreviation(), Is.EqualTo("CA"));
                Assert.That(States.Colorado.GetAbbreviation(), Is.EqualTo("CO"));
                Assert.That(States.Connecticut.GetAbbreviation(), Is.EqualTo("CT"));
                Assert.That(States.Delaware.GetAbbreviation(), Is.EqualTo("DE"));
                Assert.That(States.Florida.GetAbbreviation(), Is.EqualTo("FL"));
                Assert.That(States.Georgia.GetAbbreviation(), Is.EqualTo("GA"));
                Assert.That(States.Hawaii.GetAbbreviation(), Is.EqualTo("HI"));
                Assert.That(States.Idaho.GetAbbreviation(), Is.EqualTo("ID"));
                Assert.That(States.Illinois.GetAbbreviation(), Is.EqualTo("IL"));
                Assert.That(States.Indiana.GetAbbreviation(), Is.EqualTo("IN"));
                Assert.That(States.Iowa.GetAbbreviation(), Is.EqualTo("IA"));
                Assert.That(States.Kansas.GetAbbreviation(), Is.EqualTo("KS"));
                Assert.That(States.Kentucky.GetAbbreviation(), Is.EqualTo("KY"));
                Assert.That(States.Louisiana.GetAbbreviation(), Is.EqualTo("LA"));
                Assert.That(States.Maine.GetAbbreviation(), Is.EqualTo("ME"));
                Assert.That(States.Maryland.GetAbbreviation(), Is.EqualTo("MD"));
                Assert.That(States.Massachusetts.GetAbbreviation(), Is.EqualTo("MA"));
                Assert.That(States.Michigan.GetAbbreviation(), Is.EqualTo("MI"));
                Assert.That(States.Minnesota.GetAbbreviation(), Is.EqualTo("MN"));
                Assert.That(States.Mississippi.GetAbbreviation(), Is.EqualTo("MS"));
                Assert.That(States.Missouri.GetAbbreviation(), Is.EqualTo("MO"));
                Assert.That(States.Montana.GetAbbreviation(), Is.EqualTo("MT"));
                Assert.That(States.Nebraska.GetAbbreviation(), Is.EqualTo("NE"));
                Assert.That(States.Nevada.GetAbbreviation(), Is.EqualTo("NV"));
                Assert.That(States.NewHampshire.GetAbbreviation(), Is.EqualTo("NH"));
                Assert.That(States.NewJersey.GetAbbreviation(), Is.EqualTo("NJ"));
                Assert.That(States.NewMexico.GetAbbreviation(), Is.EqualTo("NM"));
                Assert.That(States.NewYork.GetAbbreviation(), Is.EqualTo("NY"));
                Assert.That(States.NorthCarolina.GetAbbreviation(), Is.EqualTo("NC"));
                Assert.That(States.NorthDakota.GetAbbreviation(), Is.EqualTo("ND"));
                Assert.That(States.Ohio.GetAbbreviation(), Is.EqualTo("OH"));
                Assert.That(States.Oklahoma.GetAbbreviation(), Is.EqualTo("OK"));
                Assert.That(States.Oregon.GetAbbreviation(), Is.EqualTo("OR"));
                Assert.That(States.Pennsylvania.GetAbbreviation(), Is.EqualTo("PA"));
                Assert.That(States.RhodeIsland.GetAbbreviation(), Is.EqualTo("RI"));
                Assert.That(States.SouthCarolina.GetAbbreviation(), Is.EqualTo("SC"));
                Assert.That(States.SouthDakota.GetAbbreviation(), Is.EqualTo("SD"));
                Assert.That(States.Tennessee.GetAbbreviation(), Is.EqualTo("TN"));
                Assert.That(States.Texas.GetAbbreviation(), Is.EqualTo("TX"));
                Assert.That(States.Utah.GetAbbreviation(), Is.EqualTo("UT"));
                Assert.That(States.Vermont.GetAbbreviation(), Is.EqualTo("VT"));
                Assert.That(States.Virginia.GetAbbreviation(), Is.EqualTo("VA"));
                Assert.That(States.Washington.GetAbbreviation(), Is.EqualTo("WA"));
                Assert.That(States.WestVirginia.GetAbbreviation(), Is.EqualTo("WV"));
                Assert.That(States.Wisconsin.GetAbbreviation(), Is.EqualTo("WI"));
                Assert.That(States.Wyoming.GetAbbreviation(), Is.EqualTo("WY"));
            });
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithInvalidState_ShouldReturnEmptyString()
        {
            // Arrange
            var invalidState = (States)999;

            // Act
            var result = invalidState.GetAbbreviation();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithValidStates_ShouldReturnCorrectStateModel()
        {
            // Arrange & Act
            var californiaModel = States.California.ToStateModel();
            var texasModel = States.Texas.ToStateModel();
            var newYorkModel = States.NewYork.ToStateModel();

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
                States.Alabama, States.Alaska, States.Arizona, States.Arkansas, States.California,
                States.Colorado, States.Connecticut, States.Delaware, States.Florida, States.Georgia,
                States.Hawaii, States.Idaho, States.Illinois, States.Indiana, States.Iowa,
                States.Kansas, States.Kentucky, States.Louisiana, States.Maine, States.Maryland,
                States.Massachusetts, States.Michigan, States.Minnesota, States.Mississippi, States.Missouri,
                States.Montana, States.Nebraska, States.Nevada, States.NewHampshire, States.NewJersey,
                States.NewMexico, States.NewYork, States.NorthCarolina, States.NorthDakota, States.Ohio,
                States.Oklahoma, States.Oregon, States.Pennsylvania, States.RhodeIsland, States.SouthCarolina,
                States.SouthDakota, States.Tennessee, States.Texas, States.Utah, States.Vermont,
                States.Virginia, States.Washington, States.WestVirginia, States.Wisconsin, States.Wyoming
            };

            // Act & Assert
            foreach (var state in allStates)
            {
                var model = state.ToStateModel();
                
                Assert.That(model, Is.Not.Null);
                Assert.That(model.Name, Is.Not.Null.And.Not.Empty);
                Assert.That(model.Abbreviation, Is.Not.Null.And.Not.Empty);
                Assert.That(model.Abbreviation, Has.Length.EqualTo(2));
            }
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithInvalidState_ShouldReturnModelWithEnumNameAndEmptyAbbreviation()
        {
            // Arrange
            var invalidState = (States)999;

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
                var newHampshireModel = States.NewHampshire.ToStateModel();
                Assert.That(newHampshireModel.Name, Is.EqualTo("New Hampshire"));
                Assert.That(newHampshireModel.Abbreviation, Is.EqualTo("NH"));

                var newJerseyModel = States.NewJersey.ToStateModel();
                Assert.That(newJerseyModel.Name, Is.EqualTo("New Jersey"));
                Assert.That(newJerseyModel.Abbreviation, Is.EqualTo("NJ"));

                var newMexicoModel = States.NewMexico.ToStateModel();
                Assert.That(newMexicoModel.Name, Is.EqualTo("New Mexico"));
                Assert.That(newMexicoModel.Abbreviation, Is.EqualTo("NM"));

                var newYorkModel = States.NewYork.ToStateModel();
                Assert.That(newYorkModel.Name, Is.EqualTo("New York"));
                Assert.That(newYorkModel.Abbreviation, Is.EqualTo("NY"));

                var northCarolinaModel = States.NorthCarolina.ToStateModel();
                Assert.That(northCarolinaModel.Name, Is.EqualTo("North Carolina"));
                Assert.That(northCarolinaModel.Abbreviation, Is.EqualTo("NC"));

                var northDakotaModel = States.NorthDakota.ToStateModel();
                Assert.That(northDakotaModel.Name, Is.EqualTo("North Dakota"));
                Assert.That(northDakotaModel.Abbreviation, Is.EqualTo("ND"));

                var rhodeIslandModel = States.RhodeIsland.ToStateModel();
                Assert.That(rhodeIslandModel.Name, Is.EqualTo("Rhode Island"));
                Assert.That(rhodeIslandModel.Abbreviation, Is.EqualTo("RI"));

                var southCarolinaModel = States.SouthCarolina.ToStateModel();
                Assert.That(southCarolinaModel.Name, Is.EqualTo("South Carolina"));
                Assert.That(southCarolinaModel.Abbreviation, Is.EqualTo("SC"));

                var southDakotaModel = States.SouthDakota.ToStateModel();
                Assert.That(southDakotaModel.Name, Is.EqualTo("South Dakota"));
                Assert.That(southDakotaModel.Abbreviation, Is.EqualTo("SD"));

                var westVirginiaModel = States.WestVirginia.ToStateModel();
                Assert.That(westVirginiaModel.Name, Is.EqualTo("West Virginia"));
                Assert.That(westVirginiaModel.Abbreviation, Is.EqualTo("WV"));
            });
        }
    }
}
