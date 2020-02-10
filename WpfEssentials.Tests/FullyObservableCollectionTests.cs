using System.Collections.Generic;
using Xunit;

namespace WpfEssentials.Tests
{
    public class FullyObservableCollectionTests
    {
        [Fact]
        public void ModifyItem()
        {
            List<ItemStateChangedEventArgs> raisedEventArgs = new List<ItemStateChangedEventArgs>();
            FullyObservableCollection<TestObject> c = new FullyObservableCollection<TestObject>();

            c.ItemStateChanged += delegate (object sender, ItemStateChangedEventArgs e)
            {
                raisedEventArgs.Add(e);
            };

            TestObject o0 = new TestObject();
            TestObject o1 = new TestObject();

            c.Add(o0);
            c.Add(o1);

            o0.Description = "Test0";
            c[1].Description = "Test1";

            Assert.Empty(raisedEventArgs);
        }

        [Fact]
        public void ModifyItemObservable()
        {
            List<ItemStateChangedEventArgs> raisedEventArgs = new List<ItemStateChangedEventArgs>();
            FullyObservableCollection<TestObject2> c = new FullyObservableCollection<TestObject2>();

            c.ItemStateChanged += delegate (object sender, ItemStateChangedEventArgs e)
            {
                raisedEventArgs.Add(e);
            };

            TestObject2 o0 = new TestObject2();
            TestObject2 o1 = new TestObject2();

            c.Add(o0);
            c.Add(o1);

            o0.Description = "Test0";
            c[1].Description = "Test1";

            Assert.Equal(2, raisedEventArgs.Count);
            Assert.Equal(0, raisedEventArgs[0].ItemIndex);
            Assert.Equal(1, raisedEventArgs[1].ItemIndex);
            Assert.Equal(nameof(TestObject2.Description), raisedEventArgs[0].PropertyName);
            Assert.Equal(nameof(TestObject2.Description), raisedEventArgs[1].PropertyName);
        }

        #region Test Objects
        public class TestObject
        {
            public string Description { get; set; }
        }

        public class TestObject2 : ObservableObject
        {
            private string m_description;

            public string Description
            {
                get { return m_description; }
                set { m_description = value; OnPropertyChanged(); }
            }
        }
        #endregion
    }
}
