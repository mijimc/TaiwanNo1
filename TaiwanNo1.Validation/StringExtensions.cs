using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TaiwanNo1.Validation
{
    public static partial class StringExtensions
    {
        private static Dictionary<char, int> AreaCodeMappings { get; } = new Dictionary<char, int>
        {
            ['A'] = 1, // 10 -> 1 * 1 + 9 * 0 = 1
            ['B'] = 0, // 11 -> 1 * 1 + 9 * 1 = 10
            ['C'] = 9, // 12 -> 1 * 1 + 9 * 2 = 19
            ['D'] = 8, // 13 -> 1 * 1 + 9 * 3 = 28
            ['E'] = 7, // 14 -> 1 * 1 + 9 * 4 = 37
            ['F'] = 6, // 15 -> 1 * 1 + 9 * 5 = 46
            ['G'] = 5, // 16 -> 1 * 1 + 9 * 6 = 55
            ['H'] = 4, // 17 -> 1 * 1 + 9 * 7 = 64
            ['I'] = 9, // 34 -> 1 * 3 + 9 * 4 = 39
            ['J'] = 3, // 18 -> 1 * 1 + 9 * 8 = 73
            ['K'] = 2, // 19 -> 1 * 1 + 9 * 9 = 82
            ['L'] = 2, // 20 -> 1 * 2 + 9 * 0 = 2
            ['M'] = 1, // 21 -> 1 * 2 + 9 * 1 = 11
            ['N'] = 0, // 22 -> 1 * 2 + 9 * 2 = 20
            ['O'] = 8, // 35 -> 1 * 3 + 9 * 5 = 48
            ['P'] = 9, // 23 -> 1 * 2 + 9 * 3 = 29
            ['Q'] = 8, // 24 -> 1 * 2 + 9 * 4 = 38
            ['R'] = 7, // 25 -> 1 * 2 + 9 * 5 = 47
            ['S'] = 6, // 26 -> 1 * 2 + 9 * 6 = 56
            ['T'] = 5, // 27 -> 1 * 2 + 9 * 7 = 65
            ['U'] = 4, // 28 -> 1 * 2 + 9 * 8 = 74
            ['V'] = 3, // 29 -> 1 * 2 + 9 * 9 = 83
            ['W'] = 1, // 32 -> 1 * 3 + 9 * 2 = 21
            ['X'] = 3, // 30 -> 1 * 3 + 9 * 0 = 3
            ['Y'] = 2, // 31 -> 1 * 3 + 9 * 1 = 12
            ['Z'] = 0, // 33 -> 1 * 3 + 9 * 3 = 30
        };
        private static Dictionary<char, int> GenderCodeMappings { get; } = new Dictionary<char, int>
        {
            ['1'] = 8, // 1  -> 8 * 1 = 8
            ['2'] = 6, // 2  -> 8 * 2 = 16
            ['8'] = 4, // 8  -> 8 * 8 = 64
            ['9'] = 2, // 9  -> 8 * 9 = 72
            ['A'] = 0, // 10 -> 8 * 0 = 0
            ['B'] = 8, // 11 -> 8 * 1 = 8
            ['C'] = 6, // 12 -> 8 * 2 = 16
            ['D'] = 4, // 13 -> 8 * 3 = 24
        };


        /// <summary>
        /// Verify that the input string is a valid ROC (Taiwan) national identification card number.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Whether it is a valid card number.</returns>
        public static bool IsTwIdValid(this string input)
        {
            bool isValid;

            if (new Regex("^([A-Z]{1})([1-2]{1})(\\d{8})$").IsMatch(input))
            {
                isValid = IsChecksumValid(input);
            }
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Verify that the input string is a valid ROC (Taiwan) Resident Certificate card number.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="verifyOldFormat">Whether to verify the old format of Resident Certificate card number.</param>
        /// <returns>Whether it is a valid card number.</returns>
        public static bool IsTwRcValid(this string input, bool verifyOldFormat = false)
        {
            bool isValid;
            Regex regex;

            if (verifyOldFormat)
            {
                regex = new Regex("^([A-Z]{1})([A-D8-9]{1})(\\d{8})$");
            }
            else
                regex = new Regex("^([A-Z]{1})([8-9]{1})(\\d{8})$");

            if (regex.IsMatch(input))
            {
                isValid = IsChecksumValid(input);
            }
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Verify that the input string is a valid ROC (Taiwan) Tax ID Number.
        /// </summary>
        /// <param name="taxId">Input string.</param>
        /// <returns>Whether it is a valid Tax ID Number.</returns>
        public static bool IsTwTaxIdVaild(this string taxId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(taxId))
                {
                    return false;
                }

                Regex regex = new Regex(@"^\d{8}$");
                if (!regex.Match(taxId).Success)
                {
                    return false;
                }

                int[] idNoArray = taxId.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
                // 邏輯乘數
                int[] weight = new int[] { 1, 2, 1, 2, 1, 2, 4, 1 };

                int sum = 0;    // 總和
                int sumFor7 = 1;
                for (int i = 0; i < idNoArray.Length; i++)
                {
                    int subSum = idNoArray[i] * weight[i];
                    // 乘積是兩位數的要將兩位數相加
                    sum += (subSum / 10)   // 商數
                         + (subSum % 10);  // 餘數                
                }
                if (idNoArray[6] == 7)
                {
                    // 若第7碼 = 7，則會出現兩種數值都算對，因此要特別處理。
                    sumFor7 = sum + 1;
                }
                // 舊式 10 ，新式 5，統一採用新式
                return (sum % 5 == 0) || (sumFor7 % 5 == 0);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify that the checksum of the ID card number is correct.
        /// </summary>
        /// <param name="id">ID card number.</param>
        /// <returns>Whether the checksum is correct.</returns>
        private static bool IsChecksumValid(string id)
        {
            int sum = 0;
            char areaCode = id[0];
            char genderCode = id[1];

            sum += AreaCodeMappings[areaCode];
            sum += GenderCodeMappings[genderCode];

            for (int i = 2; i <= 9; i++)
            {
                if (i == 9)
                {
                    sum += (int)char.GetNumericValue(id[i]);
                }
                else
                    sum += (int)char.GetNumericValue(id[i]) * (9 - i);
            }

            return sum % 10 == 0;
        }
    }
}