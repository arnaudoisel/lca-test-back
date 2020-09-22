using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LuccaDevises
{
    /// <summary>
    /// Input file reader.
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// Reads an input file and build an <see cref="Input"/> object.
        /// </summary>
        /// <remarks>
        /// <para>Example:</para>
        /// <code>
        /// EUR;550;JPY<br/>
        /// 6<br/>
        /// AUD;CHF;0.9661<br/>
        /// JPY;KRW;13.1151<br/>
        /// EUR;CHF;1.2053<br/>
        /// AUD;JPY;86.0305<br/>
        /// EUR;USD;1.2989<br/>
        /// JPY;INR;0.6571<br/>
        /// </code>
        /// </remarks>
        /// <param name="filePath">The path to the input file on system.</param>
        /// <returns>Input object containing currencies information for conversion.</returns>
		/// <exception cref="ArgumentException">If the file format is incorrect.</exception>
        public Input Read(string filePath)
        {
            string[] lines = File.ReadAllLines(@filePath);

            if (lines.Length < 3)
                throw new ArgumentException("Input file should contain at least 3 lines.");

            Index i1 = 1;
            Index i2 = ^0;
            string[] currencyChangeRatesLines = lines[i1..i2];

            return InputBuilder.GetInstance()
                .ReadFirstLine(lines[0])
                .ReadCurrencyChangeRatesLines(currencyChangeRatesLines)
                .Build();
        }
    }
}
