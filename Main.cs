using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace pilot
{

    public class Main : IExternalApplication
    {
        private static string _tabName;
        private static string _panelName;
        private static RibbonPanel _ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            _tabName = "pilot";
            _panelName = "pilot";
            application.CreateRibbonTab(_tabName);
            _ribbonPanel = application.CreateRibbonPanel(_tabName, _panelName);

            TestCommand.CreatePushButtonAndAddToPanel(_ribbonPanel);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }


    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class TestCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("Test", "Command");
            return Result.Succeeded;
        }

        public static void CreatePushButtonAndAddToPanel(RibbonPanel ribbonPanel)
        {
            var declaringType = MethodBase.GetCurrentMethod()?.DeclaringType;
            if (declaringType == null) return;
            var pushButtonName = declaringType?.Name;
            const string pushButtonTextName = "Test Command";
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyLocation = assembly.Location;
            const string iconName = "icon.png";
            const string fullClassName = "pilot.TestCommand";
            const string toolTipInfo = "Sample tool tip of this command";

            var pushButtonData = new PushButtonData(
                name: pushButtonName,
                text: pushButtonTextName,
                assemblyName: assemblyLocation,
                className: fullClassName
            )
            {
                ToolTip = toolTipInfo,
                Image = ImageUtilities.LoadImage(assembly, iconName),
                LargeImage = ImageUtilities.LoadImage(assembly, iconName),
                ToolTipImage = ImageUtilities.LoadImage(assembly, iconName)
            };
            ribbonPanel.AddItem(pushButtonData);
        }
    }


    public static class ImageUtilities
    {
        public static BitmapImage LoadImage(Assembly assembly, string name)
        {
            var img = new BitmapImage();
            try
            {
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(name));
                var stream = assembly.GetManifestResourceStream(resourceName);
                img.BeginInit();
                img.StreamSource = stream;
                img.EndInit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return img;
        }
    }



}
