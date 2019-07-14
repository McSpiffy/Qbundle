Friend Class clsQ
    Public WithEvents ProcHandler As clsProcessHandler
    '  Public WithEvents App As clsApp
    Public settings As clsSettings
    Public Accounts As clsAccounts
    Public Service As clsServiceHandler
    Public WithEvents AppManager As clsAppManager

    Public Sub New()
        QGlobal.Init()
        AppManager = New clsAppManager
        ProcHandler = New clsProcessHandler
        settings = New clsSettings
        settings.LoadSettings()
        Accounts = New clsAccounts
        Accounts.LoadAccounts()
        Service = New clsServiceHandler
    End Sub
End Class
