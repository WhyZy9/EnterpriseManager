Type.registerNamespace("Sys.Extended.UI.HtmlEditor.ToolbarButtons"),Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter=function(t){Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter.initializeBase(this,[t])},Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter.prototype={checkState:function(){return!!Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter.callBaseMethod(this,"checkState")&&this._designPanel._textAlignState("center")},callMethod:function(){return!!Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter.callBaseMethod(this,"callMethod")&&void this._designPanel._execCommand("JustifyCenter")}},Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter.registerClass("Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter",Sys.Extended.UI.HtmlEditor.ToolbarButtons.EditorToggleButton);