define("ContactPageV2", ["UsrTodoSchema"], function() {
	return {
		entitySchemaName: "Contact",
		attributes: {},
		messages: {
			"GetContactId": {
				mode: BPMSoft.MessageMode.PTP,
				direction: BPMSoft.MessageDirectionType.SUBSCRIBE
			},
			"TodoListChanged": {
				mode: BPMSoft.MessageMode.PTP,
				direction: BPMSoft.MessageDirectionType.SUBSCRIBE
			},
			"ChangeDashboardTab": {
				mode: this.BPMSoft.MessageMode.BROADCAST,
				direction: this.BPMSoft.MessageDirectionType.SUBSCRIBE
			},
			"OnReloadTodoData": {
				mode: this.BPMSoft.MessageMode.PTP,
				direction: this.BPMSoft.MessageDirectionType.PUBLISH
			}
		},
		modules: /**SCHEMA_MODULES*/{
			"UsrTodo": {
				"config": {
					"schemaName": "UsrTodoSchema",
					"isSchemaConfigInitialized": true,
					"useHistoryState": false,
					"showMask": true,
					"parameters": {
						"viewModelConfig": {}
					}
				}
			}
		}/**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/{}/**SCHEMA_DETAILS*/,
		businessRules: /**SCHEMA_BUSINESS_RULES*/{}/**SCHEMA_BUSINESS_RULES*/,
		methods: {

			onEntityInitialized: function() {
				this.callParent(arguments);
				this.sandbox.publish("OnReloadTodoData", null, [this.getTodoModuleSandboxId()]);
			},

			getTodoSchemaSandboxId: function() {
				return this.sandbox.id + "_module_UsrTodo";
			},

			getTodoModuleSandboxId: function() {
				return this.getTodoSchemaSandboxId() + "_UsrTodoModule";
			},

			subscribeSandboxEvents: function() {
				this.callParent(arguments);
				this.sandbox.subscribe("GetContactId", _ => this.$Id, this, [this.getTodoSchemaSandboxId()]);
				this.sandbox.subscribe("TodoListChanged", () => {
					this.sandbox.publish("ReloadDashboardItems")
				}, this, [this.getTodoModuleSandboxId()]);
				this.sandbox.subscribe("ChangeDashboardTab", (tabName) => {
					this.sandbox.publish("OnReloadTodoData", null, [this.getTodoModuleSandboxId()]);
				}, this);
			}

		},
		dataModels: /**SCHEMA_DATA_MODELS*/{}/**SCHEMA_DATA_MODELS*/,
		diff: /**SCHEMA_DIFF*/[
			{
				"operation": "insert",
				"name": "Tab91b480c3TabLabel",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Tab91b480c3TabLabelTabCaption"
					},
					"items": [],
					"order": 4
				},
				"parentName": "Tabs",
				"propertyName": "tabs",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "Tab91b480c3TabLabelGroup5e5b6cab",
				"values": {
					"caption": {
						"bindTo": "Resources.Strings.Tab91b480c3TabLabelGroup5e5b6cabGroupCaption"
					},
					"itemType": 15,
					"markerValue": "added-group",
					"items": []
				},
				"parentName": "Tab91b480c3TabLabel",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"parentName": "Tab91b480c3TabLabelGroup5e5b6cab",
				"propertyName": "items",
				"name": "UsrTodo",
				"values": {
					"itemType": this.BPMSoft.ViewItemType.MODULE
				},
				"index": 1
			},
			{
				"operation": "merge",
				"name": "ESNTab",
				"values": {
					"order": 6
				}
			}
		]/**SCHEMA_DIFF*/
	};
});
