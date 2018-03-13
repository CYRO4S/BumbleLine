Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class JSON
    ' Locale var
    Private _LOCALE As String = ""

    ' JSON readers & writers
    Private jsnUI As JObject
    Private jsnUS As JObject

    ''' <summary>
    ''' Create a new JSON Class instance
    ''' </summary>
    ''' <param name="LocaleString">Locale String (eg. en-US)</param>
    Public Sub New(ByVal LocaleString As String)
        _LOCALE = LocaleString
        jsnUI = CType(JsonConvert.DeserializeObject(New IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory & $"\Locales\{_LOCALE}.json", Text.Encoding.UTF8).ReadToEnd), JObject)
        jsnUS = CType(JsonConvert.DeserializeObject(New IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory & $"\Config\Config.json", Text.Encoding.UTF8).ReadToEnd), JObject)
    End Sub

    ''' <summary>
    ''' Get UI string from locale JSON
    ''' </summary>
    ''' <param name="KEY">UI string key in JSON file</param>
    ''' <returns></returns>
    Public Function GetUIString(ByVal KEY As String) As String
        Return jsnUI.Item(KEY).ToString
    End Function

    ''' <summary>
    ''' Get configuration string
    ''' </summary>
    ''' <param name="KEY">Config string key in JSON file</param>
    ''' <returns></returns>
    Public Function GetConfigString(ByVal KEY As String) As String
        Return jsnUS.Item(KEY).ToString
    End Function

    ''' <summary>
    ''' Set configuration string
    ''' </summary>
    ''' <param name="KEY">Config string key in JSON file</param>
    ''' <param name="VALUE">New value of key</param>
    Public Sub SetConfigString(ByVal KEY As String, ByVal VALUE As String)
        Dim sr As New IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory & "\Config\Config.json", Text.Encoding.UTF8)
        jsnUS = CType(JsonConvert.DeserializeObject(sr.ReadToEnd), JObject)
        sr.Close()
        sr.Dispose()
        jsnUS.Item(KEY) = VALUE
        Dim strJSON As String = JsonConvert.SerializeObject(jsnUS, Formatting.Indented)
        Dim sw As New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory & $"\Config\Config.json", False, Text.Encoding.UTF8)
        sw.Write(strJSON)
        sw.Close()
        sw.Dispose()
    End Sub

End Class
