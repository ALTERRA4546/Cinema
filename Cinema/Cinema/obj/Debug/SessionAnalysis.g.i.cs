// Updated by XamlIntelliSenseFileGenerator 02.09.2024 22:32:59
#pragma checksum "..\..\SessionAnalysis.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "027619CC11C70A1E9834E5A67175E62753D2BC5E3DABF12C7A90064716787A6C"
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


namespace Cinema
{


    /// <summary>
    /// SessionAnalysis
    /// </summary>
    public partial class SessionAnalysis : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Cinema;component/sessionanalysis.xaml", System.UriKind.Relative);

#line 1 "..\..\SessionAnalysis.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button AddSession;
        internal System.Windows.Controls.Button RemoveSession;
        internal System.Windows.Controls.TextBox FindData;
        internal System.Windows.Controls.CheckBox FindDateChecker;
        internal System.Windows.Controls.DatePicker FindSessionData;
        internal System.Windows.Controls.TextBlock FindCounterData;
        internal System.Windows.Controls.ListView SessionList;
        internal System.Windows.Controls.GridViewColumn SessionCodeGrid;
        internal System.Windows.Controls.GridViewColumn SessionCoverGrid;
        internal System.Windows.Controls.GridViewColumn SessionInfoGird;
        internal System.Windows.Controls.GridViewColumn SessionDateTimeGrid;
        internal System.Windows.Controls.GridViewColumn SessionPriceGrid;
    }
}
