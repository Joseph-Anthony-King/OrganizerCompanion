using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class MXStateExtensionsShould
    {
        [Test, Category("Extensions")]
        public void GetName_WithValidStates_ShouldReturnCorrectNames()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(MXStates.Aguascalientes.GetName(), Is.EqualTo("Aguascalientes"));
                Assert.That(MXStates.BajaCalifornia.GetName(), Is.EqualTo("Baja California"));
                Assert.That(MXStates.BajaCaliforniaSur.GetName(), Is.EqualTo("Baja California Sur"));
                Assert.That(MXStates.Campeche.GetName(), Is.EqualTo("Campeche"));
                Assert.That(MXStates.Chiapas.GetName(), Is.EqualTo("Chiapas"));
                Assert.That(MXStates.Chihhuahua.GetName(), Is.EqualTo("Chihuahua"));
                Assert.That(MXStates.CoahuilaDeZaragoza.GetName(), Is.EqualTo("Coahuila de Zaragoza"));
                Assert.That(MXStates.Colima.GetName(), Is.EqualTo("Colima"));
                Assert.That(MXStates.Durango.GetName(), Is.EqualTo("Durango"));
                Assert.That(MXStates.Guanajuato.GetName(), Is.EqualTo("Guanajuato"));
                Assert.That(MXStates.Guerrero.GetName(), Is.EqualTo("Guerrero"));
                Assert.That(MXStates.Hidalgo.GetName(), Is.EqualTo("Hidalgo"));
                Assert.That(MXStates.Jalisco.GetName(), Is.EqualTo("Jalisco"));
                Assert.That(MXStates.México.GetName(), Is.EqualTo("México"));
                Assert.That(MXStates.CiudadDeMéxico.GetName(), Is.EqualTo("Ciudad de México"));
                Assert.That(MXStates.MichoacánDeOcampo.GetName(), Is.EqualTo("Michoacán de Ocampo"));
                Assert.That(MXStates.Morelos.GetName(), Is.EqualTo("Morelos"));
                Assert.That(MXStates.Nayarit.GetName(), Is.EqualTo("Nayarit"));
                Assert.That(MXStates.NuevoLeón.GetName(), Is.EqualTo("Nuevo León"));
                Assert.That(MXStates.Oaxaca.GetName(), Is.EqualTo("Oaxaca"));
                Assert.That(MXStates.Puebla.GetName(), Is.EqualTo("Puebla"));
                Assert.That(MXStates.Querétaro.GetName(), Is.EqualTo("Querétaro"));
                Assert.That(MXStates.QuintanaRoo.GetName(), Is.EqualTo("Quintana Roo"));
                Assert.That(MXStates.SanLuisPotosí.GetName(), Is.EqualTo("San Luis Potosí"));
                Assert.That(MXStates.Sinaloa.GetName(), Is.EqualTo("Sinaloa"));
                Assert.That(MXStates.Sonora.GetName(), Is.EqualTo("Sonora"));
                Assert.That(MXStates.Tabasco.GetName(), Is.EqualTo("Tabasco"));
                Assert.That(MXStates.Tamaulipas.GetName(), Is.EqualTo("Tamaulipas"));
                Assert.That(MXStates.Tlaxcala.GetName(), Is.EqualTo("Tlaxcala"));
                Assert.That(MXStates.VeracruzDeIgnacioDeLaLlave.GetName(), Is.EqualTo("Veracruz de Ignacio de la Llave"));
                Assert.That(MXStates.Yucatán.GetName(), Is.EqualTo("Yucatán"));
                Assert.That(MXStates.Zacatecas.GetName(), Is.EqualTo("Zacatecas"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithInvalidState_ShouldReturnEnumName()
        {
            // Arrange
            var invalidState = (MXStates)999;

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
                Assert.That(MXStates.Aguascalientes.GetAbbreviation(), Is.EqualTo("AG"));
                Assert.That(MXStates.BajaCalifornia.GetAbbreviation(), Is.EqualTo("BC"));
                Assert.That(MXStates.BajaCaliforniaSur.GetAbbreviation(), Is.EqualTo("BS"));
                Assert.That(MXStates.Campeche.GetAbbreviation(), Is.EqualTo("CM"));
                Assert.That(MXStates.Chiapas.GetAbbreviation(), Is.EqualTo("CS"));
                Assert.That(MXStates.Chihhuahua.GetAbbreviation(), Is.EqualTo("CH"));
                Assert.That(MXStates.CoahuilaDeZaragoza.GetAbbreviation(), Is.EqualTo("CO"));
                Assert.That(MXStates.Colima.GetAbbreviation(), Is.EqualTo("CL"));
                Assert.That(MXStates.Durango.GetAbbreviation(), Is.EqualTo("DG"));
                Assert.That(MXStates.Guanajuato.GetAbbreviation(), Is.EqualTo("GT"));
                Assert.That(MXStates.Guerrero.GetAbbreviation(), Is.EqualTo("GR"));
                Assert.That(MXStates.Hidalgo.GetAbbreviation(), Is.EqualTo("HG"));
                Assert.That(MXStates.Jalisco.GetAbbreviation(), Is.EqualTo("JA"));
                Assert.That(MXStates.México.GetAbbreviation(), Is.EqualTo("MX"));
                Assert.That(MXStates.CiudadDeMéxico.GetAbbreviation(), Is.EqualTo("DF"));
                Assert.That(MXStates.MichoacánDeOcampo.GetAbbreviation(), Is.EqualTo("MI"));
                Assert.That(MXStates.Morelos.GetAbbreviation(), Is.EqualTo("MO"));
                Assert.That(MXStates.Nayarit.GetAbbreviation(), Is.EqualTo("NA"));
                Assert.That(MXStates.NuevoLeón.GetAbbreviation(), Is.EqualTo("NL"));
                Assert.That(MXStates.Oaxaca.GetAbbreviation(), Is.EqualTo("OA"));
                Assert.That(MXStates.Puebla.GetAbbreviation(), Is.EqualTo("PU"));
                Assert.That(MXStates.Querétaro.GetAbbreviation(), Is.EqualTo("QT"));
                Assert.That(MXStates.QuintanaRoo.GetAbbreviation(), Is.EqualTo("QR"));
                Assert.That(MXStates.SanLuisPotosí.GetAbbreviation(), Is.EqualTo("SL"));
                Assert.That(MXStates.Sinaloa.GetAbbreviation(), Is.EqualTo("SI"));
                Assert.That(MXStates.Sonora.GetAbbreviation(), Is.EqualTo("SO"));
                Assert.That(MXStates.Tabasco.GetAbbreviation(), Is.EqualTo("TB"));
                Assert.That(MXStates.Tamaulipas.GetAbbreviation(), Is.EqualTo("TM"));
                Assert.That(MXStates.Tlaxcala.GetAbbreviation(), Is.EqualTo("TL"));
                Assert.That(MXStates.VeracruzDeIgnacioDeLaLlave.GetAbbreviation(), Is.EqualTo("VZ"));
                Assert.That(MXStates.Yucatán.GetAbbreviation(), Is.EqualTo("YU"));
                Assert.That(MXStates.Zacatecas.GetAbbreviation(), Is.EqualTo("ZA"));
            });
        }

        [Test, Category("Extensions")]
        public void GetAbbreviation_WithInvalidState_ShouldReturnEmptyString()
        {
            // Arrange
            var invalidState = (MXStates)999;

            // Act
            var result = invalidState.GetAbbreviation();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithValidStates_ShouldReturnCorrectStateModel()
        {
            // Arrange & Act
            var jalisco = MXStates.Jalisco.ToStateModel();
            var ciudadDeMéxico = MXStates.CiudadDeMéxico.ToStateModel();
            var nuevoleon = MXStates.NuevoLeón.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jalisco.Name, Is.EqualTo("Jalisco"));
                Assert.That(jalisco.Abbreviation, Is.EqualTo("JA"));

                Assert.That(ciudadDeMéxico.Name, Is.EqualTo("Ciudad de México"));
                Assert.That(ciudadDeMéxico.Abbreviation, Is.EqualTo("DF"));

                Assert.That(nuevoleon.Name, Is.EqualTo("Nuevo León"));
                Assert.That(nuevoleon.Abbreviation, Is.EqualTo("NL"));
            });
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithAllStates_ShouldCreateValidStateModels()
        {
            // Arrange
            var allStates = new[]
            {
                MXStates.Aguascalientes, MXStates.BajaCalifornia, MXStates.BajaCaliforniaSur, MXStates.Campeche,
                MXStates.Chiapas, MXStates.Chihhuahua, MXStates.CoahuilaDeZaragoza, MXStates.Colima,
                MXStates.Durango, MXStates.Guanajuato, MXStates.Guerrero, MXStates.Hidalgo,
                MXStates.Jalisco, MXStates.México, MXStates.CiudadDeMéxico, MXStates.MichoacánDeOcampo,
                MXStates.Morelos, MXStates.Nayarit, MXStates.NuevoLeón, MXStates.Oaxaca,
                MXStates.Puebla, MXStates.Querétaro, MXStates.QuintanaRoo, MXStates.SanLuisPotosí,
                MXStates.Sinaloa, MXStates.Sonora, MXStates.Tabasco, MXStates.Tamaulipas,
                MXStates.Tlaxcala, MXStates.VeracruzDeIgnacioDeLaLlave, MXStates.Yucatán, MXStates.Zacatecas
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
            var invalidState = (MXStates)999;

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
                var bajaCaliforniaModel = MXStates.BajaCalifornia.ToStateModel();
                Assert.That(bajaCaliforniaModel.Name, Is.EqualTo("Baja California"));
                Assert.That(bajaCaliforniaModel.Abbreviation, Is.EqualTo("BC"));

                var bajaCaliforniaSurModel = MXStates.BajaCaliforniaSur.ToStateModel();
                Assert.That(bajaCaliforniaSurModel.Name, Is.EqualTo("Baja California Sur"));
                Assert.That(bajaCaliforniaSurModel.Abbreviation, Is.EqualTo("BS"));

                var coahuilaDeZaragozaModel = MXStates.CoahuilaDeZaragoza.ToStateModel();
                Assert.That(coahuilaDeZaragozaModel.Name, Is.EqualTo("Coahuila de Zaragoza"));
                Assert.That(coahuilaDeZaragozaModel.Abbreviation, Is.EqualTo("CO"));

                var ciudadDeMexicoModel = MXStates.CiudadDeMéxico.ToStateModel();
                Assert.That(ciudadDeMexicoModel.Name, Is.EqualTo("Ciudad de México"));
                Assert.That(ciudadDeMexicoModel.Abbreviation, Is.EqualTo("DF"));

                var michoacanDeOcampoModel = MXStates.MichoacánDeOcampo.ToStateModel();
                Assert.That(michoacanDeOcampoModel.Name, Is.EqualTo("Michoacán de Ocampo"));
                Assert.That(michoacanDeOcampoModel.Abbreviation, Is.EqualTo("MI"));

                var nuevoLeonModel = MXStates.NuevoLeón.ToStateModel();
                Assert.That(nuevoLeonModel.Name, Is.EqualTo("Nuevo León"));
                Assert.That(nuevoLeonModel.Abbreviation, Is.EqualTo("NL"));

                var quintanaRooModel = MXStates.QuintanaRoo.ToStateModel();
                Assert.That(quintanaRooModel.Name, Is.EqualTo("Quintana Roo"));
                Assert.That(quintanaRooModel.Abbreviation, Is.EqualTo("QR"));

                var sanLuisPotosiModel = MXStates.SanLuisPotosí.ToStateModel();
                Assert.That(sanLuisPotosiModel.Name, Is.EqualTo("San Luis Potosí"));
                Assert.That(sanLuisPotosiModel.Abbreviation, Is.EqualTo("SL"));

                var veracruzModel = MXStates.VeracruzDeIgnacioDeLaLlave.ToStateModel();
                Assert.That(veracruzModel.Name, Is.EqualTo("Veracruz de Ignacio de la Llave"));
                Assert.That(veracruzModel.Abbreviation, Is.EqualTo("VZ"));
            });
        }

        [Test, Category("Extensions")]
        public void ToStateModel_WithStatesContainingSpecialCharacters_ShouldHandleCorrectly()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                var mexicoModel = MXStates.México.ToStateModel();
                Assert.That(mexicoModel.Name, Is.EqualTo("México"));
                Assert.That(mexicoModel.Abbreviation, Is.EqualTo("MX"));

                var michoacanModel = MXStates.MichoacánDeOcampo.ToStateModel();
                Assert.That(michoacanModel.Name, Is.EqualTo("Michoacán de Ocampo"));
                Assert.That(michoacanModel.Abbreviation, Is.EqualTo("MI"));

                var nuevoLeonModel = MXStates.NuevoLeón.ToStateModel();
                Assert.That(nuevoLeonModel.Name, Is.EqualTo("Nuevo León"));
                Assert.That(nuevoLeonModel.Abbreviation, Is.EqualTo("NL"));

                var queretaroModel = MXStates.Querétaro.ToStateModel();
                Assert.That(queretaroModel.Name, Is.EqualTo("Querétaro"));
                Assert.That(queretaroModel.Abbreviation, Is.EqualTo("QT"));

                var sanLuisPotosiModel = MXStates.SanLuisPotosí.ToStateModel();
                Assert.That(sanLuisPotosiModel.Name, Is.EqualTo("San Luis Potosí"));
                Assert.That(sanLuisPotosiModel.Abbreviation, Is.EqualTo("SL"));

                var yucatanModel = MXStates.Yucatán.ToStateModel();
                Assert.That(yucatanModel.Name, Is.EqualTo("Yucatán"));
                Assert.That(yucatanModel.Abbreviation, Is.EqualTo("YU"));
            });
        }
    }
}
