using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace EditorUITest
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für TextEditorFormTest
    /// </summary>
    [CodedUITest]
    public class TextEditorFormTest
    {
        public TextEditorFormTest()
        {
        }

        [TestMethod]
        public void EditorFormTest()
        {
            this.UIMap.EditorFormTestRecord();
            MessageBox.Show(new Form() { TopMost = true }, "Öffne das AREL Script der ImageAugmentation und ändere den Text und speichere. Schließe das Fenster dann.", "Schritt 1/4");
            this.UIMap.EditorFormDuplicateRecord();
            MessageBox.Show(new Form() { TopMost = true }, "Öffne das AREL Script der ImageAugmentation", "Schritt 2/4");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist der Text der gleiche wie in der anderen Augmentation?", "Test 1", MessageBoxButtons.YesNo) == DialogResult.Yes);
            MessageBox.Show(new Form() { TopMost = true }, "Ändere den Text, speichere, schließe das Text Fenster und wechsle zur ersten Szene", "Schritt 3/4");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne das AREL Script der ImageAugmentation", "Schritt 4/4");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist der Text der gleich geblieben?", "Test 2", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        #region Zusätzliche Testattribute

        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:

        ApplicationUnderTest aut;

        ////Verwenden Sie "TestInitialize", um vor dem Ausführen der einzelnen Tests Code auszuführen 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            string folder = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string strDirName;
            int intLocation;
            intLocation = folder.IndexOf("\\TestResults");
            strDirName = folder.Substring(0, intLocation);
            aut = ApplicationUnderTest.Launch(strDirName + "\\bin\\Debug\\ARdevKit.exe");
            aut.CloseOnPlaybackCleanup = true;
        }

        #endregion

        /// <summary>
        ///Dient zum Abrufen oder Festlegen des Textkontexts, der Informationen über
        ///den aktuellen Testlauf  und dessen Funktionalität bereitstellt.
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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
