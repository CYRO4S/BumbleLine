Imports Ionic.Zip

Module Zip

    ''' <summary>
    ''' Unzip a ZIP file to temporary directory
    ''' </summary>
    ''' <param name="ZipFile">Path of the ZIP file</param>
    Public Function Unzip(ByVal ZipFile As String) As Integer
        Try
            Dim z As New ZipFile(ZipFile)
            z.ExtractAll(GetTempPath, ExtractExistingFileAction.OverwriteSilently)
        Catch ex As Exception
            Return 1
        End Try
        Return 0
    End Function

    ''' <summary>
    ''' Clear temporary directory
    ''' </summary>
    Public Sub Cleaning()
        If IO.Directory.Exists(GetTempPath) Then
            IO.Directory.Delete(GetTempPath, True)
        End If
    End Sub

    ''' <summary>
    ''' Returns system temporary directory
    ''' </summary>
    ''' <returns>String</returns>
    Public Function GetTempPath() As String
        Dim tmpPath As String
        Dim tmp = System.Environment.GetEnvironmentVariable("TEMP")
        Dim info As New IO.DirectoryInfo(tmp)
        tmpPath = info.FullName
        Return (tmpPath & "\BumbleLine")
    End Function

End Module
