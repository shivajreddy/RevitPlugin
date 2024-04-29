# Revit API project as a boiler plate

What does this include
- References
	- RevitAPI.dll, RevitAPIUI.dll
	- PresentationCore, System.XAML, WindowsBase



How to duplicate this project.

1. copy paste the root of this project folder

2. In solution explorer, right click 'Solution', rename to new solution name

3. In solution explorer, right click on project - 'C# pilot', rename to new project name

4. Right click on project, select properties, go to 'Application' tab
   - Change the 'Assembly Name' and 'Default Namespace'
   - rename the namespace inside 'Main.cs' file

5. references: revitapi.dll and revitapiui.dll, are copied properly
But just recheck their location just in case

6.1. Update the .addin file
	- In solution explorer, rename the .addin file
	Edit the .addin file
	- set name
	- set assembly, this is the location of compiled .dll file. Our Post-Build script should copy the built .dll file into this location
	- fullclassname, this is the entry point of the application
	- client id
		- in VisualStudio press 'Tools' > 'Create GUID'
		- make a new guid using python: import uuid, uuid.uuid4()
6.2. Edit the 'Build Event', right click on the project, properties, 'Build Events' tab, click on 'Edit Post-build Event'
	 The placeholder values used here is <pilot>. so say your project is `troy`, they you would remove `<pilot>` with `troy` with out backticks.
	- create a directory with the project name to save dll files in the root folder, here root means the folder that revit looks for .addin files
	`if not exist` make sure to use the new name of the project
	- `Xcopy` reaname the from .addin file, this says that this .addin file in the prject root is copied
	- `Xcopy` rename the to folder
	and in the 'Debug' tab of the project properties, make sure that proper revit version .exe is selected

7. Make sure to use the correct `fullClassName` for the commands
in the 'CreatePushButtonAndAddToPanel' method of each command

8. adding new icons ie., .png images for commands
	- you can add new .png to your project in any new directory also. but make sure to set the
	'Build Action' property to 'Embedded Resource' of the newly added .png file 
	- the 'ImageUtilities' class basically finds the embedded resources of given name, so this image
	can be in any directory in the given project, as long as it's an embedded resource.


