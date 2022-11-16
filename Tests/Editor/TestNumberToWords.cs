using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Voxell.Speech.Common;
using System.Text.RegularExpressions;

public class TestNumberToWords
{
    void TestNumber(int number)
    {
        var words = NumberToWords.ToWords($"{number}");
        Assert.IsTrue(Regex.IsMatch(words.Replace(" ", ""), @"^[a-z.-]+$"), words);
    }
    void TestRange(int start, int end)
    {
        for (int i = start; i <= end; i++)
            TestNumber(i);
    }

    [Test] public void _0_9() => TestRange(0, 9);
    [Test] public void _10_99() => TestRange(10, 99);
    [Test] public void _1000_1999() => TestRange(1000, 1999);
    [Test] public void _2000_2999() => TestRange(2000, 2999);
    [Test] public void _3000_3999() => TestRange(3000, 3999);
    [Test] public void _4000_4999() => TestRange(4000, 4999);
    [Test] public void _5000_5999() => TestRange(5000, 5999);
    [Test] public void _15000_15999() => TestRange(15000, 15999);
    [Test] public void _1000000_1010000() => TestRange(1000000, 1010000);

    [Test] public void _123123123() => TestNumber(123123123);
    [Test] public void _456456456() => TestNumber(456456456);
    [Test] public void _111111111() => TestNumber(111111111);
    [Test] public void _222222222() => TestNumber(222222222);
    [Test] public void _333333333() => TestNumber(333333333);
    [Test] public void _444444444() => TestNumber(444444444);

    [Test] public void _15999_15000() => TestRange(-15999, -15000);
}
