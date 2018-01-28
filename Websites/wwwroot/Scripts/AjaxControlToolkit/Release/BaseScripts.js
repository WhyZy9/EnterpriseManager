Type.registerNamespace("Sys.Extended.UI"),Sys.Extended.UI.BehaviorBase=function(e){Sys.Extended.UI.BehaviorBase.initializeBase(this,[e]),this._clientStateFieldID=null,this._pageRequestManager=null,this._partialUpdateBeginRequestHandler=null,this._partialUpdateEndRequestHandler=null},Sys.Extended.UI.BehaviorBase.prototype={initialize:function(){Sys.Extended.UI.BehaviorBase.callBaseMethod(this,"initialize")},dispose:function(){Sys.Extended.UI.BehaviorBase.callBaseMethod(this,"dispose"),this._pageRequestManager&&(this._partialUpdateBeginRequestHandler&&(this._pageRequestManager.remove_beginRequest(this._partialUpdateBeginRequestHandler),this._partialUpdateBeginRequestHandler=null),this._partialUpdateEndRequestHandler&&(this._pageRequestManager.remove_endRequest(this._partialUpdateEndRequestHandler),this._partialUpdateEndRequestHandler=null),this._pageRequestManager=null)},get_ClientStateFieldID:function(){return this._clientStateFieldID},set_ClientStateFieldID:function(e){this._clientStateFieldID!=e&&(this._clientStateFieldID=e,this.raisePropertyChanged("ClientStateFieldID"))},get_ClientState:function(){if(this._clientStateFieldID){var e=document.getElementById(this._clientStateFieldID);if(e)return e.value}return null},set_ClientState:function(e){if(this._clientStateFieldID){var t=document.getElementById(this._clientStateFieldID);t&&(t.value=e)}},registerPartialUpdateEvents:function(){Sys&&Sys.WebForms&&Sys.WebForms.PageRequestManager&&(this._pageRequestManager=Sys.WebForms.PageRequestManager.getInstance(),this._pageRequestManager&&(this._partialUpdateBeginRequestHandler=Function.createDelegate(this,this._partialUpdateBeginRequest),this._pageRequestManager.add_beginRequest(this._partialUpdateBeginRequestHandler),this._partialUpdateEndRequestHandler=Function.createDelegate(this,this._partialUpdateEndRequest),this._pageRequestManager.add_endRequest(this._partialUpdateEndRequestHandler)))},_partialUpdateBeginRequest:function(e,t){},_partialUpdateEndRequest:function(e,t){}},Sys.Extended.UI.BehaviorBase.registerClass("Sys.Extended.UI.BehaviorBase",Sys.UI.Behavior),Sys.Extended.UI.DynamicPopulateBehaviorBase=function(e){Sys.Extended.UI.DynamicPopulateBehaviorBase.initializeBase(this,[e]),this._DynamicControlID=null,this._DynamicContextKey=null,this._DynamicServicePath=null,this._DynamicServiceMethod=null,this._cacheDynamicResults=!1,this._dynamicPopulateBehavior=null,this._populatingHandler=null,this._populatedHandler=null},Sys.Extended.UI.DynamicPopulateBehaviorBase.prototype={initialize:function(){Sys.Extended.UI.DynamicPopulateBehaviorBase.callBaseMethod(this,"initialize"),this._populatingHandler=Function.createDelegate(this,this._onPopulating),this._populatedHandler=Function.createDelegate(this,this._onPopulated)},dispose:function(){this._populatedHandler&&(this._dynamicPopulateBehavior&&this._dynamicPopulateBehavior.remove_populated(this._populatedHandler),this._populatedHandler=null),this._populatingHandler&&(this._dynamicPopulateBehavior&&this._dynamicPopulateBehavior.remove_populating(this._populatingHandler),this._populatingHandler=null),this._dynamicPopulateBehavior&&(this._dynamicPopulateBehavior.dispose(),this._dynamicPopulateBehavior=null),Sys.Extended.UI.DynamicPopulateBehaviorBase.callBaseMethod(this,"dispose")},populate:function(e){this._dynamicPopulateBehavior&&this._dynamicPopulateBehavior.get_element()!=$get(this._DynamicControlID)&&(this._dynamicPopulateBehavior.dispose(),this._dynamicPopulateBehavior=null),!this._dynamicPopulateBehavior&&this._DynamicControlID&&this._DynamicServiceMethod&&(this._dynamicPopulateBehavior=$create(Sys.Extended.UI.DynamicPopulateBehavior,{id:this.get_id()+"_DynamicPopulateBehavior",ContextKey:this._DynamicContextKey,ServicePath:this._DynamicServicePath,ServiceMethod:this._DynamicServiceMethod,cacheDynamicResults:this._cacheDynamicResults},null,null,$get(this._DynamicControlID)),this._dynamicPopulateBehavior.add_populating(this._populatingHandler),this._dynamicPopulateBehavior.add_populated(this._populatedHandler)),this._dynamicPopulateBehavior&&this._dynamicPopulateBehavior.populate(e?e:this._DynamicContextKey)},_onPopulating:function(e,t){this.raisePopulating(t)},_onPopulated:function(e,t){this.raisePopulated(t)},get_dynamicControlID:function(){return this._DynamicControlID},get_DynamicControlID:this.get_dynamicControlID,set_dynamicControlID:function(e){this._DynamicControlID!=e&&(this._DynamicControlID=e,this.raisePropertyChanged("dynamicControlID"),this.raisePropertyChanged("DynamicControlID"))},set_DynamicControlID:this.set_dynamicControlID,get_dynamicContextKey:function(){return this._DynamicContextKey},get_DynamicContextKey:this.get_dynamicContextKey,set_dynamicContextKey:function(e){this._DynamicContextKey!=e&&(this._DynamicContextKey=e,this.raisePropertyChanged("dynamicContextKey"),this.raisePropertyChanged("DynamicContextKey"))},set_DynamicContextKey:this.set_dynamicContextKey,get_dynamicServicePath:function(){return this._DynamicServicePath},get_DynamicServicePath:this.get_dynamicServicePath,set_dynamicServicePath:function(e){this._DynamicServicePath!=e&&(this._DynamicServicePath=e,this.raisePropertyChanged("dynamicServicePath"),this.raisePropertyChanged("DynamicServicePath"))},set_DynamicServicePath:this.set_dynamicServicePath,get_dynamicServiceMethod:function(){return this._DynamicServiceMethod},get_DynamicServiceMethod:this.get_dynamicServiceMethod,set_dynamicServiceMethod:function(e){this._DynamicServiceMethod!=e&&(this._DynamicServiceMethod=e,this.raisePropertyChanged("dynamicServiceMethod"),this.raisePropertyChanged("DynamicServiceMethod"))},set_DynamicServiceMethod:this.set_dynamicServiceMethod,get_cacheDynamicResults:function(){return this._cacheDynamicResults},set_cacheDynamicResults:function(e){this._cacheDynamicResults!=e&&(this._cacheDynamicResults=e,this.raisePropertyChanged("cacheDynamicResults"))},add_populated:function(e){this.get_events().addHandler("populated",e)},remove_populated:function(e){this.get_events().removeHandler("populated",e)},raisePopulated:function(e){var t=this.get_events().getHandler("populated");t&&t(this,e)},add_populating:function(e){this.get_events().addHandler("populating",e)},remove_populating:function(e){this.get_events().removeHandler("populating",e)},raisePopulating:function(e){var t=this.get_events().getHandler("populating");t&&t(this,e)}},Sys.Extended.UI.DynamicPopulateBehaviorBase.registerClass("Sys.Extended.UI.DynamicPopulateBehaviorBase",Sys.Extended.UI.BehaviorBase),Sys.Extended.UI.ControlBase=function(e){Sys.Extended.UI.ControlBase.initializeBase(this,[e]),this._clientStateField=null,this._callbackTarget=null,this._onsubmit$delegate=Function.createDelegate(this,this._onsubmit),this._oncomplete$delegate=Function.createDelegate(this,this._oncomplete),this._onerror$delegate=Function.createDelegate(this,this._onerror)},Sys.Extended.UI.ControlBase.__doPostBack=function(e,t){if(!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())for(var i=0;i<Sys.Extended.UI.ControlBase.onsubmitCollection.length;i++)Sys.Extended.UI.ControlBase.onsubmitCollection[i]();Function.createDelegate(window,Sys.Extended.UI.ControlBase.__doPostBackSaved)(e,t)},Sys.Extended.UI.ControlBase.prototype={initialize:function(){Sys.Extended.UI.ControlBase.callBaseMethod(this,"initialize"),this._clientStateField&&this.loadClientState(this._clientStateField.value),"undefined"!=typeof Sys.WebForms&&"undefined"!=typeof Sys.WebForms.PageRequestManager?(Array.add(Sys.WebForms.PageRequestManager.getInstance()._onSubmitStatements,this._onsubmit$delegate),null!=Sys.Extended.UI.ControlBase.__doPostBackSaved&&"undefined"!=typeof Sys.Extended.UI.ControlBase.__doPostBackSaved||(Sys.Extended.UI.ControlBase.__doPostBackSaved=window.__doPostBack,window.__doPostBack=Sys.Extended.UI.ControlBase.__doPostBack,Sys.Extended.UI.ControlBase.onsubmitCollection=new Array),Array.add(Sys.Extended.UI.ControlBase.onsubmitCollection,this._onsubmit$delegate)):$addHandler(document.forms[0],"submit",this._onsubmit$delegate)},dispose:function(){"undefined"!=typeof Sys.WebForms&&"undefined"!=typeof Sys.WebForms.PageRequestManager?(Array.remove(Sys.Extended.UI.ControlBase.onsubmitCollection,this._onsubmit$delegate),Array.remove(Sys.WebForms.PageRequestManager.getInstance()._onSubmitStatements,this._onsubmit$delegate)):$removeHandler(document.forms[0],"submit",this._onsubmit$delegate),Sys.Extended.UI.ControlBase.callBaseMethod(this,"dispose")},findElement:function(e){return $get(this.get_id()+"_"+e.split(":").join("_"))},get_clientStateField:function(){return this._clientStateField},set_clientStateField:function(e){if(this.get_isInitialized())throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_CannotSetClientStateField);this._clientStateField!=e&&(this._clientStateField=e,this.raisePropertyChanged("clientStateField"))},loadClientState:function(e){},saveClientState:function(){return null},_invoke:function(e,t,i){if(!this._callbackTarget)throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_ControlNotRegisteredForCallbacks);if("undefined"==typeof WebForm_DoCallback)throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_PageNotRegisteredForCallbacks);for(var a=[],n=0;n<t.length;n++)a[n]=t[n];var s=this.saveClientState();if(null!=s&&!String.isInstanceOfType(s))throw Error.invalidOperation(Sys.Extended.UI.Resources.ExtenderBase_InvalidClientStateType);var o=Sys.Serialization.JavaScriptSerializer.serialize({name:e,args:a,state:this.saveClientState()});WebForm_DoCallback(this._callbackTarget,o,this._oncomplete$delegate,i,this._onerror$delegate,!0)},_oncomplete:function(e,t){if(e=Sys.Serialization.JavaScriptSerializer.deserialize(e),e.error)throw Error.create(e.error);this.loadClientState(e.state),t(e.result)},_onerror:function(e,t){throw Error.create(e)},_onsubmit:function(){return this._clientStateField&&(this._clientStateField.value=this.saveClientState()),!0}},Sys.Extended.UI.ControlBase.registerClass("Sys.Extended.UI.ControlBase",Sys.UI.Control);var isUnminified=/param/.test(function(e){});Sys.Extended.Deprecated=function(e,t){isUnminified&&window.console&&console.warn&&(1==arguments.length?console.warn(e+" is deprecated."):console.warn(e+" is deprecated. Use "+t+" instead."))};