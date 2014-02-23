using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ARdevKit.Model.Project.File
{
    static class Helper
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Copies a passed file to the passed directory. </summary>
        ///
        /// <remarks>   Imanuel, 19.01.2014. </remarks>
        ///
        /// <param name="srcFile">          Source file. </param>
        /// <param name="destDirectory">    Pathname of the destination directory. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Copy(string srcFile, string destDirectory)
        {
            Copy(srcFile, destDirectory, Path.GetFileName(srcFile));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Copies a passed file to the passed directory and renames it to the passed name. </summary>
        ///
        /// <remarks>   Imanuel, 27.01.2014. </remarks>
        ///
        /// <param name="srcFile">          Source file. </param>
        /// <param name="destDirectory">    Pathname of the destination directory. </param>
        /// <param name="newFileName">          Name of the new file. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void Copy(string srcFile, string destDirectory, string newFileName)
        {
            // Checks on it's owen, if a directory already exists.
            Directory.CreateDirectory(destDirectory);

            string destFile = Path.Combine(destDirectory, newFileName);
            if (!System.IO.File.Equals(srcFile, destFile))
            {
                try
                {
                    System.IO.File.Copy(srcFile, destFile, true);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message, "Error!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

            }
        }

        public static bool FileExists(string rootPath, string filename)
        {
            if (System.IO.File.Exists(Path.Combine(rootPath, filename)))
                return true;

            foreach (string subDir in Directory.GetDirectories(rootPath))
            {
                if (FileExists(subDir, filename))
                    return true;
            }

            return false;
        }
    }
}
