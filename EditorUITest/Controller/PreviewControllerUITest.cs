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
            copyPasteTemplate("Chart", "TF11210 and TF11220");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat der Options-Pfad einen fast identischen Pfad wie das original (der letzte Ordner hat die ID der Kopie als Namen)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die ID der Chart sich geändert?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Ändere den Options-Pfad der neuen Chart.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Unterscheidet sich der Options-Pfad der Kopie mit dem Original?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Füge eine FileSource der kopierten Chart hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Füge etwas der Option-File der kopierten Chart hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Kopiere diese Chart und füge die in der selben Szene ein.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist der Inhalt vom Options-File der neuen Kopie identisch mit der alten Kopie?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            MessageBox.Show(new Form() { TopMost = true }, "Ändere etwas in der Query-File.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist der Inhalt der QueryFile von der neuen Kopie NICHT identisch mit der alten Kopie?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void copyPaste2()
        {
            copyPasteTemplate("ImageAugmentation", "TF11210 and TF11220");

            MessageBox.Show(new Form() { TopMost = true }, "Ändere das Bild der neuen ImageAugmentation.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Unterscheidet sich das Bild der Kopie mit dem Original?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void copyPaste3()
        {
            copyPasteTemplate("VideoAugmentation", "TF11210 and TF11220");

            MessageBox.Show(new Form() { TopMost = true }, "Ändere das Video der neuen VideoAugmentation.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Unterscheidet sich das Video der Kopie mit dem Original?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void copyPast4()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Jeweils eine Augmentation von jedem Typ hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Füge bei der Chart eine (beliebige) Source hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Erstelle eine zweite Szene", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Kopiere jeweils eine Augmentation und füge diese in die zweite Szene ein (mit der Tastenkombination STRG+C und STRG+V)", "Schritt 1/2");

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Haben alle kopierten Augmentationen die selben Eigenschaften wie deren Original (Ausnahmen sind die Eigenschaften Option, TranslationVector und ID)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void copyPasteDelete()
        {
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", "Schritt 1/2");
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Augmentation dem Projekt hinzu.", "Schritt 1/2");

            MessageBox.Show(new Form() { TopMost = true }, "Kopiere und füge die Augmentation in dieselbe Szene ein", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die kopierte Augmentation die selben Eigenschaften wie das Original (Ausnahmen sind die Eigenschaften Option, TranslationVector und ID)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Erstelle eine zweite Szene mit einer (beliebigen) Trackable und füge da wieder die Augmentation ein", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die kopierte Augmentation die selben Eigenschaften wie das Original (Ausnahmen sind die Eigenschaften Option, TranslationVector und ID)?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Lösche die zuerst erstelle Augmentation in der ersten Szene.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist die gelöschte Augmentation noch auf dem Previewpanel?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Gehe zur zweiten Szene, kopiere diese und lösch danach die Augmentation. Danach gehe zur ersten Szene und füge da die Augmentation ein", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist die gelöschte Augmentation noch auf dem Previewpanel in der zweiten Szene?", "Test 4",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die kopierte Augmentation die selben Eigenschaten wie das original (Ausnahmen sind die Eigenschaften Option, TranslationVector und ID)?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        public void switchDuplicateDeleteScene1()  // TF12510 - TF12550
        {
            var pathFile = System.IO.Path.GetFullPath(@"\res\testFiles\testProjects\everyAugmentation");
            
            MessageBox.Show(new Form() { TopMost = true }, "Öffne das Projekt im Ordner " + pathFile + " .", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind zwei verfügbare Szenen zur Auswahl?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            
            // Switch between scenes
            MessageBox.Show(new Form() { TopMost = true }, "Wechsele zur Szene 2. Merke dir dabei die Positionen der Elemente in Szene 1 (ggf. mit einem Screenshot)", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wird Szene 2 im Szenen-Wechsel-Bereich farblich hervorgehoben?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            
            MessageBox.Show(new Form() { TopMost = true }, "Bewege die Augmentationen von deren ursprünglichen Position und wechsele zurück zu Szene 1.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Haben sich die Positionen der Elemente in Szene 1 NICHT verändert?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            
            // Duplicate a scene
            MessageBox.Show(new Form() { TopMost = true }, "Drücke mit rechtsklick auf Szene 1 und klicke auf Duplizieren.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind 3 Szenen im Szenen-Wechsel-Bereich zu sehen?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sehen Szene 1 und Szene 2 identisch aus?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Charts von Szene 1 und Szene 2 bis auf die Eigenschaften Option und deren Sourcen identisch?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Sind die Eigenschaften Option und Source zwischen den Charts NICHT identisch, also Unterscheiden sich nur am letzten Name des letzten Ordners?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            // Delete a scene
            MessageBox.Show(new Form() { TopMost = true }, "Lösche Szene 1.", "Schritt 1/2");
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist Szene 1 gelöscht worden?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist im Szenen-Wechsel-Bereich nur noch zwei Szene vorhanden?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist die neue Szene 1 die ehemalige Szene 2 und die neue Szene 2 die ehemalige Szene 3?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Haben sich die Positionen der Elemente in Szene 1 und 2 NICHT verändert?", "Schritt 1/2",
                MessageBoxButtons.YesNo) == DialogResult.Yes);
        }        
        
        
        private void copyPasteTemplate(string augmentation, string testName)
        {
            // Prepare project
            MessageBox.Show(new Form() { TopMost = true }, "Füge einen (beliebigen) Trackable dem Projekt hinzu.", testName);
            MessageBox.Show(new Form() { TopMost = true }, "Füge eine " + augmentation + " dem Projekt hinzu.", testName);

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist der Button in Bearbeiten -> Einfügen disabled?", testName,
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Kopiere die " + augmentation + " (Bearbeiten -> Kopieren).", testName);

            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Ist der Button in Bearbeiten -> Einfügen enabled?", testName,
                MessageBoxButtons.YesNo) == DialogResult.Yes);

            MessageBox.Show(new Form() { TopMost = true }, "Füge die " + augmentation + " in die selbe Szene ein.", testName);
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Hat die kopierte " + augmentation + " die selben Eigenschaten wie das original (Ausnahmen sind die Eigenschaften Option, TranslationVector und ID)?", testName,
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
