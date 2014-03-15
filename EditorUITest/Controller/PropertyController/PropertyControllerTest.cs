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


namespace EditorUITest.Controller.PropertyController
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für PropertyControllerTest
    /// </summary>
    [CodedUITest]
    public class PropertyControllerTest
    {
        public PropertyControllerTest()
        {
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void EmptyPicturePathTest()
        {
            this.UIMap.EmptyPicturePathRecord();
            this.UIMap.EmptyPicturePathAssertion();
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangePicturePathTest()
        {
            Clipboard.SetText("C:\\2.jpg");
            this.UIMap.ChangePicturePathRecord();
            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt geändert?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void EmptyImagePathTest()
        {
            this.UIMap.EmptyImagePathRecord();
            this.UIMap.EmptyImagePathAssertion();
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangeImagePathTest()
        {
            Clipboard.SetText("C:\\2.jpg");
            this.UIMap.ChangeImagePathRecord();
            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt geändert?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void EmptyResPathTest()
        {
            this.UIMap.EmptyResPathRecord();
            this.UIMap.EmptyResPathAssertion();
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangeResPathImageTest()
        {
            Clipboard.SetText("C:\\2.jpg");
            this.UIMap.ChangeResPathImageRecord();
            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt geändert?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangeResPathVideoTest()
        {
            Clipboard.SetText("C:\\video.alpha.3g2");
            this.UIMap.ChangeResPathVideoRecord();
            Assert.IsTrue(MessageBox.Show("Wurde das Bild korrekt geändert?", "ASSERTION!", MessageBoxButtons.YesNo) == DialogResult.Yes);
            this.UIMap.CloseWithoutSave();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void UseSliderTest()
        {
            this.UIMap.UseSliderRecord();
            MessageBox.Show("Ändere den Wert mit dem Slider auf 0.5", "Teststep");
            this.UIMap.UseSliderAssertion();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void UseFileDialogTest()
        {
            Clipboard.SetText("C:\\1.jpg");
            this.UIMap.UseFileDialogRecord();
            MessageBox.Show("Wähle mit dem FileDialog das Bild 2.jpg", "Teststep");
            this.UIMap.UseFileDialogAssertion();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void UseFileDialogAugmentationTest()
        {
            this.UIMap.UseFileDialogAugmentationRecord();
            MessageBox.Show("Wähle mit dem FileDialog das Bild 2.jpg", "Teststep");
            this.UIMap.UseFileDialogAugmentationAssertion();
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangeHeightWidthTest()
        {
            Clipboard.SetText("100");
            this.UIMap.ChangeHeightWidthRecord();
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurde die Größe korrekt angepasst?", "Assertion!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangeIDMarkerTest()
        {
            this.UIMap.ChangeIDMarkerRecord();
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurde der Marker korrekt angepasst?", "Assertion!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        [TestMethod]
        [TestCategory("PropertyController")]
        public void ChangeRotatingTest()
        {
            this.UIMap.ChangeRotatingRecord();
            Assert.IsTrue(MessageBox.Show(new Form() { TopMost = true }, "Wurde das Bild korrekt gedreht?", "Assertion!", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        #region Zusätzliche Testattribute

        ApplicationUnderTest aut;

        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:

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
