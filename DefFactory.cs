using Microsoft.Win32;
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace FallMinLibTools
{
    public abstract partial class DefFactory
    {
        public static string Create(string strLibFileName)
        {
            string strDefLibValue = string.Empty;

            IntPtr hLibFileBase = LibFactory.OpenLibFile(strLibFileName);
            IntPtr hFileMapping = LibFactory.CreateMapFile(hLibFileBase);
            IntPtr hMapViewOfFile = LibFactory.MapFileOfView(hFileMapping);

            IMAGE_DOS_HEADER sDosHearder = LibFactory.GetDosHearder(hMapViewOfFile);

            IntPtr pNTHeader = LibFactory.GetNTHeader(hMapViewOfFile, sDosHearder);
            IMAGE_NT_HEADERS sNTHeader = LibFactory.GetNTHeader(pNTHeader);

            IntPtr pExportDirectory = LibFactory.GetExportDirectory(pNTHeader, hMapViewOfFile, sNTHeader);
            IMAGE_EXPORT_DIRECTORY sExportDirectory = LibFactory.GetExportDirectory(pExportDirectory);

            IntPtr ppExportOfNames = LibFactory.GetExportOfNames(pNTHeader, hMapViewOfFile, sExportDirectory);

            for (uint i = 0, nNoOfExports = sExportDirectory.NumberOfNames; i < nNoOfExports; i++)
            {
                strDefLibValue += LibFactory.GetExportOfNames(pNTHeader, hMapViewOfFile, ppExportOfNames, i);
                strDefLibValue += string.Format(" @{0}\r\n", (i + 1));
            }

            LibFactory.UnmapViewOfFile(hMapViewOfFile);
            LibFactory.CloseMapFile(hFileMapping);
            LibFactory.CloseLibFile(hLibFileBase);

            if (strDefLibValue.Length > 0)
                strDefLibValue = string.Format("LIBRARY \"{0}\"\r\nEXPORTS\r\n",
                    Path.GetFileNameWithoutExtension(strLibFileName)) + strDefLibValue;

            return strDefLibValue;
        }

        public static string Compile(string strDefLibValue, string strLibFileName)
        {
            if (strDefLibValue.Length <= 0)
                throw new Exception("Unable to compile the lib file def.");

            string strLibLinkFile = DefFactory.LibLinkFileName;
            if (File.Exists(strLibLinkFile) != true)
                throw new Exception("Missing link.exe lib.exe mspdb60.dll file, it cannot be compiled.");

            if (Directory.Exists(DefFactory.AppCachePathName) != true)
                Directory.CreateDirectory(DefFactory.AppCachePathName);

            string strLinkDefFile = DefFactory.AppCachePathName + Path.GetFileNameWithoutExtension(strLibFileName) + ".def";
            string strOutLibFile = Path.GetDirectoryName(strLibFileName) + @"\" + Path.GetFileNameWithoutExtension(strLibFileName) + ".lib";

            File.WriteAllText(strLinkDefFile, strDefLibValue);
            DefFactory.ComplieLibFile(strLibLinkFile, string.Format(@"/def:{0} /machine:i386 /out:{1}", strLinkDefFile, strOutLibFile));
            File.Delete(strLinkDefFile);

            return strOutLibFile;
        }

        public static void ComplieLibFile(string strLibLinkFile, string strLibLinkArgument)
        {
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = strLibLinkArgument;
            p.StartInfo.FileName = strLibLinkFile;
            p.Start();
            p.WaitForExit();
        }

        public static string LibLinkFileName
        {
            get
            {
                return Application.StartupPath + @"\lib.exe";
            }
        }

        public static string AppCachePathName
        {
            get
            {
                return Application.StartupPath + @"\Cache\";
            }
        }
    }
}
