using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataMining.DecisionTree.SplitQualityAlgorithm;

namespace DataMiningUnitTest
{
    [TestClass]
    public class DecisionTreeTests
    {
        [TestMethod]
        public void GiniSplit_GiniIndex_CommonTest()
        {
            // Arrange
            var giniSplit = new GiniSplit();
            var testSplit = new List<object>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };

            // Act
            var actual = giniSplit.GiniIndex(testSplit);

            // Assert
            Assert.AreEqual(0.81481481481481481481481481481481, actual, 0.000000000000001);
        }

        [TestMethod]
        public void GiniSplit_SplitQuality_CommonTest()
        {
            // Arrange
            var giniSplit = new GiniSplit();
            List<object> firstSplit = new List<object>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
            List<object> secondSplit = new List<object>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };

            // Act
            var actual = giniSplit.CalcSplitQuality(new List<List<object>>() { firstSplit, secondSplit });

            // Assert
            Assert.AreEqual(0.81481481481481481481481481481482, actual, 0.000000000000001);
        }

        //[TestMethod]
        //public void GiniSplit_CompareTo_BetterSplitTest()
        //{
        //    // Arrange
        //    var giniSplit = new GiniSplit();
        //    var betterGiniSplit = new GiniSplit();
            
        //    List<double> firstSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> secondSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    giniSplit.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit }, firstSplit.Count + secondSplit.Count);
            
        //    List<double> fourthSplit = new List<double>() { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
        //    List<double> fifthSplit = new List<double>() { 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0 };
        //    betterGiniSplit.CalcSplitQuality(new List<List<double>>() { fourthSplit, fifthSplit }, fourthSplit.Count + fifthSplit.Count);

        //    // Act
        //    var actual = giniSplit.CompareTo(betterGiniSplit);

        //    // Assert
        //    Assert.AreEqual<int>(1, actual);
        //}

        //[TestMethod]
        //public void GiniSplit_CompareTo_WorseSplitTest()
        //{
        //    // Arrange
        //    var worseGiniSplit = new GiniSplit();
        //    var giniSplit = new GiniSplit();

        //    List<double> firstSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> secondSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    worseGiniSplit.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit }, firstSplit.Count + secondSplit.Count);

        //    List<double> fourthSplit = new List<double>() { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
        //    List<double> fifthSplit = new List<double>() { 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0 };
        //    giniSplit.CalcSplitQuality(new List<List<double>>() { fourthSplit, fifthSplit }, fourthSplit.Count + fifthSplit.Count);

        //    // Act
        //    var actual = giniSplit.CompareTo(worseGiniSplit);

        //    // Assert
        //    Assert.AreEqual<int>(-1, actual);
        //}

        //[TestMethod]
        //public void GiniSplit_CompareTo_EqualSplitTest()
        //{
        //    // Arrange
        //    var giniSplit = new GiniSplit();
        //    var equalGiniSplit = new GiniSplit();

        //    List<double> firstSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> secondSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    equalGiniSplit.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit }, firstSplit.Count + secondSplit.Count);

        //    List<double> fourthSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> fifthSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    giniSplit.CalcSplitQuality(new List<List<double>>() { fourthSplit, fifthSplit }, fourthSplit.Count + fifthSplit.Count);

        //    // Act
        //    var actual = giniSplit.CompareTo(equalGiniSplit);

        //    // Assert
        //    Assert.AreEqual<int>(0, actual);
        //}

        [TestMethod]
        public void GiniSplitOptimized_GiniIndex_CommonTest()
        {
            // Arrange
            var giniSplit = new GiniSplitOptimized();
            var testSplit = new List<object>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };

            // Act
            var actual = giniSplit.GiniIndexOptimized(testSplit);

            // Assert
            Assert.AreEqual(15.0, actual, 0.0);
        }

        [TestMethod]
        public void GiniSplitOptimized_SplitQuality_CommonTest()
        {
            // Arrange
            var giniSplit = new GiniSplitOptimized();
            List<object> firstSplit = new List<object>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
            List<object> secondSplit = new List<object>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };

            // Act
            var actual = giniSplit.CalcSplitQuality(new List<List<object>>() { firstSplit, secondSplit });

            // Assert
            Assert.AreEqual(3.3333333333333333333333333333333, actual, 0.000000000000001);
        }

        //[TestMethod]
        //public void GiniSplitOptimized_CompareTo_BetterSplitTest()
        //{
        //    // Arrange
        //    var giniSplit = new GiniSplitOptimized();
        //    var betterGiniSplit = new GiniSplitOptimized();

        //    List<double> firstSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> secondSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    giniSplit.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit }, firstSplit.Count + secondSplit.Count);

        //    List<double> fourthSplit = new List<double>() { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
        //    List<double> fifthSplit = new List<double>() { 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0 };
        //    betterGiniSplit.CalcSplitQuality(new List<List<double>>() { fourthSplit, fifthSplit }, fourthSplit.Count + fifthSplit.Count);

        //    // Act
        //    var actual = giniSplit.CompareTo(betterGiniSplit);

        //    // Assert
        //    Assert.AreEqual<int>(1, actual);
        //}

        //[TestMethod]
        //public void GiniSplitOptimized_CompareTo_WorseSplitTest()
        //{
        //    // Arrange
        //    var worseGiniSplit = new GiniSplitOptimized();
        //    var giniSplit = new GiniSplitOptimized();

        //    List<double> firstSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> secondSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    worseGiniSplit.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit }, firstSplit.Count + secondSplit.Count);

        //    List<double> fourthSplit = new List<double>() { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
        //    List<double> fifthSplit = new List<double>() { 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0, 7.0 };
        //    giniSplit.CalcSplitQuality(new List<List<double>>() { fourthSplit, fifthSplit }, fourthSplit.Count + fifthSplit.Count);

        //    // Act
        //    var actual = giniSplit.CompareTo(worseGiniSplit);

        //    // Assert
        //    Assert.AreEqual<int>(-1, actual);
        //}

        //[TestMethod]
        //public void GiniSplitOptimized_CompareTo_EqualSplitTest()
        //{
        //    // Arrange
        //    var giniSplit = new GiniSplitOptimized();
        //    var equalGiniSplit = new GiniSplitOptimized();

        //    List<double> firstSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> secondSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    equalGiniSplit.CalcSplitQuality(new List<List<double>>() { firstSplit, secondSplit }, firstSplit.Count + secondSplit.Count);

        //    List<double> fourthSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    List<double> fifthSplit = new List<double>() { 1.1, 2.0, 1.3, 1.1, 0.0, -2.0, -1.0, 2.0, -1.0 };
        //    giniSplit.CalcSplitQuality(new List<List<double>>() { fourthSplit, fifthSplit }, fourthSplit.Count + fifthSplit.Count);

        //    // Act
        //    var actual = giniSplit.CompareTo(equalGiniSplit);

        //    // Assert
        //    Assert.AreEqual<int>(0, actual);
        //}
    }
}