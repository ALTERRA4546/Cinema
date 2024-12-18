﻿#pragma checksum "..\..\EmployeeControls.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5AF226BBFACB26F16C1268AC828123492D0C3FB31821D15AA9A8F2B32C610CC1"
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
    /// EmployeeControls
    /// </summary>
    public partial class EmployeeControls : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddEmployee;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveEmployee;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FindData;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FindCounterData;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EmployeeList;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn EmployeeCodeGrid;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn ImageGrid;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn EmployeeInfoGrid;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\EmployeeControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn AuthorizationGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Cinema;component/employeecontrols.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EmployeeControls.xaml"
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
            
            #line 9 "..\..\EmployeeControls.xaml"
            ((Cinema.EmployeeControls)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\EmployeeControls.xaml"
            ((Cinema.EmployeeControls)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Page_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddEmployee = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\EmployeeControls.xaml"
            this.AddEmployee.Click += new System.Windows.RoutedEventHandler(this.AddEmployee_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RemoveEmployee = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\EmployeeControls.xaml"
            this.RemoveEmployee.Click += new System.Windows.RoutedEventHandler(this.RemoveEmployee_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FindData = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\EmployeeControls.xaml"
            this.FindData.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FindData_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.FindCounterData = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.EmployeeList = ((System.Windows.Controls.ListView)(target));
            
            #line 22 "..\..\EmployeeControls.xaml"
            this.EmployeeList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EmployeeList_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.EmployeeCodeGrid = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 8:
            this.ImageGrid = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 9:
            this.EmployeeInfoGrid = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 10:
            this.AuthorizationGrid = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

