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


namespace PreviewController_TextEditorForm_Tests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class ManualPreviewControllerTest
    {
        public ManualPreviewControllerTest()
        {
        }

        private void createProject01()
        {
            //Preperation
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen ID-Marker hinzu", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge ein Bild als Augmentation hinzu", "Testschritt!");
        }

        private void createProject02()
        {
            //Preperation
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen ID-Marker hinzu", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge ein Diagramm hinzu und wähle \"res\\highcharts\\barChartColumn\\xmlOptions.js\" als Vorlage aus", "Testschritt!");
        }

        private void createProject03()
        {
            //Preperation
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen ID-Marker hinzu", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge ein Diagramm hinzu und wähle \"res\\highcharts\\barChartColumn\\xmlOptions.js\" als Vorlage aus", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Füge dem Diagramm eine \"File Source\" hinzu und wähle \"res\\highcharts\\barChartColumn\\data.xml\" als Vorlage aus", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Wähle \"res\\highcharts\\barChartColumn\\xmlQuery.js\" als Datenbankanfrage", "Testschritt!");
        }

        [TestMethod()]
        public void TF12210()
        {
            createProject01();

            MessageBox.Show(new Form() { TopMost = true }, "Verschiebe das angezeigte Bild mittels Drag & Drop", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wurde das Objekt korrekt verschoben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void TF12220()
        {
            createProject01();

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Bild", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Hat sich das Kontextmenu geoeffnet?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"kopieren\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Oeffne AREL Script\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"loeschen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void TF12231()
        {
            createProject02();

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Diagramm", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Hat sich das Kontextmenu geoeffnet?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"kopieren\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Oeffne AREL Optionen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Oeffne AREL Script\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"loeschen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void TF12232()
        {
            createProject03();

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Diagramm", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Hat sich das Kontextmenu geoeffnet?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"kopieren\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Oeffne AREL Optionen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Oeffne AREL Script\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"loeschen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Source anzeigen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"Source loeschen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"QueryFile oeffnen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"SourceFile oeffnen\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void TF12240()
        {
            createProject01();

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Diagramm und waehle \"kopieren\" im Kontextmenu aus", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf eine leere Stelle im Vorschaubereich", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Hat sich das Kontextmenu geoeffnet?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Wurde die Option \"einfuegen\" (als klickbar) angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf eine leere Stelle im Vorschaubereich und waehle \"einfuegen\"", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt eingefuegt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void TF12250()
        {
            createProject03();

            Assert.IsTrue(MessageBox.Show("Wurde das Symbol der verknuepften Quelle korrekt im Diagramm angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void addSource_removeSource()
        {
            createProject03();

            Assert.IsTrue(MessageBox.Show("Wurde das Symbol der verknuepften Quelle korrekt im Diagramm angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Diagramm und waehle \"Source loeschen\" im Kontextmenu aus", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Wurde die verknuepfte Source korrekt entfernt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void addPreviewable_removePreviewable01()
        {
            createProject01();

            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt hinzugefuegt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Bild und waehle \"loeschen\" im Kontextmenu aus", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt entfernt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void addPreviewable_removePreviewable02()
        {
            createProject02();

            Assert.IsTrue(MessageBox.Show("Wurde das Diagramm korrekt hinzugefuegt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklicke auf das Diagramm und waehle \"loeschen\" im Kontextmenu aus", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Wurde das Diagramm korrekt entfernt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void setCurrentElementTest()
        {
            createProject01();

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf den ID-Marker", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Klicke wieder auf das Bild", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Konnte sich korrekt erkennen lassen, welches das aktuell ausgewaehlte Objekt im Vorschaubereich ist?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod()]
        public void rescalePreviewPanelTest()
        {
            createProject01();

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf eine freie Stelle im Vorschaubereich", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Wird im PropertyPanel (rechte Seite) die aktuelle Aufloesung angezeigt", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Trage eine andere Aufloesung ein", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Hat sich die Groesse des Vorschaubereiches korrekt geaendert?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }



        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ApplicationUnderTest aut;

        ////Use TestInitialize to run code before running each test 
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

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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
