Imports System.Runtime.CompilerServices

Namespace Utils
    Public Module StringExtensions

        <Extension()>
        Public Function IsNotEmpty(ByVal str As String) As Boolean
            Return (String.IsNullOrWhiteSpace(str) <> True) Or (str <> String.Empty)
        End Function

        <Extension()>
        Public Function IsEmpty(ByVal str As String) As Boolean
            Return String.IsNullOrWhiteSpace(str) Or (str <> String.Empty)
        End Function

    End Module

End Namespace
