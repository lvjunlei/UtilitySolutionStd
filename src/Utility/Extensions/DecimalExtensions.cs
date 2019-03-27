#region DecimalExtensions 文件信息
/***********************************************************
**文 件 名：DecimalExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-08 10:10:31 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Extensions
{
    /// <summary>
    /// DecimalExtensions
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// 转换为汉字钱数格式
        /// </summary>
        /// <param name="number">钱数</param>
        /// <param name="isTraditional">是否是传统汉字格式（默认）</param>
        /// <returns></returns>
        public static string ToChinese(this decimal number, bool isTraditional = true)
        {
            var traditionNums = isTraditional ? "零壹贰叁肆伍陆柒捌玖" : "零一二三四五六七八九"; //0-9所对应的汉字
            var traditionUnits = isTraditional ? "兆仟佰拾亿仟佰拾万仟佰拾元角分" : "兆千百十亿千百十万千百十元角分"; //数字位所对应的汉字
            var chinese = "";  //人民币大写金额形式

            var ch2 = "";//数字位的汉字读法
            var nzero = 0;//用来计算连续的零值是几个

            number = Math.Round(Math.Abs(number), 2);//将number取绝对值并四舍五入取2位小数
            var str4 = ((long)(number * 100)).ToString();
            var j = str4.Length;
            if (j > 15) { return "溢出"; }
            traditionUnits = traditionUnits.Substring(15 - j);//取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (var i = 0; i < j; i++)
            {
                var str3 = str4.Substring(i, 1);//从原number值中取出的值
                var temp = Convert.ToInt32(str3);//从原number值中取出的值
                string ch1;//数字的汉语读法
                if (i != j - 3 && i != j - 7 && i != j - 11 && i != j - 15)
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + traditionNums.Substring(temp * 1, 1);
                            ch2 = traditionUnits.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = traditionNums.Substring(temp * 1, 1);
                            ch2 = traditionUnits.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + traditionNums.Substring(temp * 1, 1);
                        ch2 = traditionUnits.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = traditionNums.Substring(temp * 1, 1);
                            ch2 = traditionUnits.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = traditionUnits.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == j - 11 || i == j - 3)
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = traditionUnits.Substring(i, 1);
                }
                chinese = chinese + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    chinese = chinese + '整';
                }
            }
            if (number == 0)
            {
                chinese = "零元整";
            }
            return chinese;
        }
    }
}
