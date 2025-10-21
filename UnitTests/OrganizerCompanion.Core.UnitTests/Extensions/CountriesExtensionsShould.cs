using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class CountriesExtensionsShould
    {
        [Test, Category("Extensions")]
        public void GetName_WithValidCountry_ShouldReturnCorrectName()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Countries.UnitedStates.GetName(), Is.EqualTo("United States"));
                Assert.That(Countries.Canada.GetName(), Is.EqualTo("Canada"));
                Assert.That(Countries.UnitedKingdom.GetName(), Is.EqualTo("United Kingdom"));
                Assert.That(Countries.Australia.GetName(), Is.EqualTo("Australia"));
                Assert.That(Countries.Germany.GetName(), Is.EqualTo("Germany"));
                Assert.That(Countries.France.GetName(), Is.EqualTo("France"));
                Assert.That(Countries.Japan.GetName(), Is.EqualTo("Japan"));
                Assert.That(Countries.Brazil.GetName(), Is.EqualTo("Brazil"));
                Assert.That(Countries.China.GetName(), Is.EqualTo("China"));
                Assert.That(Countries.India.GetName(), Is.EqualTo("India"));
            });
        }

        [Test, Category("Extensions")]
        public void GetIsoCode_WithValidCountry_ShouldReturnCorrectIsoCode()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Countries.UnitedStates.GetIsoCode(), Is.EqualTo("US"));
                Assert.That(Countries.Canada.GetIsoCode(), Is.EqualTo("CA"));
                Assert.That(Countries.UnitedKingdom.GetIsoCode(), Is.EqualTo("GB"));
                Assert.That(Countries.Australia.GetIsoCode(), Is.EqualTo("AU"));
                Assert.That(Countries.Germany.GetIsoCode(), Is.EqualTo("DE"));
                Assert.That(Countries.France.GetIsoCode(), Is.EqualTo("FR"));
                Assert.That(Countries.Japan.GetIsoCode(), Is.EqualTo("JP"));
                Assert.That(Countries.Brazil.GetIsoCode(), Is.EqualTo("BR"));
                Assert.That(Countries.China.GetIsoCode(), Is.EqualTo("CN"));
                Assert.That(Countries.India.GetIsoCode(), Is.EqualTo("IN"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithSpecialCharactersInName_ShouldReturnCorrectName()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Countries.IvoryCoast.GetName(), Is.EqualTo("Côte d'Ivoire"));
                Assert.That(Countries.SaoTomeAndPrincipe.GetName(), Is.EqualTo("São Tomé and Príncipe"));
                Assert.That(Countries.BosniaAndHerzegovina.GetName(), Is.EqualTo("Bosnia and Herzegovina"));
                Assert.That(Countries.AntiguaAndBarbuda.GetName(), Is.EqualTo("Antigua and Barbuda"));
                Assert.That(Countries.TrinidadAndTobago.GetName(), Is.EqualTo("Trinidad and Tobago"));
                Assert.That(Countries.SaintKittsAndNevis.GetName(), Is.EqualTo("Saint Kitts and Nevis"));
                Assert.That(Countries.SaintVincentAndTheGrenadines.GetName(), Is.EqualTo("Saint Vincent and the Grenadines"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithCompoundCountryNames_ShouldReturnCorrectName()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Countries.CentralAfricanRepublic.GetName(), Is.EqualTo("Central African Republic"));
                Assert.That(Countries.CongoDemocraticRepublic.GetName(), Is.EqualTo("Democratic Republic of the Congo"));
                Assert.That(Countries.DominicanRepublic.GetName(), Is.EqualTo("Dominican Republic"));
                Assert.That(Countries.EquatorialGuinea.GetName(), Is.EqualTo("Equatorial Guinea"));
                Assert.That(Countries.PapuaNewGuinea.GetName(), Is.EqualTo("Papua New Guinea"));
                Assert.That(Countries.SolomonIslands.GetName(), Is.EqualTo("Solomon Islands"));
                Assert.That(Countries.MarshallIslands.GetName(), Is.EqualTo("Marshall Islands"));
            });
        }

        [Test, Category("Extensions")]
        public void GetIsoCode_WithAllCountries_ShouldReturnValidTwoLetterCodes()
        {
            // Arrange
            var allCountries = Enum.GetValues<Countries>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var country in allCountries)
                {
                    var isoCode = country.GetIsoCode();
                    Assert.That(isoCode, Is.Not.Null, $"ISO code should not be null for {country}");
                    Assert.That(isoCode.Length, Is.EqualTo(2), $"ISO code should be 2 characters for {country}, but was: '{isoCode}'");
                    Assert.That(isoCode, Does.Match("^[A-Z]{2}$"), $"ISO code should be two uppercase letters for {country}, but was: '{isoCode}'");
                }
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithAllCountries_ShouldReturnValidNames()
        {
            // Arrange
            var allCountries = Enum.GetValues<Countries>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var country in allCountries)
                {
                    var name = country.GetName();
                    Assert.That(name, Is.Not.Null.And.Not.Empty, $"Name should not be null or empty for {country}");
                    Assert.That(name.Length, Is.GreaterThan(0), $"Name should have content for {country}");
                }
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithSampleFromEachAlphabetSection_ShouldReturnCorrectNames()
        {
            // Arrange, Act & Assert - Testing representative countries from each alphabetical section
            Assert.Multiple(() =>
            {
                // A section
                Assert.That(Countries.Afghanistan.GetName(), Is.EqualTo("Afghanistan"));
                Assert.That(Countries.Angola.GetName(), Is.EqualTo("Angola"));
                Assert.That(Countries.Austria.GetName(), Is.EqualTo("Austria"));
                
                // B section
                Assert.That(Countries.Bahrain.GetName(), Is.EqualTo("Bahrain"));
                Assert.That(Countries.Belgium.GetName(), Is.EqualTo("Belgium"));
                Assert.That(Countries.Botswana.GetName(), Is.EqualTo("Botswana"));
                
                // C section
                Assert.That(Countries.Cambodia.GetName(), Is.EqualTo("Cambodia"));
                Assert.That(Countries.Chile.GetName(), Is.EqualTo("Chile"));
                Assert.That(Countries.Croatia.GetName(), Is.EqualTo("Croatia"));
                
                // D section
                Assert.That(Countries.Denmark.GetName(), Is.EqualTo("Denmark"));
                Assert.That(Countries.Dominica.GetName(), Is.EqualTo("Dominica"));
                
                // E section
                Assert.That(Countries.Ecuador.GetName(), Is.EqualTo("Ecuador"));
                Assert.That(Countries.Egypt.GetName(), Is.EqualTo("Egypt"));
                Assert.That(Countries.Estonia.GetName(), Is.EqualTo("Estonia"));
                
                // F section
                Assert.That(Countries.Finland.GetName(), Is.EqualTo("Finland"));
                Assert.That(Countries.Fiji.GetName(), Is.EqualTo("Fiji"));
                
                // G section
                Assert.That(Countries.Georgia.GetName(), Is.EqualTo("Georgia"));
                Assert.That(Countries.Ghana.GetName(), Is.EqualTo("Ghana"));
                Assert.That(Countries.Greece.GetName(), Is.EqualTo("Greece"));
                
                // H section
                Assert.That(Countries.Haiti.GetName(), Is.EqualTo("Haiti"));
                Assert.That(Countries.Hungary.GetName(), Is.EqualTo("Hungary"));
                
                // I section
                Assert.That(Countries.Iceland.GetName(), Is.EqualTo("Iceland"));
                Assert.That(Countries.Indonesia.GetName(), Is.EqualTo("Indonesia"));
                Assert.That(Countries.Italy.GetName(), Is.EqualTo("Italy"));
                
                // J section
                Assert.That(Countries.Jamaica.GetName(), Is.EqualTo("Jamaica"));
                Assert.That(Countries.Jordan.GetName(), Is.EqualTo("Jordan"));
                
                // K section
                Assert.That(Countries.Kazakhstan.GetName(), Is.EqualTo("Kazakhstan"));
                Assert.That(Countries.Kenya.GetName(), Is.EqualTo("Kenya"));
                Assert.That(Countries.Kuwait.GetName(), Is.EqualTo("Kuwait"));
                
                // L section
                Assert.That(Countries.Latvia.GetName(), Is.EqualTo("Latvia"));
                Assert.That(Countries.Lebanon.GetName(), Is.EqualTo("Lebanon"));
                Assert.That(Countries.Libya.GetName(), Is.EqualTo("Libya"));
                
                // M section
                Assert.That(Countries.Malaysia.GetName(), Is.EqualTo("Malaysia"));
                Assert.That(Countries.Mexico.GetName(), Is.EqualTo("Mexico"));
                Assert.That(Countries.Morocco.GetName(), Is.EqualTo("Morocco"));
                
                // N section
                Assert.That(Countries.Nepal.GetName(), Is.EqualTo("Nepal"));
                Assert.That(Countries.Netherlands.GetName(), Is.EqualTo("Netherlands"));
                Assert.That(Countries.Nigeria.GetName(), Is.EqualTo("Nigeria"));
                
                // O section
                Assert.That(Countries.Oman.GetName(), Is.EqualTo("Oman"));
                
                // P section
                Assert.That(Countries.Pakistan.GetName(), Is.EqualTo("Pakistan"));
                Assert.That(Countries.Peru.GetName(), Is.EqualTo("Peru"));
                Assert.That(Countries.Poland.GetName(), Is.EqualTo("Poland"));
                
                // Q section
                Assert.That(Countries.Qatar.GetName(), Is.EqualTo("Qatar"));
                
                // R section
                Assert.That(Countries.Romania.GetName(), Is.EqualTo("Romania"));
                Assert.That(Countries.Russia.GetName(), Is.EqualTo("Russia"));
                Assert.That(Countries.Rwanda.GetName(), Is.EqualTo("Rwanda"));
                
                // S section
                Assert.That(Countries.Senegal.GetName(), Is.EqualTo("Senegal"));
                Assert.That(Countries.Singapore.GetName(), Is.EqualTo("Singapore"));
                Assert.That(Countries.Spain.GetName(), Is.EqualTo("Spain"));
                
                // T section
                Assert.That(Countries.Thailand.GetName(), Is.EqualTo("Thailand"));
                Assert.That(Countries.Turkey.GetName(), Is.EqualTo("Turkey"));
                Assert.That(Countries.Tuvalu.GetName(), Is.EqualTo("Tuvalu"));
                
                // U section
                Assert.That(Countries.Uganda.GetName(), Is.EqualTo("Uganda"));
                Assert.That(Countries.Ukraine.GetName(), Is.EqualTo("Ukraine"));
                Assert.That(Countries.Uruguay.GetName(), Is.EqualTo("Uruguay"));
                
                // V section
                Assert.That(Countries.Venezuela.GetName(), Is.EqualTo("Venezuela"));
                Assert.That(Countries.Vietnam.GetName(), Is.EqualTo("Vietnam"));
                
                // Y section
                Assert.That(Countries.Yemen.GetName(), Is.EqualTo("Yemen"));
                
                // Z section
                Assert.That(Countries.Zambia.GetName(), Is.EqualTo("Zambia"));
                Assert.That(Countries.Zimbabwe.GetName(), Is.EqualTo("Zimbabwe"));
            });
        }

        [Test, Category("Extensions")]
        public void GetIsoCode_WithSampleFromEachAlphabetSection_ShouldReturnCorrectIsoCodes()
        {
            // Arrange, Act & Assert - Testing representative countries from each alphabetical section
            Assert.Multiple(() =>
            {
                // A section
                Assert.That(Countries.Afghanistan.GetIsoCode(), Is.EqualTo("AF"));
                Assert.That(Countries.Angola.GetIsoCode(), Is.EqualTo("AO"));
                Assert.That(Countries.Austria.GetIsoCode(), Is.EqualTo("AT"));
                
                // B section
                Assert.That(Countries.Bahrain.GetIsoCode(), Is.EqualTo("BH"));
                Assert.That(Countries.Belgium.GetIsoCode(), Is.EqualTo("BE"));
                Assert.That(Countries.Botswana.GetIsoCode(), Is.EqualTo("BW"));
                
                // C section
                Assert.That(Countries.Cambodia.GetIsoCode(), Is.EqualTo("KH"));
                Assert.That(Countries.Chile.GetIsoCode(), Is.EqualTo("CL"));
                Assert.That(Countries.Croatia.GetIsoCode(), Is.EqualTo("HR"));
                
                // D section
                Assert.That(Countries.Denmark.GetIsoCode(), Is.EqualTo("DK"));
                Assert.That(Countries.Dominica.GetIsoCode(), Is.EqualTo("DM"));
                
                // E section
                Assert.That(Countries.Ecuador.GetIsoCode(), Is.EqualTo("EC"));
                Assert.That(Countries.Egypt.GetIsoCode(), Is.EqualTo("EG"));
                Assert.That(Countries.Estonia.GetIsoCode(), Is.EqualTo("EE"));
                
                // F section
                Assert.That(Countries.Finland.GetIsoCode(), Is.EqualTo("FI"));
                Assert.That(Countries.Fiji.GetIsoCode(), Is.EqualTo("FJ"));
                
                // G section
                Assert.That(Countries.Georgia.GetIsoCode(), Is.EqualTo("GE"));
                Assert.That(Countries.Ghana.GetIsoCode(), Is.EqualTo("GH"));
                Assert.That(Countries.Greece.GetIsoCode(), Is.EqualTo("GR"));
                
                // H section
                Assert.That(Countries.Haiti.GetIsoCode(), Is.EqualTo("HT"));
                Assert.That(Countries.Hungary.GetIsoCode(), Is.EqualTo("HU"));
                
                // I section
                Assert.That(Countries.Iceland.GetIsoCode(), Is.EqualTo("IS"));
                Assert.That(Countries.Indonesia.GetIsoCode(), Is.EqualTo("ID"));
                Assert.That(Countries.Italy.GetIsoCode(), Is.EqualTo("IT"));
                
                // J section
                Assert.That(Countries.Jamaica.GetIsoCode(), Is.EqualTo("JM"));
                Assert.That(Countries.Jordan.GetIsoCode(), Is.EqualTo("JO"));
                
                // K section
                Assert.That(Countries.Kazakhstan.GetIsoCode(), Is.EqualTo("KZ"));
                Assert.That(Countries.Kenya.GetIsoCode(), Is.EqualTo("KE"));
                Assert.That(Countries.Kuwait.GetIsoCode(), Is.EqualTo("KW"));
                
                // L section
                Assert.That(Countries.Latvia.GetIsoCode(), Is.EqualTo("LV"));
                Assert.That(Countries.Lebanon.GetIsoCode(), Is.EqualTo("LB"));
                Assert.That(Countries.Libya.GetIsoCode(), Is.EqualTo("LY"));
                
                // M section
                Assert.That(Countries.Malaysia.GetIsoCode(), Is.EqualTo("MY"));
                Assert.That(Countries.Mexico.GetIsoCode(), Is.EqualTo("MX"));
                Assert.That(Countries.Morocco.GetIsoCode(), Is.EqualTo("MA"));
                
                // N section
                Assert.That(Countries.Nepal.GetIsoCode(), Is.EqualTo("NP"));
                Assert.That(Countries.Netherlands.GetIsoCode(), Is.EqualTo("NL"));
                Assert.That(Countries.Nigeria.GetIsoCode(), Is.EqualTo("NG"));
                
                // O section
                Assert.That(Countries.Oman.GetIsoCode(), Is.EqualTo("OM"));
                
                // P section
                Assert.That(Countries.Pakistan.GetIsoCode(), Is.EqualTo("PK"));
                Assert.That(Countries.Peru.GetIsoCode(), Is.EqualTo("PE"));
                Assert.That(Countries.Poland.GetIsoCode(), Is.EqualTo("PL"));
                
                // Q section
                Assert.That(Countries.Qatar.GetIsoCode(), Is.EqualTo("QA"));
                
                // R section
                Assert.That(Countries.Romania.GetIsoCode(), Is.EqualTo("RO"));
                Assert.That(Countries.Russia.GetIsoCode(), Is.EqualTo("RU"));
                Assert.That(Countries.Rwanda.GetIsoCode(), Is.EqualTo("RW"));
                
                // S section
                Assert.That(Countries.Senegal.GetIsoCode(), Is.EqualTo("SN"));
                Assert.That(Countries.Singapore.GetIsoCode(), Is.EqualTo("SG"));
                Assert.That(Countries.Spain.GetIsoCode(), Is.EqualTo("ES"));
                
                // T section
                Assert.That(Countries.Thailand.GetIsoCode(), Is.EqualTo("TH"));
                Assert.That(Countries.Turkey.GetIsoCode(), Is.EqualTo("TR"));
                Assert.That(Countries.Tuvalu.GetIsoCode(), Is.EqualTo("TV"));
                
                // U section
                Assert.That(Countries.Uganda.GetIsoCode(), Is.EqualTo("UG"));
                Assert.That(Countries.Ukraine.GetIsoCode(), Is.EqualTo("UA"));
                Assert.That(Countries.Uruguay.GetIsoCode(), Is.EqualTo("UY"));
                
                // V section
                Assert.That(Countries.Venezuela.GetIsoCode(), Is.EqualTo("VE"));
                Assert.That(Countries.Vietnam.GetIsoCode(), Is.EqualTo("VN"));
                
                // Y section
                Assert.That(Countries.Yemen.GetIsoCode(), Is.EqualTo("YE"));
                
                // Z section
                Assert.That(Countries.Zambia.GetIsoCode(), Is.EqualTo("ZM"));
                Assert.That(Countries.Zimbabwe.GetIsoCode(), Is.EqualTo("ZW"));
            });
        }

        [Test, Category("Extensions")]
        public void GetName_WithCountriesThatHaveSpacesAndHyphens_ShouldReturnCorrectFormat()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Countries.NewZealand.GetName(), Is.EqualTo("New Zealand"));
                Assert.That(Countries.SouthAfrica.GetName(), Is.EqualTo("South Africa"));
                Assert.That(Countries.SouthKorea.GetName(), Is.EqualTo("South Korea"));
                Assert.That(Countries.NorthKorea.GetName(), Is.EqualTo("North Korea"));
                Assert.That(Countries.CzechRepublic.GetName(), Is.EqualTo("Czech Republic"));
                Assert.That(Countries.CostaRica.GetName(), Is.EqualTo("Costa Rica"));
                Assert.That(Countries.ElSalvador.GetName(), Is.EqualTo("El Salvador"));
                Assert.That(Countries.SierraLeone.GetName(), Is.EqualTo("Sierra Leone"));
                Assert.That(Countries.BurkinaFaso.GetName(), Is.EqualTo("Burkina Faso"));
                Assert.That(Countries.CaboVerde.GetName(), Is.EqualTo("Cabo Verde"));
                Assert.That(Countries.GuineaBissau.GetName(), Is.EqualTo("Guinea-Bissau"));
                Assert.That(Countries.TimorLeste.GetName(), Is.EqualTo("Timor-Leste"));
                Assert.That(Countries.SouthSudan.GetName(), Is.EqualTo("South Sudan"));
            });
        }

        [Test, Category("Extensions")]
        public void GetIsoCode_WithCountriesHavingUniqueIsoCodes_ShouldReturnCorrectCodes()
        {
            // Test some countries with potentially confusing or unique ISO codes
            Assert.Multiple(() =>
            {
                Assert.That(Countries.Switzerland.GetIsoCode(), Is.EqualTo("CH")); // Not SW
                Assert.That(Countries.Greece.GetIsoCode(), Is.EqualTo("GR")); // Not GC
                Assert.That(Countries.Norway.GetIsoCode(), Is.EqualTo("NO")); // Not NW
                Assert.That(Countries.SouthKorea.GetIsoCode(), Is.EqualTo("KR")); // Not SK
                Assert.That(Countries.NorthKorea.GetIsoCode(), Is.EqualTo("KP")); // Not NK
                Assert.That(Countries.SouthAfrica.GetIsoCode(), Is.EqualTo("ZA")); // Not SA
                Assert.That(Countries.UnitedArabEmirates.GetIsoCode(), Is.EqualTo("AE")); // Not UAE
                Assert.That(Countries.VaticanCity.GetIsoCode(), Is.EqualTo("VA")); // Not VC
                Assert.That(Countries.CzechRepublic.GetIsoCode(), Is.EqualTo("CZ")); // Not CR
            });
        }

        [Test, Category("Extensions")]
        public void GetName_ConsistencyCheck_SameResultOnMultipleCalls()
        {
            // Arrange
            var country = Countries.Japan;

            // Act
            var name1 = country.GetName();
            var name2 = country.GetName();
            var name3 = country.GetName();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(name1, Is.EqualTo(name2));
                Assert.That(name2, Is.EqualTo(name3));
                Assert.That(name1, Is.EqualTo("Japan"));
            });
        }

        [Test, Category("Extensions")]
        public void GetIsoCode_ConsistencyCheck_SameResultOnMultipleCalls()
        {
            // Arrange
            var country = Countries.Japan;

            // Act
            var isoCode1 = country.GetIsoCode();
            var isoCode2 = country.GetIsoCode();
            var isoCode3 = country.GetIsoCode();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(isoCode1, Is.EqualTo(isoCode2));
                Assert.That(isoCode2, Is.EqualTo(isoCode3));
                Assert.That(isoCode1, Is.EqualTo("JP"));
            });
        }

        [Test, Category("Extensions")]
        public void CountryEnumAndDictionary_ShouldHaveSameCount()
        {
            // This test ensures all enum values are mapped in the dictionary
            // Arrange
            var enumValues = Enum.GetValues<Countries>();
            
            // Act & Assert
            foreach (var country in enumValues)
            {
                // Both methods should return non-empty results
                var name = country.GetName();
                var isoCode = country.GetIsoCode();
                
                Assert.Multiple(() =>
                {
                    Assert.That(name, Is.Not.Null.And.Not.Empty, $"Name missing for {country}");
                    Assert.That(isoCode, Is.Not.Null.And.Not.Empty, $"ISO code missing for {country}");
                    Assert.That(isoCode.Length, Is.EqualTo(2), $"ISO code should be 2 chars for {country}");
                });
            }
        }

        [Test, Category("Extensions")]
        public void GetName_WithInvalidEnumValue_ShouldReturnEnumToString()
        {
            // This test covers the fallback behavior when TryGetValue fails
            // Arrange - Create an invalid enum value by casting
            var invalidCountry = (Countries)999;

            // Act
            var result = invalidCountry.GetName();

            // Assert
            Assert.That(result, Is.EqualTo(invalidCountry.ToString()));
        }

        [Test, Category("Extensions")]
        public void GetIsoCode_WithInvalidEnumValue_ShouldReturnEmptyString()
        {
            // This test covers the fallback behavior when TryGetValue fails
            // Arrange - Create an invalid enum value by casting
            var invalidCountry = (Countries)999;

            // Act
            var result = invalidCountry.GetIsoCode();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}
