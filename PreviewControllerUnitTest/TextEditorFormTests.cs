using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ARdevKit.Tests
{
    [TestClass()]
    public class TextEditorFormTests
    {
        [TestMethod()]
        public void constructorTest()
        {
            //arrange

            //act
            TextEditorForm texter = new TextEditorForm();

            //assert
            Assert.IsNotNull(texter);
            Assert.IsInstanceOfType(texter, typeof(TextEditorForm));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void constructorTest_nullArgument()
        {
            //arrange
            String path = null;

            //act
            TextEditorForm texter = new TextEditorForm(path);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void constructorTest_emptyString()
        {
            //arrange
            String path = "";

            //act
            TextEditorForm texter = new TextEditorForm(path);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void constructorTest_wrongPath01()
        {
            //arrange
            String path = " ";

            //act
            TextEditorForm texter = new TextEditorForm(path);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void constructorTest_wrongPath02()
        {
            //arrange
            String path = "this.file.is.definitly.not.there";

            //act
            TextEditorForm texter = new TextEditorForm(path);

            //assert
            //assert is handled by the ExcpectedException
        }

        //any valid file can be loaded in the editor...
        [TestMethod()]
        public void constructorTest_picture()
        {
            //arrange
            String path = System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\metaioman_target.png";

            //act
            TextEditorForm texter = new TextEditorForm(path);

            //assert
            Assert.IsNotNull(texter);
            Assert.IsInstanceOfType(texter, typeof(TextEditorForm));
        }

        [TestMethod()]
        public void constructorTest_validFile()
        {
            //arrange
            String path = System.IO.Directory.GetCurrentDirectory() + "\\res\\templates\\customUserEventTemplate.txt";

            //act
            TextEditorForm texter = new TextEditorForm(path);

            //assert
            Assert.IsNotNull(texter);
            Assert.IsInstanceOfType(texter, typeof(TextEditorForm));
        }
    }
}
