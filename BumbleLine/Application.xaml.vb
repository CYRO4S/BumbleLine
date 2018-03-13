Class Application

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。
    Private Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)

        If IO.Directory.Exists(GetTempPath) Then
            IO.Directory.Delete(GetTempPath, True)
        End If

        If (e.Args.Length > 0) Then
            Dim strFile As String = e.Args(0)
            If (IO.File.Exists(strFile)) Then
                Dim winMain As New MainWindow
                winMain.LoadFirstContent(strFile)
                winMain.Show()
            Else
                Dim winConfig As New ConfigWindow
                winConfig.Show()
            End If
        Else
            Dim winConfig As New ConfigWindow
            winConfig.Show()
        End If


    End Sub

End Class
