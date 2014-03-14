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
        public void TF11150_TF11151_TF20030_TF20050_TF20060_TF20080_TF20090()
        {
            bool isInTecoNetwork;
            //Preperation
            MessageBox.Show(new Form() { TopMost = true }, "Lade das Projekt \"res\\testFiles\\testProjects\\networkProject\"", "Testschritt!");

            //TF11150
            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken)", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird die IP des mobilen Geräts angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.No);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Projekt an Gerät senden\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird eine Meldung angezeigt, dass kein Gerät zum Versenden verfügbar ist?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Gerätedebugmodus starten\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird eine Meldung angezeigt, dass kein Gerät verfügbar ist?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Starte den Player auf dem mobilen Gerät und warte, bis er vollständig geladen hat", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird das Webcambild angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            //TF11151
            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Liste aktualisieren\"", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken)", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird die IP des mobilen Geräts angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Projekt an Gerät senden\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird eine Meldung \"Das Projekt wurde versand.\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Startet der Player neu?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            isInTecoNetwork = MessageBox.Show("Befinden sich Editor und Player im teco-Netzwerk?", "Branch!", MessageBoxButtons.YesNo) == DialogResult.Yes;
            MessageBox.Show(new Form() { TopMost = true }, "Halte den ID-Marker mit der ID 1 vor die Kamera des mobilen Geräts", "Testschritt!");
            if (isInTecoNetwork)
            {
                Assert.IsTrue(MessageBox.Show("Sind zwei mit Graphen gefüllte Diagramme zu sehen?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            else
            {
                Assert.IsTrue(MessageBox.Show("Ist ein Diagramm mit Graphen und eines ohne zu sehen?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            }

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Gerätedebugmodus starten\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Öffnet sich ein neues Fenster mit einem Textfeld?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            //TF20030
            MessageBox.Show(new Form() { TopMost = true }, "Halte wieder den ID-Marker mit der ID 1 vor die Kamera des mobilen Geräts", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird im Debugfenster die erkannte id angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            //TF20050
            Assert.IsTrue(MessageBox.Show("Werden im Debugfenster Informationen über die Verbindung zum Server angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            if (isInTecoNetwork)
            {
                //TF20060
                Assert.IsTrue(MessageBox.Show("Werden im Debugfenster Informationen über die gesendete Anfrage und empfangene Antwort angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
        }

         [TestMethod]
        public void TF42010_TF42020_TF42030()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Öffnen sie das Project everyAugmentation", "Testschritt!");

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Liste aktualisieren\"", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken)", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird die IP des mobilen Geräts angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Projekt an Gerät senden\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird eine Meldung \"Das Projekt wurde versand.\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Startet der Player neu?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Gerätedebugmodus starten\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Öffnet sich nun auf das DebugWindow?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            
            MessageBox.Show(new Form() { TopMost = true }, "Filmen sie nun mit dem Player Trackable 1", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird im DebugWindow(Console) eine Ausgabe angezeigt mit 'Tracked coordinateSystemID: 1 ?'", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "klicken sie nun auf die ImageAugmentation", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Geht nun ein Popup auf mit 'foo'?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Steht nun im DebugWindow 'Loaded Event successfully?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void TF42040_TF42050_TF42060()
        {
            Assert.IsTrue(MessageBox.Show("Befinden sich Editor und Player im teco-Netzwerk?", "Branch!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            MessageBox.Show(new Form() { TopMost = true }, "Öffnen sie das Project everyAugmentation", "Testschritt!");

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Liste aktualisieren\"", "Testschritt!");
            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken)", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird die IP des mobilen Geräts angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Projekt an Gerät senden\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Wird eine Meldung \"Das Projekt wurde versand.\" angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Startet der Player neu?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Klicke auf \"Datei\" und geh mit der Maus auf \"Projekt versenden\" (ohne zu klicken) und klicke auf \"Gerätedebugmodus starten\"", "Testschritt!");
            Assert.IsTrue(MessageBox.Show("Öffnet sich nun auf das DebugWindow?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Filmen sie nun mit dem Player Trackable 2", "Testschritt!");

            Assert.IsTrue(MessageBox.Show("Steht nun im DebugWindow 'Loaded data for ... successfully?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("Ist der letzte eintrag 'Loaded data successfully?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show("können sie in dem DebugWindow scrollen?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void TF12110() //jede TestMethode ist ein manuellerTest. Aussagekräftigen Namen verwenden ggf. Nummber aus Pflichtenheft (TF...)
        {
            MessageBox.Show(new Form() { TopMost = true },"Öffne das Project onlyOneTrackable im Ordner Test(Ui)", "Testschritt!");

            MessageBox.Show(new Form() { TopMost = true }, "Wechsel im linken DropDownMenü(aktueller eintrag Trackable) zu Augmentation ", "Testschritt!");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Werden Chart, ImageAugmentation & VideoAugmentation angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Wechsel im DropDownMenü zu Source!", "Testschritt!");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Werden DbSource & FileSource angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Wechsel im DropDownMenü zu Trackable!", "Testschritt!");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird IDMarker angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Löschen sie per Rechtsklick -> Löschen auf dem Trackable das Trackable", "Testschritt");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Werden nun ImageTrackable, IDMarker & PictureMarker angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Fügen sie ein PictureMarker hinzu", "Testschritt");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird nun nur noch der PictureMarker angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "löschen sie per Rechtsklick -> Löschen auf dem Trackable das Trackable und fügen sie wie zuvor nun ein ImageTrackable hinzu");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird nun nur noch das ImageTrackable angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void TF12120() {
            MessageBox.Show(new Form() { TopMost = true }, "Öffne das Project onlyOneTrackable im Ordner Test(Ui)", "Testschritt!");

            MessageBox.Show(new Form() { TopMost = true }, "Ziehen sie nun eine Chart in das PreviewPanel mit Option -> barChartColumn/options.js", "Testschritt!");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Liegt die Chart nun da wo sie sie gedropt haben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Ziehen sie nun eine ImageAugmentation in das PreviewPanel", "Testschritt!");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Liegt die ImageAugmentation nun da wo sie sie gedropt haben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Ziehen sie nun eine VideoAugmentation in das PreviewPanel, eine Testdatei finden sie in dem Test(UI) Ordner", "Testschritt!");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Liegt die VideoAugmentation nun da wo sie sie gedropt haben?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void TF12160()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Öffne das Project testProject im Ordner Test(Ui)", "Testschritt!");

            MessageBox.Show(new Form() { TopMost = true }, "Ziehen sie nun eine DbSource auf die Chart, Option Datei finden sie in barChartColumn/options.js", "Testschritt");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird nun ein Source-Zeichen auf der Augmentation angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            MessageBox.Show(new Form() { TopMost = true }, "Rechtsklick auf die Augmentation -> Source anzeigen", "Testschritt");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird die Source rechts angezeigt?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
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
