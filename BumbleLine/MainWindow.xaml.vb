Imports System.Globalization

Class MainWindow

    'Global Variables
    Private JSN As JSON
    Private Exec As String = ""
    Private Index As Integer = 0

#Region "Navigation"

    Private Sub btnBack_Click(sender As Object, e As RoutedEventArgs)
        If 0 = Index Then Exit Sub
        Index -= 1
        ShowContent(Index)
    End Sub

    Private Sub btnDone_Click(sender As Object, e As RoutedEventArgs)
        Index += 1
        ShowContent(Index)
    End Sub

    Private Sub btnLaunch_Click(sender As Object, e As RoutedEventArgs)
        If Exec = "" Then Exit Sub
        Try
            Process.Start(Exec)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Load locale strings
    ''' </summary>
    Private Sub LoadLocaleStrings()
        btnBack.ToolTip = JSN.GetUIString("UI_TIP_BACK")
        btnLaunch.ToolTip = JSN.GetUIString("UI_TIP_LAUNCH")
        btnDone.ToolTip = JSN.GetUIString("UI_TIP_DONE")
    End Sub

    Private Sub ShowContent(ByVal i As Integer)
        If 0 = i Then
            'btnBack.IsEnabled = False
            btnBack.Visibility = Visibility.Collapsed
        Else
            'btnBack.IsEnabled = True
            btnBack.Visibility = Visibility.Visible
        End If

        txtTitle.Text = INIGet(i, "title", "void", GetTempPath() & "\lines.ini")
        txtContent.Text = INIGet(i, "content", "void", GetTempPath() & "\lines.ini")

        Dim strExec As String = INIGet(i, "exec", "void", GetTempPath() & "\lines.ini")
        If Not "void" = strExec Then
            btnLaunch.Visibility = Visibility.Visible
            Exec = strExec
        Else
            btnLaunch.Visibility = Visibility.Collapsed
            Exec = ""
        End If

        If IO.File.Exists(GetTempPath() & $"\{i}.png") Then
            pnlImage.Visibility = Visibility.Visible
            imgContent.Source = New BitmapImage(New Uri(GetTempPath() & $"\{i}.png", UriKind.Absolute))
        Else
            pnlImage.Visibility = Visibility.Collapsed
        End If

        If "void" = INIGet(i + 1, "title", "void", GetTempPath() & "\lines.ini") Then
            btnDone.Visibility = Visibility.Hidden
        Else
            btnDone.Visibility = Visibility.Visible
        End If

    End Sub

    Public Sub LoadFirstContent(ByVal Path As String)
        If Not IO.File.Exists(Path) Then
            MessageBox.Show("Script file not found.", "", MessageBoxButton.OK, MessageBoxImage.Information)
            Me.Close()
        End If

        Unzip(Path)
        Index = 0
        ShowContent(Index)
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        JSN = New JSON("en-US")
        If IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & $"\Locales\{JSN.GetConfigString("preflang")}.json") Then
            JSN = New JSON(JSN.GetConfigString("preflang"))
        Else
            If IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & $"\Locales\{CultureInfo.CurrentCulture.Name}.json") Then
                JSN = New JSON(CultureInfo.CurrentCulture.Name)
            Else
                JSN = New JSON("en-US")
            End If
        End If
        'Get UI Strings
        LoadLocaleStrings()

    End Sub

#End Region

End Class
