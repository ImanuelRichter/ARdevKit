using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARdevKit;

namespace EditorTest
{
    [TestClass]
    public class EditorWindowFormTest
    {
        private EditorWindow ew;

        [TestInitialize]
        public void initTest()
        {
            ew = new EditorWindow();
        }

        [TestMethod]
        public void testCreateNewProject1()
        {
            ew.createNewProject(null);
            Assert.IsTrue(ew.project.Name == null);
        }

        [TestMethod]
        public void testCreateNewProject2()
        {
            ew.createNewProject("");
            Assert.IsTrue(String.Compare(ew.project.Name , "") == 0);
        }
    }
}
