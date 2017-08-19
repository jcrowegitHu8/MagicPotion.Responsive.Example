
var IngredientEditModal = React.createClass({
	propTypes: {
		validationRules: React.PropTypes.array
	},

	getDefaultProps() {
		return {
			validationRules: [
				//ruleRunner("Name", "Name", requiredRule, minLengthRule(3)),
				//ruleRunner("Color", "Color", requiredRule, minLengthRule(3)),
				ruleRunner("Description", "Description", requiredRule),
				//ruleRunner("EffectType", "Effect", requiredRule),
			]
		};
	},

	getInitialState() {
		return {
			showLoadingBox: false,
			data: {
				"Id": null,
				"Name": "",
				"Color": "",
				"Description": "",
				"Effect": "",
				"EffectType": null,
				"ImportId": ""
			},
			effects: [],
			showErrors: false,
			validationErrors: {},
		}
	},

	errorFor(field) {
		return this.state.validationErrors[field] || "";
	},

	runValidation() {
		var state = this.state;
		var data = this.state.data;
		state.validationErrors = run(data, this.props.validationRules);
		this.setState(state);

	},

	handleSubmitClicked(e) {
		this.runValidation();
		this.setState({ showErrors: true });
		if ($.isEmptyObject(this.state.validationErrors) === false) return null;
		return this.handleSubmit(e);
	},

	handleSubmit: function (e) {
		e.preventDefault();

		var postModel = this.state.data;
		var xhr = new XMLHttpRequest();
		xhr.open('post', this.props.editUrl, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function () {
			xhr.onreadystatechange = function () {
				if (xhr.readyState == 4 && xhr.status == 200) {
					this.props.onClose(true);
				}

			}.bind(this);
			xhr.send(JSON.stringify(postModel));
		}.bind(this));
	},

	LoadEffects() {
		var xhr = new XMLHttpRequest();
		xhr.open('get', this.props.effectsUrl, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function () {
			xhr.onload = function () {
				var data = JSON.parse(xhr.responseText);
				this.setState({ effects: data, showLoadingBox: false });
			}.bind(this);
			xhr.send();
		}.bind(this));
	},


	shouldComponentUpdate(nextProps, nextState) {
		// return a boolean value
		if (this.props.editId != nextProps.editId) {
			this.handleRefreshIngredientData();
		}
		return true;
	},

	handleRefreshIngredientData() {
		var url = this.props.editUrl;
		var id = this.props.parentState.editId;
		if (id && id.length > 0) {
			url += '?id=' + id;
		}

		var xhr = new XMLHttpRequest();
		xhr.open('get', url, true);
		xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
		this.setState({ showLoadingBox: true }, function () {
			xhr.onload = function () {
				var data = JSON.parse(xhr.responseText);
				this.setState({
					data: data
					, showLoadingBox: false
					, showErrors: false
					, validationErrors: {}
				});
				this.LoadEffects();
			}.bind(this);
			xhr.send();
		}.bind(this));
	},

	handleChange(e) {
		var data = this.state.data;
		var bindName = e.target.getAttribute('data-bind-name');
		DataBindHelper.assign(data, bindName, e.target.value);
		this.setState({ data });
	},

	commonValidate(e) {

	},

	isFieldRequired(bindName) {
		if (!this.props.validationRules)
			return '';

		var result = this.props.validationRules.filter(function(rule) { return rule.dataField === bindName });
		if (result.length > 0) {
			return 'requiredField';
		}
		return '';
	},

	renderTextAreaValidationControl(id, bindName, initialValue, labelText) {
		var required = this.isFieldRequired(bindName);

		return (
			<div className="form-group">
				<label className={required} >{labelText}</label>

				<TextAreaWithValidation
					className="form-control"
					id={id}
					dataBindName={bindName}
					showError={this.state.showErrors}
					text={initialValue}
					onFieldChanged={this.handleChange}
					errorList={this.state.validationErrors}
				/>
			</div>
		);
	},

	renderTextValidationControl(id, bindName, initialValue, labelText) {
		var required = this.isFieldRequired(bindName);

		return (
			<div className="form-group">
				<label className={required} >{labelText}</label>

				<TextWithValidation
					className="form-control"
					id={id}
					dataBindName={bindName}
					showError={this.state.showErrors}
					text={initialValue}
					onFieldChanged={this.handleChange}
					errorList={this.state.validationErrors}
					/>
			</div>
		);
	},

	renderDropDownValidationControl(id,bindName, initialValue, items, labelText) {
		var required = this.isFieldRequired(bindName);
		return (
			<div className="form-group">
				<label className={required}>{labelText}</label>
				<LabelValueDropDownWithValidation
					id={id}
					titleClass="form-control"
					optionClass=""
					selectClass=""
					show={true}
					items={items}
					valueField="Id"
					labelField="Name"
					onFieldChanged={this.handleChange}
					selected={initialValue}
					dataBindName={bindName}
					showError={this.state.showErrors}
					errorList={this.state.validationErrors}
				/>
			</div>
		);
	},

	getDisplayStyle() {
		return this.props.parentState.show ? "block" : "none";
	},

	renderModalbody() {

		return (
			<div>
				<div className="">
					<form onSubmit={this.handleSubmitClicked}>

						<div className="row">
							<div className="col-md-6 col-lg-4">
								{this.renderTextValidationControl("Name"
									, "Name"
									, this.state.data.Name
									, "Name")}

							</div>
							<div className="col-md-6 col-lg-4">
								{this.renderTextValidationControl("Color"
									, "Color"
									, this.state.data.Color
									, "Color")}
							</div>
							<div className="col-md-6 col-lg-4">
								{this.renderDropDownValidationControl("EffectType",
									"EffectType"
									, this.state.data.EffectType
									, this.state.effects
									, "Effect")}
							</div>
						</div>

						<div className="row">
							
							
							<div className="col-xs-12">
								{this.renderTextAreaValidationControl("Description"
									, "Description"
									, this.state.data.Description
									, "Description")}
							</div>
						</div>
					</form>
				</div>
			</div>

		);
	},

	render() {

		return (
			<div className="modal" style={{ display: this.getDisplayStyle()}}
				tabIndex="-1" role="dialog">
				<div className="modal-dialog modal-lg" role="dialog">
					<div className="modal-content">
						<div className={'modal-header'}>
							<button type="button"
								className="close"
								data-dismiss="modal"
								aria-label="Close"
								onClick={this.props.onClose}>
								<span aria-hidden="true">&times;</span>
							</button>
							<h4 className="modal-title">{this.props.parentState.title}</h4>
						</div>
						<div className="modal-body">
							{this.renderModalbody()}
						</div>
						<div className="modal-footer">
							<button
								type="button"
								className="btn btn-link"
								data-dismiss="modal"
								onClick={this.props.onClose}>
								Cancel
							</button>
							<button
								type="button"
								className="btn btn-primary"
								data-dismiss="modal"
								
								onClick={this.handleSubmitClicked}>
								Save
							</button>
						</div>
					</div>
				</div>
			</div>
		);
	},

});

