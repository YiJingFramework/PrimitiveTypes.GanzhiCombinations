using Microsoft.VisualStudio.TestTools.UnitTesting;
using YiJingFramework.PrimitiveTypes.GanzhiCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.PrimitiveTypes.GanzhiCombinations.Tests;

[TestClass()]
public class TianganOrDizhiTests
{
    [TestMethod()]
    public void ConvertingTest()
    {
        for (int i = 0; i < 10; i++)
        {
            var tiangan = (Tiangan)i;
            var td = new TianganOrDizhi(tiangan);

            Assert.AreEqual(false, td.IsDizhi);
            Assert.AreEqual(true, td.IsTiangan);
            Assert.AreEqual(new TianganOrDizhi(tiangan.Next()), td.Next());
            Assert.AreEqual(new TianganOrDizhi(tiangan.Next(123)), td.Next(123));
            Assert.AreEqual(new TianganOrDizhi(tiangan.Next(-123)), td.Next(-123));
            Assert.AreEqual(new TianganOrDizhi(tiangan + 1232), td + 1232);
            Assert.AreEqual(new TianganOrDizhi(tiangan - 78), td - 78);
            Assert.AreEqual(tiangan.ToString(), td.ToString());
            Assert.AreEqual(tiangan.ToString("C"), td.ToString("C"));
            Assert.AreEqual(tiangan.ToString("G"), td.ToString("G"));
            Assert.AreEqual(tiangan.ToString(null), td.ToString(null));
            Assert.AreEqual(false, td.TryAsDizhi(out _));
            Assert.AreEqual(true, td.TryAsTiangan(out var tianganBack));
            Assert.AreEqual(tiangan, tianganBack);
            Assert.AreEqual(tiangan, (Tiangan)td);
            _ = Assert.ThrowsException<InvalidCastException>(() => (Dizhi)td);
        }

        for (int i = 0; i < 12; i++)
        {
            var dizhi = (Dizhi)i;
            var td = new TianganOrDizhi(dizhi);

            Assert.AreEqual(true, td.IsDizhi);
            Assert.AreEqual(false, td.IsTiangan);
            Assert.AreEqual(new TianganOrDizhi(dizhi.Next()), td.Next());
            Assert.AreEqual(new TianganOrDizhi(dizhi.Next(123)), td.Next(123));
            Assert.AreEqual(new TianganOrDizhi(dizhi.Next(-123)), td.Next(-123));
            Assert.AreEqual(new TianganOrDizhi(dizhi + 1232), td + 1232);
            Assert.AreEqual(new TianganOrDizhi(dizhi - 78), td - 78);
            Assert.AreEqual(dizhi.ToString(), td.ToString());
            Assert.AreEqual(dizhi.ToString("C"), td.ToString("C"));
            Assert.AreEqual(dizhi.ToString("G"), td.ToString("G"));
            Assert.AreEqual(dizhi.ToString(null), td.ToString(null));
            Assert.AreEqual(true, td.TryAsDizhi(out var dizhiBack));
            Assert.AreEqual(dizhi, dizhiBack);
            Assert.AreEqual(false, td.TryAsTiangan(out _));
            _ = Assert.ThrowsException<InvalidCastException>(() => (Tiangan)td);
            Assert.AreEqual(dizhi, (Dizhi)td);
        }
    }

    [TestMethod()]
    public void ComparingTest()
    {
        for (int i = 0; i < 20000; i++)
        {
            var fir = Random.Shared.Next(0, 10 + 12);
            var sec = Random.Shared.Next(0, 10 + 12);
            TianganOrDizhi firF;
            if(fir < 10)
                firF = new TianganOrDizhi((Tiangan)fir);
            else
                firF = new TianganOrDizhi((Dizhi)(fir - 10));
            TianganOrDizhi secF;
            if (sec < 10)
                secF = new TianganOrDizhi((Tiangan)sec);
            else
                secF = new TianganOrDizhi((Dizhi)(sec - 10));
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