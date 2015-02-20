using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PANE.Framework.Utility
{
    public class EnumBinder
    {
        public enum ApprovalStatus
        {
            Approved = 1,
            Pending,
            Declined,
            Cancelled
        }
        public static List<object> GetEnumNames(string enumStringType)
        {
            List<object> result = new List<object>();
            Type enumType = Type.GetType(enumStringType);
            foreach (object enumValue in Enum.GetValues(enumType))
            {
                string enumName = Enum.GetName(enumType, enumValue);
                var data = new { Name = SpaceWords(enumName), Value = enumValue};
                result.Add(data);
            }
            return result;
        }

        private static string SpaceWords(string words)
        {
            StringBuilder result = new StringBuilder();
            foreach (char letter in words.ToCharArray())
            {
                if (char.IsUpper(letter))
                {
                    result.Append(' ');
                }
                result.Append(letter);
            }
            return result.Replace('_',' ').ToString();
        }

        public static string SplitAtCapitalLetters(string stringToSplit)
        {
            string finalString = Regex.Replace(stringToSplit, "([A-Z])", " $1", RegexOptions.Compiled).Trim();

            if (finalString.Length == 0)
                return finalString;

            finalString = String.Format("{0}{1}", finalString.Substring(0, 1).ToUpper(), finalString.Substring(1, finalString.Length - 1));

            StringBuilder result = new StringBuilder();
            //This part is responsible for joining the ONE-LETTER strings.
            string[] moreCheck = finalString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (moreCheck.Length == 1)
            {
                return finalString;
            }
            result.Append(moreCheck[0].Trim());
            bool addSpace = moreCheck[0].Trim().Length > 1;

            for (int i = 1; i < moreCheck.Length; i++)
            {
                if (moreCheck[i].Trim().Length == 1)
                {
                    result.AppendFormat("{0}{1}", addSpace == true && i == 1 ? " " : "", moreCheck[i].Trim());
                }
                else
                {
                    result.AppendFormat(" {0}", moreCheck[i].Trim());
                }
            }
            return result.ToString();
        }

        public static string[] CapitalLetters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    }
  
}
