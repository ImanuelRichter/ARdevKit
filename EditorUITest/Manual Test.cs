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
