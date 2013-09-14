﻿#pragma checksum "..\..\..\TestWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F78B32DC5BE27C496D2A7896A88D28EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.17626
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ShinyChat {
    
    
    /// <summary>
    /// TestWindow
    /// </summary>
    public partial class TestWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConnectToServerBtn;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DisconnectFromServerBtn;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox JoinChannelText;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button JoinChannelBtn;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SendMessageText;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendToServerBtn;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\TestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageReceivedTxt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ShinyChat;component/testwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TestWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ConnectToServerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\TestWindow.xaml"
            this.ConnectToServerBtn.Click += new System.Windows.RoutedEventHandler(this.ConnectToServerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DisconnectFromServerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\TestWindow.xaml"
            this.DisconnectFromServerBtn.Click += new System.Windows.RoutedEventHandler(this.DisconnectFromServerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.JoinChannelText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.JoinChannelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\TestWindow.xaml"
            this.JoinChannelBtn.Click += new System.Windows.RoutedEventHandler(this.JoinChannelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SendMessageText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.SendToServerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\TestWindow.xaml"
            this.SendToServerBtn.Click += new System.Windows.RoutedEventHandler(this.SendToServerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.MessageReceivedTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

