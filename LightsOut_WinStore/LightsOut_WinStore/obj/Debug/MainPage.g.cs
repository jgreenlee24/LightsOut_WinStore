﻿

#pragma checksum "C:\Users\fmccown\Documents\Visual Studio 2013\Projects\LightsOut_WinStore - CommandBar\LightsOut_WinStore\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2222DF2EB7AC6169FB5FB3909DC114E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightsOut_WinStore
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 32 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.paintCanvas_Tapped;
                 #line default
                 #line hidden
                #line 32 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).SizeChanged += this.paintCanvas_SizeChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 38 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.NewGameButton_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 39 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ChangeSize_OnClick;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 40 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarHelpButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


