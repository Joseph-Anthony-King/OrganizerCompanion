using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class PronounExtensionsShould
    {
        [Test, Category("Extensions")]
        public void GetSubject_WithTraditionalPronouns_ShouldReturnCorrectSubject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetSubject(), Is.EqualTo("she"));
                Assert.That(Pronouns.HeHim.GetSubject(), Is.EqualTo("he"));
            });
        }

        [Test, Category("Extensions")]
        public void GetSubject_WithGenderNeutralPronouns_ShouldReturnCorrectSubject()
        {
            // Arrange & Act & Assert
            Assert.That(Pronouns.TheyThem.GetSubject(), Is.EqualTo("they"));
        }

        [Test, Category("Extensions")]
        public void GetSubject_WithNeoPronouns_ShouldReturnCorrectSubject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.XeXir.GetSubject(), Is.EqualTo("xe"));
                Assert.That(Pronouns.ZeZir.GetSubject(), Is.EqualTo("ze"));
                Assert.That(Pronouns.EyEm.GetSubject(), Is.EqualTo("ey"));
                Assert.That(Pronouns.FaeFaer.GetSubject(), Is.EqualTo("fae"));
                Assert.That(Pronouns.VeVer.GetSubject(), Is.EqualTo("ve"));
                Assert.That(Pronouns.PerPer.GetSubject(), Is.EqualTo("per"));
            });
        }

        [Test, Category("Extensions")]
        public void GetSubject_WithOtherOptions_ShouldReturnEmptyString()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.PreferNotToSay.GetSubject(), Is.EqualTo(""));
                Assert.That(Pronouns.Other.GetSubject(), Is.EqualTo(""));
            });
        }

        [Test, Category("Extensions")]
        public void GetObject_WithTraditionalPronouns_ShouldReturnCorrectObject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetObject(), Is.EqualTo("her"));
                Assert.That(Pronouns.HeHim.GetObject(), Is.EqualTo("him"));
            });
        }

        [Test, Category("Extensions")]
        public void GetObject_WithGenderNeutralPronouns_ShouldReturnCorrectObject()
        {
            // Arrange & Act & Assert
            Assert.That(Pronouns.TheyThem.GetObject(), Is.EqualTo("them"));
        }

        [Test, Category("Extensions")]
        public void GetObject_WithNeoPronouns_ShouldReturnCorrectObject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.XeXir.GetObject(), Is.EqualTo("xir"));
                Assert.That(Pronouns.ZeZir.GetObject(), Is.EqualTo("zir"));
                Assert.That(Pronouns.EyEm.GetObject(), Is.EqualTo("em"));
                Assert.That(Pronouns.FaeFaer.GetObject(), Is.EqualTo("faer"));
                Assert.That(Pronouns.VeVer.GetObject(), Is.EqualTo("ver"));
                Assert.That(Pronouns.PerPer.GetObject(), Is.EqualTo("per"));
            });
        }

        [Test, Category("Extensions")]
        public void GetObject_WithOtherOptions_ShouldReturnEmptyString()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.PreferNotToSay.GetObject(), Is.EqualTo(""));
                Assert.That(Pronouns.Other.GetObject(), Is.EqualTo(""));
            });
        }

        [Test, Category("Extensions")]
        public void GetPossessive_WithTraditionalPronouns_ShouldReturnCorrectPossessive()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetPossessive(), Is.EqualTo("hers"));
                Assert.That(Pronouns.HeHim.GetPossessive(), Is.EqualTo("his"));
            });
        }

        [Test, Category("Extensions")]
        public void GetPossessive_WithGenderNeutralPronouns_ShouldReturnCorrectPossessive()
        {
            // Arrange & Act & Assert
            Assert.That(Pronouns.TheyThem.GetPossessive(), Is.EqualTo("theirs"));
        }

        [Test, Category("Extensions")]
        public void GetPossessive_WithNeoPronouns_ShouldReturnCorrectPossessive()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.XeXir.GetPossessive(), Is.EqualTo("xirs"));
                Assert.That(Pronouns.ZeZir.GetPossessive(), Is.EqualTo("zirs"));
                Assert.That(Pronouns.EyEm.GetPossessive(), Is.EqualTo("eirs"));
                Assert.That(Pronouns.FaeFaer.GetPossessive(), Is.EqualTo("faers"));
                Assert.That(Pronouns.VeVer.GetPossessive(), Is.EqualTo("vers"));
                Assert.That(Pronouns.PerPer.GetPossessive(), Is.EqualTo("pers"));
            });
        }

        [Test, Category("Extensions")]
        public void GetPossessive_WithOtherOptions_ShouldReturnEmptyString()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.PreferNotToSay.GetPossessive(), Is.EqualTo(""));
                Assert.That(Pronouns.Other.GetPossessive(), Is.EqualTo(""));
            });
        }

        [Test, Category("Extensions")]
        public void GetDisplayFormat_WithTraditionalPronouns_ShouldReturnCorrectDisplayFormat()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetDisplayFormat(), Is.EqualTo("she/her"));
                Assert.That(Pronouns.HeHim.GetDisplayFormat(), Is.EqualTo("he/him"));
            });
        }

        [Test, Category("Extensions")]
        public void GetDisplayFormat_WithGenderNeutralPronouns_ShouldReturnCorrectDisplayFormat()
        {
            // Arrange & Act & Assert
            Assert.That(Pronouns.TheyThem.GetDisplayFormat(), Is.EqualTo("they/them"));
        }

        [Test, Category("Extensions")]
        public void GetDisplayFormat_WithNeoPronouns_ShouldReturnCorrectDisplayFormat()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.XeXir.GetDisplayFormat(), Is.EqualTo("xe/xir"));
                Assert.That(Pronouns.ZeZir.GetDisplayFormat(), Is.EqualTo("ze/zir"));
                Assert.That(Pronouns.EyEm.GetDisplayFormat(), Is.EqualTo("ey/em"));
                Assert.That(Pronouns.FaeFaer.GetDisplayFormat(), Is.EqualTo("fae/faer"));
                Assert.That(Pronouns.VeVer.GetDisplayFormat(), Is.EqualTo("ve/ver"));
                Assert.That(Pronouns.PerPer.GetDisplayFormat(), Is.EqualTo("per/per"));
            });
        }

        [Test, Category("Extensions")]
        public void GetDisplayFormat_WithOtherOptions_ShouldReturnCorrectDisplayFormat()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.PreferNotToSay.GetDisplayFormat(), Is.EqualTo("Prefer not to say"));
                Assert.That(Pronouns.Other.GetDisplayFormat(), Is.EqualTo("Other"));
            });
        }

        [Test, Category("Extensions")]
        public void GetFormattedSubject_WithCapitalizeTrue_ShouldReturnCapitalizedSubject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetFormattedSubject(true), Is.EqualTo("She"));
                Assert.That(Pronouns.HeHim.GetFormattedSubject(true), Is.EqualTo("He"));
                Assert.That(Pronouns.TheyThem.GetFormattedSubject(true), Is.EqualTo("They"));
                Assert.That(Pronouns.XeXir.GetFormattedSubject(true), Is.EqualTo("Xe"));
                Assert.That(Pronouns.ZeZir.GetFormattedSubject(true), Is.EqualTo("Ze"));
                Assert.That(Pronouns.EyEm.GetFormattedSubject(true), Is.EqualTo("Ey"));
                Assert.That(Pronouns.FaeFaer.GetFormattedSubject(true), Is.EqualTo("Fae"));
                Assert.That(Pronouns.VeVer.GetFormattedSubject(true), Is.EqualTo("Ve"));
                Assert.That(Pronouns.PerPer.GetFormattedSubject(true), Is.EqualTo("Per"));
            });
        }

        [Test, Category("Extensions")]
        public void GetFormattedSubject_WithCapitalizeFalse_ShouldReturnLowercaseSubject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetFormattedSubject(false), Is.EqualTo("she"));
                Assert.That(Pronouns.HeHim.GetFormattedSubject(false), Is.EqualTo("he"));
                Assert.That(Pronouns.TheyThem.GetFormattedSubject(false), Is.EqualTo("they"));
                Assert.That(Pronouns.XeXir.GetFormattedSubject(false), Is.EqualTo("xe"));
                Assert.That(Pronouns.ZeZir.GetFormattedSubject(false), Is.EqualTo("ze"));
                Assert.That(Pronouns.EyEm.GetFormattedSubject(false), Is.EqualTo("ey"));
                Assert.That(Pronouns.FaeFaer.GetFormattedSubject(false), Is.EqualTo("fae"));
                Assert.That(Pronouns.VeVer.GetFormattedSubject(false), Is.EqualTo("ve"));
                Assert.That(Pronouns.PerPer.GetFormattedSubject(false), Is.EqualTo("per"));
            });
        }

        [Test, Category("Extensions")]
        public void GetFormattedSubject_WithDefaultParameter_ShouldReturnCapitalizedSubject()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.SheHer.GetFormattedSubject(), Is.EqualTo("She"));
                Assert.That(Pronouns.HeHim.GetFormattedSubject(), Is.EqualTo("He"));
                Assert.That(Pronouns.TheyThem.GetFormattedSubject(), Is.EqualTo("They"));
            });
        }

        [Test, Category("Extensions")]
        public void GetFormattedSubject_WithOtherOptions_ShouldReturnEmptyString()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(Pronouns.PreferNotToSay.GetFormattedSubject(), Is.EqualTo(""));
                Assert.That(Pronouns.Other.GetFormattedSubject(), Is.EqualTo(""));
                Assert.That(Pronouns.PreferNotToSay.GetFormattedSubject(false), Is.EqualTo(""));
                Assert.That(Pronouns.Other.GetFormattedSubject(false), Is.EqualTo(""));
            });
        }

        [Test, Category("Extensions")]
        public void GetSubject_WithUndefinedPronounValue_ShouldFallbackToEnumString()
        {
            // Arrange - Using an undefined enum value (casting from int)
            var undefinedPronoun = (Pronouns)999;

            // Act
            var result = undefinedPronoun.GetSubject();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test, Category("Extensions")]
        public void GetObject_WithUndefinedPronounValue_ShouldFallbackToEnumString()
        {
            // Arrange - Using an undefined enum value (casting from int)
            var undefinedPronoun = (Pronouns)999;

            // Act
            var result = undefinedPronoun.GetObject();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test, Category("Extensions")]
        public void GetPossessive_WithUndefinedPronounValue_ShouldFallbackToEnumString()
        {
            // Arrange - Using an undefined enum value (casting from int)
            var undefinedPronoun = (Pronouns)999;

            // Act
            var result = undefinedPronoun.GetPossessive();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test, Category("Extensions")]
        public void GetDisplayFormat_WithUndefinedPronounValue_ShouldFallbackToEnumString()
        {
            // Arrange - Using an undefined enum value (casting from int)
            var undefinedPronoun = (Pronouns)999;

            // Act
            var result = undefinedPronoun.GetDisplayFormat();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test, Category("Extensions")]
        public void GetFormattedSubject_WithUndefinedPronounValue_ShouldFallbackToCapitalizedEnumString()
        {
            // Arrange - Using an undefined enum value (casting from int)
            var undefinedPronoun = (Pronouns)999;

            // Act
            var resultCapitalized = undefinedPronoun.GetFormattedSubject(true);
            var resultLowercase = undefinedPronoun.GetFormattedSubject(false);

            // Assert
            Assert.That(resultCapitalized, Is.EqualTo("999"));
            Assert.That(resultLowercase, Is.EqualTo("999"));
        }
    }
}
