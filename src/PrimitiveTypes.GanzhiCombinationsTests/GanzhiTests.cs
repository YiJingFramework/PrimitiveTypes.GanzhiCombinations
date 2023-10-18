using Microsoft.VisualStudio.TestTools.UnitTesting;
using YiJingFramework.PrimitiveTypes.GanzhiCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.PrimitiveTypes.GanzhiCombinations.Tests;

[TestClass()]
public class GanzhiTests
{
    [TestMethod()]
    public void ConvertingTest()
    {
        Assert.AreEqual(1, Ganzhi.FromGanzhi(Tiangan.Jia, Dizhi.Zi).Index);
        Assert.AreEqual(4, Ganzhi.FromIndex(4).Index);
        Assert.AreEqual(2, Ganzhi.FromIndex(62).Index);

        Assert.AreEqual("Yimao", Ganzhi.FromGanzhi(Tiangan.Yi, Dizhi.Mao).ToString());
        Assert.AreEqual("乙卯", Ganzhi.FromGanzhi(Tiangan.Yi, Dizhi.Mao).ToString("C"));

        Assert.AreEqual("癸亥", (Ganzhi.FromIndex(0)).ToString("C"));
        Assert.AreEqual("辛酉", (Ganzhi.FromIndex(-2)).ToString("C"));

        for (int i = -1019, j = 1; i < 1000; i++)
        {
            Assert.AreEqual(Ganzhi.FromIndex(j), Ganzhi.FromIndex(i));
            j++;
            if (j == 61)
                j = 1;
        }

        Assert.AreEqual(Ganzhi.FromIndex(15 + 212), Ganzhi.FromIndex(15).Next(212));
        Assert.AreEqual(Ganzhi.FromIndex(15 - 28222), Ganzhi.FromIndex(15).Next(-28222));

        Assert.AreEqual(Ganzhi.FromIndex(15).Next(212), Ganzhi.FromIndex(15) + 212);
        Assert.AreEqual(Ganzhi.FromIndex(15).Next(-28222), Ganzhi.FromIndex(15) - 28222);

        Assert.AreEqual(1, Ganzhi.FromIndex(2) - Ganzhi.FromIndex(1));
        Assert.AreEqual(0, Ganzhi.FromIndex(0) - Ganzhi.FromIndex(0));
        Assert.AreEqual(59, Ganzhi.FromIndex(1) - Ganzhi.FromIndex(2));
    }

    [TestMethod()]
    public void ComparingTest()
    {
        Random r = new Random();
        for (int i = 0; i < 20000; i++)
        {
            var fir = r.Next(1, 13);
            var sec = r.Next(1, 13);
            var firF = Ganzhi.FromIndex(fir);
            var secF = Ganzhi.FromIndex(sec);
            if (fir == sec)
            {
                Assert.AreEqual(0, firF.CompareTo(secF));
                Assert.AreEqual(0, secF.CompareTo(firF));
                Assert.AreEqual(true, firF.Equals(secF));
                Assert.AreEqual(true, secF.Equals(firF));
                Assert.AreEqual(true, firF.Equals((object)secF));
                Assert.AreEqual(true, secF.Equals((object)firF));
                Assert.AreEqual(firF.GetHashCode(), secF.GetHashCode());
                Assert.AreEqual(true, firF == secF);
                Assert.AreEqual(true, secF == firF);
                Assert.AreEqual(false, firF != secF);
                Assert.AreEqual(false, secF != firF);
            }

            else if (fir < sec)
            {
                Assert.AreEqual(-1, firF.CompareTo(secF));
                Assert.AreEqual(1, secF.CompareTo(firF));
                Assert.AreEqual(false, firF.Equals(secF));
                Assert.AreEqual(false, secF.Equals(firF));
                Assert.AreEqual(false, firF.Equals((object)secF));
                Assert.AreEqual(false, secF.Equals((object)firF));
                Assert.AreNotEqual(firF.GetHashCode(), secF.GetHashCode());
                Assert.AreEqual(false, firF == secF);
                Assert.AreEqual(false, secF == firF);
                Assert.AreEqual(true, firF != secF);
                Assert.AreEqual(true, secF != firF);
            }

            else // fir > sec
            {
                Assert.AreEqual(1, firF.CompareTo(secF));
                Assert.AreEqual(-1, secF.CompareTo(firF));
                Assert.AreEqual(false, firF.Equals(secF));
                Assert.AreEqual(false, secF.Equals(firF));
                Assert.AreEqual(false, firF.Equals((object)secF));
                Assert.AreEqual(false, secF.Equals((object)firF));
                Assert.AreNotEqual(firF.GetHashCode(), secF.GetHashCode());
                Assert.AreEqual(false, firF == secF);
                Assert.AreEqual(false, secF == firF);
                Assert.AreEqual(true, firF != secF);
                Assert.AreEqual(true, secF != firF);
            }
            Assert.AreEqual(false, firF.Equals(null));
            Assert.AreEqual(false, secF.Equals(new object()));
        }
    }
}