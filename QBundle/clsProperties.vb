Imports System.IO

Friend Class clsProperties
    Private ReadOnly m_Properties As Hashtable

    Sub New()
        m_Properties = New Hashtable
    End Sub

    Friend Sub Add(key As String, value As String)
        m_Properties.Remove(key)
        m_Properties.Add(key, value)
    End Sub

    Friend Sub Delete(key As String)
        Try
            m_Properties.Remove(key)
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
    End Sub

    Friend Function Load(FileName As String)
        Try
            If File.Exists(FileName) Then
                Dim sr = New StreamReader(FileName)
                Dim line As String
                Dim key As String
                Dim value As String
                Do While sr.Peek <> - 1
                    line = sr.ReadLine
                    If line = Nothing OrElse line.Length = 0 OrElse line.StartsWith("#") Then
                        Continue Do
                    End If
                    key = Trim(line.Split("=")(0))
                    value = Trim(line.Split("=")(1))
                    Add(key, value)
                Loop
                sr.Close()
                sr.Dispose()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return False
    End Function

    Friend Function Save(FileName As String)
        Try
            Dim Data = ""
            For Each key As String In m_Properties.Keys
                Data &= key & " = " & m_Properties.Item(key) & vbCrLf
            Next
            File.WriteAllText(FileName, Data)
        Catch ex As Exception
            Generic.WriteDebug(ex)
        End Try
        Return True
    End Function

    Friend Function GetProperty(key As String)

        Return m_Properties.Item(key)
    End Function

    Friend Function GetProperty(key As String, defValue As String) As String

        Dim value As String = GetProperty(key)
        If value = Nothing Then
            value = defValue
        End If

        Return value
    End Function
End Class
