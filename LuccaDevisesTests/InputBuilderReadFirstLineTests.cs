using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuccaDevises;
using System;

namespace LuccaDevisesTests
{

    [TestClass]
    public class InputBuilderReadFirstLineTests
    {
        private const string ValidCurrency1 = "EUR";
        private const string ValidCurrency2 = "JPY";
        private const string ValidAmount = "550";
        private const decimal ValidAmountDecimal = 550m;

        [TestMethod]
        public void ReadFirstLine_WithValidLine_ShouldCorrectlyReadFirstLine()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                ValidAmount,
                ValidCurrency2);

            InputBuilder builder = InputBuilder.GetInstance().ReadFirstLine(line);

            Input input = builder.Build();

            Assert.AreEqual(ValidCurrency1, input.SourceCurrency, "Source currency was not read correctly in first line.");
            Assert.AreEqual(ValidAmountDecimal, input.Amount, "Amount to convert was not read correctly in first line.");
            Assert.AreEqual(ValidCurrency2, input.DestinationCurrency, "Destination currency was not read correctly in first line.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Missing source currency was read from first line.")]
        public void ReadFirstLine_WithMissingSourceCurrency_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                "",
                ValidAmount,
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Too short source currency was read from first line.")]
        public void ReadFirstLine_WithSourceCurrencyTooShort_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                "EU",
                ValidAmount,
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Too big source currency was read from first line.")]
        public void ReadFirstLine_WithSourceCurrencyTooBig_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                "EURR",
                ValidAmount,
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Missing amount was read from first line.")]
        public void ReadFirstLine_WithMissingAmount_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                "",
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Not integer amount was read from first line.")]
        public void ReadFirstLine_WithAmountNotInteger_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                "invalid_amount",
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Negative amount was read from first line.")]
        public void ReadFirstLine_WithNegativeAmount_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                -15,
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Decimal amount was read from first line.")]
        public void ReadFirstLine_WithDecimalAmount_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                "15.2",
                ValidCurrency2);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Missing destination currency was read from first line.")]
        public void ReadFirstLine_WithMissingDestinationCurrency_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                ValidAmount,
                "");

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Too short destination currency was read from first line.")]
        public void ReadFirstLine_WithDestinationCurrencyTooShort_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                ValidAmount,
                "JP");

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Too big destination currency was read from first line.")]
        public void ReadFirstLine_WithDestinationCurrencyTooBig_ShouldThrowException()
        {
            string line = string.Format("{0};{1};{2}",
                ValidCurrency1,
                ValidAmount,
                "JPYY");

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "First line with missing column was read.")]
        public void ReadFirstLine_Without3Columns_ShouldThrowException()
        {
            string line = string.Format("{0};{1}",
                ValidCurrency1,
                ValidAmount);

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Empty first line was read.")]
        public void ReadFirstLine_Empty_ShouldThrowException()
        {
            string line = "";

            InputBuilder.GetInstance().ReadFirstLine(line);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),
            "Null first line was read.")]
        public void ReadFirstLine_Null_ShouldThrowException()
        {
            string line = null;

            InputBuilder.GetInstance().ReadFirstLine(line);
        }
    }
}
