﻿#pragma checksum "..\..\BookerPanel.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F6BFA380A26263B6E5FB6EDDBECA11FD554B6645E8F450B815A091B80034AF55"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Cinema;
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


namespace Cinema {
    
    
    /// <summary>
    /// BookerPanel
    /// </summary>
    public partial class BookerPanel : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\BookerPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem MoviePage;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\BookerPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem SessionPage;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\BookerPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem TicketPage;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\BookerPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentPage;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\BookerPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Exit;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\BookerPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame PageManager;
        
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
            System.Uri resourceLocater = new System.Uri("/Cinema;component/bookerpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BookerPanel.xaml"
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
            
            #line 8 "..\..\BookerPanel.xaml"
            ((Cinema.BookerPanel)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\BookerPanel.xaml"
            ((Cinema.BookerPanel)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MoviePage = ((System.Windows.Controls.MenuItem)(target));
            
            #line 12 "..\..\BookerPanel.xaml"
            this.MoviePage.Click += new System.Windows.RoutedEventHandler(this.MoviePage_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SessionPage = ((System.Windows.Controls.MenuItem)(target));
            
            #line 13 "..\..\BookerPanel.xaml"
            this.SessionPage.Click += new System.Windows.RoutedEventHandler(this.SessionPage_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TicketPage = ((System.Windows.Controls.MenuItem)(target));
            
            #line 14 "..\..\BookerPanel.xaml"
            this.TicketPage.Click += new System.Windows.RoutedEventHandler(this.TicketPage_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CurrentPage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Exit = ((System.Windows.Controls.TextBlock)(target));
            
            #line 18 "..\..\BookerPanel.xaml"
            this.Exit.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PageManager = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
