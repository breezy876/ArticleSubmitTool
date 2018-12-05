using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace ArticleSubmitTool.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var newEl = new XElement("Parent",
                new XElement("Child", 
                    new XAttribute("Test", "1"), 
                    new [] { new XElement("TestChild1"), null, null, new XElement("TestChild2") }));

            var str = newEl.ToString();
        }
    }
}
