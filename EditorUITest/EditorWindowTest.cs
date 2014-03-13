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
    /// Zusammenfassungsbeschreibung für CodedUITest1
    /// </summary>
    [CodedUITest]
    public class EditorWindowTest
    {
        public EditorWindowTest()
        {
        }

        [TestMethod]
        public void createNewProject1()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Öffne ein neues Projekt (Datei -> Neu).", "Schritt 1/1");
            Assert.IsTrue(MessageBox.Show("Hat sich NICHTS geändert?", "Test 1", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void createNewProject2()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt zu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt, ohne das vorherige abzuspeichern.", "Schritt 2/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das Previewpanel leer?", "Test 2", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Zeigt das PropertyGrid KEINE Informationen (sofern nicht auf das Previewpanel geklickt wurde)?", "Test 2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird im ElementSelectionPanel bei 'Trackables' drei Trackables angezeigt?", "Test 2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void saveLoad()
        {
            createNewProject3();
            load1();

        }

        private void createNewProject3()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt zu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt und speichere dabei ab.", "Schritt 2/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das Previewpanel leer?", "Test 3", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Zeigt das PropertyGrid KEINE Informationen (sofern nicht auf das Previewpanel geklickt wurde)?", "Test 3",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird im ElementSelectionPanel bei 'Trackables' drei Trackables angezeigt?", "Test 3",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        private void load1()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Lade das zuvor gespeicherte Projekt.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Enthält das Previewpanel das Trackable und ist in der Mitte des Panels positioniert?", "Test 4", 
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird im ElementSelectionPanel nur das Trackable angezeigt, was auch hinzugeüft wurde?", "Test 3", 
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        private void save1()
        {

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
        //    // Wählen Sie zum Generieren von Code für den Test im Kontextmenü "Code für Coded UI-Test generieren" aus, und wählen Sie eine der Menüelemente aus.
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
