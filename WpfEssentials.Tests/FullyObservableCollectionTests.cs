using System.Collections.Generic;
using WpfEssentials.Collections;
using WpfEssentials.Events;
using Xunit;

namespace WpfEssentials.Tests
{
    public class FullyObservableCollectionTests
    {
        public class TestObject : ObservableObject
        {
            private string m_description;

            public string Description
            {
                get { return m_description; }
                set { m_description = value; OnPropertyChanged(); }
            }
        }

        [Fact]
        public void ItemPropertyChangedRaised()
        {
            List<ItemPropertyChangedEventArgs> raisedEventArgs = new List<ItemPropertyChangedEventArgs>();
            FullyObservableCollection<TestObject> c = new FullyObservableCollection<TestObject>();

            c.ItemPropertyChanged += delegate (object sender, ItemPropertyChangedEventArgs e)
            {
                raisedEventArgs.Add(e);
            };

            TestObject o0 = new TestObject();
            TestObject o1 = new TestObject();

            c.Add(o0);
            c.Add(o1);

            o0.Description = "Test0";
            c[1].Description = "Test1";

            Assert.Equal(2, raisedEventArgs.Count);
            Assert.Equal(0, raisedEventArgs[0].ItemIndex);
            Assert.Equal(1, raisedEventArgs[1].ItemIndex);
            Assert.Equal(nameof(TestObject.Description), raisedEventArgs[0].PropertyName);
            Assert.Equal(nameof(TestObject.Description), raisedEventArgs[1].PropertyName);
        }
    }
}
