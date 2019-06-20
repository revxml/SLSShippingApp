
' ShipRush registration script.
' This script is part of the ShipRush SDK
' Please refer to the SDK documentation for usage and details

' (c) 2011 Z-Firm LLC ALL RIGHTS RESERVED
'  ZF Case 29301, ZF Case 29085



Dim catalog
Dim applications
Dim application
 
Set catalog = CreateObject("COMAdmin.COMAdminCatalog")
Set applications = catalog.GetCollection("Applications")
call applications.Populate
 
For i = 0 to applications.Count - 1
  Set appObj = applications.Item(i)
  If appObj.Name = "ShipRush" Then
    applications.Remove(i)
    applications.SaveChanges
    Exit For
  End If
Next

Set application = applications.Add()
application.Value("Name") = "ShipRush"
application.Value("Description") = "ShipRush COM+ control"
application.Value("Activation") = "1"
application.Value("Identity") = "Interactive User"
application.Value("ConcurrentApps") = 10
application.Value("ApplicationAccessChecksEnabled") = "0"

applications.SaveChanges

call catalog.ImportComponent(application.Value("ID"), "ZRush_ShipRush.ZFShippingPanel")

Set application = Nothing
Set applications = Nothing
Set catalog = Nothing

Wscript.Echo "ShipRush successfully configured for COM+. Thank you!"
