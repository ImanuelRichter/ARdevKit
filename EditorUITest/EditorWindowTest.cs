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
        public void createNewProject1() // /TF11110/
        {
            MessageBox.Show(new Form() { TopMost = true }, "Öffne ein neues Projekt (Datei -> Neu).", "/TF11110/");
            Assert.IsTrue(MessageBox.Show("Hat sich NICHTS geändert?", "/TF11110/", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void createNewProject2() // /TF11110/
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt zu.", "/TF11110/");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt, ohne das vorherige abzuspeichern.", "/TF11110/");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das Previewpanel leer?", "Test 2", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Zeigt das PropertyGrid KEINE Informationen (sofern nicht auf das Previewpanel geklickt wurde)?", "/TF11110/",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird im ElementSelectionPanel bei 'Trackables' drei Trackables angezeigt?", "/TF11110/",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void createNewProject3()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt und speichere dabei ab.", "Schritt 2/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das Previewpanel leer?", "Test 3", MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Zeigt das PropertyGrid KEINE Informationen (sofern nicht auf das Previewpanel geklickt wurde)?", "Test 3",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird im ElementSelectionPanel bei 'Trackables' drei Trackables angezeigt?", "Test 3",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void saveLoad1() // /TF11120/ and /TF11130/
        {
            // Prepare project
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Speichere das Projekt ab (Datei -> Speichern).", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt (Datei -> Neu)", "Schritt 1/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurdes du NICHT abgefragt, ob du speichern willst?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            // Load files
            MessageBox.Show(new Form() { TopMost = true }, "Lade das zuvor gespeicherte Projekt. (Datei -> Öffnen)", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Enthält das Previewpanel das Trackable und ist in der Mitte des Panels positioniert?", "Test 4", 
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird im ElementSelectionPanel nur der Typ von Trackable angezeigt, was auch in der Szene hinzugefügt wurde?", "Test 4", 
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Zeigt das PropertyGrid KEINE Informationen (sofern nicht auf das Previewpanel geklickt wurde)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            // Prepare for save files
            MessageBox.Show(new Form() { TopMost = true }, "Füge zwei (beliebige) Augmentation hinzu.", "Schritt 2/3");
            MessageBox.Show(new Form() { TopMost = true }, "Speichere das Projekt unter einem neuen Namen im selben Verzeichnis ab (Datei -> Speichere unter).", "Schritt 1/3");

            // Save and Load
            MessageBox.Show(new Form() { TopMost = true }, "Füge eine weitere Augmentation hinzu.", "Schritt 2/3");
            MessageBox.Show(new Form() { TopMost = true }, "Lade das Projekt neu, ohne abzuspeichern (Datei -> Laden). Merke dir dabei die Positionen der Elemente auf dem Previewpanel.", "Schritt 2/3");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurdest du befragt, ob du das Projekt speichern willst?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sieht das erstellte Projekt genau so aus, wie du es zuvor abgespeichert hattest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void saveLoad2() // /TF11120/ and /TF11130/
        {
            // Prepare project
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen IDMarker, zwei ImageAugmentationen und eine Chart ein.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle eine zweite Szene mit IDMarker als Trackable und zwei ImageAugmentationen", "Schritt 1/2");

            // Save project
            MessageBox.Show(new Form() { TopMost = true }, "Speichere das Projekt ab. (Merke dir die Positionen der Elemente ggf. mit Screenshots)", "Schritt 1/2");

            // Load files
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt (Datei -> Neu) und danach öffne das vorher gespeicherte Projekt.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sieht das Projekt genau so aus, wie du es vorher abgespeichert hattest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void saveLoad3() // /TF11120/ and /TF11130/
        {
            // Prepare
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen IDMarker, eine ImageAugmentationen eine Chart und eine VideoAugmentation hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt und speichere dabei das vorherige ab.", "Schritt 1/2");

            // Changes filePath
            MessageBox.Show(new Form() { TopMost = true }, "Schiebe das vorhin gespeicherte Projekt in einem anderen Ordner und lade das Projekt", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sieht das Projekt genau so aus, wie du es vorher abgespeichert hattest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void saveLoad4() // /TF11120/ and /TF11130/
        {
            // Prepare
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen PictureMarker, ein ImageAugmentationen eine Chart und eine VideoAugmentation hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Füge der Chart eine DbSource hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Schreibe in der Query irgendetwas hinzu (merke dir dabei, was du hinzugefügt hast) (Kontextmenü -> QueryFile öffnen).", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle ein neues Projekt und speichere dabei das vorherige ab.", "Schritt 1/2");

            // Changes filePath
            MessageBox.Show(new Form() { TopMost = true }, "Schiebe das vorhin gespeicherte Projekt in einem anderen Ordner und lade das Projekt", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sieht das Projekt genau so aus, wie du es vorher abgespeichert hattest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das, was du in der Query hinzugefügt hattest, im Projekt enthalten?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void saveLoad5() // /TF11120/ and /TF11130/
        {
            // Prepare
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen ImageTrackable, ein ImageAugmentationen eine Chart und eine VideoAugmentation hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Füge der Chart eine FileSource hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Klicke mit der rechten Maustaste auf die ImageAugmentation und klicke auf 'Öffne AREL Script'.", "Schritt 1/2");

            // Change filePath
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sieht das Projekt genau so aus, wie du es vorher abgespeichert hattest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Änderungen im AREL Script von der ImageAugmentation vorhanden?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void export1() // /TF11160/
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen IDMarker, eine ImageAugmentationen zwei Charts hinzu.", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Füge der ImageAugmentation ein AREL Script hinzu und füge da etwas ein.", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Füge der Chart1 eine FileSource hinzu", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne die Option Datei der Chart (Kontextmenü -> Öffne Optionen) und schreibe etwas rein", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne die SourceFile der Chart (Kontextmenü -> SourceFile öffnen) und schreibe etwas rein", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Füge der Chart2 eine DbSource hinzu", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne die QueryFile der Chart (Kontextmenü -> QueryFile öffnen) und schreibe etwas rein", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Exportiere das Projekt (Datei -> Exportieren)", "Schritt 1/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurdest du nach einem Speicherort abgefragt?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Kam eine Meldung nach dem Export?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            if ((MessageBox.Show(new Form() { TopMost = true }, "War das exportieren laut der Meldung erfolgreich?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                // General files from export for AREL
                MessageBox.Show(new Form() { TopMost = true }, "Gehe zum Speicherort des exportieren Projekt", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien arel.js, arelConfig.xml, areltest.html und der Ordner Assets vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                MessageBox.Show(new Form() { TopMost = true }, "Gehe in den Assets-Ordner rein", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien anchor.png, arelGlue.js, highcharts.js, jquery-2.0.3.js und TrackingData_Marker.xml vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                // Files from imageAugmentation
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien imageAugmentation1_Event.js und das Bild, was du für die ImageAugmentation verwendet hast, vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Öffne imageAugmentation1_Event.js mit einem beliebigen Editor. Sind die hinzugefügten Änderungen vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);


                // Files from Chart
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die zwei Ordner chart1 und chart2 vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                MessageBox.Show(new Form() { TopMost = true }, "Gehe in den chart1-Ordner rein", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien chart.js, options.js und query.js vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
                MessageBox.Show(new Form() { TopMost = true }, "Öffne die option.js und deine SourceFile mit einem beliebigen Editor", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Änderungen in der option.js und deiner SourceFile vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                MessageBox.Show(new Form() { TopMost = true }, "Gehe in den chart2-Ordner rein", "Schritt 1/2");
                MessageBox.Show(new Form() { TopMost = true }, "Öffne die query.js mit einem beliebigen Editor", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Änderungen in der query.js vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            else
            {
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Kam eine entsprechende Meldung, dass das exportieren NICHT erfolgreich war?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
        }

        [TestMethod]
        public void export2()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen IDMarker, eine ImageAugmentationen eine Chart und eine VideoAugmentation hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle eine neue Szene und füge einen IDMarker, eine ImageAugmentationen und zwei Charts hinzu.", "Schritt 1/2");
            
            MessageBox.Show(new Form() { TopMost = true }, "Füge der Chart1 eine FileSource hinzu", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne die Option Datei der Chart (Kontextmenü -> Öffne Optionen) und schreibe etwas rein", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne die SourceFile der Chart (Kontextmenü -> SourceFile öffnen) und schreibe etwas rein", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Füge der Chart2 eine DbSource hinzu", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Öffne die QueryFile der Chart (Kontextmenü -> QueryFile öffnen) und schreibe etwas rein", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Exportiere das Projekt (Datei -> Exportieren)", "Schritt 1/2");

            if ((MessageBox.Show(new Form() { TopMost = true }, "War das exportieren laut der Meldung erfolgreich?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                // General files from export for AREL
                MessageBox.Show(new Form() { TopMost = true }, "Gehe zum Speicherort des exportieren Projekt", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien arel.js, arelConfig.xml, areltest.html und der Ordner Assets vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                MessageBox.Show(new Form() { TopMost = true }, "Gehe in den Assets-Ordner rein", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien anchor.png, arelGlue.js, highcharts.js, jquery-2.0.3.js und TrackingData_Marker.xml vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                // File from VideoAugmentation
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das Video, dass du eingefügt hattest vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                // File from ImageAugmentation
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist das Bild, was du für die ImageAugmentation verwendet hast, vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                // Files from Chart
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die zwei Ordner chart1 und chart2 vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                MessageBox.Show(new Form() { TopMost = true }, "Gehe in den chart1-Ordner rein", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Dateien chart.js, options.js und query.js vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
                MessageBox.Show(new Form() { TopMost = true }, "Öffne die option.js und deine SourceFile mit einem beliebigen Editor", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Änderungen in der option.js und deiner SourceFile vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);

                MessageBox.Show(new Form() { TopMost = true }, "Gehe in den chart2-Ordner rein", "Schritt 1/2");
                MessageBox.Show(new Form() { TopMost = true }, "Öffne die query.js mit einem beliebigen Editor", "Schritt 1/2");
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Änderungen in der query.js vorhanden?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            else
            {
                Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Kam eine entsprechende Meldung, dass das exportieren NICHT erfolgreich war?", "Test 4",
                    MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
        }

        [TestMethod]
        public void quit1() // /TF11180/
        {
            MessageBox.Show(new Form() { TopMost = true }, "Beende das Programm, ohne abzuspeichern (Datei -> Beenden)", "Schritt 1/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat sich das Programm beendet?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void quit2() // /TF11180/
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Beende das Programm und speichere dabei ab (Datei -> Beenden)", "Schritt 1/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurdest du abgefragt, ob du das Projekt speichern wolltest?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat sich das Programm beendet?", "Test 4",
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

        ///Verwenden Sie "TestCleanup", um nach dem Ausführen der einzelnen Tests Code auszuführen
        [TestCleanup()]
        public void MyTestCleanup()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Beenden das Programm, ohne abzuspeichern.", "Test end");
            MessageBox.Show(new Form() { TopMost = true }, "Lösche das Projekt und alles, was damit erstellt wurde vom Speicherort (sofern in diesem Test etwas gespeichert wurde).", "Test end");
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
    }
}
