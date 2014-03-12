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
using System.IO;


namespace EditorUITest
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für Manual_Test
    /// </summary>
    [CodedUITest]
    public class Manual_Test
    {
        public Manual_Test()
        {
        }

        [TestMethod] //bei der Durchführung eines manuellen Testfalls wird der Testfall ganz normal gestartet. Anweisungen erscheinen dann als Messageboxen. Auf diese weise kann bei Bedarf die Codeabdeckung gemessen werden.
        public void DemoManuellerTest() //jede TestMethode ist ein manuellerTest. Aussagekräftigen Namen verwenden ggf. Nummber aus Pflichtenheft (TF...)
        {
            MessageBox.Show("Hier wird Schritt für Schritt beschrieben was der Tester bei der Ausführung des Testfalls tun soll. Wenn der Schritt ausgeführt wurde muss OK geklickt werden.", "Testschritt!");
            //diese Assertion öffnet einen Ja/Nein Dialog in dem man anklickt ob der Testfall erfolgreich war. Dadurch wird das Ergebnis im Test-Explorer sichtbar.
            Assert.IsTrue(MessageBox.Show("Ist der Testfall erfolgreich?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            //Eine MessageBox für den Testschritt und eine für die Assertion. Dies kann für einen Testfall beliebig oft wiederholt werden.
            //Es können auch automatisierte und manuelle Schritte kombiniert werden.
        }

        [TestMethod]
        public void TF12110() //jede TestMethode ist ein manuellerTest. Aussagekräftigen Namen verwenden ggf. Nummber aus Pflichtenheft (TF...)
        {
            MessageBox.Show("Öffne das Project onlyOneTrackable im Ordner Test(Ui)", "Testschritt!");

            MessageBox.Show("Wechsel im linken DropDownMenü(aktueller eintrag Trackable) zu Augmentation ", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Werden Chart, ImageAugmentation & VideoAugmentation angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("Wechsel im DropDownMenü zu Source!", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Werden DbSource & FileSource angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("Wechsel im DropDownMenü zu Trackable!", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird IDMarker angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("Löschen sie per Rechtsklick -> Löschen auf dem Trackable das Trackable", "Testschritt");
            Assert.IsTrue(MessageBox.Show("Werden nun ImageTrackable, IDMarker & PictureMarker angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("Fügen sie ein PictureMarker hinzu","Testschritt");
            Assert.IsTrue(MessageBox.Show("Wird nun nur noch der PictureMarker angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("löschen sie per Rechtsklick -> Löschen auf dem Trackable das Trackable und fügen sie wie zuvor nun ein ImageTrackable hinzu");
            Assert.IsTrue(MessageBox.Show("Wird nun nur noch das ImageTrackable angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void TF12120() {
            MessageBox.Show("Öffne das Project onlyOneTrackable im Ordner Test(Ui)", "Testschritt!");

            MessageBox.Show("Ziehen sie nun eine Chart in das PreviewPanel mit Option -> barChartColumn/options.js", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Liegt die Chart nun da wo sie sie gedropt haben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("Ziehen sie nun eine ImageAugmentation in das PreviewPanel", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Liegt die ImageAugmentation nun da wo sie sie gedropt haben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show("Ziehen sie nun eine VideoAugmentation in das PreviewPanel, eine Testdatei finden sie in dem Test(UI) Ordner", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Liegt die VideoAugmentation nun da wo sie sie gedropt haben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void TF12160()
        {
            MessageBox.Show("Öffne das Project testProject im Ordner Test(Ui)", "Testschritt!");

            MessageBox.Show("Ziehen sie nun eine DbSource auf die Chart, Option Datei finden sie in barChartColumn/options.js", "Testschritt");
            Assert.IsTrue(MessageBox.Show("Wird nun ein Source-Zeichen auf der Augmentation angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            MessageBox.Show("Rechtsklick auf die Augmentation -> Source anzeigen", "Testschritt");
            Assert.IsTrue(MessageBox.Show("Wird die Source rechts angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        #region Zusätzliche Testattribute

        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:

        ApplicationUnderTest aut;

        ////Verwenden Sie "TestInitialize", um vor dem Ausführen der einzelnen Tests Code auszuführen 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            string folder = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
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
