using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Xml;
using System;

public class PostProcessForFixXcodeCrashBug : IPostprocessBuildWithReport
{
    public int callbackOrder => int.MaxValue - 1;

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report == null)
        {
            return;
        }

        if (report.summary.result is not (BuildResult.Succeeded or BuildResult.Unknown))
        {
            return;
        }
        
        var attributeName = "disablePerformanceAntipatternChecker";
        var buildName = Path.GetFileNameWithoutExtension(report.summary.outputPath);
        var xcschemePath = Path.Combine(report.summary.outputPath,
            $"{buildName}.xcodeproj", "xcshareddata",
            "xcschemes", "Waiting Firetruck.xcscheme");
        xcschemePath = xcschemePath.Replace("\\", "/");
        if (File.Exists(xcschemePath))
        {
            try
            {
                var setAttribute = false;
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(xcschemePath);

                if (xmlDocument.DocumentElement != null &&
                    xmlDocument.DocumentElement["LaunchAction"] != null)
                {
                    var launchAction = xmlDocument.DocumentElement["LaunchAction"];

                    if (launchAction.HasAttribute(attributeName))
                    {
                        if (launchAction.GetAttribute(attributeName) != "YES")
                        {
                            // Change attribute value
                            setAttribute = true;
                        }
                    }
                    else
                    {
                        // Append attribute
                        setAttribute = true;
                    }

                    if (setAttribute)
                    {
                        launchAction.SetAttribute(attributeName, "YES");
                    }
                }

                if (setAttribute)
                {
                    xmlDocument.Save(xcschemePath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }
}