using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EnvInt.Win32.FieldTech.Library
{
    public static class clsCommonFunctions
    {
        /// <summary>
        /// This function will check the TAG Valid Format as per Standard.
        /// In case of any invalid format, message will be set in "resultMsg" string
        /// </summary>
        /// <param name="tagFormat"></param>
        /// <returns>Return Boolean Value, TRUE in case of Valid Format and FALSE in case of Invalid formation</returns>
        public static bool ValidTagFormat(string strNewTag, ref string resultMsg)
        {
            bool blnResult = true;

            if (!string.IsNullOrEmpty(Convert.ToString(strNewTag)))
            {
                string strValidTagFormat = "";
                try
                {
                    strValidTagFormat = Convert.ToString(Globals.CurrentProjectData.LDARTAGFormat);
                }
                catch { }

                if (!(string.IsNullOrEmpty(Convert.ToString(strValidTagFormat)))) // If format is NULL or Empty, means Format not available and do not need to check validity
                {
                    if (strValidTagFormat != "None")  // If format type is NONE, means Format not applicable and do not need to check validity
                    {
                        switch (strValidTagFormat)
                        {
                            case "Integer":
                                #region start
                                //string[] arrCase1 = strNewTag.Split('-');
                                //if (arrCase1.Count() != 1)
                                //{
                                //    blnResult = false;
                                //    resultMsg = "Tag format is not correct.";
                                //}
                                //else
                                //{
                                string intValue = strNewTag; // arrCase1[0];
                                Int64 tagIntValue = 0;
                                if (!(Int64.TryParse(intValue, out tagIntValue)))
                                {
                                    blnResult = false;
                                    resultMsg = "Please check Tag Format. Valid format is '[" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits]'.";
                                }
                                else
                                {
                                    //  Check Tag interger value should be in Range
                                    Int64 rangeStart = 0;
                                    Int64 rangeTo = 0;
                                    try
                                    {
                                        rangeStart = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsFrom);
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        rangeTo = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsTo);
                                    }
                                    catch
                                    { }

                                    if (Globals.CurrentProjectData.LDARTAGStartsFrom.Length != strNewTag.Length) //arrCase1[0].Length
                                    {
                                        blnResult = false;
                                        resultMsg = "Number of digits should be " + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " for Integer Value in Tag format.";

                                    }
                                    else
                                    {
                                        if (!((rangeStart <= tagIntValue) && (rangeTo >= tagIntValue)))
                                        {
                                            blnResult = false;
                                            resultMsg = "Tag format is not correct. Integer value should be in range between from " + rangeStart + " to " + rangeTo + ".";
                                        }
                                    }
                                }
                                //}
                                #endregion
                                break;
                            case "Integer-Alpha":
                                #region start
                                string[] arrCase2 = new string[2]; // strNewTag.Split('-');
                                if (!(strNewTag.Contains("-")))
                                {
                                    Char[] chkString2 = strNewTag.ToCharArray();
                                    int cntAlpha2 = 0;
                                    for (int i = strNewTag.Length - 1; i >= 0; i--)
                                    {
                                        if (Regex.IsMatch(Convert.ToString(chkString2[i]), @"[A-Z]"))
                                        {
                                            cntAlpha2 = cntAlpha2 + 1;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    try
                                    {
                                        arrCase2[1] = strNewTag.Substring(strNewTag.Length - cntAlpha2, cntAlpha2);
                                    }
                                    catch
                                    {
                                        arrCase2[1] = "0";
                                    }


                                    //if (arrCase2.Count() != 2)
                                    //{
                                    //    blnResult = false;
                                    //    resultMsg = "Tag format is not correct.";
                                    //}
                                    //else
                                    //{

                                    try
                                    {
                                        arrCase2[0] = strNewTag.Substring(0, strNewTag.Length - cntAlpha2);
                                    }
                                    catch
                                    {
                                        arrCase2[0] = "ex";
                                    }
                                }
                                else
                                {
                                    arrCase2[0] = "ex";
                                    arrCase2[1] = "0";
                                }
                                string intValue2 = arrCase2[0];

                                Int64 tagIntValue2 = 0;
                                if (!(Int64.TryParse(intValue2, out tagIntValue2)))
                                {
                                    blnResult = false;
                                    resultMsg = "Please check Tag Format. Valid format is '[" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                }
                                else
                                {
                                    //  Check Tag interger value should be in Range
                                    Int64 rangeStart = 0;
                                    Int64 rangeTo = 0;
                                    try
                                    {
                                        rangeStart = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsFrom);
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        rangeTo = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsTo);
                                    }
                                    catch
                                    { }

                                    if (Globals.CurrentProjectData.LDARTAGStartsFrom.Length != arrCase2[0].Length)
                                    {
                                        blnResult = false;
                                        resultMsg = "Please check Tag Format. Valid format is '[" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                             
                                    }
                                    else
                                    {
                                        if (!((rangeStart <= tagIntValue2) && (rangeTo >= tagIntValue2)))
                                        {
                                            blnResult = false;
                                            resultMsg = "Tag format is not correct. Integer value should be in between range from " + rangeStart + " to " + rangeTo + ".";
                                        }
                                        else
                                        {
                                            try
                                            {
                                                arrCase2[1] = strNewTag.Replace(strNewTag.Substring(0, Globals.CurrentProjectData.LDARTAGStartsFrom.Length), "");
                                            }
                                            catch
                                            {
                                                arrCase2[1] = "0";
                                            }
                                            string strAlpha = arrCase2[1];
                                            if (strAlpha.Length >= 1)
                                            {
                                                Char[] convertedString = strAlpha.ToCharArray();
                                                bool isAlpha = true;
                                                for (int i = 0; i < strAlpha.Length; i++)
                                                {
                                                    if (!(Regex.IsMatch(Convert.ToString(convertedString[i]), @"[A-Z]")))
                                                    {
                                                        isAlpha = false;
                                                    }
                                                }
                                                if (!(isAlpha))
                                                {
                                                    blnResult = false;
                                                    resultMsg = "Please check Tag Format. Valid format is '[" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                                }
                                            }
                                            else
                                            {
                                                blnResult = false;
                                                resultMsg = "Please check Tag Format. Valid format is '[" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                            }
                                        }
                                    }
                                }
                                //}
                                #endregion
                                break;
                            case "String-Integer":
                                #region start

                                string[] arrCase3 = new string[2];

                                string stringIntegerPart3 = strNewTag;

                                if (stringIntegerPart3.Contains("-"))
                                {
                                    string[] strDevide = stringIntegerPart3.Split('-');

                                    if (strDevide.Count() > 2)
                                    {
                                        arrCase3[1] = "a";
                                        arrCase3[0] = "";
                                    }
                                    else
                                    {
                                        arrCase3[0] = strDevide[0] + "-";
                                        arrCase3[1] = strDevide[1];
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        arrCase3[1] = stringIntegerPart3.Substring(stringIntegerPart3.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length, Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                    }
                                    catch
                                    {
                                        arrCase3[1] = "a";
                                    }

                                    try
                                    {
                                        arrCase3[0] = stringIntegerPart3.Substring(0, stringIntegerPart3.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                    }
                                    catch
                                    {
                                        arrCase3[0] = "";
                                    }
                                }

                                //if (arrCase3.Count() != 2)
                                //{
                                //    blnResult = false;
                                //    resultMsg = "Tag format is not correct.";
                                //}
                                //else
                                //{
                                string arrString = arrCase3[0];
                                if (arrString.Length <= 0)
                                {
                                    blnResult = false;
                                    resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits]'.";
                                }
                                else
                                {
                                    string intValue3 = arrCase3[1];
                                    Int64 tagIntValue3 = 0;
                                    if (!(Int64.TryParse(intValue3, out tagIntValue3)))
                                    {
                                        blnResult = false;
                                        resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits]'.";
                                    }
                                    else
                                    {
                                        //  Check Tag interger value should be in Range
                                        Int64 rangeStart = 0;
                                        Int64 rangeTo = 0;
                                        try
                                        {
                                            rangeStart = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsFrom);
                                        }
                                        catch
                                        { }
                                        try
                                        {
                                            rangeTo = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsTo);
                                        }
                                        catch
                                        { }

                                        if (Globals.CurrentProjectData.LDARTAGStartsFrom.Length != arrCase3[1].Length)
                                        {
                                            blnResult = false;
                                            resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits]'.";

                                        }
                                        else
                                        {
                                            if (!((rangeStart <= tagIntValue3) && (rangeTo >= tagIntValue3)))
                                            {
                                                blnResult = false;
                                                resultMsg = "Tag format is not correct. Integer value should be in between range from " + rangeStart + " to " + rangeTo + ".";
                                            }
                                        }
                                    }
                                }
                                //}
                                #endregion
                                break;
                            case "String-Integer-Alpha":
                                #region start
                                string[] arrCase4 = new string[3]; // strNewTag.Split('-');
                                Char[] chkString = strNewTag.ToCharArray();
                                int cntAlpha = 0;
                                for (int i = strNewTag.Length - 1; i >= 0; i--)
                                {
                                    if (Regex.IsMatch(Convert.ToString(chkString[i]), @"[A-Z]"))
                                    {
                                        cntAlpha = cntAlpha + 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                try
                                {
                                    arrCase4[2] = strNewTag.Substring(strNewTag.Length - cntAlpha, cntAlpha);
                                }
                                catch
                                {
                                    arrCase4[2] = "0";
                                }

                                string stringIntegerPart = "";

                                try
                                {
                                    stringIntegerPart = strNewTag.Substring(0, strNewTag.Length - cntAlpha);
                                }
                                catch
                                {
                                    stringIntegerPart = "";
                                }

                                if (stringIntegerPart.Contains("-"))
                                {
                                    string[] strDevide = stringIntegerPart.Split('-');

                                    if (strDevide.Count() > 2)
                                    {
                                        arrCase4[1] = "0";
                                        arrCase4[0] = "";
                                    }
                                    else
                                    {
                                        arrCase4[0] = strDevide[0] + "-";
                                        arrCase4[1] = strDevide[1];
                                     
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        arrCase4[1] = stringIntegerPart.Substring(stringIntegerPart.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length, Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                    }
                                    catch
                                    {
                                        arrCase4[1] = "0";
                                    }

                                    try
                                    {
                                        arrCase4[0] = stringIntegerPart.Substring(0, stringIntegerPart.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                    }
                                    catch
                                    {
                                        arrCase4[0] = "";
                                    }
                                }

                                //if (arrCase4.Count() != 3)
                                //{
                                //    blnResult = false;
                                //    resultMsg = "Tag format is not correct.";
                                //}
                                //else
                                //{
                                string arrString4 = arrCase4[0];
                                if (arrString4.Length <= 0)
                                {
                                    blnResult = false;
                                    resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                }
                                else
                                {
                                    string intValue4 = arrCase4[1];
                                    Int64 tagIntValue4 = 0;
                                    if (!(Int64.TryParse(intValue4, out tagIntValue4)))
                                    {
                                        blnResult = false;
                                        resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                    }
                                    else
                                    {
                                        //  Check Tag interger value should be in Range
                                        Int64 rangeStart = 0;
                                        Int64 rangeTo = 0;
                                        try
                                        {
                                            rangeStart = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsFrom);
                                        }
                                        catch
                                        { }
                                        try
                                        {
                                            rangeTo = Convert.ToInt64(Globals.CurrentProjectData.LDARTAGStartsTo);
                                        }
                                        catch
                                        { }
                                        if (Globals.CurrentProjectData.LDARTAGStartsFrom.Length != arrCase4[1].Length)
                                        {
                                            blnResult = false;
                                            resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";

                                        }
                                        else
                                        {
                                            if (!((rangeStart <= tagIntValue4) && (rangeTo >= tagIntValue4)))
                                            {
                                                blnResult = false;
                                                resultMsg = "Tag format is not correct. Integer value should be in between range from " + rangeStart + " to " + rangeTo + ".";
                                            }
                                            else
                                            {
                                                string strAlpha = arrCase4[2];
                                                if (strAlpha.Length >= 1)
                                                {

                                                    Char[] convertedString = strAlpha.ToCharArray();
                                                    bool isAlpha = true;
                                                    for (int i = 0; i < strAlpha.Length; i++)
                                                    {
                                                        if (!(Regex.IsMatch(Convert.ToString(convertedString[i]), @"[A-Z]")))
                                                        {
                                                            isAlpha = false;
                                                        }
                                                    }
                                                    if (!(isAlpha))
                                                    {
                                                        blnResult = false;
                                                        resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                                    }
                                                }
                                                else
                                                {
                                                    blnResult = false;
                                                    resultMsg = "Please check Tag Format. Valid format is '[String][" + Globals.CurrentProjectData.LDARTAGStartsFrom.Length + " Digits][Alphabet(s)]'.";
                                                }
                                            }
                                        }
                                    }
                                }
                                //}
                                #endregion
                                break;
                        }
                    }
                }
            }
            else
            {
                blnResult = false;
                resultMsg = "Tag cannot be empty.";
            }
            return blnResult;
        }

        /// <summary>
        /// This function will 
        ///     a. get the Previous Tag
        ///     b. increment its integer value by 1
        ///     c. replace the last digit by X
        ///     d. return the new tag 
        /// </summary>
        /// <param name="nonXTag"></param>
        /// <returns></returns>
        public static string getXTag(string nonXTag)
        {
            string strValidTagFormat = "";
            try
            {
                strValidTagFormat = Convert.ToString(Globals.CurrentProjectData.LDARTAGFormat);
            }
            catch { }

            if (!(string.IsNullOrEmpty(Convert.ToString(strValidTagFormat)))) // If format is NULL or Empty, means Format not available and do not need to check validity
            {
                if (strValidTagFormat != "None")  // If format type is NONE, means Format not applicable and do not need to check validity
                {
                    switch (strValidTagFormat)
                    {
                        case "Integer":
                            #region case Integer
                            Int64 tagIntValue = 0;
                            if (Int64.TryParse(nonXTag, out tagIntValue))
                            {
                                tagIntValue = tagIntValue + 1;

                                string newvalue = Convert.ToString(tagIntValue);
                                if (newvalue.Length > nonXTag.Length)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    if (newvalue.Length < nonXTag.Length)
                                    {
                                        // prefix 0s and replace
                                        Int32 numberOfZeros = nonXTag.Length - newvalue.Length;
                                        string prefixZero = "";
                                        for (Int32 cnt = 0; cnt < numberOfZeros; cnt++)
                                        {
                                            prefixZero = prefixZero + "0";
                                        }
                                        newvalue = prefixZero + newvalue;
                                        nonXTag = newvalue;
                                    }
                                    else
                                    {
                                        // means equal. now replace
                                        nonXTag = newvalue;
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "Integer-Alpha":
                            #region case Integer-Alpha
                            string[] arrCase2 = new string[2]; // strNewTag.Split('-');
                            //if (arrCase2.Count() == 2)
                            //{
                            if (nonXTag.Contains("-"))
                            {
                                arrCase2[1] = "0";
                                arrCase2[0] = "ex";
                            }
                            else
                            {
                                Char[] chkString2 = nonXTag.ToCharArray();
                                int cntAlpha2 = 0;
                                for (int i = nonXTag.Length - 1; i >= 0; i--)
                                {
                                    if (Regex.IsMatch(Convert.ToString(chkString2[i]), @"[A-Z]"))
                                    {
                                        cntAlpha2 = cntAlpha2 + 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                try
                                {
                                    arrCase2[1] = nonXTag.Substring(nonXTag.Length - cntAlpha2, cntAlpha2);
                                }
                                catch
                                {
                                    arrCase2[1] = "0";
                                }


                                //if (arrCase2.Count() != 2)
                                //{
                                //    blnResult = false;
                                //    resultMsg = "Tag format is not correct.";
                                //}
                                //else
                                //{

                                try
                                {
                                    arrCase2[0] = nonXTag.Substring(0, nonXTag.Length - cntAlpha2);
                                }
                                catch
                                {
                                    arrCase2[0] = "ex";
                                }
                            }
                            string intValue2 = arrCase2[0];

                            Int64 tagIntValue2 = 0;
                            if (Int64.TryParse(intValue2, out tagIntValue2))
                            {
                                tagIntValue2 = tagIntValue2 + 1;

                                string newvalue = Convert.ToString(tagIntValue2);
                                if (newvalue.Length > intValue2.Length)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    if (newvalue.Length < intValue2.Length)
                                    {
                                        // prefix 0s and replace
                                        Int32 numberOfZeros = intValue2.Length - newvalue.Length;
                                        string prefixZero = "";
                                        for (Int32 cnt = 0; cnt < numberOfZeros; cnt++)
                                        {
                                            prefixZero = prefixZero + "0";
                                        }
                                        newvalue = prefixZero + newvalue;
                                        nonXTag = newvalue + arrCase2[1];
                                    }
                                    else
                                    {
                                        // means equal. now replace
                                        nonXTag = newvalue + arrCase2[1];
                                    }
                                }
                            }
                            //}
                            #endregion
                            break;
                        case "String-Integer":
                            #region start
                            string[] arrCase3 = new string[2];

                            string stringIntegerPart3 = nonXTag;

                            if (stringIntegerPart3.Contains("-"))
                            {

                                string[] strDevide = stringIntegerPart3.Split('-');
                                if (strDevide.Count() > 2)
                                {
                                    arrCase3[0] = "";
                                    arrCase3[1] = "a";
                                }
                                else
                                {
                                    arrCase3[0] = strDevide[0] + "-";
                                    arrCase3[1] = strDevide[1];
                                }
                            }
                            else
                            {
                                try
                                {
                                    arrCase3[1] = stringIntegerPart3.Substring(stringIntegerPart3.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length, Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                }
                                catch
                                {
                                    arrCase3[1] = "a";
                                }

                                try
                                {
                                    arrCase3[0] = stringIntegerPart3.Substring(0, stringIntegerPart3.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                }
                                catch
                                {
                                    arrCase3[0] = "";
                                }
                            }
                            //if (arrCase3.Count() == 2)
                            //{
                            string intValue3 = arrCase3[1];
                            Int64 tagIntValue3 = 0;
                            if (Int64.TryParse(intValue3, out tagIntValue3))
                            {
                                tagIntValue3 = tagIntValue3 + 1;

                                string newvalue = Convert.ToString(tagIntValue3);
                                if (newvalue.Length > intValue3.Length)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    if (newvalue.Length < intValue3.Length)
                                    {
                                        // prefix 0s and replace
                                        Int32 numberOfZeros = intValue3.Length - newvalue.Length;
                                        string prefixZero = "";
                                        for (Int32 cnt = 0; cnt < numberOfZeros; cnt++)
                                        {
                                            prefixZero = prefixZero + "0";
                                        }
                                        newvalue = prefixZero + newvalue;
                                        // nonXTag = arrCase3[0] + "-" + newvalue;
                                        nonXTag = arrCase3[0] + newvalue;
                                    }
                                    else
                                    {
                                        // means equal. now replace
                                        //  nonXTag = arrCase3[0] + "-" + newvalue;
                                        nonXTag = arrCase3[0] + newvalue;
                                    }
                                }
                            }
                            //}
                            #endregion
                            break;
                        case "String-Integer-Alpha":
                            #region start
                            string[] arrCase4 = new string[3]; // strNewTag.Split('-');
                            Char[] chkString = nonXTag.ToCharArray();
                            int cntAlpha = 0;
                            for (int i = nonXTag.Length - 1; i >= 0; i--)
                            {
                                if (Regex.IsMatch(Convert.ToString(chkString[i]), @"[A-Z]"))
                                {
                                    cntAlpha = cntAlpha + 1;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            try
                            {
                                arrCase4[2] = nonXTag.Substring(nonXTag.Length - cntAlpha, cntAlpha);
                            }
                            catch
                            {
                                arrCase4[2] = "0";
                            }

                            string stringIntegerPart = "";

                            try
                            {
                                stringIntegerPart = nonXTag.Substring(0, nonXTag.Length - cntAlpha);
                            }
                            catch
                            {
                                stringIntegerPart = "";
                            }

                            if (stringIntegerPart.Contains("-"))
                            {
                                string[] strDevide = stringIntegerPart.Split('-');

                                if (strDevide.Count() > 2)
                                {
                                    arrCase4[0] = "";
                                    arrCase4[1] = "0";
                                }
                                else
                                {
                                    arrCase4[0] = strDevide[0] + "-";
                                    arrCase4[1] = strDevide[1];
                                }
                            }
                            else
                            {
                                try
                                {
                                    arrCase4[1] = stringIntegerPart.Substring(stringIntegerPart.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length, Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                }
                                catch
                                {
                                    arrCase4[1] = "0";
                                }

                                try
                                {
                                    arrCase4[0] = stringIntegerPart.Substring(0, stringIntegerPart.Length - Globals.CurrentProjectData.LDARTAGStartsFrom.Length);
                                }
                                catch
                                {
                                    arrCase4[0] = "";
                                }
                            }

                            //if (arrCase4.Count() == 3)
                            //{
                            string intValue = arrCase4[1];
                            Int64 tagIntValue4 = 0;
                            if (Int64.TryParse(intValue, out tagIntValue4))
                            {
                                tagIntValue4 = tagIntValue4 + 1;

                                string newvalue = Convert.ToString(tagIntValue4);
                                if (newvalue.Length > intValue.Length)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    if (newvalue.Length < intValue.Length)
                                    {
                                        // prefix 0s and replace
                                        Int32 numberOfZeros = intValue.Length - newvalue.Length;
                                        string prefixZero = "";
                                        for (Int32 cnt = 0; cnt < numberOfZeros; cnt++)
                                        {
                                            prefixZero = prefixZero + "0";
                                        }
                                        newvalue = prefixZero + newvalue;
                                        //nonXTag = arrCase4[0] + "-" + newvalue + "-" + arrCase4[2];
                                        nonXTag = arrCase4[0] + newvalue + arrCase4[2];
                                    }
                                    else
                                    {
                                        // means equal. now replace
                                        //nonXTag = arrCase4[0] + "-" + newvalue + "-" + arrCase4[2];
                                        nonXTag = arrCase4[0] + newvalue + arrCase4[2];
                                    }
                                }
                            }
                            //}
                            #endregion
                            break;
                    }
                }
            }

            return nonXTag.Substring(0, nonXTag.Length - 1) + "X";
        }

        public static string getFormatMessage(string msg)
        {
            return msg;
        }
    }
}
