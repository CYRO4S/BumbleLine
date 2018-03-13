Module FileAssociation

    Public Sub RegisterApplication()

        My.Computer.Registry.CurrentUser.CreateSubKey("Software\CYROFORCE.NET")
        My.Computer.Registry.CurrentUser.CreateSubKey("Software\CYROFORCE.NET\BumbleLine")
        My.Computer.Registry.CurrentUser.CreateSubKey("Software\CYROFORCE.NET\BumbleLine\Capabilities")
        My.Computer.Registry.CurrentUser.CreateSubKey("Software\CYROFORCE.NET\BumbleLine\Capabilities\FileAssociations")

        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\CYROFORCE.NET\BumbleLine\Capabilities", "ApplicationDescription", "BumbleLine™ Interpreter")
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\CYROFORCE.NET\BumbleLine\Capabilities\FileAssociations", ".bls", "BumbleLine")
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\RegisteredApplications", "BumbleLine", "Software\CYROFORCE.NET\BumbleLine\Capabilities")
    End Sub

    Public Sub UnregApplication()
        My.Computer.Registry.CurrentUser.DeleteSubKeyTree("Software\CYROFORCE.NET\BumbleLine")
    End Sub

    Public Sub RegisterFileType()

        My.Computer.Registry.ClassesRoot.CreateSubKey(".bls")
        My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\.bls", "", "BumbleLine")

        My.Computer.Registry.ClassesRoot.CreateSubKey("BumbleLine")
        My.Computer.Registry.ClassesRoot.CreateSubKey("BumbleLine\Application")
        My.Computer.Registry.ClassesRoot.CreateSubKey("BumbleLine\DefaultIcon")
        My.Computer.Registry.ClassesRoot.CreateSubKey("BumbleLine\shell")
        My.Computer.Registry.ClassesRoot.CreateSubKey("BumbleLine\shell\open")
        My.Computer.Registry.ClassesRoot.CreateSubKey("BumbleLine\shell\open\command")

        My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\BumbleLine\shell", "", "open")
        My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\BumbleLine\shell\open\command", "", $"{AppDomain.CurrentDomain.BaseDirectory}\BumbleLine.exe %1")
        My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\BumbleLine\DefaultIcon", "", $"{AppDomain.CurrentDomain.BaseDirectory}\Resources\file.ico,0")
        My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\BumbleLine\Application", "ApplicationCompany", "CYROFORCE.NET")
        My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\BumbleLine", "", "BumbleLine™ Script")
    End Sub

    Public Sub UnregFileType()
        My.Computer.Registry.ClassesRoot.DeleteSubKeyTree(".bls")
        My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("BumbleLine")
    End Sub

End Module
