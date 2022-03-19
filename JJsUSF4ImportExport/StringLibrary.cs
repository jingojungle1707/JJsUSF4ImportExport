using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJsUSF4ImportExport
{
    public static class StringLibrary
    {
        #region ErrorStrings
        public static string STR_ERR_EmptyPath = "Empty path. No files loaded.";
        public static string STR_ERR_InvalidPath = "Invalid input path. No files loaded.";
        public static string STR_ERR_NoFilesInInputDirectory = "No USF4 model files (*.emo, *.emm, *.emb) found in Input Directory.";
        public static string STR_ERR_NoFilesInColladaDirectory = "No .dae files found in Collada Directory.";
        public static string STR_ERR_InvalidColladaPath = "Invalid Collada path. No files loaded.";
        public static string STR_ERR_InvalidColladaFile = "Failed to load Collada file: ";

        public static string STR_ERR_BoneMismatch = "Failed to match Collada bone to target EMO skeleton. No mesh imported.";
        #endregion

        #region IOFileStrings
        public static string STR_IO_Config = "config.dat";
        #endregion
    }
}
