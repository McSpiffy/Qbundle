Imports System.IO
Imports System.Reflection

Friend Class clsSettings
    'NRS
    Private _autoip As Boolean
    Private _WalletException As Boolean
    Private _DynPlatform As Boolean
    Private _useOpenCL As Boolean
    Private _Cpulimit As Integer
    Private _ListenIf As String
    Private _ListenPeer As String
    Private _ConnectFrom As String
    Private _AutoStart As Boolean
    Private _Broadcast As String
    Private _LaunchString As String
    'DB
    Private _DbType As Integer
    Private _DbServer As String 'connectionstring from now on
    Private _DbUser As String
    Private _DbName As String
    Private _DbPass As String

    'JAVA
    Private _JavaType As Integer

    'general
    Private _FirstRun As Boolean
    Private _CheckForUpdates As Boolean
    Private _Upgradev As Integer
    Private _AlwaysAdmin As Boolean
    Private _Repo As String
    Private _QBMode As Integer
    Private _Debug As Boolean
    Private _UseOnlineWallet As Boolean
    Private _NTPCheck As Boolean
    Private _MinToTray As Boolean
    Private _CoinMarket As Boolean
    Private _NoDirectLogin As Boolean
    Private _Currency As String

    'Plotting And Mining
    Private _Plots As String
    'dynamic plotting
    Private _DynPlotEnabled As Boolean
    Private _DynPlotPath As String
    Private _DynPlotAcc As String
    Private _DynPlotSize As Integer
    Private _DynPlotFree As Integer
    Private _DynPlotHide As Boolean
    Private _DynThreads As Integer
    Private _DynRam As Integer
    Private _DynPlotType As Integer
    'minner
    Private _SoloMining As Boolean
    Private _MiningServer As String
    Private _UpdateServer As String
    Private _InfoServer As String
    Private _MiningServerPort As Integer
    Private _UpdateServerPort As Integer
    Private _InfoServerPort As Integer
    Private _Deadline As String
    Private _Hddwakeup As Boolean
    Private _ShowWinner As Boolean
    Private _UseMultithread As Boolean


    'NRS
    Public Property AutoIp As Boolean
        Get
            Return _autoip
        End Get
        Set
            _autoip = value
        End Set
    End Property

    Public Property WalletException As Boolean
        Get
            Return _WalletException
        End Get
        Set
            _WalletException = value
        End Set
    End Property

    Public Property DynPlatform As Boolean
        Get
            Return _DynPlatform
        End Get
        Set
            _DynPlatform = value
        End Set
    End Property

    Public Property useOpenCL As Boolean
        Get
            Return _useOpenCL
        End Get
        Set
            _useOpenCL = value
        End Set
    End Property

    Public Property Cpulimit As Integer
        Get
            Return _Cpulimit
        End Get
        Set
            _Cpulimit = value
        End Set
    End Property

    Public Property ListenIf As String
        Get
            Return _ListenIf
        End Get
        Set
            _ListenIf = value
        End Set
    End Property

    Public Property ListenPeer As String
        Get
            Return _ListenPeer
        End Get
        Set
            _ListenPeer = value
        End Set
    End Property

    Public Property ConnectFrom As String
        Get
            Return _ConnectFrom
        End Get
        Set
            _ConnectFrom = value
        End Set
    End Property

    Public Property AutoStart As Boolean
        Get
            Return _AutoStart
        End Get
        Set
            _AutoStart = value
        End Set
    End Property

    Public Property Broadcast As String
        Get
            Return _Broadcast
        End Get
        Set
            _Broadcast = value
        End Set
    End Property

    Public Property LaunchString As String
        Get
            Return _LaunchString
        End Get
        Set
            _LaunchString = value
        End Set
    End Property
    'DB
    Public Property DbType As Integer
        Get
            Return _DbType
        End Get
        Set
            _DbType = value
        End Set
    End Property

    Public Property DbServer As String
        Get
            Return _DbServer
        End Get
        Set
            _DbServer = value
        End Set
    End Property

    Public Property DbName As String
        Get
            Return _DbName
        End Get
        Set
            _DbName = value
        End Set
    End Property

    Public Property DbUser As String
        Get
            Return _DbUser
        End Get
        Set
            _DbUser = value
        End Set
    End Property

    Public Property DbPass As String
        Get
            Return _DbPass
        End Get
        Set
            _DbPass = value
        End Set
    End Property
    'JAVA
    Public Property JavaType As Integer
        Get
            Return _JavaType
        End Get
        Set
            _JavaType = value
        End Set
    End Property
    'General
    Public Property FirstRun As Boolean
        Get
            Return _FirstRun
        End Get
        Set
            _FirstRun = value
        End Set
    End Property

    Public Property CheckForUpdates As Boolean
        Get
            Return _CheckForUpdates
        End Get
        Set
            _CheckForUpdates = value
        End Set
    End Property

    Public Property Upgradev As Integer
        Get
            Return _Upgradev
        End Get
        Set
            _Upgradev = value
        End Set
    End Property

    Public Property AlwaysAdmin As Boolean
        Get
            Return _AlwaysAdmin
        End Get
        Set
            _AlwaysAdmin = value
        End Set
    End Property

    Public Property Repo As String
        Get
            Return _Repo
        End Get
        Set
            _Repo = value
        End Set
    End Property

    Public Property QBMode As Integer
        Get
            Return _QBMode
        End Get
        Set
            _QBMode = value
        End Set
    End Property

    Public Property DebugMode As Boolean
        Get
            Return _Debug
        End Get
        Set
            _Debug = value
        End Set
    End Property

    Public Property UseOnlineWallet As Boolean
        Get
            Return _UseOnlineWallet
        End Get
        Set
            _UseOnlineWallet = value
        End Set
    End Property

    Public Property NTPCheck As Boolean
        Get
            Return _NTPCheck
        End Get
        Set
            _NTPCheck = value
        End Set
    End Property
    'Plotting And Mining
    Public Property Plots As String
        Get
            Return _Plots
        End Get
        Set
            _Plots = value
        End Set
    End Property
    'dynamic plotting
    Public Property DynPlotEnabled As Boolean
        Get
            Return _DynPlotEnabled
        End Get
        Set
            _DynPlotEnabled = value
        End Set
    End Property

    Public Property DynPlotPath As String
        Get
            Return _DynPlotPath
        End Get
        Set
            _DynPlotPath = value
        End Set
    End Property

    Public Property DynPlotAcc As String
        Get
            Return _DynPlotAcc
        End Get
        Set
            _DynPlotAcc = value
        End Set
    End Property

    Public Property DynPlotSize As Integer
        Get
            Return _DynPlotSize
        End Get
        Set
            _DynPlotSize = value
        End Set
    End Property

    Public Property DynPlotFree As Integer
        Get
            Return _DynPlotFree
        End Get
        Set
            _DynPlotFree = value
        End Set
    End Property

    Public Property DynPlotHide As Boolean
        Get
            Return _DynPlotHide
        End Get
        Set
            _DynPlotHide = value
        End Set
    End Property

    Public Property DynPlotType As Integer
        Get
            Return _DynPlotType
        End Get
        Set
            _DynPlotType = value
        End Set
    End Property

    Public Property DynThreads As Integer
        Get
            Return _DynThreads
        End Get
        Set
            _DynThreads = value
        End Set
    End Property

    Public Property DynRam As Integer
        Get
            Return _DynRam
        End Get
        Set
            _DynRam = value
        End Set
    End Property

    Public Property MinToTray As Boolean
        Get
            Return _MinToTray
        End Get
        Set
            _MinToTray = value
        End Set
    End Property

    Public Property GetCoinMarket As Boolean
        Get
            Return _CoinMarket
        End Get
        Set
            _CoinMarket = value
        End Set
    End Property

    Public Property Currency As String
        Get
            Return _Currency
        End Get
        Set
            _Currency = value
        End Set
    End Property

    Public Property NoDirectLogin As Boolean
        Get
            Return _NoDirectLogin
        End Get
        Set
            _NoDirectLogin = value
        End Set
    End Property

    Public Property Solomining As Boolean
        Get
            Return _SoloMining
        End Get
        Set
            _SoloMining = value
        End Set
    End Property

    Public Property MiningServer As String
        Get
            Return _MiningServer
        End Get
        Set
            _MiningServer = value
        End Set
    End Property

    Public Property UpdateServer As String
        Get
            Return _UpdateServer
        End Get
        Set
            _UpdateServer = value
        End Set
    End Property

    Public Property InfoServer As String
        Get
            Return _InfoServer
        End Get
        Set
            _InfoServer = value
        End Set
    End Property

    Public Property Deadline As String
        Get
            Return _Deadline
        End Get
        Set
            _Deadline = value
        End Set
    End Property

    Public Property MiningServerPort As Integer
        Get
            Return _MiningServerPort
        End Get
        Set
            _MiningServerPort = value
        End Set
    End Property

    Public Property UpdateServerPort As Integer
        Get
            Return _UpdateServerPort
        End Get
        Set
            _UpdateServerPort = value
        End Set
    End Property

    Public Property InfoServerPort As Integer
        Get
            Return _InfoServerPort
        End Get
        Set
            _InfoServerPort = value
        End Set
    End Property

    Public Property Hddwakeup As Boolean
        Get
            Return _Hddwakeup
        End Get
        Set
            _Hddwakeup = value
        End Set
    End Property

    Public Property ShowWinner As Boolean
        Get
            Return _ShowWinner
        End Get
        Set
            _ShowWinner = value
        End Set
    End Property

    Public Property UseMultithread As Boolean
        Get
            Return _UseMultithread
        End Get
        Set
            _UseMultithread = value
        End Set
    End Property


    Friend Sub New()
        _autoip = False
        _WalletException = True
        _DynPlatform = True
        _useOpenCL = False
        _Cpulimit = 0
        _ListenIf = "127.0.0.1;8125"
        _ListenPeer = "0.0.0.0;8123"
        _ConnectFrom = "0.0.0.0"
        _Broadcast = ""
        _DbType = 0
        _DbServer = "127.0.0.1:3306"
        _DbName = "burstwallet"
        _DbUser = ""
        _DbPass = ""
        _Currency = "USD"
        _JavaType = QGlobal.AppNames.JavaInstalled
        _LaunchString = "-cp conf -jar burst.jar --headless"

        _FirstRun = True
        _CheckForUpdates = False
        _Upgradev = 213
        _AlwaysAdmin = False
        _Repo = QGlobal.UpdateMirrors(0)
        _QBMode = 1 '0 = AIO 1 = Launcher
        _Debug = False
        _UseOnlineWallet = False
        _NTPCheck = False
        _MinToTray = False
        _CoinMarket = False
        _AutoStart = True
        _NoDirectLogin = False
        _Plots = ""

        _DynPlotEnabled = False
        _DynPlotPath = ""
        _DynPlotAcc = ""
        _DynPlotSize = 10
        _DynPlotFree = 10
        _DynPlotHide = True
        _DynRam = 1
        _DynThreads = 1
        _DynPlotType = 2

        _SoloMining = True
        _MiningServer = ""
        _UpdateServer = ""
        _InfoServer = ""
        _MiningServerPort = 8124
        _UpdateServerPort = 8124
        _InfoServerPort = 8124
        _Deadline = "80000000"
        _Hddwakeup = True
        _ShowWinner = False
        _UseMultithread = True
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
