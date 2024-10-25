 define("UsrTodoSchema", ["UsrTodoModule"], function () {
	 return {
		 mixins: {},
		 details: /**SCHEMA_DETAILS*/{}/**SCHEMA_DETAILS*/,
		 attributes: {},
		 messages: {
			 "GetContactId": {
				 mode: BPMSoft.MessageMode.PTP,
				 direction: BPMSoft.MessageDirectionType.PUBLISH
			 }
		 },
		 modules: /**SCHEMA_MODULES*/{}/**SCHEMA_MODULES*/,
		 methods: {

			 getTodoModuleName: function() {
				 return "UsrTodoModule";
			 },

			 getTodoModuleSandboxId: function() {
				 return this.sandbox.id + "_" + this.getTodoModuleName();
			 },

			 onRender: function() {
				 this.callParent(arguments);
				 this.loadTodoModule();
			 },

			 onDestroy: function() {
				 this.sandbox.unloadModule(this.getTodoModuleName());
				 this.callParent(arguments);
			 },

			 loadTodoModule: function() {
				 let contactId = this.sandbox.publish("GetContactId", null, [this.sandbox.id]);
				 this.sandbox.loadModule(this.getTodoModuleName(), {
					 renderTo: Ext.get("UsrTodoContainer"),
					 keepAlive: false,
					 instanceConfig: {
						 ngValue: {
							 contactId: contactId
						 }
					 }
				 });
			 }
		 },
		 diff: /**SCHEMA_DIFF*/[
			 {
				 "operation": "insert",
				 "name": "UsrTodoContainer",
				 "values": {
					 "id": "UsrTodoContainer",
					 "itemType": BPMSoft.ViewItemType.CONTAINER,
					 "items": [],
				 }
			 },
		 ], /**SCHEMA_DIFF*/
		 rules: {}
	 };
 });
