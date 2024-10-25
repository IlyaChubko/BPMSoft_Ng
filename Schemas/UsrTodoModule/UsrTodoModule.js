 define("UsrTodoModule", ["NgTodoComponent"], function () {
	 Ext.define("BPMSoft.configuration.UsrTodoModule", {
		 alternateClassName: "BPMSoft.UsrTodoModule",
		 extend: "BPMSoft.BaseModule",
		 Ext: null,
		 sandbox: null,
		 BPMSoft: null,
		 viewModel: null,
		 view: null,
		 ngComponent: null,
		 ngValue: null,
		 messages: {
			 "TodoListChanged": {
				 mode: BPMSoft.MessageMode.PTP,
				 direction: BPMSoft.MessageDirectionType.PUBLISH
			 },
			 "OnReloadTodoData": {
				 mode: BPMSoft.MessageMode.PTP,
				 direction: BPMSoft.MessageDirectionType.SUBSCRIBE
			 }
		 },

		 init: function() {
			 this.sandbox.registerMessages(this.messages);
			 this.callParent(arguments);
		 },

		 render: function(renderTo) {
			 this.callParent(arguments);
			 const ngComponent = document.createElement("ng-todo");
			 ngComponent.setAttribute("id", this.sandbox.id);
			 this.ngComponent = ngComponent;
			 this.initNgComponentAttributes();
			 this.initNgComponentEvents();
			 renderTo.appendChild(ngComponent);
		 },

		 initNgComponentAttributes: function() {
			 const ngComponent = this.ngComponent;
			 if (ngComponent) {
				 ngComponent.contactId = this.ngValue.contactId;
				 ngComponent.sandbox = this.sandbox;
			 }
		 },

		 initNgComponentEvents: function() {
			 const ngComponent = this.ngComponent;
			 if (ngComponent) {
				 ngComponent.addEventListener("TodoListChanged", () => this.sandbox.publish("TodoListChanged", null, [this.sandbox.id]));
			 }
		 },

		 destroy: function () {
			 this.ngComponent = null;
		 }
	 });
	 return BPMSoft.UsrTodoModule;
 });
