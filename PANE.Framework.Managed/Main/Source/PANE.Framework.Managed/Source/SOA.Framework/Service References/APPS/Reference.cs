﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOA.Framework.APPS {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TrailItem", Namespace="http://schemas.datacontract.org/2004/07/PANE.Framework.Utility")]
    [System.SerializableAttribute()]
    public partial class TrailItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueAfterField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueBeforeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValueAfter {
            get {
                return this.ValueAfterField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueAfterField, value) != true)) {
                    this.ValueAfterField = value;
                    this.RaisePropertyChanged("ValueAfter");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValueBefore {
            get {
                return this.ValueBeforeField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueBeforeField, value) != true)) {
                    this.ValueBeforeField = value;
                    this.RaisePropertyChanged("ValueBefore");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ApprovalResponse", Namespace="http://schemas.datacontract.org/2004/07/PANE.Framework.Managed.Utility")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<SOA.Framework.APPS.TrailItem>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.Framework.APPS.TrailItem))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SOA.Framework.APPS.ApproverWCFException))]
    public partial class ApprovalResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DisplayCustomMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RequestInitiatorEmailMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ResponseObjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessfulField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomMessage {
            get {
                return this.CustomMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomMessageField, value) != true)) {
                    this.CustomMessageField = value;
                    this.RaisePropertyChanged("CustomMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool DisplayCustomMessage {
            get {
                return this.DisplayCustomMessageField;
            }
            set {
                if ((this.DisplayCustomMessageField.Equals(value) != true)) {
                    this.DisplayCustomMessageField = value;
                    this.RaisePropertyChanged("DisplayCustomMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RequestInitiatorEmailMessage {
            get {
                return this.RequestInitiatorEmailMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.RequestInitiatorEmailMessageField, value) != true)) {
                    this.RequestInitiatorEmailMessageField = value;
                    this.RaisePropertyChanged("RequestInitiatorEmailMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object ResponseObject {
            get {
                return this.ResponseObjectField;
            }
            set {
                if ((object.ReferenceEquals(this.ResponseObjectField, value) != true)) {
                    this.ResponseObjectField = value;
                    this.RaisePropertyChanged("ResponseObject");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Successful {
            get {
                return this.SuccessfulField;
            }
            set {
                if ((this.SuccessfulField.Equals(value) != true)) {
                    this.SuccessfulField = value;
                    this.RaisePropertyChanged("Successful");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ApproverWCFException", Namespace="http://schemas.datacontract.org/2004/07/PANE.Framework.Managed.Utility")]
    [System.SerializableAttribute()]
    public partial class ApproverWCFException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="APPS.ApprovalService")]
    public interface ApprovalService {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ApprovalService/GetApprovalObject", ReplyAction="urn:ApprovalService/GetApprovalObjectResponse")]
        System.Collections.Generic.List<SOA.Framework.APPS.TrailItem> GetApprovalObject(byte[] dataBefore, byte[] dataAfter, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ApprovalService/SaveApprovedObject", ReplyAction="urn:ApprovalService/SaveApprovedObjectResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(SOA.Framework.APPS.ApproverWCFException), Action="urn:ApprovalService/SaveApprovedObjectApproverWCFExceptionFault", Name="ApproverWCFException", Namespace="http://schemas.datacontract.org/2004/07/PANE.Framework.Managed.Utility")]
        SOA.Framework.APPS.ApprovalResponse SaveApprovedObject(byte[] dataToApprove, string assemblyName, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ApprovalService/UpdateApprovedObject", ReplyAction="urn:ApprovalService/UpdateApprovedObjectResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(SOA.Framework.APPS.ApproverWCFException), Action="urn:ApprovalService/UpdateApprovedObjectApproverWCFExceptionFault", Name="ApproverWCFException", Namespace="http://schemas.datacontract.org/2004/07/PANE.Framework.Managed.Utility")]
        SOA.Framework.APPS.ApprovalResponse UpdateApprovedObject(byte[] dataToApprove, string assemblyName, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:ApprovalService/DeleteApprovedObject", ReplyAction="urn:ApprovalService/DeleteApprovedObjectResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(SOA.Framework.APPS.ApproverWCFException), Action="urn:ApprovalService/DeleteApprovedObjectApproverWCFExceptionFault", Name="ApproverWCFException", Namespace="http://schemas.datacontract.org/2004/07/PANE.Framework.Managed.Utility")]
        SOA.Framework.APPS.ApprovalResponse DeleteApprovedObject(byte[] dataToApprove, string assemblyName, string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ApprovalServiceChannel : SOA.Framework.APPS.ApprovalService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ApprovalServiceClient : System.ServiceModel.ClientBase<SOA.Framework.APPS.ApprovalService>, SOA.Framework.APPS.ApprovalService {
        
        public ApprovalServiceClient() {
        }
        
        public ApprovalServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ApprovalServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ApprovalServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ApprovalServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<SOA.Framework.APPS.TrailItem> GetApprovalObject(byte[] dataBefore, byte[] dataAfter, string username) {
            return base.Channel.GetApprovalObject(dataBefore, dataAfter, username);
        }
        
        public SOA.Framework.APPS.ApprovalResponse SaveApprovedObject(byte[] dataToApprove, string assemblyName, string username) {
            return base.Channel.SaveApprovedObject(dataToApprove, assemblyName, username);
        }
        
        public SOA.Framework.APPS.ApprovalResponse UpdateApprovedObject(byte[] dataToApprove, string assemblyName, string username) {
            return base.Channel.UpdateApprovedObject(dataToApprove, assemblyName, username);
        }
        
        public SOA.Framework.APPS.ApprovalResponse DeleteApprovedObject(byte[] dataToApprove, string assemblyName, string username) {
            return base.Channel.DeleteApprovedObject(dataToApprove, assemblyName, username);
        }
    }
}