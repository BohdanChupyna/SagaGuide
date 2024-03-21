using System.Collections.Generic;
using System.IO;

namespace SagaGuide.UnitTests;

public static class TestUtils
{
    public const string GcsMasterLibraryFolderPath = @"E:\development\gcs\gcs_master_library\Library";
    
    public static List<string> GetFilePathsByExtension(string folderPath, string targetExtension)
    {
        List<string> filePaths = new List<string>();
        
        // Process all files in the current folder that match the target extension
        foreach (string filePath in Directory.GetFiles(folderPath, "*" + targetExtension))
        {
            filePaths.Add(filePath);
        }

        // Recursively process all subdirectories
        foreach (string subdirectoryPath in Directory.GetDirectories(folderPath))
        {
            filePaths.AddRange(GetFilePathsByExtension(subdirectoryPath, targetExtension));
        }
        
        return filePaths;
    }
}