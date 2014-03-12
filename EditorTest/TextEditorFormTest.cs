using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit;

namespace EditorTest
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für TextEditorFormTest
    /// </summary>
    [TestClass]
    public class TextEditorFormTest
    {
        public TextEditorFormTest()
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
        [TestCategory("CustomUserEvent")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TextFormNullPathTest()
        {
            TextEditorForm form = new TextEditorForm(null);
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        [ExpectedException(typeof(ArgumentException))]
        public void TextFormEmptyPathTest()
        {
            TextEditorForm form = new TextEditorForm("");
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        [ExpectedException(typeof(ArgumentException))]
        public void TextFormInvalidPathTest()
        {
            TextEditorForm form=new TextEditorForm("C:\\invalidpath\\test.txt");
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        public void TextFormEmptyFileTest()
        {
            TextEditorForm form = new TextEditorForm("C:\\empty.txt");
            Assert.IsTrue(form.Value.Length==0);
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        public void TextFormOneLineFileTest()
        {
            TextEditorForm form = new TextEditorForm("C:\\thisonesayshi.txt");
            Assert.IsTrue(form.Value.Length == 1);
            Assert.IsTrue(form.Value[0].Equals("hi"));
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        public void TextFormMultiLineFileTest()
        {
            TextEditorForm form = new TextEditorForm("C:\\thisonesayshitwice.txt");
            Assert.IsTrue(form.Value.Length == 2);
            Assert.IsTrue(form.Value[0].Equals("hi"));
            Assert.IsTrue(form.Value[1].Equals("hi"));
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        [ExpectedException(typeof(ArgumentException))]
        public void TextFormMuchToBigFileTest()
        {
            TextEditorForm form = new TextEditorForm("C:\\716MB");
            Assert.IsTrue(form.Value.Length > 0);
        }

        [TestMethod]
        [TestCategory("CustomUserEvent")]
        public void TextFormBigFileTest()
        {
            TextEditorForm form = new TextEditorForm("C:\\33MB");
            Assert.IsTrue(form.Value.Length > 0);
        }
    }
}
