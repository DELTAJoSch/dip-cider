﻿#pragma checksum "..\..\..\Views\MapTimed.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "410E11C4D2599A825BA4C13899627CBD82D826EE2782D388F4C5E81262409295"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CIDER.Views;
using Microsoft.Maps.MapControl.WPF;
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


namespace CIDER.Views {
    
    
    /// <summary>
    /// MapTimed
    /// </summary>
    public partial class MapTimed : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Views\MapTimed.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slValue;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\MapTimed.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Maps.MapControl.WPF.Map map;
        
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
            System.Uri resourceLocater = new System.Uri("/CIDER;component/views/maptimed.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MapTimed.xaml"
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
            
            #line 10 "..\..\..\Views\MapTimed.xaml"
            ((CIDER.Views.MapTimed)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.onUnload);
            
            #line default
            #line hidden
            return;
            case 2:
            this.slValue = ((System.Windows.Controls.Slider)(target));
            
            #line 19 "..\..\..\Views\MapTimed.xaml"
            this.slValue.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.slValueChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.map = ((Microsoft.Maps.MapControl.WPF.Map)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

