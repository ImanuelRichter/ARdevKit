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


namespace EditorUITest.Controller
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für CodedUITest1
    /// </summary>
    [CodedUITest]
    public class PreviewControllerUITest
    {
        public PreviewControllerUITest()
        {
        }
            /*
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurdest du abgefragt, ob du das Projekt speichern wolltest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
             */

        [TestMethod]
        public void copyPaste1()
        {
            // Prepare project
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Füge eine Chart dem Projekt hinzu.", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Kopiere die Chart und füge sie in die gleiche Scene ein (Bearbeiten -> Kopieren).", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die kopierte Chart die selben Eigenschaten wie das original (Ausnahmen sind die Eigenschaften Option und TranslationVector)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die ID der Chart sich geändert?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat der Options-Pfad einen fast identischen Pfad wie das original (der letzte Ordner hat die ID als Namen)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
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

        ////Verwenden Sie "TestCleanup", um nach dem Ausführen der einzelnen Tests Code auszuführen
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //    MessageBox.Show(new Form() { TopMost = true }, "Beenden das Programm, ohne abzuspeichern.", "Test end");
        //    MessageBox.Show(new Form() { TopMost = true }, "Lösche das Projekt und alles, was damit erstellt wurde vom Speicherort (sofern in diesem Test etwas gespeichert wurde).", "Test end");
        //}

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
    }
}
