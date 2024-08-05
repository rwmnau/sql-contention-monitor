Imports System.Resources

Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("SQL Contention Monitor")> 
<Assembly: AssemblyDescription("This application will watch SQL Servers and alert the user to any contention issues that arise, ie Blocking locks. These blocking locks prevent processes from accessing necessary data while it's in use by another process, but can unitentionally lead to major performance issues in a database system. By using this tool, you can watch for these blocking locks, and work to resolve them as they occur.")> 
<Assembly: AssemblyCompany("Visual Monkey Development")> 
<Assembly: AssemblyProduct("SQL Contention Monitor")> 
<Assembly: AssemblyCopyright("Copyright © Visual Monkey Development 2008")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("d5f0b6b3-3455-40bd-b5ef-e1a0f04142cc")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.*")> 
<Assembly: AssemblyFileVersion("1.0.*")> 

<Assembly: NeutralResourcesLanguageAttribute("en-US")> 