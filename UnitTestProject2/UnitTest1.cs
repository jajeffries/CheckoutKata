using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

 //Item   Unit      Special
 //        Price     Price
 // --------------------------
 //   A     50       3 for 130
 //   B     30       2 for 45
 //   C     20
 //   D     15
namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        private Checkout CreateCheckout
        {
            get { return new Checkout(new Dictionary<string, 
            Func<int,int>>{{"A", i => i == 3 ? 130: 50 * i  }, 
            {"B", i => i * 30}}); }
        }



        [TestMethod]
        public void No_items_should_total_zero()
        {
            var actual = CreateCheckout.Total();
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void A_item_should_total_50()
        {
            var checkout = CreateCheckout;
            checkout.Scan("A");
            var actual = checkout.Total();
            Assert.AreEqual(50, actual);
        }

        [TestMethod]
        public void B1_item_should_total_30()
        {
            var checkout = CreateCheckout;
            checkout.Scan("B");
            var actual = checkout.Total();
            Assert.AreEqual(30, actual);
        }

        [TestMethod]
        public void A_plus_B_should_total_80()
        {
            var checkout = CreateCheckout;
            checkout.Scan("A");
            checkout.Scan("B");
            var actual = checkout.Total();
            Assert.AreEqual(80, actual);
        }

        [TestMethod]
        public void A_times_three_should_total_130()
        {
            var checkout = CreateCheckout;
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            var actual = checkout.Total();
            Assert.AreEqual(130, actual);
        }
    }

    public class Checkout
    {
        private readonly Dictionary<string, Func<int, int>> _prices = new Dictionary<string, Func<int, int>>();
        private readonly Dictionary<string, int> _bag = new Dictionary<string, int>();

        public Checkout(Dictionary<string, Func<int, int>> prices)
        {
            _prices = prices;
        }

        public int Total()
        {
            return _bag.Sum(itemCount => _prices[itemCount.Key](itemCount.Value));
        }

        public void Scan(string item)
        {
            if (_bag.ContainsKey(item))
                _bag[item]++;
            else
                _bag.Add(item, 1);
        }
    }
}
