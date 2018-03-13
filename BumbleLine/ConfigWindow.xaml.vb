Imports System.Globalization
Imports System.Security.Principal

Public Class ConfigWindow

    Private JSN As JSON

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
        'Load Settings
        LoadSettings()
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If "" = txtPrefLocale.Text Then
            JSN.SetConfigString("preflang", "none")
        Else
            JSN.SetConfigString("preflang", txtPrefLocale.Text)
        End If

        If IsRunAsAdmin() = True Then
            If True = chkFileExt.IsChecked Then
                If Not "1" = JSN.GetConfigString("fileext") Then
                    RegisterApplication()
                    RegisterFileType()
                    JSN.SetConfigString("fileext", "1")
                    Dim proExplorer() As Process = Process.GetProcessesByName("explorer")
                    proExplorer(0).Kill()
                End If
            Else
                If Not "0" = JSN.GetConfigString("fileext") Then
                    UnregApplication()
                    UnregFileType()
                    JSN.SetConfigString("fileext", "0")
                    Dim proExplorer() As Process = Process.GetProcessesByName("explorer")
                    proExplorer(0).Kill()
                End If
            End If

        End If


    End Sub


#Region "Private Methods"

    ''' <summary>
    ''' Load locale strings
    ''' </summary>
    Private Sub LoadLocaleStrings()
        txtTitle.Text = JSN.GetUIString("UI_SET_TITLE")
        txtType.Text = JSN.GetUIString("UI_SET_TYPE")
        txtTypeDetail.Text = JSN.GetUIString("UI_SET_TYPE_DETAIL")
        txtUAC.Text = JSN.GetUIString("UI_SET_UAC")
        txtLocale.Text = JSN.GetUIString("UI_SET_LOCALE")
        txtLocaleDetail.Text = JSN.GetUIString("UI_SET_LOCALE_DETAIL")
        txtLocaleInfo.Text = JSN.GetUIString("UI_SET_LOCALE_INFO")
        txtAbout.Text = JSN.GetUIString("UI_SET_ABOUT")
        txtVer.Text = JSN.GetUIString("UI_SET_VER")
        txtCopyright.Text = JSN.GetUIString("UI_SET_COPYRIGHT")
        txtFree.Text = JSN.GetUIString("UI_SET_FREE")
        txtLicense.Text = JSN.GetUIString("UI_SET_LICENSE")
        txtWebsite.Text = JSN.GetUIString("UI_SET_WEB")
        txtMail.Text = JSN.GetUIString("UI_SET_MAIL")
    End Sub

    Private Sub LoadSettings()
        If "1" = JSN.GetConfigString("fileext") Then
            chkFileExt.IsChecked = True
        Else
            chkFileExt.IsChecked = False
        End If

        If "none" = JSN.GetConfigString("preflang") Then
            txtPrefLocale.Text = CultureInfo.CurrentCulture.Name.ToString
        Else
            txtPrefLocale.Text = JSN.GetConfigString("preflang")
        End If

        If Not IsRunAsAdmin() = True Then
            txtUAC.Visibility = Visibility.Visible
            chkFileExt.IsEnabled = False
        Else
            txtUAC.Visibility = Visibility.Collapsed
            chkFileExt.IsEnabled = True
        End If
    End Sub


    Function IsRunAsAdmin() As Boolean
        Dim principal As New WindowsPrincipal(WindowsIdentity.GetCurrent)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

#End Region
End Class
