﻿#pragma checksum "..\..\..\Pages\SettingsControls.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A27CB69C147BD6EBD2D1FB25AF20F0BCA1D1677E43335AF3A63EA46BD4E3DB17"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Cinema.Pages;
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


namespace Cinema.Pages {
    
    
    /// <summary>
    /// SettingsControls
    /// </summary>
    public partial class SettingsControls : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Pages\SettingsControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Hall;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\SettingsControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SelectedRowHall;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\SettingsControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SelectedPlaceHall;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\SettingsControls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveHallSettings;
        
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
            System.Uri resourceLocater = new System.Uri("/Cinema;component/pages/settingscontrols.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\SettingsControls.xaml"
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
            
            #line 9 "..\..\..\Pages\SettingsControls.xaml"
            ((Cinema.Pages.SettingsControls)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Page_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Hall = ((System.Windows.Controls.StackPanel)(target));
            
            #line 12 "..\..\..\Pages\SettingsControls.xaml"
            this.Hall.SizeChanged += new System.Windows.SizeChangedEventHandler(this.Hall_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SelectedRowHall = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\Pages\SettingsControls.xaml"
            this.SelectedRowHall.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectedRowHall_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SelectedPlaceHall = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\..\Pages\SettingsControls.xaml"
            this.SelectedPlaceHall.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectedPlaceHall_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SaveHallSettings = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Pages\SettingsControls.xaml"
            this.SaveHallSettings.Click += new System.Windows.RoutedEventHandler(this.SaveHallSettings_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
