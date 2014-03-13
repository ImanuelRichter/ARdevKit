using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit.Model.Project;
using System.IO;

namespace EditorTest.Model.Project
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für CustomUserEventTest
    /// </summary>
    [TestClass]
    public class CustomUserEventTest
    {
        public CustomUserEventTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("CustomUserEvent")]
        public void NullTest()
        {
            CustomUserEvent c = new CustomUserEvent(null);
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        public void CreateUserEventTest()
        {
            string id = "eineID";
            CustomUserEvent c = new CustomUserEvent(id);
            string folder = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string debugPath = folder + "\\Debug";
            Assert.IsTrue(System.IO.Directory.Exists(debugPath + "\\tmp\\" + id + "\\"));
            Assert.IsTrue(System.IO.File.Exists(debugPath + "\\tmp\\" + id + "\\" + id + "_Event.js"));
            string content = System.IO.File.ReadAllText(debugPath + "\\res\\templates\\customUserEventTemplate.txt");
            content = content.Replace("#element", id);
            Assert.IsTrue(content.Equals(System.IO.File.ReadAllText(debugPath + "\\tmp\\" + id + "\\" + id + "_Event.js")));
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        public void CreateUserEventCheckFilePath()
        {
            string id = "eineID";
            CustomUserEvent c = new CustomUserEvent(id);
            Assert.IsTrue(System.IO.File.Exists(c.FilePath));
        }
    }
}
