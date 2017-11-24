namespace GroupAggregationLibrary.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GroupAggregationLibrary.Models;

    using NUnit.Framework;

    [TestFixture]
    public class GroupAggregationProviderTests
    {
        [Test]
        public void AggregateExtendedDataSummarizeQuantity()
        {
            var list = this.GetExtendedList();

            var provider = new GroupAggregationProvider<ExtendedData>();

            var result = provider.Execute(
                list,
                new List<string> { "Name" },
                new List<string> { "Price" },
                new List<string> { "Quantity", "StockQuantity" });

            Assert.AreEqual(2, result.Count());

            ExtendedData[] arrayResult = result.ToArray();

            var data1 = arrayResult[0];

            this.AssertDataObject(data1, 0, "GRE", 21, 5);
            Assert.AreEqual(6, data1.StockQuantity);

            var data2 = arrayResult[1];

            this.AssertDataObject(data2, 0, "RT", 85, 33);
            Assert.AreEqual(90, data2.StockQuantity);
        }

        [Test]
        public void AggregateByTwoGroups()
        {
            var list = this.GetList();
            
            var provider = new GroupAggregationProvider<Data>();

            IEnumerable<Data> result = provider.Execute(list, new List<string> { "Name", "Price" });

            Assert.AreEqual(8, result.Count());

            // Aggregated
            // new Data { Id = 3, Name = "AA", Quantity = 43, Price = 6 },
            // new Data { Id = 3, Name = "AA", Quantity = 2, Price = 6 }, 
        }

        [Test]
        public void EmptyAverageList()
        {
            List<Data> list = this.GetList();

            var provider = new GroupAggregationProvider<Data>();
            var result = provider.Execute(
                list,
                new List<string> { "Name" },
                null,
                new List<string> { "Quantity" });

            var listedResult = result.ToList();

            var data1 = listedResult[0];

            this.AssertDataObject(data1, 0, "AAA", 21, 0);
        }

        [Test]
        public void InvalidFieldNameInQuantityList()
        {
            List<Data> item = this.GetList(1);

            var provider = new GroupAggregationProvider<Data>();

            try
            {
                provider.Execute(item, new List<string> { "AAA" }, new List<string> { "Fake" });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NullReferenceException>(ex);

                Assert.AreEqual(ex.Message, Constants.Messages.PropertyNotFound);
            }
        }

        [Test]
        public void InvalidFieldNameInSumList()
        {
            List<Data> item = this.GetList(1);

            var provider = new GroupAggregationProvider<Data>();

            try
            {
                provider.Execute(item, new List<string> { "AAA" }, null, new List<string> { "Fake" });
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<NullReferenceException>(ex);

                Assert.AreEqual(ex.Message, Constants.Messages.PropertyNotFound);
            }
        }

        [Test]
        public void ReceiveFourGroupsOneSumOneAverage()
        {
            var list = this.GetList();

            GroupAggregationProvider<Data> provider = new GroupAggregationProvider<Data>();

            IEnumerable<Data> result = provider.Execute(
                list,
                new List<string> { "Name" },
                new List<string> { "Price" },
                new List<string> { "Quantity" });

            Assert.NotNull(result);
            Assert.AreEqual(4, result.Count());

            List<Data> listedResult = result.ToList();

            var data1 = listedResult[0];

            this.AssertDataObject(data1, 0, "AAA", 21, 5);

            var Data = listedResult[1];

            this.AssertDataObject(Data, 0, "AA", 51, 33);

            var data3 = listedResult[2];

            this.AssertDataObject(data3, 0, "ADA", 24, 6);

            var data4 = listedResult[3];

            this.AssertDataObject(data4, 0, "AEA", 42, 345);
        }

        private void AssertDataObject<T>(T actualData, int id, string name, int quantity, double price) where T : Data
        {
            Assert.AreEqual(id, actualData.Id);
            Assert.AreEqual(name, actualData.Name);
            Assert.AreEqual(quantity, actualData.Quantity);
            Assert.AreEqual(price, actualData.Price);
        }

        private List<ExtendedData> GetExtendedList(int count = 7)
        {
            var list = new List<ExtendedData>
                {
                    new ExtendedData { Id = 1, Name = "GRE", Quantity = 5, Price = 5, StockQuantity = 1 },
                    new ExtendedData { Id = 2, Name = "GRE", Quantity = 6, Price = 6, StockQuantity = 2 },
                    new ExtendedData { Id = 34, Name = "GRE", Quantity = 10, Price = 4, StockQuantity = 3 },
                    //
                    new ExtendedData { Id = 3, Name = "RT", Quantity = 42, Price = 6, StockQuantity = 11 },
                    new ExtendedData { Id = 4, Name = "RT", Quantity = 35, Price = 67, StockQuantity = 45 },
                    new ExtendedData { Id = 3, Name = "RT", Quantity = 1, Price = 6, StockQuantity = 15 },
                    new ExtendedData { Id = 4, Name = "RT", Quantity = 7, Price = 53, StockQuantity = 19 },
                };
            return list.Take(count).ToList();
        }

        private List<Data> GetList(int count = 9)
        {
            var list = new List<Data>
                {
                    // 1
                    new Data{ Id = 1, Name = "AAA", Quantity = 5, Price = 5 },
                    new Data { Id = 2, Name = "AAA", Quantity = 6, Price = 6 },
                    new Data { Id = 34, Name = "AAA", Quantity = 10, Price = 4 },

                    // 2
                    new Data { Id = 3, Name = "AA", Quantity = 43, Price = 6 },
                    new Data { Id = 4, Name = "AA", Quantity = 3, Price = 67 },
                    new Data { Id = 3, Name = "AA", Quantity = 2, Price = 6 },
                    new Data { Id = 4, Name = "AA", Quantity = 3, Price = 53 },

                    // 3
                    new Data { Id = 3, Name = "ADA", Quantity = 24, Price = 6 },

                    // 4
                    new Data { Id = 4, Name = "AEA", Quantity = 42, Price = 345 }
                };
            return list.Take(count).ToList();
        }
    }
}