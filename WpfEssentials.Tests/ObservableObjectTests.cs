using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace WpfEssentials.Tests
{
    public class ObservableObjectTests
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
        public void PropertyChangedRaised()
        {
            List<string> raisedEvents = new List<string>();
            TestObject o = new TestObject();

            o.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                raisedEvents.Add(e.PropertyName);
            };
            o.Description = "Test";

            Assert.Single(raisedEvents);
            Assert.Equal(nameof(TestObject.Description), raisedEvents[0]);
        }
    }
}
