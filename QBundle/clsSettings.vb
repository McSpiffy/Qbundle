Imports System.IO
Imports System.Reflection

Friend Class clsSettings
    'NRS
    'DB
    'connectionstring from now on

    'JAVA
    'general
    Private _Debug As Boolean

    'Plotting And Mining
    'dynamic plotting
    'minner


    'NRS
    Public Property AutoIp As Boolean

    Public Property WalletException As Boolean

    Public Property DynPlatform As Boolean

    Public Property useOpenCL As Boolean

    Public Property Cpulimit As Integer

    Public Property ListenIf As String

    Public Property ListenPeer As String

    Public Property ConnectFrom As String

    Public Property AutoStart As Boolean

    Public Property Broadcast As String

    Public Property LaunchString As String

    Public Property DbType As Integer

    Public Property DbServer As String

    Public Property DbName As String

    Public Property DbUser As String

    Public Property DbPass As String

    Public Property JavaType As Integer

    Public Property FirstRun As Boolean

    Public Property CheckForUpdates As Boolean

    Public Property Upgradev As Integer

    Public Property AlwaysAdmin As Boolean

    Private Property Repo As String

    Public Property QBMode As Integer

    Public Property DebugMode As Boolean
        Get
            Return _Debug
        End Get
        Set
            _Debug = value
        End Set
    End Property

    Public Property UseOnlineWallet As Boolean

    Public Property NTPCheck As Boolean

    Public Property Plots As String

    Public Property DynPlotEnabled As Boolean

    Public Property DynPlotPath As String

    Public Property DynPlotAcc As String

    Public Property DynPlotSize As Integer

    Public Property DynPlotFree As Integer

    Public Property DynPlotHide As Boolean

    Public Property DynPlotType As Integer

    Public Property DynThreads As Integer

    Public Property DynRam As Integer

    Public Property MinToTray As Boolean

    Public Property GetCoinMarket As Boolean

    Public Property Currency As String

    Public Property NoDirectLogin As Boolean

    Public Property Solomining As Boolean

    Public Property MiningServer As String

    Public Property UpdateServer As String

    Public Property InfoServer As String

    Public Property Deadline As String

    Public Property MiningServerPort As Integer

    Public Property UpdateServerPort As Integer

    Public Property InfoServerPort As Integer

    Public Property Hddwakeup As Boolean

    Public Property ShowWinner As Boolean

    Public Property UseMultithread As Boolean

    Friend Sub New()
        AutoIp = False
        WalletException = True
        DynPlatform = True
        useOpenCL = False
        Cpulimit = 0
        ListenIf = "127.0.0.1;8125"
        ListenPeer = "0.0.0.0;8123"
        ConnectFrom = "0.0.0.0"
        Broadcast = ""
        DbType = 0
        DbServer = "127.0.0.1:3306"
        DbName = "burstwallet"
        DbUser = ""
        DbPass = ""
        Currency = "USD"
        JavaType = QGlobal.AppNames.JavaInstalled
        LaunchString = "-cp conf -jar burst.jar --headless"

        FirstRun = True
        CheckForUpdates = False
        Upgradev = 214
        AlwaysAdmin = False
        Repo = QGlobal.UpdateMirrors(0)
        QBMode = 1 '0 = AIO 1 = Launcher
        _Debug = False
        UseOnlineWallet = False
        NTPCheck = False
        MinToTray = False
        GetCoinMarket = False
        AutoStart = True
        NoDirectLogin = False
        Plots = ""

        DynPlotEnabled = False
        DynPlotPath = ""
        DynPlotAcc = ""
        DynPlotSize = 10
        DynPlotFree = 10
        DynPlotHide = True
        DynRam = 1
        DynThreads = 1
        DynPlotType = 2

        Solomining = True
        MiningServer = ""
        UpdateServer = ""
        InfoServer = ""
        MiningServerPort = 8124
        UpdateServerPort = 8124
        InfoServerPort = 8124
        Deadline = "80000000"
        Hddwakeup = True
        ShowWinner = False
        UseMultithread = True
    End Sub


    Friend Sub LoadSettings()

        Try
            Dim param = ""
            Dim Cell() As String = Nothing
            If File.Exists(QGlobal.AppDir & "\BWL.ini") Then
                Dim lines() As String = File.ReadAllLines(QGlobal.AppDir & "\BWL.ini")
                For Each line As String In lines 'lets populate
                    Try
                        If line.Contains("=") Then
                            Cell = Split(line, "=")
                            If UBound(Cell) > 0 Then
                                param = Cell(1)
                                If UBound(Cell) > 1 Then
                                    For t = 2 To UBound(Cell)
                                        param &= "=" & Cell(t)
                                    Next
                                End If
                                CallByName(Me, Cell(0), CallType.Set, param)
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If
        Catch ex As Exception
            If _Debug = True Then Generic.DebugMe = True
        End Try
    End Sub

    Friend Sub SaveSettings()
        Try
            Dim Sdata = ""
            Dim RP() As PropertyInfo = Me.GetType().GetProperties()
            For Each prop In RP
                Sdata &= prop.Name & "=" & CStr(prop.GetValue(Me)) & vbCrLf
            Next
            File.WriteAllText(QGlobal.SettingsDir & "\BWL.ini", Sdata)
        Catch ex As Exception
            If _Debug = True Then Generic.DebugMe = True
        End Try
    End Sub
End Class
