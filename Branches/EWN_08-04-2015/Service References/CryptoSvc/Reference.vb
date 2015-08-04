﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.17929
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace CryptoSvc
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="CryptoSvc.ICCCCrypto")>  _
    Public Interface ICCCCrypto
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ICCCCrypto/encrypt", ReplyAction:="http://tempuri.org/ICCCCrypto/encryptResponse")>  _
        Function encrypt(ByVal valueToEncrypt As String, ByVal appName As String, ByVal empID As Integer) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ICCCCrypto/decrypt", ReplyAction:="http://tempuri.org/ICCCCrypto/decryptResponse")>  _
        Function decrypt(ByVal valueToDecrypt As String, ByVal appName As String, ByVal empID As Integer) As String
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ICCCCryptoChannel
        Inherits CryptoSvc.ICCCCrypto, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class CCCCryptoClient
        Inherits System.ServiceModel.ClientBase(Of CryptoSvc.ICCCCrypto)
        Implements CryptoSvc.ICCCCrypto
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function encrypt(ByVal valueToEncrypt As String, ByVal appName As String, ByVal empID As Integer) As String Implements CryptoSvc.ICCCCrypto.encrypt
            Return MyBase.Channel.encrypt(valueToEncrypt, appName, empID)
        End Function
        
        Public Function decrypt(ByVal valueToDecrypt As String, ByVal appName As String, ByVal empID As Integer) As String Implements CryptoSvc.ICCCCrypto.decrypt
            Return MyBase.Channel.decrypt(valueToDecrypt, appName, empID)
        End Function
    End Class
End Namespace
