﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.General = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkCheckForUpdates = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkAlwaysAdmin = New System.Windows.Forms.CheckBox()
        Me.chkDebug = New System.Windows.Forms.CheckBox()
        Me.chkOnlineWallets = New System.Windows.Forms.CheckBox()
        Me.chkNTP = New System.Windows.Forms.CheckBox()
        Me.chkMinToTray = New System.Windows.Forms.CheckBox()
        Me.chkCoinmarket = New System.Windows.Forms.CheckBox()
        Me.chkNoDirectLogin = New System.Windows.Forms.CheckBox()
        Me.chkWalletException = New System.Windows.Forms.CheckBox()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rJava0 = New System.Windows.Forms.RadioButton()
        Me.rJava1 = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.nrCores = New System.Windows.Forms.NumericUpDown()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblRecommendedCPU = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblMaxCores = New System.Windows.Forms.Label()
        Me.chkOpenCL = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.chkDynPlatform = New System.Windows.Forms.CheckBox()
        Me.cmbListen = New System.Windows.Forms.ComboBox()
        Me.nrListenPort = New System.Windows.Forms.NumericUpDown()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lstConnectFrom = New System.Windows.Forms.ListBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.chkAutoIP = New System.Windows.Forms.CheckBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtAddAllow = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cmbPeerIP = New System.Windows.Forms.ComboBox()
        Me.nrPeerPort = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.chkAutoStart = New System.Windows.Forms.CheckBox()
        Me.txtBroadcast = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.General.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.nrCores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrListenPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nrPeerPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'General
        '
        Me.General.Controls.Add(Me.Button6)
        Me.General.Controls.Add(Me.Panel2)
        Me.General.Controls.Add(Me.Label1)
        Me.General.Location = New System.Drawing.Point(104, 4)
        Me.General.Name = "General"
        Me.General.Size = New System.Drawing.Size(752, 378)
        Me.General.TabIndex = 3
        Me.General.Text = "General"
        Me.General.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(185, 25)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "General settings"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cmbCurrency)
        Me.Panel2.Controls.Add(Me.chkWalletException)
        Me.Panel2.Controls.Add(Me.chkNoDirectLogin)
        Me.Panel2.Controls.Add(Me.chkCoinmarket)
        Me.Panel2.Controls.Add(Me.chkMinToTray)
        Me.Panel2.Controls.Add(Me.chkNTP)
        Me.Panel2.Controls.Add(Me.chkOnlineWallets)
        Me.Panel2.Controls.Add(Me.chkDebug)
        Me.Panel2.Controls.Add(Me.chkAlwaysAdmin)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.chkCheckForUpdates)
        Me.Panel2.Location = New System.Drawing.Point(24, 44)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(699, 289)
        Me.Panel2.TabIndex = 10
        '
        'chkCheckForUpdates
        '
        Me.chkCheckForUpdates.AutoSize = True
        Me.chkCheckForUpdates.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCheckForUpdates.Checked = True
        Me.chkCheckForUpdates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCheckForUpdates.Location = New System.Drawing.Point(12, 33)
        Me.chkCheckForUpdates.Name = "chkCheckForUpdates"
        Me.chkCheckForUpdates.Size = New System.Drawing.Size(202, 17)
        Me.chkCheckForUpdates.TabIndex = 1
        Me.chkCheckForUpdates.Text = "Turn on software update notification. "
        Me.chkCheckForUpdates.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Configuration"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(29, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(263, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "This will not download and install any updates by itself."
        '
        'chkAlwaysAdmin
        '
        Me.chkAlwaysAdmin.AutoSize = True
        Me.chkAlwaysAdmin.Location = New System.Drawing.Point(12, 90)
        Me.chkAlwaysAdmin.Name = "chkAlwaysAdmin"
        Me.chkAlwaysAdmin.Size = New System.Drawing.Size(289, 17)
        Me.chkAlwaysAdmin.TabIndex = 4
        Me.chkAlwaysAdmin.Text = "Always start Qbundle to run with administrator privileges."
        Me.chkAlwaysAdmin.UseVisualStyleBackColor = True
        '
        'chkDebug
        '
        Me.chkDebug.AutoSize = True
        Me.chkDebug.Location = New System.Drawing.Point(12, 111)
        Me.chkDebug.Name = "chkDebug"
        Me.chkDebug.Size = New System.Drawing.Size(222, 17)
        Me.chkDebug.TabIndex = 5
        Me.chkDebug.Text = "Debug mode. (save Qbundle errors to file)"
        Me.chkDebug.UseVisualStyleBackColor = True
        '
        'chkOnlineWallets
        '
        Me.chkOnlineWallets.AutoSize = True
        Me.chkOnlineWallets.Location = New System.Drawing.Point(12, 69)
        Me.chkOnlineWallets.Name = "chkOnlineWallets"
        Me.chkOnlineWallets.Size = New System.Drawing.Size(447, 17)
        Me.chkOnlineWallets.TabIndex = 6
        Me.chkOnlineWallets.Text = "Allow Qbundle to connect to remote wallets when local wallet is offline or not sy" &
    "ncronized."
        Me.chkOnlineWallets.UseVisualStyleBackColor = True
        '
        'chkNTP
        '
        Me.chkNTP.AutoSize = True
        Me.chkNTP.Location = New System.Drawing.Point(12, 133)
        Me.chkNTP.Name = "chkNTP"
        Me.chkNTP.Size = New System.Drawing.Size(223, 17)
        Me.chkNTP.TabIndex = 7
        Me.chkNTP.Text = "Check computer time against NTP server."
        Me.chkNTP.UseVisualStyleBackColor = True
        '
        'chkMinToTray
        '
        Me.chkMinToTray.AutoSize = True
        Me.chkMinToTray.Location = New System.Drawing.Point(12, 155)
        Me.chkMinToTray.Name = "chkMinToTray"
        Me.chkMinToTray.Size = New System.Drawing.Size(167, 17)
        Me.chkMinToTray.TabIndex = 8
        Me.chkMinToTray.Text = "Minimize Qbundle to tray icon."
        Me.chkMinToTray.UseVisualStyleBackColor = True
        '
        'chkCoinmarket
        '
        Me.chkCoinmarket.AutoSize = True
        Me.chkCoinmarket.Location = New System.Drawing.Point(12, 178)
        Me.chkCoinmarket.Name = "chkCoinmarket"
        Me.chkCoinmarket.Size = New System.Drawing.Size(272, 17)
        Me.chkCoinmarket.TabIndex = 9
        Me.chkCoinmarket.Text = "Get price info from Coinmarketcap and show price in"
        Me.chkCoinmarket.UseVisualStyleBackColor = True
        '
        'chkNoDirectLogin
        '
        Me.chkNoDirectLogin.AutoSize = True
        Me.chkNoDirectLogin.Location = New System.Drawing.Point(11, 199)
        Me.chkNoDirectLogin.Name = "chkNoDirectLogin"
        Me.chkNoDirectLogin.Size = New System.Drawing.Size(230, 17)
        Me.chkNoDirectLogin.TabIndex = 10
        Me.chkNoDirectLogin.Text = "Disable account direct login in wallet mode."
        Me.chkNoDirectLogin.UseVisualStyleBackColor = True
        '
        'chkWalletException
        '
        Me.chkWalletException.AutoSize = True
        Me.chkWalletException.Checked = True
        Me.chkWalletException.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWalletException.Location = New System.Drawing.Point(11, 220)
        Me.chkWalletException.Name = "chkWalletException"
        Me.chkWalletException.Size = New System.Drawing.Size(292, 17)
        Me.chkWalletException.TabIndex = 46
        Me.chkWalletException.Text = "Restart wallet if exception occur (max once every 60min)"
        Me.chkWalletException.UseVisualStyleBackColor = True
        '
        'cmbCurrency
        '
        Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Items.AddRange(New Object() {"AUD", "BRL", "CAD", "CHF", "CLP", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PKR", "PLN", "RUB", "SEK", "SGD", "THB", "TRY", "TWD", "USD", "ZAR"})
        Me.cmbCurrency.Location = New System.Drawing.Point(290, 176)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(68, 21)
        Me.cmbCurrency.TabIndex = 47
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.Location = New System.Drawing.Point(644, 339)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(100, 33)
        Me.Button6.TabIndex = 11
        Me.Button6.Text = "Save"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Button4)
        Me.TabPage3.Controls.Add(Me.Panel3)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Location = New System.Drawing.Point(104, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(752, 378)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Java"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(19, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 25)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Java settings"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.rJava1)
        Me.Panel3.Controls.Add(Me.rJava0)
        Me.Panel3.Location = New System.Drawing.Point(24, 44)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(699, 93)
        Me.Panel3.TabIndex = 9
        '
        'rJava0
        '
        Me.rJava0.AutoSize = True
        Me.rJava0.Location = New System.Drawing.Point(11, 30)
        Me.rJava0.Name = "rJava0"
        Me.rJava0.Size = New System.Drawing.Size(146, 17)
        Me.rJava0.TabIndex = 1
        Me.rJava0.TabStop = True
        Me.rJava0.Text = "Use system installed java."
        Me.rJava0.UseVisualStyleBackColor = True
        '
        'rJava1
        '
        Me.rJava1.AutoSize = True
        Me.rJava1.Location = New System.Drawing.Point(11, 53)
        Me.rJava1.Name = "rJava1"
        Me.rJava1.Size = New System.Drawing.Size(109, 17)
        Me.rJava1.TabIndex = 2
        Me.rJava1.TabStop = True
        Me.rJava1.Text = "Use Portable java"
        Me.rJava1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 16)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Java Types"
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(644, 339)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(100, 33)
        Me.Button4.TabIndex = 12
        Me.Button4.Text = "Save"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.Label23)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Location = New System.Drawing.Point(104, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(752, 378)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Wallet"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(644, 339)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 33)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(19, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(227, 25)
        Me.Label23.TabIndex = 11
        Me.Label23.Text = "Local wallet settings"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.txtBroadcast)
        Me.Panel4.Controls.Add(Me.chkAutoStart)
        Me.Panel4.Controls.Add(Me.Label34)
        Me.Panel4.Controls.Add(Me.Label26)
        Me.Panel4.Controls.Add(Me.nrPeerPort)
        Me.Panel4.Controls.Add(Me.cmbPeerIP)
        Me.Panel4.Controls.Add(Me.Label33)
        Me.Panel4.Controls.Add(Me.Label22)
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.Label31)
        Me.Panel4.Controls.Add(Me.Label30)
        Me.Panel4.Controls.Add(Me.txtAddAllow)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.chkAutoIP)
        Me.Panel4.Controls.Add(Me.Label25)
        Me.Panel4.Controls.Add(Me.Button5)
        Me.Panel4.Controls.Add(Me.Button2)
        Me.Panel4.Controls.Add(Me.lstConnectFrom)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.Label24)
        Me.Panel4.Controls.Add(Me.nrListenPort)
        Me.Panel4.Controls.Add(Me.cmbListen)
        Me.Panel4.Controls.Add(Me.chkDynPlatform)
        Me.Panel4.Controls.Add(Me.Label21)
        Me.Panel4.Controls.Add(Me.Label20)
        Me.Panel4.Controls.Add(Me.chkOpenCL)
        Me.Panel4.Controls.Add(Me.lblMaxCores)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.lblRecommendedCPU)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.nrCores)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Location = New System.Drawing.Point(24, 44)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(699, 286)
        Me.Panel4.TabIndex = 12
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 204)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(262, 13)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "During sync of blockchain cpu usage can be intense. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'nrCores
        '
        Me.nrCores.Location = New System.Drawing.Point(46, 161)
        Me.nrCores.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.nrCores.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrCores.Name = "nrCores"
        Me.nrCores.Size = New System.Drawing.Size(40, 20)
        Me.nrCores.TabIndex = 10
        Me.nrCores.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(91, 164)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(41, 13)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "of max "
        '
        'lblRecommendedCPU
        '
        Me.lblRecommendedCPU.AutoSize = True
        Me.lblRecommendedCPU.Location = New System.Drawing.Point(201, 145)
        Me.lblRecommendedCPU.Name = "lblRecommendedCPU"
        Me.lblRecommendedCPU.Size = New System.Drawing.Size(42, 13)
        Me.lblRecommendedCPU.TabIndex = 6
        Me.lblRecommendedCPU.Text = "6 cores"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(9, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 16)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Basic settings:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 145)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(195, 13)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "Recomended value for your computer is" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lblMaxCores
        '
        Me.lblMaxCores.AutoSize = True
        Me.lblMaxCores.Location = New System.Drawing.Point(125, 164)
        Me.lblMaxCores.Name = "lblMaxCores"
        Me.lblMaxCores.Size = New System.Drawing.Size(42, 13)
        Me.lblMaxCores.TabIndex = 13
        Me.lblMaxCores.Text = "6 cores"
        '
        'chkOpenCL
        '
        Me.chkOpenCL.AutoSize = True
        Me.chkOpenCL.Location = New System.Drawing.Point(13, 185)
        Me.chkOpenCL.Name = "chkOpenCL"
        Me.chkOpenCL.Size = New System.Drawing.Size(255, 17)
        Me.chkOpenCL.TabIndex = 14
        Me.chkOpenCL.Text = "Try to use GPU acceleration during sync events."
        Me.chkOpenCL.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 219)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(277, 39)
        Me.Label20.TabIndex = 15
        Me.Label20.Text = "The use of graphic card will offload cpu during sync times" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and most probably imp" &
    "rove the speed of sync." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Using gpu requires OpenCL to be installed."
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(402, 77)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(126, 13)
        Me.Label21.TabIndex = 16
        Me.Label21.Text = "Listen for API request on:"
        '
        'chkDynPlatform
        '
        Me.chkDynPlatform.AutoSize = True
        Me.chkDynPlatform.Checked = True
        Me.chkDynPlatform.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDynPlatform.Location = New System.Drawing.Point(12, 77)
        Me.chkDynPlatform.Name = "chkDynPlatform"
        Me.chkDynPlatform.Size = New System.Drawing.Size(156, 17)
        Me.chkDynPlatform.TabIndex = 17
        Me.chkDynPlatform.Text = "Use dynamic platform name"
        Me.chkDynPlatform.UseVisualStyleBackColor = True
        '
        'cmbListen
        '
        Me.cmbListen.FormattingEnabled = True
        Me.cmbListen.Location = New System.Drawing.Point(405, 93)
        Me.cmbListen.Name = "cmbListen"
        Me.cmbListen.Size = New System.Drawing.Size(164, 21)
        Me.cmbListen.TabIndex = 18
        '
        'nrListenPort
        '
        Me.nrListenPort.Location = New System.Drawing.Point(604, 93)
        Me.nrListenPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrListenPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrListenPort.Name = "nrListenPort"
        Me.nrListenPort.Size = New System.Drawing.Size(67, 20)
        Me.nrListenPort.TabIndex = 19
        Me.nrListenPort.Value = New Decimal(New Integer() {8125, 0, 0, 0})
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(574, 96)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(29, 13)
        Me.Label24.TabIndex = 21
        Me.Label24.Text = "Port:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(403, 122)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(107, 13)
        Me.Label27.TabIndex = 22
        Me.Label27.Text = "Allow API traffic from:"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(9, 128)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(140, 16)
        Me.Label28.TabIndex = 27
        Me.Label28.Text = "Advanced settings:"
        '
        'lstConnectFrom
        '
        Me.lstConnectFrom.FormattingEnabled = True
        Me.lstConnectFrom.Location = New System.Drawing.Point(405, 159)
        Me.lstConnectFrom.Name = "lstConnectFrom"
        Me.lstConnectFrom.Size = New System.Drawing.Size(164, 69)
        Me.lstConnectFrom.TabIndex = 28
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(570, 137)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(64, 22)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "Add"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(450, 228)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(120, 22)
        Me.Button5.TabIndex = 30
        Me.Button5.Text = "Remove selected"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(400, 12)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(143, 16)
        Me.Label25.TabIndex = 32
        Me.Label25.Text = "Connection settings"
        '
        'chkAutoIP
        '
        Me.chkAutoIP.AutoSize = True
        Me.chkAutoIP.Checked = True
        Me.chkAutoIP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoIP.Location = New System.Drawing.Point(12, 31)
        Me.chkAutoIP.Name = "chkAutoIP"
        Me.chkAutoIP.Size = New System.Drawing.Size(147, 17)
        Me.chkAutoIP.TabIndex = 33
        Me.chkAutoIP.Text = "Automatic peer broadcast"
        Me.chkAutoIP.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(10, 164)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(29, 13)
        Me.Label29.TabIndex = 34
        Me.Label29.Text = "Use:"
        '
        'txtAddAllow
        '
        Me.txtAddAllow.Location = New System.Drawing.Point(405, 138)
        Me.txtAddAllow.Name = "txtAddAllow"
        Me.txtAddAllow.Size = New System.Drawing.Size(164, 20)
        Me.txtAddAllow.TabIndex = 35
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(573, 176)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(64, 13)
        Me.Label30.TabIndex = 36
        Me.Label30.Text = "192.168.1.3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(573, 190)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(81, 13)
        Me.Label31.TabIndex = 37
        Me.Label31.Text = "192.168.1.0/24"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(576, 203)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(114, 13)
        Me.Label32.TabIndex = 38
        Me.Label32.Text = "fe80:db8:abcd:ea::/64"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(574, 161)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(50, 13)
        Me.Label22.TabIndex = 39
        Me.Label22.Text = "Example:"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(402, 32)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(135, 13)
        Me.Label33.TabIndex = 40
        Me.Label33.Text = "Listen for peer requests on:"
        '
        'cmbPeerIP
        '
        Me.cmbPeerIP.FormattingEnabled = True
        Me.cmbPeerIP.Location = New System.Drawing.Point(405, 48)
        Me.cmbPeerIP.Name = "cmbPeerIP"
        Me.cmbPeerIP.Size = New System.Drawing.Size(164, 21)
        Me.cmbPeerIP.TabIndex = 41
        '
        'nrPeerPort
        '
        Me.nrPeerPort.Location = New System.Drawing.Point(604, 49)
        Me.nrPeerPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nrPeerPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nrPeerPort.Name = "nrPeerPort"
        Me.nrPeerPort.Size = New System.Drawing.Size(67, 20)
        Me.nrPeerPort.TabIndex = 42
        Me.nrPeerPort.Value = New Decimal(New Integer() {8123, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(574, 52)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(29, 13)
        Me.Label26.TabIndex = 43
        Me.Label26.Text = "Port:"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(576, 217)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(88, 13)
        Me.Label34.TabIndex = 44
        Me.Label34.Text = "0.0.0.0 = Anyone"
        '
        'chkAutoStart
        '
        Me.chkAutoStart.AutoSize = True
        Me.chkAutoStart.Checked = True
        Me.chkAutoStart.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoStart.Location = New System.Drawing.Point(12, 97)
        Me.chkAutoStart.Name = "chkAutoStart"
        Me.chkAutoStart.Size = New System.Drawing.Size(123, 17)
        Me.chkAutoStart.TabIndex = 46
        Me.chkAutoStart.Text = "Autostart local wallet"
        Me.chkAutoStart.UseVisualStyleBackColor = True
        '
        'txtBroadcast
        '
        Me.txtBroadcast.Location = New System.Drawing.Point(67, 47)
        Me.txtBroadcast.Name = "txtBroadcast"
        Me.txtBroadcast.Size = New System.Drawing.Size(209, 20)
        Me.txtBroadcast.TabIndex = 47
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "broadcast:"
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.General)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControl1.ItemSize = New System.Drawing.Size(35, 100)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(860, 386)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 2
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(860, 386)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.General.ResumeLayout(False)
        Me.General.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.nrCores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrListenPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nrPeerPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents General As TabPage
    Friend WithEvents Button6 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmbCurrency As ComboBox
    Friend WithEvents chkWalletException As CheckBox
    Friend WithEvents chkNoDirectLogin As CheckBox
    Friend WithEvents chkCoinmarket As CheckBox
    Friend WithEvents chkMinToTray As CheckBox
    Friend WithEvents chkNTP As CheckBox
    Friend WithEvents chkOnlineWallets As CheckBox
    Friend WithEvents chkDebug As CheckBox
    Friend WithEvents chkAlwaysAdmin As CheckBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents chkCheckForUpdates As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Button4 As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents rJava1 As RadioButton
    Friend WithEvents rJava0 As RadioButton
    Friend WithEvents Label11 As Label
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents txtBroadcast As TextBox
    Friend WithEvents chkAutoStart As CheckBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents nrPeerPort As NumericUpDown
    Friend WithEvents cmbPeerIP As ComboBox
    Friend WithEvents Label33 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents txtAddAllow As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents chkAutoIP As CheckBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents lstConnectFrom As ListBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents nrListenPort As NumericUpDown
    Friend WithEvents cmbListen As ComboBox
    Friend WithEvents chkDynPlatform As CheckBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents chkOpenCL As CheckBox
    Friend WithEvents lblMaxCores As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents lblRecommendedCPU As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents nrCores As NumericUpDown
    Friend WithEvents Label14 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TabControl1 As TabControl
End Class
